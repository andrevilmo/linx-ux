using Linx.LinqExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Linx.Tools;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;
using System.Data.SQLite;
using System.Net;
using System.Runtime.Serialization.Json;

namespace Linx.License.Client
{

    public partial class Licensecing
    {
        const string _LOG_FILE = @"c:\temp\LicenseAgentLog.txt";
        const string _KEY = "LINXSISTEMAS0312141017200871742000";
        const string _FUNC_KEY = "LINX174557892000";
        static string dbFile = Path.Combine(System.IO.Path.GetTempPath(), "RLST1218891720035678.DT1");
        static string uniqueMachineKey = new Identity().Value();

        private string configFile = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Linx.License.Client.cfg");
        LicenseContext _context;
        bool hasLog = false;

        DateTime currentStart, currentEnd;

        private void StartLog()
        {
            if (hasLog)
                currentStart = DateTime.Now;
        }
        
        private void EndtLog(string tag)
        {
            if (hasLog)
            {
                currentEnd = DateTime.Now;
                System.IO.File.AppendAllText(_LOG_FILE, String.Format("{0}: Ended in {1} milliseconds.\r\n", tag, currentEnd.Subtract(currentStart).TotalMilliseconds));
            }
        }
        
        public Licensecing(bool log = false)
        {
            hasLog = log;
            if (hasLog)
            {
                System.IO.File.WriteAllText(_LOG_FILE, "");
            }
            
            this.CheckDB();
            _context = new LicenseContext(new SQLiteConnection() { ConnectionString = @"Data Source=" + dbFile + ";" });
        }



        #region Public Methods
        /// <summary>
        /// Validar uma licença.
        /// Body:
        /// {
        ///     "IdLicenca" : 4,
        ///     "IdCliente": "65161419000170",
        ///     "Usuario" : "usuarioTeste1"
        /// }     
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>        
        public string Validar(string licenseInfo)
        {
            LicencaRetorno dataResult = null;
            try
            {
                string decInfo = Decrypt(licenseInfo);

                LicencaInfo info = SerializationManager<LicencaInfo>.JsonToObject(decInfo);

                dataResult = this.ValidarLicenca(info);
            }
            catch (Exception excp)
            {
                dataResult = new LicencaRetorno()
                {
                    Tipo = 3,
                    Descricao = Domains.RETORNO_TIPO.Erro.DisplayName,
                    Mensagem = excp.GetCompleteMessage(),
                    Valor = false

                };
            }

            return Encrypt(SerializationManager<LicencaRetorno>.ObjectToJson(dataResult));
        }

        /// <summary>
        /// Enviar log para o servidor remoto de licenças.
        /// Body:
        /// {
        ///    "IdProduto" : 3,
        ///    "IdCliente": "65161419000170",
        ///    "Usuario" : "usuarioTeste1",
        ///    "IdUsuario" : null,
        ///    "NomeAutenticacao" : null,    
        ///    "CodigoFilial" : "",
        ///    "NomeFilial" : "",
        ///    "IndicaLoja" : false,
        ///    "IdLinx" : null,
        ///     "Detalhes" : [ "Teste1", "Teste2" ]
        /// }     
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>        
        public string SalvarLog(string logContent)
        {
            LicencaRetorno dataResult;
            try
            {
                string decInfo = Decrypt(logContent);

                LogInfo log = SerializationManager<LogInfo>.JsonToObject(decInfo);

                dataResult = this.SalvarLicencaLog(log);
            }
            catch (Exception excp)
            {
                dataResult = new LicencaRetorno()
                {
                    Tipo = 3,
                    Descricao = Domains.RETORNO_TIPO.Erro.DisplayName,
                    Mensagem = excp.GetCompleteMessage(),
                    Valor = false

                };
            }

            return Encrypt(SerializationManager<LicencaRetorno>.ObjectToJson(dataResult));
        }

