using Linx.LinqExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Linx.Tools;

namespace Linx.License.Client
{
    public partial class LicenseContext
    {
        /// <summary>
        /// Verificar se existe uma licença para o produto.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<LicencaRetorno> ValidarLicenca(LicencaInfo info)
        {
            LicencaRetorno result = new LicencaRetorno();

            if (info != null)
            {
                //Pegar o endereço do servidor remnoto de licenças 
                var remoteAddress = GetLicenseServerAddress();
                if (!String.IsNullOrEmpty(remoteAddress))
                {
                    info.Terminal = Environment.MachineName;
                    info.Chave = this.GetKey();

                    //Verificar/Ajustar o controle de requisição a licença local
                    var licencaReq = this.LicencaRequisicao.FirstOrDefault(e => e.IdCliente == info.IdCliente && e.IdLicenca == info.IdLicenca && e.Chave == info.Chave);
                    if (licencaReq == null)
                    {
                        licencaReq = new Client.LicencaRequisicao()
                        {
                            IdCliente = info.IdCliente,
                            IdLicenca = info.IdLicenca,
                            Usuario = info.Usuario,
                            Chave = info.Chave,
                            Terminal = info.Terminal
                        };

                        this.LicencaRequisicao.Add(licencaReq);
                        this.SaveBaseChanges();
                    }
                    else
                    {
                        if (licencaReq.Terminal != info.Terminal || licencaReq.Usuario != info.Usuario)
                        {
                            licencaReq.Terminal = info.Terminal;
                            licencaReq.Usuario = info.Usuario;
                            this.DetectChanges();
                            this.SaveBaseChanges();
                        }
                    }

                    //Obter a licença local
                    var licencaUso = this.LicencaUso.Where(e => e.IdLR == licencaReq.IdLR && e.LxStatusChave == 1).OrderByDescending(e => e.Data).FirstOrDefault();
                    if (licencaUso != null)
                    {
                        if (!this.LicencaExpirada(licencaUso) && licencaUso.DataProcesso.Date == DateTime.Now.Date)
                        {
                            SetMessageResult(result, licencaUso);
                            return result;
                        }
                    }

                    Exception error = null;
                    //Requisitar uma licença do servidor remoto de licenças
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(remoteAddress);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var jsonInString = JsonConvert.SerializeObject(info);
                            HttpResponseMessage response = await client.PostAsync("LinxLicenseServerLicenciamento/ValidateLicense", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();

                            //Convertendo a resposta do serbidor de licenças                         
                            var lUso = JsonConvert.DeserializeObject<LicencaUso>(responseBody);
                            if (licencaUso == null)
                                licencaUso = lUso;
                            else
                            {
                                licencaUso.IdLicencaUso = lUso.IdLicencaUso;
                                licencaUso.LxStatusChave = licencaUso.LxStatusChave;
                                licencaUso.Periodicidade = licencaUso.Periodicidade;
                                licencaUso.DiasOffline = licencaUso.DiasOffline;
                                licencaUso.Mensagem = licencaUso.Mensagem;
                                licencaUso.TemporaryIdLicencaUso = licencaUso.TemporaryIdLicencaUso;
                            }
                            licencaUso.Data = DateTime.Now.Date;
                        }
                    }
                    catch (Exception exp)
                    {
                        error = exp;
                    }

                    if (licencaUso != null)
                    {
                        if (licencaUso.IdLR == 0)
                        {
                            licencaUso.IdLR = licencaReq.IdLR;
                            licencaUso.LicencaRequisicao = licencaReq;
                        }
                        licencaUso.DataProcesso = DateTime.Now.Date;
                    }

                    this.SetMessageResult(result, licencaUso, error);

                    //Salvar a licença localmente
                    if (result.Valor)
                    {
                        if (licencaUso.IdLU == 0)
                            this.LicencaUso.Add(licencaUso);
                        else
                            this.DetectChanges();
                        this.SaveBaseChanges();
                    }
                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in appsettings.json!"));
                }

            }

            return result;
        }

        public async Task<LicencaRetorno> RemoverLicenca(LicencaInfo info)
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
                    try
                    {
                        //Enviar remoção para o servidor remoto de licenças
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(remoteAddress);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var jsonInString = JsonConvert.SerializeObject(info);
                            HttpResponseMessage response = await client.PostAsync("LinxLicenseServerLicenciamento/RemoveLicense", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                            response.EnsureSuccessStatusCode();
                            result.Valor = true;
                        }

                    }
                    catch (Exception exp)
                    {
                        error = exp;
                    }

                    this.SetMessageResult(result, null, error);

                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in appsettings.json!"));
                }

            }

            return result;
        }

        /// <summary>
        /// Saqlvar Log de produto licenciado.
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>
        public async Task<LicencaRetorno> SalvarLog(LogInfo logContent)
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

                    Exception error = null;
                    try
                    {
                        //Enviar log para o servidor remoto de licenças
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(remoteAddress);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var jsonInString = JsonConvert.SerializeObject(logContent);
                            HttpResponseMessage response = await client.PostAsync("LinxLicenseServerLicenciamento/LogUpdate", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();

                            //Converter resposta do servidor
                            result.Valor = JsonConvert.DeserializeObject<bool>(responseBody);
                        }
                    }
                    catch (Exception exp)
                    {
                        error = exp;
                    }

                    this.SetMessageResult(result, null, error);

                }
                else
                {
                    this.SetMessageResult(result, null, new Exception("[LicenseService] tag does not found in appsettings.json!"));
                }
            }

            return result;
        }

        #region Util

        public void SetMessageResult(LicencaRetorno result, LicencaUso licencaUso, Exception exp = null)
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
                    result.Valor = (licencaUso.LxStatusChave == 1 && !lExpirada);

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
                bool lExpirada = this.LicencaExpirada(licencaUso);
                result.Valor = (licencaUso.LxStatusChave == 1 && !lExpirada);

                var retStatus = Domains.STATUS_CHAVE.GetValues();

                if (licencaUso.LxStatusChave != 1)
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

        private string GetKey()
        {
            return Environment.MachineName + Environment.ProcessorCount + Environment.OSVersion.VersionString + (Environment.Is64BitOperatingSystem ? "OS64" : "OS32");
        }

        private int DiasAExpirar(LicencaUso licencaUso)
        {
            TimeSpan span = licencaUso.DataProcesso.AddDays(licencaUso.DiasOffline).Subtract(DateTime.Now);
            return ((int)span.TotalDays) + 1;
        }

        private bool LicencaExpirada(LicencaUso licencaUso)
        {
            return (licencaUso.DataProcesso.AddDays(licencaUso.DiasOffline) < DateTime.Now);
        }
        private string GetLicenseServerAddress()
        {
            string licenseServer = "";
            var config = AppSettings.Instance;
            if (config != null)
            {
                licenseServer = config.GetSection("LicenseService").Value;
            }

            return licenseServer;
        }

        #endregion


    }
}
