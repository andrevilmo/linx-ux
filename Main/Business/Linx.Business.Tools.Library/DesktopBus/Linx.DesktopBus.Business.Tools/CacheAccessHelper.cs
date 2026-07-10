using Linx.Framework.BV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Business.Tools
{
    public static class CacheAccessHelper
    {
        public static string GetConnectionString(string connectionName)
        {
            return GetConnectionString(connectionName, null);
        }

        public static string GetConnectionString(string connectionName, Dictionary<string, string> headers)
        {
            return BusinessCacheAccessHelper.GetConnectionString(connectionName, headers);
        }        
    }
}
