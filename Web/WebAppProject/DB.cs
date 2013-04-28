using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace WebAppProject
{
    public class DB
    {
        public static int MAX_PASSWORD_LENGHT = 32;
        public static int MAX_USERNAME_LENGHT = 32;

        private static int SALT_LENGHT = 32;
        private static int PASSWORD_FIELD_LENGHT = 64;
        private static int SALT_FIELD_LENGHT = 64;
        private static int ACCOUNT_NOT_FOUND = -1;

        public enum AccountState {Disabled = 0, Active = 1, Suspended = 2};

        public class AccountTable
        {
            static public string Id = "Id";
            static public string UserName   = "UserName";
            static public string Password   = "Password";
            static public string Salt       = "Salt";
            static public string Created    = "Created";
            static public string Updated    = "Updated";
            static public string State      = "State";
        }

        public class PurchaseTable
        {
            static public string ID = "ID";
            static public string Account = "Account";
            static public string Price = "Price";
            static public string Amount = "Amount";
            static public string Distance = "Distance";
            static public string Grade = "Grade";
            static public string Date = "Date";
            static public string Note = "Note";
        }

        public void ReadData()
        {
            //string dataProvider = ConfigurationManager.AppSettings["dataProvider"];
            string connectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString;
        
            using (SqlConnection connection = new SqlConnection())
            {
                string strSQL = "Select * From Accounts";
                using (SqlCommand myCommand = new SqlCommand(strSQL, connection))
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (SqlDataReader myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        string UserName = string.Empty;
                        string Password = string.Empty;
                        string Salt = string.Empty;

                        while (myDataReader.Read())
                        {
                            UserName = myDataReader[AccountTable.UserName].ToString().Trim();
                            Password = myDataReader[AccountTable.Password].ToString().Trim();
                            Salt = myDataReader[AccountTable.Salt].ToString().Trim();
                        }
                    }
                }
            }            
        }

        static public void AddAccount(string userName, string password)
        {
            ValidateUserNamePassword(userName, password);

            string sqlInsert = string.Format("Insert Into dbo.Accounts " +
                                             "({0}, {1}, {2}, {3}, {4}, {5}) Values " +
                                             "(@{0}, @{1}, @{2}, @{3}, @{4}, @{5})",
                                             AccountTable.UserName, AccountTable.Password, AccountTable.Salt, AccountTable.Created, AccountTable.Updated, AccountTable.State);

            byte[] salt = new byte[SALT_LENGHT];
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                saltGenerator.GetBytes(salt);
            }

            byte[] encriptedSaltedPassword = Authentification.MakeEncriptedSaltedPassword(password, salt);
            string encriptedSaltedPasswordString = Convert.ToBase64String(encriptedSaltedPassword);

            Debug.Print(string.Format("encriptedSaltedPasswordString.Length {0}", encriptedSaltedPasswordString.Length));
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(encriptedSaltedPasswordString.Length > PASSWORD_FIELD_LENGHT,
                    string.Format("The encriptedSaltedPasswordString is loo long:  {0}", encriptedSaltedPasswordString.Length));

            string encodedSaltBase64String = Convert.ToBase64String(salt);

            Debug.Print(string.Format("encodedSaltBase64String.Length {0}", encodedSaltBase64String.Length));
            Validator.ThrowIfTrue<ArgumentOutOfRangeException>(encodedSaltBase64String.Length > SALT_FIELD_LENGHT,
                    string.Format("The encodedSaltBase64String is loo long:  {0}", encodedSaltBase64String.Length));

            using (SqlConnection connection = new SqlConnection())
            {
                using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.UserName;
                    parameter.Value = userName;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = MAX_USERNAME_LENGHT;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.Password;
                    parameter.Value = encriptedSaltedPasswordString;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = PASSWORD_FIELD_LENGHT;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.Salt;
                    parameter.Value = encodedSaltBase64String;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = SALT_FIELD_LENGHT;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.Created;
                    parameter.Value = DateTime.Now.ToUniversalTime();
                    parameter.SqlDbType = SqlDbType.DateTime2;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.Updated;
                    parameter.Value = DateTime.Now.ToUniversalTime();
                    parameter.SqlDbType = SqlDbType.DateTime2;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.State;
                    parameter.Value = AccountState.Active;
                    parameter.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(parameter);

                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString; ;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
             }
        }

        static public bool AccessIsAllowed(string userName, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection())
            {
                string strSQL = string.Format("Select * From Accounts where {0} = @{0}", AccountTable.UserName);
                using (SqlCommand selectUserCommand = new SqlCommand(strSQL, connection))
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.UserName;
                    parameter.Value = userName;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = MAX_USERNAME_LENGHT;
                    selectUserCommand.Parameters.Add(parameter);

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (SqlDataReader myDataReader = selectUserCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        DataTable accountsTable = new DataTable();
                        accountsTable.Load(myDataReader);
                        if (accountsTable.Rows.Count != 1 || accountsTable.HasErrors)
                            return false;

                        DataRow accountRow = accountsTable.Rows[0];
                        byte[] salt = Convert.FromBase64String((string)accountRow[AccountTable.Salt]);
                        byte[] encriptedSaltedPassword = Authentification.MakeEncriptedSaltedPassword(password, salt);
                        string encriptedSaltedPasswordStringByUser = Convert.ToBase64String(encriptedSaltedPassword);
                        
                        string passwordDB = (string)accountRow[AccountTable.Password];

                        return encriptedSaltedPasswordStringByUser == passwordDB;
                    }
                }
            }            
        }

        static private void ValidateUserNamePassword(string userName, string password)
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

        static public DataTable GetAccount(string userName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection())
            {
                string strSQL = string.Format("Select * From Accounts where {0} = @{0}", AccountTable.UserName);
                using (SqlCommand selectUserCommand = new SqlCommand(strSQL, connection))
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@" + AccountTable.UserName;
                    parameter.Value = userName;
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Size = MAX_USERNAME_LENGHT;
                    selectUserCommand.Parameters.Add(parameter);

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (SqlDataReader myDataReader = selectUserCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        DataTable accountsTable = new DataTable();
                        accountsTable.Load(myDataReader);
                        if (accountsTable.Rows.Count != 1 || accountsTable.HasErrors)
                            return null;

                        return accountsTable;
                    }
                }
            }
        }

        static private int GetAccountId(string userName)
        {
            DataTable accountsTable = GetAccount(userName);
            if (accountsTable == null || accountsTable.Rows.Count < 1)
                return ACCOUNT_NOT_FOUND;

            DataRow accountRow = accountsTable.Rows[0];
            int accountId = (int)accountRow[DB.AccountTable.Id];
            return accountId;
        }

        static public bool AddPurchase(string userName, decimal price, int amount, int distance, int grade, DateTime date, string note)
        {
            try
            {
                int accountId = GetAccountId(userName);
                if (accountId == ACCOUNT_NOT_FOUND)
                    return false;

                string sqlInsert = string.Format("Insert Into dbo.Purchase " +
                                                 "({0}, {1}, {2}, {3}, {4}, {5}, {6}) Values " +
                                                 "(@{0}, @{1}, @{2}, @{3}, @{4}, @{5}, @{6})",
                                                 PurchaseTable.Account, PurchaseTable.Price, PurchaseTable.Amount, PurchaseTable.Distance, PurchaseTable.Date, PurchaseTable.Note, PurchaseTable.Grade);

                using (SqlConnection connection = new SqlConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Account;
                        parameter.Value = accountId;
                        parameter.SqlDbType = SqlDbType.Int;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Price;
                        parameter.Value = price;
                        parameter.SqlDbType = SqlDbType.Money;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Amount;
                        parameter.Value = amount;
                        parameter.SqlDbType = SqlDbType.Int;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Distance;
                        parameter.Value = distance;
                        parameter.SqlDbType = SqlDbType.Int;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Grade;
                        parameter.Value = grade;
                        parameter.SqlDbType = SqlDbType.Int;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Date;
                        parameter.Value = date;
                        parameter.SqlDbType = SqlDbType.Date;
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter();
                        parameter.ParameterName = "@" + PurchaseTable.Note;
                        parameter.Value = note;
                        parameter.SqlDbType = SqlDbType.Char;
                        command.Parameters.Add(parameter);

                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString; ;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { 
                //Log exception...
                return false;
            }
            return true;
        }

        static public DataTable GetPurchases(string userName)
        {
            DataTable purchasesTable = new DataTable(); 
            
            int accountId = GetAccountId(userName);
            if (accountId == ACCOUNT_NOT_FOUND)
                return purchasesTable;

            string connectionString = ConfigurationManager.ConnectionStrings["gasTrackerConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection())
            {
                string strSQL = string.Format("Select * From Purchase where {0} = @{0} order by Date", PurchaseTable.Account);
                using (SqlCommand selectUserCommand = new SqlCommand(strSQL, connection))
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@" + PurchaseTable.Account;
                    parameter.Value = accountId;
                    parameter.SqlDbType = SqlDbType.Int;
                    selectUserCommand.Parameters.Add(parameter);

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (SqlDataReader myDataReader = selectUserCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        purchasesTable.Load(myDataReader);
                        return purchasesTable;
                    }
                }
            }
        }
    }
}