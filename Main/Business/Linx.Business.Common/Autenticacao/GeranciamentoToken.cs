using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Threading;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Linx.Business.Common.Autenticacao
{
    public class WebDownload : WebClient
    {
        public int Timeout { get; set; }
        public WebDownload()
        {
        }

        public WebDownload(int? timeout = null)
        {
            if (timeout != null)
                this.Timeout = (int)timeout;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                if (this.Timeout > 0)
                    request.Timeout = this.Timeout;
            }
            return request;
        }
    }
    public class DadosToken
    {
        public DadosAutenticacao dadosAutorizacao { get; set; }
        public GerenciaToken _instancia { get; set; }
        public int ServicosAtivos { get; set; }
        public int ServicosProblema { get; set; }
        private List<Dictionary<string, string>> _authenticateJsonResult = new List<Dictionary<string, string>>();
        public List<Dictionary<string, string>> AuthenticateJsonResult
        {
            get
            {
                return _authenticateJsonResult;
            }

            set
            {
                _authenticateJsonResult = value;
            }
        }

        private bool _incrementaServicosAtivos = true;
        public bool IncrementaServicosAtivos
        {
            get
            {
                return _incrementaServicosAtivos;
            }
            set
            {
                _incrementaServicosAtivos = value;
            }
        }

        private bool _tryGetToken = false;
        public bool TryGetToken
        {
            get
            {
                return _tryGetToken;
            }
            set
            {
                _tryGetToken = value;
            }
        }

        private object _lockGetToken = new object();
        public object LockGetToken
        {
            get { return _lockGetToken; }
            set { _lockGetToken = value; }
        }

        private object _lockTryGetToken = new object();
        public object LockTryGetToken
        {
            get { return _lockTryGetToken; }
            set { _lockTryGetToken = value; }
        }
        public DateTime dtUltimaAtualizacaoToken { get; set; }
    }

    public class AutenticaUX
    {
        public class Authentication
        {
            public List<Dictionary<string, string>> AuthenticateJsonResult;
        }

        /// <summary>
        /// Realiza uma autenticação no UX com as informações enviadas
        /// </summary>
        /// <param name="autenticacao">objeto com informações da autenticação</param>
        /// <returns>bjeto de autenticação</returns>
        public static List<Dictionary<string, string>> Authenticate(DadosAutenticacao dadosAutorizacao)//  string userName, string password, string application, string url)
        {
            WebClient webClient = null;
            webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Proxy = ConfiguraProxy(dadosAutorizacao);

            NameValueCollection settingsAccess = new NameValueCollection();

            settingsAccess.Add("userName", dadosAutorizacao.userName);
            settingsAccess.Add("password", dadosAutorizacao.password);
            settingsAccess.Add("applicationId", dadosAutorizacao.application);
            webClient.QueryString = settingsAccess;

            Authentication re = new Authentication();
            re.AuthenticateJsonResult = new List<Dictionary<string, string>>();

            try
            {

                string retornoAutenticacao = null;
                //compatibilizando para se for necessário se autenticar no framework novo ou antigo. Se desejar se autenticar no framework novo, passar autenticaTcsAutorizacaoDomainService = false.
                //Foi criado esse parâmetro como true pois o LinxUX se comunica com o MID-e e já existem vários lugares sendo chamado o MID-e.
                //qualquer dúvida contatar ana.olivera
                if (!dadosAutorizacao.url.IsNullOrEmpty())
                    dadosAutorizacao.url = dadosAutorizacao.url.TrimStart().TrimEnd();
                if (dadosAutorizacao.autenticaTcsAutorizacaoDomainService)
                    retornoAutenticacao = webClient.DownloadString(dadosAutorizacao.url + "Linx-TCS0101-BO-TcsAutorizacao-TcsAutorizacaoDomainService.svc/json/AuthenticateJson");
                else
                    retornoAutenticacao = webClient.DownloadString(dadosAutorizacao.url + "Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/json/AuthenticateJson");

                re = (Authentication)JsonConvert.DeserializeObject(retornoAutenticacao, typeof(Authentication));
                re.AuthenticateJsonResult.ForEach(r =>
                {
                    r.ToList().ForEach(x =>
                    {
                        if (x.Key == "Key")
                        {
                            r.Remove(x.Key);
                            if (dadosAutorizacao.autenticaTcsAutorizacaoDomainService)
                                r.Add(x.Key, keyToString(x.Value));
                            else
                                r.Add(x.Key, keyToStringNovaAutenticacao(x.Value));
                            webClient.Headers[x.Key] = x.Value;
                        }
                    }
                    );
                });

                var app = new Dictionary<string, string>();
                app.Add("Key", "Application");
                app.Add("Value", dadosAutorizacao.application);
                re.AuthenticateJsonResult.Add(app);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                    throw new WebException("O autenticador conseguiu se comunicar mas ocorreu o seguinte problema: " + Util.TrataErroWebException(ex));
                else
                    throw new WebException("O autenticador não conseguiu se comunicar: " + ex.Message.ToString() + " Verifique a conexão com a internet.");
            }
            catch (Exception ex2)
            {
                throw new Exception("O autenticador não conseguiu se comunicar: " + ex2.Message.ToString() + " Verifique a conexão com a internet.");
            }
            return re.AuthenticateJsonResult;
        }

        public static string keyToString(string key)
        {
            switch (key)
            {
                case "1": return "CurrentCompany";
                case "2": return "AuthorizationToken";
                case "3": return "CurrentUser";
                case "4": return "AccessGroup";
                default: return "";
            }
        }

        public static string keyToStringNovaAutenticacao(string key)
        {
            switch (key)
            {
                case "1": return "CurrentCompany";
                case "2": return "AuthorizationToken";
                case "3": return "CurrentUser";
                case "4": return "AccessGroup";
                case "5": return "EconomicGroup";
                case "6": return "Environment";
                default: return "";
            }
        }

        public static WebProxy ConfiguraProxy(DadosAutenticacao dadosAutorizacao)
        {
            WebProxy proxy = null;
            if (dadosAutorizacao.proxyUtiliza)
            {
                if (!dadosAutorizacao.proxyPorta.IsNullOrEmpty())
                    proxy = new WebProxy(dadosAutorizacao.proxyServidor, (int)dadosAutorizacao.proxyPorta);
                else
                    proxy = new WebProxy(dadosAutorizacao.proxyServidor);

                if (!dadosAutorizacao.proxyUsuario.IsNullOrEmpty() && !dadosAutorizacao.proxySenhaUsuario.IsNullOrEmpty())
                    proxy.Credentials = new NetworkCredential(dadosAutorizacao.proxyUsuario, dadosAutorizacao.proxySenhaUsuario, dadosAutorizacao.proxyDominioUsuario);
                else
                    proxy.Credentials = CredentialCache.DefaultCredentials;
                //    proxy.GetProxy(new Uri(_url));
            }
            return proxy;
        }
    }

    public class GerenciaToken
    {
        private static List<DadosToken> lstDadosToken = new List<DadosToken>();
        private static object LockReleaseToken = new object();
        private static object LockUsaToken = new object();
        private static object CriacaoDadosToken = new object();

        private GerenciaToken()
        {
        }

        private static void TryGetToken(DadosToken d)
        {
            //Só refaz a autenticação se passou de 30 segundos do último problema de token (pois por causa das threads pode ser que alguma chegue aqui logo após já conseguir autenticar)
            if (DateTime.Now > d.dtUltimaAtualizacaoToken.AddSeconds(30))
            {
                //Várias threads podem chegar aqui agora, por isso, loca e só uma irá setar para não incrementar novos serviços e pegar um novo token, todas as outras aguardam
                lock (d.LockTryGetToken)
                {
                    if (d.IncrementaServicosAtivos == true)
                    {
                        d.IncrementaServicosAtivos = false;
                        d.TryGetToken = true;
                    }
                }

                if (d.TryGetToken)
                {
                    d.TryGetToken = false; //para as próximas threads que chegarem não tentarem pegar o token
                    //pega novo token
                    d._instancia = null;
                    GetToken(d);
                    d.IncrementaServicosAtivos = true;

                    d.dtUltimaAtualizacaoToken = DateTime.Now; //altera para a date de ultima atualização do token
                }
                else
                {
                    while (d.IncrementaServicosAtivos == false) //aguarda até o token ser refeito
                    {
                    }
                }
            }
            else
            {
                while (d.IncrementaServicosAtivos == false) //aguarda até o token ser refeito
                {
                }
            }
        }

        private static void GetToken(DadosToken d)
        {
            lock (d.LockGetToken) //Ninguem tenta pegar novo Token enquanto algum estiver pegando
            {
                if (d._instancia == null)
                {
                    d.AuthenticateJsonResult = AutenticaUX.Authenticate(d.dadosAutorizacao);
                    d._instancia = new GerenciaToken();
                }
            }
        }

        private static void UsaToken(DadosToken d)
        {
            if (!d.IncrementaServicosAtivos) //se está tentando refazer o token, aguarda para não colocar novos usuários utilizando o token
            {
                Thread.Sleep(100);
                UsaToken(d);
            }
            else
            {
                if (d._instancia == null)
                    GetToken(d); //refaz o token se necessário
                lock (LockUsaToken)
                {
                    d.ServicosAtivos++;
                }
            }
        }

        private static void ReleaseToken(DadosToken d)
        {
            lock (LockReleaseToken)
            {
                d.ServicosAtivos--;
            }
        }

        private static DadosToken CriaDadosToken(DadosAutenticacao d)
        {
            if (d == null)
                throw new Exception("Dados para autorização no UX não foram informados.");

            if (d.userName.IsNullOrEmpty() || d.password.IsNullOrEmpty() || d.application.IsNullOrEmpty() || d.url.IsNullOrEmpty())
                throw new Exception("Dados para autorização no UX não foram informados.");

            lock (CriacaoDadosToken)
            {
                var dadosToken = lstDadosToken.Where(f => f.dadosAutorizacao.userName == d.userName && f.dadosAutorizacao.url == d.url).FirstOrDefault();
                if (dadosToken == null)
                {
                    dadosToken = new DadosToken() { dadosAutorizacao = d };
                    lstDadosToken.Add(dadosToken);
                    return dadosToken;
                }
                else
                {
                    if (dadosToken.dadosAutorizacao.password != d.password ||
                        dadosToken.dadosAutorizacao.application != d.application ||
                        dadosToken.dadosAutorizacao.url != d.url ||
                        dadosToken.dadosAutorizacao.proxyDominioUsuario != d.proxyDominioUsuario ||
                        dadosToken.dadosAutorizacao.proxyPorta != d.proxyPorta ||
                        dadosToken.dadosAutorizacao.proxySenhaUsuario != d.proxySenhaUsuario ||
                        dadosToken.dadosAutorizacao.proxyServidor != d.proxyServidor ||
                        dadosToken.dadosAutorizacao.proxyUsuario != d.proxyUsuario ||
                        dadosToken.dadosAutorizacao.proxyUtiliza != d.proxyUtiliza)
                    {
                        dadosToken.dadosAutorizacao.password = d.password;
                        dadosToken.dadosAutorizacao.application = d.application;
                        dadosToken.dadosAutorizacao.url = d.url;
                        dadosToken.dadosAutorizacao.proxyDominioUsuario = d.proxyDominioUsuario;
                        dadosToken.dadosAutorizacao.proxyPorta = d.proxyPorta;
                        dadosToken.dadosAutorizacao.proxySenhaUsuario = d.proxySenhaUsuario;
                        dadosToken.dadosAutorizacao.proxyServidor = d.proxyServidor;
                        dadosToken.dadosAutorizacao.proxyUsuario = d.proxyUsuario;
                        dadosToken.dadosAutorizacao.proxyUtiliza = d.proxyUtiliza;

                        GerenciaToken.TryGetToken(dadosToken);
                    }
                    return dadosToken;
                }
            }
        }

        public static bool VerificaAutenticado(DadosAutenticacao d)
        {
            var dados = CriaDadosToken(d);

            bool result = false;
            try
            {
                GerenciaToken.UsaToken(dados);

                GerenciaToken.ReleaseToken(dados);

                result = true;
            }
            catch (WebException ex)
            {
                GerenciaToken.ReleaseToken(dados);
                string message = Util.TrataErroWebException(ex);
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return result;
        }

        private static string ExecutaHttp(TipoHttp t, DadosAutenticacao d, string endereco, string conteudo = null, string contentType = null, int? timeout = null)
        {
            var dados = CriaDadosToken(d);

            int tentativas = 1;
            int MAX = 3;
            string result = "";
            while (tentativas <= MAX)
            {
                try
                {
                    GerenciaToken.UsaToken(dados);

                    if (t == TipoHttp.GET)
                    {
                        WebDownload webClient = new WebDownload(timeout);
                        webClient.Proxy = AutenticaUX.ConfiguraProxy(d);
                        //webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                        webClient.Headers.Add(dados.AuthenticateJsonResult[0].Values.First(), dados.AuthenticateJsonResult[0].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[1].Values.First(), dados.AuthenticateJsonResult[1].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[2].Values.First(), dados.AuthenticateJsonResult[2].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[3].Values.First(), dados.AuthenticateJsonResult[3].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[4].Values.First(), dados.AuthenticateJsonResult[4].Values.Last());
                        if (!d.autenticaTcsAutorizacaoDomainService)
                        {
                            webClient.Headers.Add(dados.AuthenticateJsonResult[5].Values.First(), dados.AuthenticateJsonResult[5].Values.Last());
                            webClient.Headers.Add(dados.AuthenticateJsonResult[6].Values.First(), dados.AuthenticateJsonResult[6].Values.Last());
                        }
                        webClient.Encoding = new System.Text.UTF8Encoding();

                        result = webClient.DownloadString(endereco);
                    }
                    else
                    {
                        WebDownload webClient = new WebDownload(timeout);
                        webClient.Proxy = AutenticaUX.ConfiguraProxy(d);
                        webClient.Headers[HttpRequestHeader.ContentType] = contentType;
                        webClient.Headers.Add(dados.AuthenticateJsonResult[0].Values.First(), dados.AuthenticateJsonResult[0].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[1].Values.First(), dados.AuthenticateJsonResult[1].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[2].Values.First(), dados.AuthenticateJsonResult[2].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[3].Values.First(), dados.AuthenticateJsonResult[3].Values.Last());
                        webClient.Headers.Add(dados.AuthenticateJsonResult[4].Values.First(), dados.AuthenticateJsonResult[4].Values.Last());
                        if (!d.autenticaTcsAutorizacaoDomainService)
                        {
                            webClient.Headers.Add(dados.AuthenticateJsonResult[5].Values.First(), dados.AuthenticateJsonResult[5].Values.Last());
                            webClient.Headers.Add(dados.AuthenticateJsonResult[6].Values.First(), dados.AuthenticateJsonResult[6].Values.Last());
                        }
                        webClient.Encoding = new System.Text.UTF8Encoding();

                        result = webClient.UploadString(endereco, conteudo);
                    }

                    if (result.Contains("ERRAUT003") || result.Contains("ERRAUT004"))
                    {
                        throw new WebException(result);
                    }

                    GerenciaToken.ReleaseToken(dados);

                    break;
                }
                catch (WebException ex)
                {
                    tentativas++;
                    GerenciaToken.ReleaseToken(dados);

                    string message = Util.TrataErroWebException(ex);
                    if (message.Contains("ERRAUT003") || message.Contains("ERRAUT004"))
                    {
                        GerenciaToken.TryGetToken(dados);

                        if (tentativas > MAX)
                            throw new Exception(message);
                    }
                    else
                        throw new Exception(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return result;
        }

        public enum TipoHttp
        {
            POST = 1,
            GET = 2
        }

        // Métodos neste projeto
        //public static string PostHttp(DadosAutenticacao d, string endereco, string conteudo, string contentType, int? timeout = null)
        //{
        //    return ExecutaHttp(TipoHttp.POST, d, endereco, conteudo, contentType, timeout);
        //}

        //public static string GetHttp(DadosAutenticacao d, string endereco, int? timeout = null)
        //{
        //    return ExecutaHttp(TipoHttp.GET, d, endereco, null, null, timeout);
        //}

        // Métodos que estavam no repositório anterior srv-26
        public static string PostHttp(DadosAutenticacao d, string endereco, string conteudo, string contentType)
        {
            return ExecutaHttp(TipoHttp.POST, d, endereco, conteudo, contentType);
        }

        public static string GetHttp(DadosAutenticacao d, string endereco)
        {
            return ExecutaHttp(TipoHttp.GET, d, endereco);
        }
    }
}
