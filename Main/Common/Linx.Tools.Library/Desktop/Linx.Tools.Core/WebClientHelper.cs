using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;


namespace Linx.Tools
{
    public static class WebClientHelper
    {
        private static AuthenticationInfo _userInfo = new AuthenticationInfo() { CurrentUser = String.Empty, CurrentCompany = String.Empty, AuthorizationToken = String.Empty, AccessGroup = string.Empty, Application = string.Empty };
        public static AuthenticationInfo UserInfo { get { return _userInfo; } }

        public static void AuthenticateUser(string serviceBusAddress, string userName, string password, string applicationId)
        {
            
        }

        public static void AuthenticateUser<TChannel>(object context, string serviceBusAddress, string userName, string password, string applicationId) where TChannel : class
        {

        }

        public static string Get(Uri uriAddress)
        {
            return String.Empty;
        }

        public static string Post(Uri uriAddress, string data)
        {
            return String.Empty;
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
