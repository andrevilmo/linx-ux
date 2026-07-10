using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Configuration;
using System.Web;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV
{
    public static class BusinessCacheAccessHelper
    {
        private static Object thisLock = new Object();

        public static string GetConnectionString(string connectionName)
        {
            return GetConnectionString(connectionName, null);
        }

        public static string GetConnectionString(string connectionName, Dictionary<string, string> headers)
        {
            string connectionString = String.Empty;

            if (!LocalServiceBus.Enabled)
            {
                string authorizationConnectionName = "FrameworkAutorizacao";
                if (connectionName != authorizationConnectionName && !ConfigurationManager.ConnectionStrings[authorizationConnectionName].IsNullOrEmpty())
                {
                    lock (thisLock)
                    {
                        string result = BusinessUserServiceHelper.GetIdLinxInfo(connectionName, headers);

                        if (!result.IsNullOrEmpty())
                            connectionString = result.Right("[##]");

                        //Get from AppConfig if empty
                        if (connectionString.IsNullOrEmpty())
                            connectionString = "name=" + connectionName;
                        else
                            //Adjust quotation marks
                            connectionString = connectionString.Replace("&quot;", "\"");
                    }
                }
            }

            //Get from AppConfig if empty
            if (connectionString.IsNullOrEmpty())
                connectionString = "name=" + connectionName;

            return connectionString;
        }
    }
}
