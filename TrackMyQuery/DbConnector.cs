using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

//@sqlchow: This class is a sum of all the research I was doing on creating a dbconnection factory.

namespace TrackMyQuery
{
    class DbConnector : IDisposable
    {
        private string strConnectionString;
        private DbConnection dbConnection;
        private DbCommand dbCommand;
        private DbProviderFactory dbFactory = null;
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;
        private string strLogFile;

        public DbConnector(string connectionstring, Providers provider)
        {
            strConnectionString = connectionstring;
            switch (provider)
            {
                case Providers.SqlServer:
                    dbFactory = SqlClientFactory.Instance;
                    break;
                case Providers.OleDb:
                    dbFactory = OleDbFactory.Instance;
                    break;
                case Providers.ODBC:
                    dbFactory = OdbcFactory.Instance;
                    break;
            }

            dbConnection = dbFactory.CreateConnection();
            dbCommand = dbFactory.CreateCommand();

            dbConnection.ConnectionString = strConnectionString;
            dbCommand.Connection = dbConnection;
        }

        public DbConnector(string strConnectionString)
            : this(strConnectionString, Providers.SqlServer)
        {
        }

        public bool HandleErrors
        {
            get { return boolHandleErrors; }
            set { boolHandleErrors = value; }
        }

        public string LastError
        {
            get { return strLastError; }
        }

        public bool LogErrors
        {
            get { return boolLogError; }
            set { boolLogError = value; }
        }

        public string LogFile
        {
            get { return strLogFile; }
            set { strLogFile = value; }
        }

        public int AddParameter(string name, object value)
        {
            DbParameter dbParam = dbFactory.CreateParameter();
            dbParam.ParameterName = name;
            dbParam.Value = value;

            return dbCommand.Parameters.Add(dbParam);
        }

        public int AddParameter(DbParameter dbParam)
        {
            return dbCommand.Parameters.Add(dbParam);
        }

        public DbCommand Command
        {
            get { return dbCommand; }
        }

        public DbConnection Connection
        {
            get { return dbConnection; }
        }

        public DbDataReader ExecuteReader(string dbQuery, CommandType dbCommandType, ConnectionState dbConnectionState)
        {
            DbDataReader dbReader = null;

            dbCommand.CommandText = dbQuery;
            dbCommand.CommandType = dbCommandType;

            try
            {
                if (dbConnection.State == System.Data.ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                if (dbConnectionState == ConnectionState.CloseOnExit)
                {
                    dbReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    dbReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                dbCommand.Parameters.Clear();
            }

            return dbReader;
        }


        public DataSet ExecuteDataSet(string dbQuery, CommandType dbCommandType, ConnectionState dbConnectionState)
        {
            DataSet dbDataSet = new DataSet();
            DbDataAdapter dbAdapter = dbFactory.CreateDataAdapter();

            dbCommand.CommandText = dbQuery;
            dbCommand.CommandType = dbCommandType;
            dbAdapter.SelectCommand = dbCommand;

            try
            {
                dbAdapter.Fill(dbDataSet);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                dbCommand.Parameters.Clear();
                if (dbConnectionState == ConnectionState.CloseOnExit)
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return dbDataSet;
        }

        public object ExecuteScalar(string dbQuery, CommandType dbCommandType, ConnectionState dbConnectionState)
        {
            dbCommand.CommandText = dbQuery;
            dbCommand.CommandType = dbCommandType;
            object returnObj = null;
            try
            {
                if (dbConnection.State == System.Data.ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                returnObj = dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                dbCommand.Parameters.Clear();
                if (dbConnectionState == ConnectionState.CloseOnExit)
                {
                    dbConnection.Close();
                }
            }

            return returnObj;
        }

        private void HandleExceptions(Exception ex)
        {
            StackFrame exStack = new StackTrace(ex, true).GetFrame(0);
            string exceptionMethod = exStack.GetMethod().Name;
            string exceptionLine = exStack.GetFileLineNumber().ToString();
            string exSource = string.Format("Exception raised in {0} at line number: {1}", exceptionMethod, exceptionLine);

            if (LogErrors)
            {
                WriteToLog(exceptionMethod, exceptionLine, ex.Message);
            }

            if (HandleErrors)
            {
                strLastError = exSource + "\n" + ex.Message;
            }
            else
            {
                throw ex;
            }
        }

        private void WriteToLog(string methodName, string lineNumber, string message)
        {
            StreamWriter logWriter = File.AppendText(LogFile);
            logWriter.WriteLine(DateTime.Now.ToString() + " - [" + methodName + ":" + lineNumber + "] - " + message);
            logWriter.Close();
        }

        private void WriteToLog(string message)
        {
            StreamWriter logWriter = File.AppendText(LogFile);
            logWriter.WriteLine(DateTime.Now.ToString() + " - " + message);
            logWriter.Close();
        }

        public void Dispose()
        {
            dbCommand.Dispose();
            dbConnection.Close();
            dbConnection.Dispose();
        }
    }

    public enum Providers
    {
        SqlServer, ODBC, OleDb
    }

    public enum ConnectionState
    {
        KeepOpen, CloseOnExit
    }
}