        /// <summary>
        /// Remover uma licença.
        /// Body:
        /// {
        ///     "IdLicenca" : 4,
        ///     "IdCliente": "65161419000170",
        ///     "Usuario" : "usuarioTeste1"
        /// }     
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>       
        public string Remover(string licenseInfo)
        {
            LicencaRetorno dataResult;

            try
            {
                string decInfo = Decrypt(licenseInfo);
                LicencaInfo info = SerializationManager<LicencaInfo>.JsonToObject(decInfo);
                dataResult = this.RemoverLicenca(info);
            }
            catch (Exception excp)
            {
                dataResult = new LicencaRetorno()
                {
                    Tipo = 3,
                    Descricao = Domains.RETORNO_TIPO.Erro.DisplayName,
                    Mensagem = excp.GetCompleteMessage(),
                    Valor = false

                };
            }

            return Encrypt(SerializationManager<LicencaRetorno>.ObjectToJson(dataResult));
        }
        #endregion


        /// <summary>
        /// Verificar se existe uma licença para o produto.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private LicencaRetorno ValidarLicenca(LicencaInfo info)
        {
            LicencaRetorno result = new LicencaRetorno();
            Exception error = null;
            string ativo = Encrypt("1");
            string pendente = Encrypt("2");
            string revogado = Encrypt("3");

            if (info != null)
            {                
                //Pegar o endereço do servidor remnoto de licenças 
                var remoteAddress = GetLicenseServerAddress();
                if (!String.IsNullOrEmpty(remoteAddress))
                {
                    info.Terminal = Environment.MachineName;
                    info.Chave = this.GetKey();

                    //Verificar/Ajustar o controle de requisição a licença local
                    string idLicenca = Encrypt(info.IdLicenca.ToString());
                    string idChave = Encrypt(info.Chave);
                    string idCliente = Encrypt(info.IdCliente);
                    string usuario = Encrypt(info.Usuario);
                    string terminal = Encrypt(info.Terminal);
                    this.StartLog();
                    var licencaReq = _context.Database.SqlQuery<LicencaRequisicao>("select * from LicencaRequisicao where IdCliente = '" + idCliente + "' and IdLicenca = '" + idLicenca + "' and Chave = '" + idChave + "' order by IdLR desc limit 1").FirstOrDefault();
                    this.EndtLog("select * from LicencaRequisicao");
                    if (licencaReq == null)
                    {
                        this.StartLog();
                        licencaReq = new Client.LicencaRequisicao()
                        {
                            IdCliente = idCliente,
                            IdLicenca = idLicenca,
                            Usuario = usuario,
                            Chave = idChave,
                            Terminal = terminal
                        };
                        _context.LicencaRequisicao.Add(licencaReq);
                        _context.SaveBaseChanges();
                        this.EndtLog("Add LicencaRequisicao");
                    }
                    else
                    {
                        if (licencaReq.Terminal != terminal || licencaReq.Usuario != usuario)
                        {
                            this.StartLog();
                            licencaReq.Terminal = terminal;
                            licencaReq.Usuario = usuario;
                            _context.Database.ExecuteSqlCommand("update LicencaRequisicao set Usuario='" + usuario + "', Terminal='" + terminal + "' where IdLR=" + licencaReq.IdLR.ToString());
                            this.EndtLog("Update LicencaRequisicao");
                        }
                    }

                    //Obter a licença local
                    this.StartLog();
                    var licencaUso = _context.Database.SqlQuery<LicencaUso>("select * from LicencaUso where IdLR = " + licencaReq.IdLR.ToString() + " and LxStatusChave == '" + ativo + "' order by Data desc limit 1").FirstOrDefault();
                    this.EndtLog("select * from LicencaUso");
                    if (licencaUso != null)
                    {
                        licencaUso.DecryptData();
                        if (!this.LicencaExpirada(licencaUso) && licencaUso.Data.Date == DateTime.Now.Date)
                        {
                            SetMessageResult(result, licencaUso);
                            return result;
                        }
                    }

                    //Requisitar uma licença do servidor remoto de licenças
                    this.StartLog();
                    try
                    {   
                        var lUso = WebResponseExt.Post<LicencaUso>(remoteAddress + "/LinxLicenseServerLicenciamento/ValidateLicense", info);
                        if (licencaUso == null)
                            licencaUso = lUso;
                        else
                        {
                            licencaUso.IdLicencaUso = lUso.IdLicencaUso;
                            licencaUso.LxStatusChave = lUso.LxStatusChave;
                            licencaUso.Periodicidade = lUso.Periodicidade;
                            licencaUso.DiasOffline = lUso.DiasOffline;
                            licencaUso.Mensagem = lUso.Mensagem;
                            licencaUso.TemporaryIdLicencaUso = lUso.TemporaryIdLicencaUso;
                        }
                        licencaUso.Data = DateTime.Now.Date;
                        licencaUso.DataProcesso = licencaUso.Data;
                    }
                    catch (Exception exp)
                    {
                        error = exp.InnerException ?? exp;
                    }
                    finally
                    {
                        this.EndtLog("POST ValidateLicense");
                    }

                    if (licencaUso != null)
                    {
                        if (licencaUso.IdLR == 0)
                        {
                            licencaUso.IdLR = licencaReq.IdLR;
                            licencaUso.LicencaRequisicao = licencaReq;
                        }
                        if (error != null)
                            licencaUso.DataProcesso = DateTime.Now.Date;
                    }

                    this.SetMessageResult(result, licencaUso, error);

                    //Salvar a licença localmente
                    if (result.Valor)
                    {
                        this.StartLog();
                        licencaUso.EncryptData();
                        if (licencaUso.IdLU == 0)
                        {
                            _context.LicencaUso.Add(licencaUso);
                            _context.SaveBaseChanges();
                            this.EndtLog("Add LicencaUso");
                        }
                        else
                        {
                            _context.Database.ExecuteSqlCommand("update LicencaUso set LxStatusChave='" + licencaUso.LxStatusChave + "', Periodicidade='" + licencaUso.Periodicidade + "', DiasOffline='" + licencaUso.DiasOffline + "', Mensagem='" + licencaUso.Mensagem + "', IdLicencaUso='" + licencaUso.IdLicencaUso + "', TemporaryIdLicencaUso='" + licencaUso.TemporaryIdLicencaUso + "', Data='" + licencaUso.Data.ToString("yyyy-MM-dd HH:mm:ss") + "', DataProcesso='" + licencaUso.DataProcesso.ToString("yyyy-MM-dd HH:mm:ss") + "' where IdLU=" + licencaUso.IdLU.ToString());
                            this.EndtLog("Update LicencaUso");
                        }
                    }
                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in [" + configFile + "]!"));
                }
            }

            return result;
        }

