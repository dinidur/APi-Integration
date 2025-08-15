
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SingerWebSiteIntegration.Models
{
    public class DBConnection
    {
        OracleConnection oOracleConnection;
        OracleCommand oOracleCommand;
        OracleDataAdapter oOracleAdapter;


    public MySqlConnection GetMysqlConnection()
    {
        try
        {
            // Example connection string for MySQL
            string conString = "Server=localhost;Port=3306;Database=studentmanage;User=root;Password=;";

            var mySqlConnection = new MySqlConnection(conString);
           
            return mySqlConnection;
        }
        catch (Exception ex)
        {
            // Log or handle error
            Console.WriteLine("MySQL Connection Error: " + ex.Message);
            return null;
        }
    }


       

        public enum ReturnType
        {
            DataTable = 1,
            DataRow = 2,
            DataSet = 3
        };



        //newly added on 2018-03-27
        /// <summary>
        ///  execute querries with patameters
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="ReturnType"></param>
        /// <param name="_OraParameter"></param>
        /// <param name="cmdType"></param>
        /// <param name="orConnection"></param>
        /// <returns>objects</returns>
        public object Executes(string SelectQuery, ReturnType ReturnType, OracleParameter[] _OraParameter, CommandType cmdType, OracleConnection orConnection)
        {
            try
            {
                Object objValue = new object();
                switch (ReturnType)
                {
                    case (ReturnType.DataTable):
                        {
                            objValue = ReturnDataTable(SelectQuery, _OraParameter, cmdType, orConnection);
                        }
                        break;

                    case (ReturnType.DataSet):
                        {
                            objValue = ReturnDataSet(SelectQuery, _OraParameter, cmdType, orConnection);
                        }
                        break;

                    case (ReturnType.DataRow):
                        {
                            objValue = ReturnDataRow(SelectQuery, _OraParameter, cmdType, orConnection);
                        }
                        break;
                }
                return objValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="ReturnType"></param>
        /// <param name="cmdType"></param>
        /// <param name="orConnection"></param>
        /// <returns></returns>
        public object Executes(string SelectQuery, ReturnType ReturnType, CommandType cmdType, OracleConnection orConnection)
        {
            try
            {
                Object objValue = new object();
                switch (ReturnType)
                {
                    case (ReturnType.DataTable):
                        {
                            objValue = ReturnDataTable(SelectQuery, cmdType, orConnection);
                        }
                        break;

                    case (ReturnType.DataSet):
                        {
                            objValue = ReturnDataSet(SelectQuery, cmdType, orConnection);
                        }
                        break;

                    case (ReturnType.DataRow):
                        {
                            objValue = ReturnDataRow(SelectQuery, cmdType, orConnection);
                        }
                        break;
                }
                return objValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// With parameters return Data Table
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="_oOracleParameter"></param>
        /// <param name="cmdType"></param>
        /// <param name="oOracleConnection"></param>
        /// <returns></returns>
        private DataTable ReturnDataTable(string SelectQuery, OracleParameter[] _oOracleParameter, CommandType cmdType, OracleConnection oOracleConnection)
        {
            DataTable dt;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            dt = new DataTable();
            conn = oOracleConnection;
            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;

            for (int i = 0; i < _oOracleParameter.Length; i++)
            {
                oOracleCommand.Parameters.Add(_oOracleParameter[i]);
            }

            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(dt);
            return dt;

        }

        /// <summary>
        /// With out parameters return Data Table
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="cmdType"></param>
        /// <param name="oOracleConnection"></param>
        /// <returns></returns>
        private DataTable ReturnDataTable(string SelectQuery, CommandType cmdType, OracleConnection oOracleConnection)
        {
            DataTable dt;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            dt = new DataTable();
            conn = oOracleConnection;
            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;


            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(dt);
            return dt;

        }

        /// <summary>
        /// With parameters return Data Set
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="_oOracleParameter"></param>
        /// <param name="cmdType"></param>
        /// <param name="oOracleConnection"></param>
        /// <returns></returns>
        private DataSet ReturnDataSet(string SelectQuery, OracleParameter[] _oOracleParameter, CommandType cmdType, OracleConnection oOracleConnection)
        {
            DataSet ds;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            ds = new DataSet();

            conn = oOracleConnection;
            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;
            for (int i = 0; i < _oOracleParameter.Length; i++)
            {
                oOracleCommand.Parameters.Add(_oOracleParameter[i]);
            }

            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(ds);

            conn.Close();
            return ds;

        }

        /// <summary>
        /// With out parameters return Data Table
        /// </summary>
        /// <param name="SelectQuery"></param>
        /// <param name="cmdType"></param>
        /// <param name="oOracleConnection"></param>
        /// <returns></returns>
        private DataSet ReturnDataSet(string SelectQuery, CommandType cmdType, OracleConnection oOracleConnection)
        {
            DataSet ds;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            ds = new DataSet();

            conn = oOracleConnection;
            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;

            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(ds);

            conn.Close();
            return ds;

        }


        private DataRow ReturnDataRow(string SelectQuery, OracleParameter[] _oOracleParameter, CommandType cmdType, OracleConnection oOracleConnection)
        {

            DataTable dt;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            dt = new DataTable();
            conn = oOracleConnection;

            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;
            for (int i = 0; i < _oOracleParameter.Length; i++)
            {
                oOracleCommand.Parameters.Add(_oOracleParameter[i]);
            }

            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(dt);
            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];

        }

        private DataRow ReturnDataRow(string SelectQuery, CommandType cmdType, OracleConnection oOracleConnection)
        {

            DataTable dt;
            OracleConnection conn = null;

            oOracleCommand = new OracleCommand();
            dt = new DataTable();
            conn = oOracleConnection;

            oOracleCommand.Connection = conn;
            oOracleCommand.CommandText = SelectQuery;
            oOracleCommand.CommandType = cmdType;

            oOracleAdapter = new OracleDataAdapter(oOracleCommand);
            oOracleAdapter.Fill(dt);
            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];

        }


        public static OracleParameter AddParameter(string Name, object Value)
        {
            OracleParameter Parm;
            try
            {
                Parm = new OracleParameter();
                Parm.ParameterName = Name;
                Parm.Value = Value;

                return Parm;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
