using System;
using System.Reflection;
using System.Web.Http.Filters;

namespace Linx.Tools
{
    public class ExceptionInfo
    {
        public ExceptionInfo(Exception ex, string controllerName, string actionName) : this()
        {
            if (ex.IsNull())
                throw new ArgumentNullException("ex");

            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.Message = string.Format("[{0}]: {1}", ex.GetType().Name, ex.Message);
            if (!ex.InnerException.IsNull())
                this.InnerException = string.Format("[{0}]: {1}", ex.InnerException.GetType().Name, ex.InnerException.Message);
            //tratamento para loaderException
            Exception exTemp = ex;
            while (exTemp != null)
            {
                if (exTemp is ReflectionTypeLoadException)
                {
                    this.InnerException = "";
                    var reflection = exTemp as ReflectionTypeLoadException;
                    foreach (var e in reflection.LoaderExceptions)
                        this.InnerException += string.Format("[{0}]: {1}", e.GetType().Name, e.Message);

                }
                exTemp = exTemp.InnerException;
            }
            this.StackTrace = ex.StackTrace;

        }
        public ExceptionInfo()
        {

            this.Date = DateTime.Now;

            var request = System.Web.HttpContext.Current.Request;
            this.LogonName = System.Environment.UserDomainName + "\\" + System.Environment.UserName;
            this.HostName = System.Environment.MachineName;

            this.Method = request.HttpMethod;
            this.Path = request.RawUrl;
            this.Browser = request.Browser.Type;

            var headers = Helper.getHeaders(request);
            this.UserUid = Helper.ParseGuid(ServiceHelper.GetMessageProperty("CurrentUser", headers));
            this.CurrentCompany = Helper.ParseGuid(ServiceHelper.GetMessageProperty("CurrentCompany", headers));
            this.EconomicGroup = Helper.ParseGuid(ServiceHelper.GetMessageProperty("EconomicGroup", headers));
            this.Application = Helper.ParseGuid(ServiceHelper.GetMessageProperty("Application", headers));
            this.Environment = Helper.ParseInt(ServiceHelper.GetMessageProperty("Environment", headers));

        }

        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }


        public string Path { get; private set; }
        public string Browser { get; private set; }
        public string Method { get; private set; }

        public string LogonName { get; private set; }
        public string HostName { get; private set; }

        public Guid? UserUid { get; private set; }
        public Guid? CurrentCompany { get; private set; }
        public Guid? EconomicGroup { get; private set; }
        public Guid? Application { get; private set; }
        public int? Environment { get; private set; }

        public DateTime Date { get; private set; }


        public string Message { get; private set; }
        public string InnerException { get; private set; }
        public string StackTrace { get; private set; }






        public override string ToString()
        {
            return string.Format("{0:dd/MM/yyyy HH:mm:ss} - {1};\r\nPath:{2} - userName:{3}", this.Date, this.Message, this.Path, this.UserUid);
        }
    }

}