        /// <summary>
        /// Remover licença.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private LicencaRetorno RemoverLicenca(LicencaInfo info)
        {
            LicencaRetorno result = new LicencaRetorno();

            if (info != null)
            {
                var remoteAddress = GetLicenseServerAddress();
                if (!String.IsNullOrEmpty(remoteAddress))
                {
                    //Ajustar propriedades com informações locais
                    info.Terminal = Environment.MachineName;
                    info.Chave = this.GetKey();


                    Exception error = null;
                    this.StartLog();
                    try
                    {
                        //Enviar remoção para o servidor remoto de licenças
                        var strResult = WebResponseExt.Post(remoteAddress + "/LinxLicenseServerLicenciamento/RemoveLicense", info);
                        result.Valor = true;
                    }
                    catch (Exception exp)
                    {
                        error = exp.InnerException ?? exp;
                    }
                    finally
                    {
                        this.EndtLog("POST RemoveLicense");
                    }

                    this.SetMessageResult(result, null, error);

                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in [" + configFile + "]!"));
                }

            }

            return result;
        }

        /// <summary>
        /// Saqlvar Log de produto licenciado.
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>
        private LicencaRetorno SalvarLicencaLog(LogInfo logContent)
        {
            LicencaRetorno result = new LicencaRetorno();

            if (logContent != null)
            {
                var remoteAddress = GetLicenseServerAddress();
                if (!String.IsNullOrEmpty(remoteAddress))
                {
                    //Ajustar propriedades com informações locais
                    logContent.Terminal = Environment.MachineName;
                    logContent.Data = DateTime.Now;
                    logContent.Chave = this.GetKey();

                    this.StartLog();
                    Exception error = null;
                    try
                    {
                        //Enviar log para o servidor remoto de licenças                        
                        result.Valor = WebResponseExt.Post<bool>(remoteAddress + "/LinxLicenseServerLicenciamento/LogUpdate", logContent);
                    }
                    catch (Exception exp)
                    {
                        error = exp.InnerException ?? exp;
                    }
                    finally
                    {
                        this.EndtLog("POST LogUpdate");
                    }

                    this.SetMessageResult(result, null, error);

                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in [" + configFile + "]!"));
                }
            }

            return result;
        }

