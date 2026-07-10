using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Linx.Tools
{
    public class ExceptionLogger
    {
        #region Singleton
        private static ExceptionLogger instance;
        private static volatile object xLock = new object();
        public static ExceptionLogger Instance
        {
            get
            {
                lock (xLock)
                {
                    if (instance.IsNull())
                    {
                        lock (xLock)
                        {
                            instance = new ExceptionLogger();
                        }
                    }
                    return instance;
                }
            }
        }

        #endregion

        private const string ErrorFileName = "errors{0:yyyyMMdd}.txt";

        private IExceptionLogger _databaseLogger;
        public IExceptionLogger DatabaseLogger
        {
            get
            {
                if (_databaseLogger == null)
                {
                    _databaseLogger = ImplementationHelper<IExceptionLogger>.GetInstance("ErrorHandlingRepositoryImplementation");
                }
                return _databaseLogger;
            }
        }


        #region ctor
        private ExceptionLogger()
        {
        }
        #endregion

        public Task<ExceptionInfo> LogError(Exception ex, string controllerName, string actionName)
        {
            var info = new ExceptionInfo(ex, controllerName, actionName);

            bool logged = false;
            try { logged = PersistInDatabase(info); }
            catch { }
            if (!logged)
                PersistInFile(info);

            return Task.FromResult(info);
        }

        private bool PersistInFile(ExceptionInfo info)
        {

            var infoText = JsonConvert.SerializeObject(info);
            File.AppendAllText(GetErrorPath(), Environment.NewLine + infoText);

            return true;
        }

        private bool PersistInDatabase(ExceptionInfo info)
        {
            return DatabaseLogger.addLog(
                 DataErro: info.Date,
                 NomeControlador: info.ControllerName,
                 MetodoHttp: info.Method,
                 NomeAcao: info.ActionName,
                 EnderecoWeb: info.Path,
                 MensagemExcecao: info.Message,
                 MensagemExcecaoInterna: info.InnerException,
                 PilhaExcecao: info.StackTrace,
                 UsuarioWindows: info.LogonName,
                 NomeServidor: info.HostName,
                 UsuarioSistema: info.UserUid,
                 Empresa: info.CurrentCompany,
                 GrupoEconomico: info.EconomicGroup,
                 Aplicacao: info.Application,
                 Ambiente: info.Environment
            );
        }

        private string GetErrorPath()
        {
            var path = HttpContext.Current.Server.MapPath("/Logs");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, string.Format(ErrorFileName, DateTime.Today));
        }            

    }

}