using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.DomainServices.Server;
using System.IO;
using System.Web;

namespace Linx.Tools
{
    public static class WebClientHelper
    {
        private static WebClient clientHelper;
        private static AuthenticationInfo _userInfo = new AuthenticationInfo() { CurrentUser = String.Empty, CurrentCompany = String.Empty, AuthorizationToken = String.Empty, AccessGroup = string.Empty, Application = string.Empty };
        public static AuthenticationInfo UserInfo { get { return _userInfo; } }

        public static void AuthenticateUser(string serviceBusAddress, string userName, string password, string applicationId)
        {
            try
            {
                CreateInstance();

                Uri uriAddress = new Uri(string.Format("{0}Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/json/AuthenticateJson?userName={1}&password={2}&applicationId={3}", serviceBusAddress + (serviceBusAddress.EndsWith("/") ? String.Empty : "/"), userName, HttpUtility.UrlEncode(password), applicationId), UriKind.RelativeOrAbsolute);
                string result = clientHelper.DownloadString(uriAddress);

                result = result.Replace("\"", string.Empty);
                clientHelper.Headers["CurrentUser"] = result.Extract("Key:3,Value:", "}");
                clientHelper.Headers["CurrentCompany"] = result.Extract("Key:1,Value:", "}");
                clientHelper.Headers["AuthorizationToken"] = result.Extract("Key:2,Value:", "}");
                clientHelper.Headers["AccessGroup"] = result.Extract("Key:4,Value:", "}");
                clientHelper.Headers["Application"] = result.Extract("Key:8,Value:", "}");
                clientHelper.Headers["EconomicGroup"] = result.Extract("Key:5,Value:", "}");
                clientHelper.Headers["Environment"] = result.Extract("Key:6,Value:", "}");

                _userInfo.CurrentUser = clientHelper.Headers["CurrentUser"]; 
                _userInfo.AuthorizationToken = clientHelper.Headers["AuthorizationToken"];
                _userInfo.CurrentCompany = clientHelper.Headers["CurrentCompany"];
                _userInfo.AccessGroup = clientHelper.Headers["AccessGroup"];
                _userInfo.Application = clientHelper.Headers["Application"];
                _userInfo.EconomicGroup = clientHelper.Headers["EconomicGroup"];
                _userInfo.Environment = clientHelper.Headers["Environment"];
            }
            catch (WebException oException)
            {
                throw ShowError(oException);
            }
        }
        

       public static void AuthenticateUser<TChannel>(System.ServiceModel.ClientBase<TChannel> context, string serviceBusAddress, string userName, string password, string applicationId) where TChannel : class
        {
            try
            {
                WebClientHelper.AuthenticateUser(serviceBusAddress, userName, password, applicationId);
                Linx.Tools.LinxEndPointBehavior behaviour = new LinxEndPointBehavior(clientHelper.Headers["CurrentUser"], clientHelper.Headers["CurrentCompany"], clientHelper.Headers["AuthorizationToken"], clientHelper.Headers["Application"], clientHelper.Headers["AccessGroup"]);
                context.Endpoint.Behaviors.Add(behaviour);
            }
            catch (WebException oException)
            {
                throw ShowError(oException);
            }
        }

        public static string Get(Uri uriAddress)
        {
            try
            {
                CreateInstance();
                clientHelper.Headers["Content-Type"] = "application/json; charset=utf-8";
                return clientHelper.DownloadString(uriAddress);
            }
            catch (WebException oException)
            {
                throw ShowError(oException);
            }
        }

        public static string Post(Uri uriAddress, string data)
        {
            try
            {
                CreateInstance();
                clientHelper.Headers["Content-Type"] = "application/json; charset=utf-8";
                return clientHelper.UploadString(uriAddress, data);
            }
            catch (WebException oException)
            {
                throw ShowError(oException);
            }
        }

        private static DomainException ShowError(WebException webException)
        {
            string errorMessage = webException.Message;

            if (!webException.Response.IsNull())
            {
                string responseError = string.Empty;
                using (var reader = new StreamReader(webException.Response.GetResponseStream()))
                {
                    responseError = reader.ReadToEnd();
                }

                responseError = WebClientHelper.GetResponseErrorMessage(responseError);

                errorMessage = responseError.IsNullOrEmpty() ? errorMessage : responseError;
            }

            errorMessage = errorMessage.Contains("(404) Not Found.") ? "Endereço inválido !".Translate() : errorMessage;

            return new DomainException(errorMessage);
        }

        public static string GetResponseErrorMessage(string responseError)
        {
            responseError = responseError.Replace("\"", string.Empty);

            if (responseError.Contains("ErrorMessage:"))
            {
                responseError = responseError.Extract("ErrorMessage:", ",IsDomainException");
            }
            else if (responseError.Contains("ExceptionMessage:"))
            {
                responseError = responseError.Extract("ExceptionMessage:", ",ExceptionType");
            }
            else if (responseError.Contains("<Fault"))
            {
                responseError = responseError.Extract("<Message>", "</Message>");
            }
            else if (responseError.ToLower().Contains("<html>"))
            {
                responseError = null;
            }

            return responseError;

        }

        private static void CreateInstance()
        {
            if (clientHelper.IsNullOrEmpty())
            {
                clientHelper = new WebClient();
                clientHelper.UseDefaultCredentials = true;
            }
        }
    }


    public struct AuthenticationInfo
    {
        public string CurrentUser { get; set; }
        public string CurrentCompany { get; set; }
        public string AuthorizationToken { get; set; }
        public string AccessGroup { get; set; }
        public string Application { get; set; }
        public string EconomicGroup { get; set; }
        public string Environment { get; set; }
    }
}