        #region Util

        /// <summary>
        /// Adjust message result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="licencaUso"></param>
        /// <param name="exp"></param>
        private void SetMessageResult(LicencaRetorno result, LicencaUso licencaUso, Exception exp = null)
        {
            string expirationMessage = "A sua licença vai expirar em {0} dia(s).";
            var retTipo = Domains.RETORNO_TIPO.GetValues();

            if (exp != null)
            {
                if (licencaUso == null)
                {
                    result.Valor = false;
                    result.Tipo = 3;
                    result.Descricao = retTipo["3"];
                    result.Mensagem = exp.GetCompleteMessage();
                }
                else
                {
                    bool lExpirada = this.LicencaExpirada(licencaUso);
                    result.Valor = (licencaUso.LxStatusChave == "1" && !lExpirada);

                    if (lExpirada)
                    {
                        result.Tipo = 3;
                        result.Descricao = retTipo["3"];
                        result.Mensagem = "Licença Expirada.";
                    }
                    else if (licencaUso.Data < licencaUso.DataProcesso)
                    {
                        result.Tipo = 2;
                        result.Descricao = retTipo["2"];
                        result.Mensagem = String.Format(expirationMessage, DiasAExpirar(licencaUso));
                    }
                    else
                    {
                        result.Valor = false;
                        result.Tipo = 3;
                        result.Descricao = retTipo["3"];
                        result.Mensagem = exp.GetCompleteMessage();
                    }
                }
            }
            else
            {
                if (licencaUso == null)
                {
                    result.Tipo = 1;
                    result.Descricao = retTipo["1"];
                    result.Mensagem = "";
                }
                else
                {

                    bool lExpirada = this.LicencaExpirada(licencaUso);
                    result.Valor = (licencaUso.LxStatusChave == "1" && !lExpirada);

                    var retStatus = Domains.STATUS_CHAVE.GetValues();

                    if (licencaUso.LxStatusChave != "1")
                    {
                        result.Tipo = 3;
                        result.Descricao = retTipo["3"];
                        result.Mensagem = retStatus[licencaUso.LxStatusChave.ToString()];
                    }
                    else if (lExpirada)
                    {
                        result.Tipo = 3;
                        result.Descricao = retTipo["3"];
                        result.Mensagem = "Licença Expirada.";
                    }
                    else if (licencaUso.Data < licencaUso.DataProcesso)
                    {
                        result.Tipo = 2;
                        result.Descricao = retTipo["2"];
                        result.Mensagem = String.Format(expirationMessage, DiasAExpirar(licencaUso));
                    }
                    else
                    {
                        result.Tipo = 1;
                        result.Descricao = retTipo["1"];
                        result.Mensagem = "Licença Ativa.";
                    }
                }
            }
        }

        /// <summary>
        /// Get machine key.
        /// </summary>
        /// <returns></returns>
        private string GetKey()
        {
            return uniqueMachineKey;
        }

