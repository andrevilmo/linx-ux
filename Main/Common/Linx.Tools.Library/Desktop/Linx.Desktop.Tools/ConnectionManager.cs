using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace Linx.Tools
{
    public class ConnectionManager
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Catalog { get; set; }
        public bool WindowsAuthentication { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public ConnectionManager()
        {
        }


        public void ExecuteReader(string command, Action<AdomdDataReader> action, params AdomdParameter[] parametros)
        {
            AdomdConnection conn = null;
            try
            {
                conn = GetConnection();

                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = command;

                foreach (var p in parametros)
                    cmd.Parameters.Add(p);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        action(reader);
                    }

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public AdomdConnection GetConnection()
        {
            return new AdomdConnection(GetConnectionString());
        }

        public static string GetConnectionString(string connectionName)
        {
            var connection = ConfigurationManager.ConnectionStrings[connectionName];
            return (connection == null ? String.Empty : connection.ConnectionString);
        }

        public string GetConnectionConfiguration(string providerName = "System.Data.SqlClient")
        {
            return String.Format(@"<add name=""{0}""
            connectionString=""{1}""
            providerName=""" + providerName + @""" />", Name, GetConnectionString());
        }

        public string GetConnectionString()
        {
            string  authInfo = GetAuthentication();
            return string.Format("Data Source={0};{1}{2}", Server, (Catalog.IsNullOrEmpty() ? String.Empty : "Initial Catalog=" + Catalog + ";"), (authInfo.IsNullOrEmpty() ? String.Empty : authInfo + ";"));
        }

        private string GetAuthentication()
        {
            if (this.WindowsAuthentication)
                return "Integrated Security=SSPI";
            else
            {
                return (UserId.IsNullOrEmpty() ? String.Empty : string.Format("User ID={0};Password={1}", UserId, Password));
            }
        }
    }

    public static class ConnectionExtension
    {

        /// <summary>
        /// Convert IDataReader to IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}
