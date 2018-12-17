using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PNetCDMS
{
    class projectDB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public projectDB()
        {
            Initialize();

        }

        private void Initialize()
        {
            server = "www.db4free.net";
            database = "pnetcdma";
            uid = "pnetadmin";
            password = "Pn$3cure";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to db
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public bool Insert(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
                catch
                {
                    this.CloseConnection();
                    return false;
                }
            }
            return false;
        }

        //Update statement
        public bool Update(string query)
        {
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
            }
            catch
            {

                this.CloseConnection();
                return false;
            }
            return false;
        }

        //Delete statement
        public void Delete(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void fill(DataGridView dgv, string query)
        {
            /* This function takes a targetted datagridview and sql query and submits results into the DGV. */
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Store data from database
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    dataAdapter.SelectCommand = new MySqlCommand(query, connection);

                    //Store data from adapter to table
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);

                    //Store in binding source
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = table;

                    //display on correct datagridview
                    dgv.DataSource = bSource;
                    this.connection.Close();
                }
            }
            catch
            {
                this.CloseConnection();
            }
        }

        public void fill(ComboBox combo, string query, string displayMember, string valueMember)
        {
            /* This function takes a targetted datagridview and sql query and submits results into the DGV. */
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Store data from database
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    dataAdapter.SelectCommand = new MySqlCommand(query, connection);

                    //Store data from adapter to table
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    combo.DataSource = table;
                    combo.DisplayMember = displayMember;
                    combo.ValueMember = valueMember;

                    this.connection.Close();
                }
            }
            catch
            {
                this.CloseConnection();
            }
        }

        public string getString(string query)
        {

            string data;
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();

                    //Assign the query using CommandText
                    cmd.CommandText = query;

                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    data = cmd.ExecuteScalar().ToString();

                    //close connection
                    this.CloseConnection();
                    return data;
                }
                catch
                {
                    this.CloseConnection();
                    return null;
                }

            }
            return null;
        }
        public int getInt(string query)
        {

            int data;
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();

                    //Assign the query using CommandText
                    cmd.CommandText = query;

                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    data = Convert.ToInt32(cmd.ExecuteScalar());

                    //close connection
                    this.CloseConnection();
                    return data;
                }
                catch
                {
                    this.CloseConnection();
                    return 0;
                }

            }
            return 0;
        }

        public bool getBool(string query, bool requireBool)
        {


            if (this.OpenConnection() == true)
            {
                try
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();

                    //Assign the query using CommandText
                    cmd.CommandText = query;

                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteScalar();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
                catch
                {
                    this.CloseConnection();
                    return false;
                }

            }
            return false;
        }
        public string encrypt(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}