        /// <summary>
        /// Dias antes de expirar.
        /// </summary>
        /// <param name="licencaUso"></param>
        /// <returns></returns>
        private int DiasAExpirar(LicencaUso licencaUso)
        {
            TimeSpan span = licencaUso.Data.AddDays(int.Parse(licencaUso.DiasOffline)).Subtract(DateTime.Now.Date);
            return ((int)span.TotalDays) + 1;
        }

        /// <summary>
        /// Licença está expirada?
        /// </summary>
        /// <param name="licencaUso"></param>
        /// <returns></returns>
        private bool LicencaExpirada(LicencaUso licencaUso)
        {
            return (licencaUso.Data.AddDays(int.Parse(licencaUso.DiasOffline)) < DateTime.Now.Date);
        }

        string licenseServer = "https://svc-licensing.linxsaas.com.br";
        /// <summary>
        /// Get license server address.
        /// </summary>
        /// <returns></returns>
        private string GetLicenseServerAddress()
        {
            ////"https://svc-licensing.linxsaas.com.br"
            //try
            //{
            //    if (licenseServer.IsNullOrEmpty())
            //    {
            //        if (File.Exists(configFile))
            //        {
            //            string content = File.ReadAllText(configFile);
            //            licenseServer = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(content));
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    licenseServer = "";
            //}

            return licenseServer;
        }

        private void CheckDB()
        {
            if (!File.Exists(dbFile))
            {
                Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream("Linx.License.Client.DB.License.db"))
                {
                    using (var fileStream = File.Create(dbFile))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }
        #endregion


        #region Cryptography


        /// <summary>
        /// This method should be passed to the business area, for generating the token key.
        /// </summary>
        /// <returns></returns>
        public string GenerateToken(string publicKey)
        {
            return GetEncodedValue(publicKey + "(" + DateTime.UtcNow.Ticks.ToString() + ")");
        }

        /// <summary>
        /// Get encoded value. (Use this for getting the content for Linx.License.Client.cfg)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetEncodedValue(string data)
        {
            var aValues = System.Text.UTF8Encoding.UTF8.GetBytes(data);
            return System.Convert.ToBase64String(aValues, 0, aValues.Length);
        }

        private DateTime? KeyToTime(string key)
        {
            try
            {
                string data = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(key)).Extract(_FUNC_KEY + "(", ")");
                long ticks = long.Parse(data);
                return new DateTime(ticks);
            }
            catch
            {
                return null;
            }
        }

        private bool IsValidToken(string token)
        {
            var startTime = KeyToTime(token);
            if (startTime != null)
            {
                DateTime endTime = DateTime.UtcNow;
                TimeSpan span = endTime.Subtract(startTime.Value);
                if (Math.Abs(span.Minutes) <= 2)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Encript Data;
        /// </summary>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Encrypt(string data, string token)
        {
            if (!IsValidToken(token))
            {
                return "BAD TOKEN";
            }

            return Encrypt(data);
        }

        /// <summary>
        /// Encrypt Data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static string Encrypt(string data)
        {
            byte[] SrctArray;
            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(data);
            SrctArray = UTF8Encoding.UTF8.GetBytes(_KEY);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(_KEY));
            objcrpt.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
            objt.Clear();
            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }

        /// <summary>
        /// Decrypt Data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Decrypt(string data, string token)
        {
            if (!IsValidToken(token))
            {
                return "BAD TOKEN";
            }

            return Decrypt(data);
        }

        /// <summary>
        /// Decrypt Data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static string Decrypt(string data)
        {
            byte[] SrctArray;
            byte[] DrctArray = Convert.FromBase64String(data);
            SrctArray = UTF8Encoding.UTF8.GetBytes(_KEY);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objmdcript = new MD5CryptoServiceProvider();
            SrctArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(_KEY));
            objmdcript.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateDecryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(DrctArray, 0, DrctArray.Length);
            objt.Clear();
            return UTF8Encoding.UTF8.GetString(resArray);
        }

        #endregion

    }
}
