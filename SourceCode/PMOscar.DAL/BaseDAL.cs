// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : BaseDAL.cs
// Created Date: 12/04/2012
// Description : Base class for all DAL classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PMOscar
{
    /// <summary>
    /// A generic DAL class for data access for any mapped entity.
    /// </summary>
    public class BaseDAL
    {
        public static SqlConnection DC;


        /// <summary>
        /// Executes the SP non query.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static int ExecuteSPNonQuery(string spName, System.Collections.Generic.IList<SqlParameter> parameters)
        {
            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            int rowCount = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return rowCount;
        }


        /// <summary>
        /// Executes the SP data set.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static DataSet ExecuteSPDataSet(string spName, IList<SqlParameter> parameters)
        {
            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataSet ds = new DataSet();
            ada.SelectCommand = cmd;
            ada.Fill(ds);
            ada.SelectCommand.Connection.Close();
            return ds;
        }

        /// <summary>
        /// Executes the SP named data set.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="DataSetName">Name of the data set.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static DataSet ExecuteSPNamedDataSet(string spName, string DataSetName, IList<SqlParameter> parameters)
        {

            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataSet ds = new DataSet();
            ada.SelectCommand = cmd;
            ada.Fill(ds, DataSetName);
            ada.SelectCommand.Connection.Close();
            return ds;

        }

        /// <summary>
        /// Executes the SP data table.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static DataTable ExecuteSPDataTable(string spName, IList<SqlParameter> parameters)
        {

            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ada.SelectCommand = cmd;
            ada.Fill(dt);
            ada.SelectCommand.Connection.Close();
            return dt;

        }
        /// <summary>
        /// Excecutes sql statements which returns values
        /// </summary>
        /// <param name="spName">Name of the stored procedure</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>
        /// data reader
        /// </returns>
        public static SqlDataReader ExecuteSPReader(string spName, IList<SqlParameter> parameters)
        {
            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return rd;
        }


        /// <summary>
        /// Excecutes sql statements which returns a single value
        /// </summary>
        /// 
        /// <param name="spName">
        /// Name of the stored procedure
        /// </param>
        /// 
        /// <param name="parameters">
        /// List of parameters
        /// </param>
        /// 
        /// <returns>
        /// integer
        /// </returns>
        public static int ExecuteSPScalar(string spName, IList<SqlParameter> parameters)
        {
            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            int ReturnScaler = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
            return ReturnScaler;
        }
       public static int ExecuteSP(string spName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = GetConnectionObject();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }

        /// <summary>
        /// Executes the SP Phase data table.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static DataTable ExecuteSPPhaseDataTable(string spName, IList<SqlParameter> parameters)
        {

            SqlCommand cmd = GetSPCommandObject(spName, parameters);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ada.SelectCommand = cmd;
            ada.Fill(dt);
            ada.SelectCommand.Connection.Close();
            return dt;
        }

        /// <summary>
        /// Creates the command object ready to excecute.
        /// </summary>
        /// 
        /// <param name="spName">
        /// Name of the stored procedure
        /// </param>
        /// 
        /// <param name="parameters">
        /// List of parameters
        /// </param>
        /// 
        /// <returns>
        /// The command object
        /// </returns>
        public static SqlCommand GetSPCommandObject(string spName, IList<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = GetConnectionObject();
            SqlParameter Parameter = null;
            foreach (SqlParameter Parameter_loopVariable in parameters)
            {
                Parameter = Parameter_loopVariable;
                command.Parameters.Add(Parameter);
            }
            return command;
        }













        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query)
        {
            int rowCount = 0;
            SqlCommand cmd = GetCommandObject(query);
            try
            {
                rowCount = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (System.Exception ex)
            {
            }
            return rowCount;
        }


        /// <summary>
        /// Executes the data table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string query)
        {
            SqlCommand cmd = GetCommandObject(query);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ada.SelectCommand = cmd;
            ada.Fill(dt);
            ada.SelectCommand.Connection.Close();
            return dt;
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string query)
        {
            SqlCommand cmd = GetCommandObject(query);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataSet ds = new DataSet();
            ada.SelectCommand = cmd;
            ada.Fill(ds);
            ada.SelectCommand.Connection.Close();
            return ds;
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="params">The @params.</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string query, IList<SqlParameter> @params)
        {
            SqlCommand cmd = GetCommandObject(query);
            cmd.Parameters.AddRange(((List<SqlParameter>)@params).ToArray());
            SqlDataAdapter ada = new SqlDataAdapter();
            DataSet ds = new DataSet();
            ada.SelectCommand = cmd;
            ada.Fill(ds);
            ada.SelectCommand.Connection.Close();
            return ds;
        }


        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetRow(string query)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            SqlCommand cmd = GetCommandObject(query);
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (rd.Read())
            {
                for (int index = 0; index <= rd.FieldCount - 1; index++)
                {
                    row.Add(rd.GetName(index), rd.GetValue(index));
                }
            }
            rd.Close();
            return row;
        }


        /// <summary>
        /// Gets the row SP.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetRowSP(string query)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            SqlCommand cmd = GetCommandObject(query);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (rd.Read())
            {
                for (int index = 0; index <= rd.FieldCount - 1; index++)
                {
                    row.Add(rd.GetName(index), rd.GetValue(index));
                }
            }
            rd.Close();
            return row;
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="params">The @params.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetRow(string query, IList<SqlParameter> @params)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            SqlCommand cmd = GetCommandObject(query);
            cmd.Parameters.AddRange(((List<SqlParameter>)@params).ToArray());
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (rd.Read())
            {
                for (int index = 0; index <= rd.FieldCount - 1; index++)
                {
                    row.Add(rd.GetName(index), rd.GetValue(index));
                }
            }
            rd.Close();
            return row;
        }


        /// <summary>
        /// Gets the row SP.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="params">The @params.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetRowSP(string query, IList<SqlParameter> @params)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            SqlCommand cmd = GetCommandObject(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(((List<SqlParameter>)@params).ToArray());
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (rd.Read())
            {
                for (int index = 0; index <= rd.FieldCount - 1; index++)
                {
                    row.Add(rd.GetName(index), rd.GetValue(index));
                }
            }
            rd.Close();
            return row;
        }


        /// <summary>
        /// Executes the named data set.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="DataSetName">Name of the data set.</param>
        /// <param name="DataTableName">Name of the data table.</param>
        /// <returns></returns>
        public static DataSet ExecuteNamedDataSet(string query, string DataSetName, string DataTableName)
        {
            SqlCommand cmd = GetCommandObject(query);
            SqlDataAdapter ada = new SqlDataAdapter();
            DataSet ds = new DataSet(DataSetName);
            ada.SelectCommand = cmd;
            ada.Fill(ds, DataTableName);
            ada.SelectCommand.Connection.Close();
            return ds;
        }


        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string query)
        {
            SqlCommand cmd = GetCommandObject(query);
            SqlDataReader rd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return rd;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static object ExecuteScalar(string query)
        {
            SqlCommand cmd = GetCommandObject(query);
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;
        }

        /// <summary>
        /// Gets the connection object.
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnectionObject()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }

        /// <summary>
        /// Copies the employee leave.
        /// </summary>
        public static void CopyEmployeeLeave()
        {
            DataTable dtEmployeeLeave = new DataTable();
            var mySqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["OrangeHr"].ToString());
            var mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM vw_empleaveForPMOscar", mySqlConnection);
            mySqlConnection.Open();
            var mySqlDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dtEmployeeLeave);
            mySqlConnection.Close();
            if (dtEmployeeLeave.Rows.Count > 0)
            {
                ExecuteNonQuery("DELETE FROM EmployeeLeave");
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
                sqlConnection.Open();

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    sqlBulkCopy.DestinationTableName = "EmployeeLeave";
                    sqlBulkCopy.WriteToServer(dtEmployeeLeave);
                }
            }
        }

        /// <summary>
        /// Gets the command object.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static SqlCommand GetCommandObject(string query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = GetConnectionObject();
            return cmd;
        }

        /// <summary>
        /// Performs the DB operation.
        /// </summary>
        /// <param name="query">The query.</param>
        public static void performDBOperation(string query)
        {
            SqlCommand cmd = GetCommandObject(query);
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (System.Exception ex)
            {
            }
        }

        /// <summary>
        /// Create_s the column.
        /// </summary>
        /// <param name="ColName">Name of the col.</param>
        /// <param name="myTable">My table.</param>
        /// <param name="MultiplyVar">if set to <c>true</c> [multiply var].</param>
        public static void Create_Column(string ColName, DataTable myTable, bool MultiplyVar)
        {
            DataColumn DcColumn = new DataColumn();
            var _with1 = DcColumn;
            _with1.ColumnName = ColName;
            if (MultiplyVar == true)
            {
                _with1.DataType = System.Type.GetType("System.Decimal");
            }
            else
            {
                _with1.DataType = System.Type.GetType("System.String");
            }
            //.Unique = True'
            _with1.AutoIncrement = false;
            _with1.Caption = ColName;
            _with1.ReadOnly = false;
            myTable.Columns.Add(DcColumn);
        }



        /// <summary>
        /// Encrypts the text.
        /// </summary>
        /// <param name="strText">The STR text.</param>
        /// <returns></returns>
        public static string EncryptText(string strText)
        {
            return Encrypt(strText, "12345", true);
        }

        // Decrypt the text 
        public static string DecryptText(string strText)
        {
            return Decrypt(strText, "12345", true);
        }



        // The function used to encrypt a string
        public static string Encrypt(string message, string securityKey, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(message);
                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                //If hashing use get hashcode regards to the key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //Set the ClientCode as secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                //There are other 4 modes, we choose ECB(Electronic Code Book)
                tdes.Mode = CipherMode.ECB;

                //Padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();

                //Transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                //Return the encrypted data into unreadable string format
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Encrypt failed " + ex + ex.StackTrace);
            }
        }

        // The function used to decrypt the string
        public static string Decrypt(string cipherData, string securityKey, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherData);
                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                if (useHashing)
                {
                    //If hashing was used get the hash code with regards to the key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
                    hashmd5.Clear();
                }
                else
                {
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                //There are other 4 modes, we choose ECB(Electronic Code Book)
                tdes.Mode = CipherMode.ECB;
                //Padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                //Return the decrypted mesage
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw new Exception("Decrypt failed " + ex + ex.StackTrace);
            }
        }


        //Added Encryption and Decryption Common For Core and Frameworks
        static string keyString = "E546C8DF278CD5931069B522E695D4F2";

        public static string EncryptString(string text)
        {
            var key = System.Text.Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }


        /// <summary>
        /// Copies the bugzilla projects.
        /// </summary>
        public static DataTable CopyBugzillaProject()
        {
            DataTable dtBugzillaProject = new DataTable();
            var mySqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["Bugzilla"].ToString());
            var mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("SELECT id,name FROM products", mySqlConnection);
            mySqlConnection.Open();
            var mySqlDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dtBugzillaProject);
            mySqlConnection.Close();
            //if (dtBugzillaProject.Rows.Count > 0)
            //{
            //    ExecuteNonQuery("DELETE FROM EmployeeLeave");
            //    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
            //    sqlConnection.Open();

            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
            //    {
            //        sqlBulkCopy.DestinationTableName = "EmployeeLeave";
            //        sqlBulkCopy.WriteToServer(dtEmployeeLeave);
            //    }
            //}

            if (dtBugzillaProject.Rows.Count > 0)
            {
                return dtBugzillaProject;
            }
            else
            {
                return null;
            }

        }

    }
}
