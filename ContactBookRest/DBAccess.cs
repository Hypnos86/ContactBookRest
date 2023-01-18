using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NLog;

namespace ContactBookRest
{
    public class DBAccess
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        private static SqlConnection _connectionSql;
        public static string ConnectionStringSql { get; set; }  
        public static int CommandTimeoutSql { get; set; } = 30000;
        private static bool _singletonMode = false;
        public static SqlConnection ConnectionSql
        {
            get
            {
                SqlConnection sqlConnection = null;
                if (_singletonMode)
                {
                    if (_connectionSql == null)
                    {
                        sqlConnection = _connectionSql = new SqlConnection(ConnectionStringSql);
                    }
                }
                else
                {
                    sqlConnection = new SqlConnection(ConnectionStringSql);
                }

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                return sqlConnection;
            }
        }

        public static void CloseConnectionSql()
        {
            if (_connectionSql != null)
            {
                if (_connectionSql.State == ConnectionState.Open)
                {
                    _connectionSql.Close();
                }
            }
        }

        public static void CloseConnectionSql(SqlConnection sqlConnection)
        {
            if (sqlConnection != null)
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }



        public static DataSet GetDatasetSql(SqlCommand sql, Exception ex = null)
        {
            DataSet ds;
            ds = new DataSet();
            SqlDataAdapter da = null;
            //string sqlString = sql.ToString();
            sql.Connection = new SqlConnection(ConnectionStringSql);


            try
            {
                da = new SqlDataAdapter(sql);
                da.SelectCommand.CommandTimeout = CommandTimeoutSql;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ex = e;
                _logger.Error(e.Message);
            }
            finally
            {
                if (!_singletonMode && da != null)
                    CloseConnectionSql(da.SelectCommand.Connection);
            }

            return ds;
        }

        public static DataSet GetDatasetSql(string sql, Exception ex = null)
        {
            DataSet ds;
            ds = new DataSet();
            SqlDataAdapter da = null;

            try
            {
                da = new SqlDataAdapter(sql, ConnectionStringSql);
                da.SelectCommand.CommandTimeout = CommandTimeoutSql;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ex = e;
                _logger.Error(e.Message);
            }
            finally
            {
                if (!_singletonMode && da != null)
                    CloseConnectionSql(da.SelectCommand.Connection);
            }

            return ds;
        }

        public static int ExecuteNonQuerySql(SqlCommand command)
        {
            try
            {
                command.Connection = ConnectionSql;
                int result = command.ExecuteNonQuery();

                if (!_singletonMode)
                    CloseConnectionSql(command.Connection);

                return result;
            }
            catch (Exception e)
            {
                if (!_singletonMode && command != null)
                    CloseConnectionSql(command.Connection);

                _logger.Error(e.Message);
                throw (e);
            }
        }

        public static int ExecuteNonQuerySql(SqlCommand command, SqlTransaction trans)
        {
            try
            {
                if (trans.Connection.State != ConnectionState.Open)
                    trans.Connection.Open();

                command.Connection = trans.Connection;
                command.Transaction = trans;
                int result = command.ExecuteNonQuery();

                return result;
            }
            catch (Exception e)
            {
                if (!_singletonMode && command != null)
                    CloseConnectionSql(command.Connection);

                _logger.Error(e.Message);
                throw (e);
            }

        }

        public static object ExecuteScalarSql(SqlCommand command)
        {
            try
            {
                command.Connection = ConnectionSql;
                object result = command.ExecuteScalar();
                if (!_singletonMode)
                    CloseConnectionSql(command.Connection);

                return result;
            }
            catch (Exception e)
            {
                if (!_singletonMode && command != null)
                    CloseConnectionSql(command.Connection);

                _logger.Error(e.Message);
                throw (e);
            }
        }

        public static object ExecuteScalarSql(SqlCommand command, SqlTransaction trans)
        {
            try
            {
                command.Connection = trans.Connection;
                if (trans.Connection.State != ConnectionState.Open)
                    trans.Connection.Open();

                command.Transaction = trans;
                object result = command.ExecuteScalar();

                return result;
            }
            catch (Exception e)
            {
                if (!_singletonMode && command != null)
                    CloseConnectionSql(command.Connection);

                _logger.Error(e.Message);
                throw (e);
            }
        }

        public static void Commit(SqlTransaction trans)
        {
            try
            {
                SqlConnection conn = trans.Connection;
                trans.Commit();
                CloseConnectionSql(conn);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw e;
            }
        }

        public static void Rollback(SqlTransaction trans)
        {
            try
            {
                SqlConnection conn = trans.Connection;
                trans.Rollback();
                CloseConnectionSql(conn);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw e;
            }
        }


    }
}
