using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Linx.Tools;

namespace Linx.Portal
{
    public class Utils
    {
        public static string GetServiceUrl()
        {
            Hashtable config = System.Configuration.ConfigurationManager.GetSection("PortalSettings") as Hashtable;

            if (config.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());

            string url = config["authorizationServiceAddress"].ToString();
            return url + (url.EndsWith("/") ? string.Empty : "/");
        }

        public static string GetPortalUrl()
        {
            Hashtable config = System.Configuration.ConfigurationManager.GetSection("PortalSettings") as Hashtable;

            if (config.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());

            string url = config["PortalUrl"].ToString();
            return url + (url.EndsWith("/") ? string.Empty : "/");
        }

        public static bool GetRecoverPasswordOption()
        {
            bool result = true;

            Hashtable config = System.Configuration.ConfigurationManager.GetSection("PortalSettings") as Hashtable;

            if (config.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());

            var option = config["ShowRecoverPasswordOption"];

            if (!option.IsNullOrEmpty())
                result = Convert.ToBoolean(option);

            return result;
        }

        public static bool GetListEnvironmentOptionOnLogin()
        {
            bool result = true;

            Hashtable config = System.Configuration.ConfigurationManager.GetSection("PortalSettings") as Hashtable;

            if (config.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());

            var option = config["ShowListEnvironmentOptionOnLogin"];

            if (!option.IsNullOrEmpty())
                result = Convert.ToBoolean(option);

            return result;
        }
    }

}