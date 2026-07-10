using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data.Common;

namespace Linx.Tools
{
    public static class ScriptQueryManager
    {
        public static DataTable ExecuteMSSQLCommand(string connString, string commandText, params DbParameter[] parameters)
        {
            var dt = new DataTable();
            var cn = new SqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (SqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return dt;
        }
        public static void ExecuteMSSQLCommandAction(string connString, string commandText, Action<DbDataReader> action, params DbParameter[] parameters)
        {
            var cn = new SqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (SqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }


        public static DataTable ExecuteMySQLCommand(string connString, string commandText, params DbParameter[] parameters)
        {
            var dt = new DataTable();
            var cn = new MySqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (MySqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return dt;
        }
        public static void ExecuteMySQLCommandAction(string connString, string commandText, Action<DbDataReader> action, params DbParameter[] parameters)
        {
            var cn = new MySqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (MySqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }
        

        public static DataTable ExecuteSQLiteCommand(string connString, string commandText, params DbParameter[] parameters)
        {
            var dt = new DataTable();
            var cn = new SQLiteConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (SQLiteParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return dt;
        }
        public static void ExecuteSQLiteCommandAction(string connString, string commandText, Action<DbDataReader> action, params DbParameter[] parameters)
        {
            var cn = new SQLiteConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (SQLiteParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }


        public static DataTable ExecutePostgreSQLCommand(string connString, string commandText, params DbParameter[] parameters)
        {
            var dt = new DataTable();
            var cn = new NpgsqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (NpgsqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return dt;
        }
        public static void ExecutePostgreSQLCommandAction(string connString, string commandText, Action<DbDataReader> action, params DbParameter[] parameters)
        {
            var cn = new NpgsqlConnection(connString);

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (parameters != null)
                    foreach (NpgsqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

    }
}

