using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;

namespace DBAccess
{
    [XmlRoot("DBSettings")]
    public class DataAccess
    {
        [XmlElement(ElementName = "Server", IsNullable = false)]
        public String Server;
        [XmlElement(ElementName = "Database", IsNullable = false)]
        public String Database;
        [XmlElement("User")]
        public String User;

        [XmlElement("Password")]
        public String Password;

        [XmlIgnore]
        private String DecryptedPassword
        {
            get { 
                try
                {
                    return Encryption.ToDecryptMD5(Password, null, true);
                }
                catch (Exception) 
                {
                    throw new Exception("Failed to decrypt DB Password.");
                }
            }
        }

        [XmlElement("WindowsAuth")]
        public Boolean WindowsAuthentication = false;

        [XmlIgnore]
        private SqlConnection connection;

        public DataAccess() { }

        public DataAccess(string server, string database, string user, string password) 
        {
            Server = server;
            Database = database;
            User = user;
            Password = Encryption.ToEncryptMD5(password, null, true);
            BuildConnectionString();
        }

        public DataAccess(string server, string database)
        {
            Server = server;
            Database = database;
            WindowsAuthentication = true;
            BuildConnectionString();
        }

        public bool HasAllSettings()
        {
            if (Server != "" && Database != "" && (WindowsAuthentication || (User != "" && Password != "")))
                return true;
            else
                return false;
        }

        private void BuildConnectionString()
        {
            if (connection == null)
            {
                if (WindowsAuthentication)
                    connection = new SqlConnection(string.Format("Provider=SQLNCLI11.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog={1};Data Source={0};", Server, Database));
                else
                    connection = new SqlConnection(string.Format("Server={0};Database={1};User Id={2};Password={3};", Server, Database, User, DecryptedPassword));
            }
        }

        public bool Connect()
        {
            BuildConnectionString();

            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException) { }
                catch (Exception) { }

                return false;
            }
            else if (connection.State == ConnectionState.Open)
            {
                return true;
            }

            return false;
        }

        public bool TestConnection()
        {
            BuildConnectionString();

            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException) { return false; }
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                return true;
            }
            return false;
        }

        public void Disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        public int Insert(string SQL)
        {
            int num = 0;

            try
            {
                SqlCommand comm = new SqlCommand(SQL, connection);
                num = comm.ExecuteNonQuery();
            }
            catch (SqlException) { }
            catch (InvalidOperationException) { }

            return num;
        }

        public DataSet Select(string SQL)
        {
            try
            {
                SqlCommand comm = new SqlCommand(SQL, connection);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);

                da.Fill(ds);

                return ds;
            }
            catch (SqlException) { }
            catch (Exception) { }

            return null;
        }

        public void SelectWithError(string SQL)
        {
            SqlCommand comm = new SqlCommand(SQL, connection);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(comm);

            da.Fill(ds);
        }
    }
}
