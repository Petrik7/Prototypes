using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Diagnostics;

namespace WebAppProject
{
    public class DB
    {
        public static int MAX_PASSWORD_LENGHT = 32;
        public static int MAX_USERNAME_LENGHT = 32;

        private static int SOLT_LENGHT = 32;
        private static int PASSWORD_FIELD_LENGHT = 64;
        private static int SOLT_FIELD_LENGHT = 64;

        public void ReadData()
        {
            string dataProvider = ConfigurationManager.AppSettings["dataProvider"];
            string connectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString;
        
            // Create an open a connection.
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string strSQL = "Select * From Accounts";
                using (SqlCommand myCommand = new SqlCommand(strSQL, connection))
                {
                    using (SqlDataReader myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        string UserName = string.Empty;
                        string Password = string.Empty;
                        string Solt = string.Empty;

                        while (myDataReader.Read())
                        {
                            UserName = myDataReader["UserName"].ToString().Trim();
                            Password = myDataReader["Password"].ToString().Trim();
                            Solt = myDataReader["Solt"].ToString().Trim();
                        }
                    }
                }
            }            
        }

        public static void AddAccount(string userName, string password)
        {
            ValidateUserNamePassword(userName, password);

            string sqlInsert = string.Format("Insert Into dbo.Accounts " +
                                                "(UserName, Password, Solt) Values " +
                                                "(@UserName, @Password, @Solt)");

            byte[] solt = new byte[SOLT_LENGHT];
            using (RNGCryptoServiceProvider soltGenerator = new RNGCryptoServiceProvider())
            {
                soltGenerator.GetBytes(solt);
            }

            byte[] soltedPassword = new byte[solt.Length + password.Length];
            byte[] encriptedSoltedPassword = Authentification.MakeEncriptedSoltedPassword(password, solt);
            string encriptedSoltedPasswordString = Convert.ToBase64String(encriptedSoltedPassword);

            Debug.Print(string.Format("encriptedSoltedPasswordString.Length {0}", encriptedSoltedPasswordString.Length));
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(encriptedSoltedPasswordString.Length > PASSWORD_FIELD_LENGHT,
                    string.Format("The encriptedSoltedPasswordString is loo long:  {0}", encriptedSoltedPasswordString.Length));

            string encodedSoltBase64String = Convert.ToBase64String(solt);

            Debug.Print(string.Format("encodedSoltBase64String.Length {0}", encodedSoltBase64String.Length));
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(encodedSoltBase64String.Length > SOLT_FIELD_LENGHT,
                    string.Format("The encodedSoltBase64String is loo long:  {0}", encodedSoltBase64String.Length));

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString; ;
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@UserName";
                    parameter.Value = userName;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = MAX_USERNAME_LENGHT;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Password";
                    parameter.Value = encriptedSoltedPasswordString;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = PASSWORD_FIELD_LENGHT;
                    command.Parameters.Add(parameter);

                    byte[] verifyPassword = Convert.FromBase64String((string)parameter.Value);
                    bool good = Authentification.PassworsAreEqual(encriptedSoltedPassword, verifyPassword);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@Solt";
                    parameter.Value = encodedSoltBase64String;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = SOLT_FIELD_LENGHT;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
             }
        }

        private static void ValidateUserNamePassword(string userName, string password)
        { 
            Validator.ThrowIfNullOrEmpty<ArgumentNullException>(userName, "userName cannot be null o empty");
            Validator.ThrowIfNullOrEmpty<ArgumentNullException>(password, "password cannot be null o empty");
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(
                userName.Length > MAX_USERNAME_LENGHT,
                string.Format("userName is too long, the max lenght is {0}", MAX_USERNAME_LENGHT));
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(
                password.Length > MAX_PASSWORD_LENGHT,
                string.Format("password is too long, the max lenght is {0}", MAX_PASSWORD_LENGHT));
        }
    }
}