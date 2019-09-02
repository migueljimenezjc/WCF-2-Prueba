using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SevicioWCF.App_Code
{
    public abstract class BaseDAO
    {
        private ConnectionStringSettings m_connString;
        private DbProviderFactory m_dataProvider;
        private DbConnection m_dbConn;
        private string m_key;

        public BaseDAO(ConnectionStringSettings connStr)
        {
            m_connString = connStr;
            m_dataProvider = DbProviderFactories.GetFactory(m_connString.ProviderName);
        }

        protected DbCommand CreateCommand()
        {
            return m_dataProvider.CreateCommand();
        }

        protected DbConnection CreateConnection()
        {
            if (m_dbConn == null)
                m_dbConn = m_dataProvider.CreateConnection();
            m_dbConn.ConnectionString = m_connString.ConnectionString;
            return m_dbConn;
        }

        protected DataSet GetDataSet(DbCommand cmd)
        {
            DbConnection dbConn = CreateConnection();
            DbDataAdapter dAdapter = GetDataAdapter(cmd);
            DataSet result = new DataSet();
            cmd.Connection = dbConn;
            dAdapter.Fill(result);
            dbConn.Close();
            return result;
        }

        protected DataRow GetDataRow(DbCommand cmd)
        {
            DataSet dsResult = GetDataSet(cmd);
            if (dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables[0].Rows.Count > 0)
                    return dsResult.Tables[0].Rows[0];
            }
            return null/* TODO Change to default(_) if this is not a reference type */;
        }

        protected object ExecuteNonQuery(DbCommand cmd)
        {
            DbConnection dbConn = CreateConnection();
            object result = null;
            cmd.Connection = dbConn;
            dbConn.Open();
            result = cmd.ExecuteNonQuery();
            dbConn.Close();
            return result;
        }

        protected int ExecuteNonQueryIdentity(DbCommand cmd)
        {
            DbConnection dbConn = CreateConnection();
            int result = -1;
            cmd.Connection = dbConn;
            dbConn.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT @@IDENTITY";
            result = System.Convert.ToInt32(cmd.ExecuteScalar());
            dbConn.Close();
            return result;
        }

        protected int ExecuteScalar(DbCommand cmd)
        {
            DbConnection dbConn = CreateConnection();
            int result = -1;
            cmd.Connection = dbConn;
            dbConn.Open();
            result = System.Convert.ToInt32(cmd.ExecuteScalar());
            dbConn.Close();
            return result;
        }

        private DbDataAdapter GetDataAdapter(string query, DbConnection dbConn)
        {
            DbDataAdapter dAdapter = m_dataProvider.CreateDataAdapter();
            DbCommand dbComm = dbConn.CreateCommand();
            dbComm.CommandText = query;
            dAdapter.SelectCommand = dbComm;
            return dAdapter;
        }

        private DbDataAdapter GetDataAdapter(DbCommand cmd)
        {
            DbDataAdapter dAdapter = m_dataProvider.CreateDataAdapter();
            dAdapter.SelectCommand = cmd;
            return dAdapter;
        }

        protected DbParameter AddWithValue(string key, object value, DbType bType)
        {
            if (containsBadData(value.ToString()))
                throw new ArgumentException("El valor contiene palabras invalidas", "value");
            DbParameter param = m_dataProvider.CreateParameter();
            param.ParameterName = key;
            param.Value = value;
            param.DbType = bType;
            return param;
        }

        private bool containsBadData(string stringToCheck)
        {
            var badData = new string[] { "drop", "delete", "insert", "update", "--" };
            for (int x = 10; x <= badData.Length; x++)
            {
                if ((stringToCheck.IndexOf(badData[x]) > -1))
                    return true;
            }
            return false;
        }
    }
}