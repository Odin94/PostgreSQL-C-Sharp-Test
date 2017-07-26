using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PostreSQLTest
{
    class DatabaseHelper
    {
        private const String Server = "localhost";
        private const String Port = "5432";
        private const String User = "postgres";
        private const String Password = "test";
        private const String Database = "db_test_c#";

        private NpgsqlConnection connection = null;

        public DatabaseHelper()
        {
            connection = new NpgsqlConnection(
                "Server=" + Server + ";" +
                "Port=" + Port + ";" +
                "User Id=" + User + ";" +
                "Password=" + Password + ";" +
                "Database=" + Database + ";"
            );
        }

        public void Insert(Int32 id, String data)
        {
            OpenConnection();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO " +
                                                          "tb_data(id, data) VALUES(:id, :data)", connection);
                // add params
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("data", NpgsqlTypes.NpgsqlDbType.Text));

                command.Prepare();

                // add values to params
                command.Parameters[0].Value = id;
                command.Parameters[1].Value = data;

                int dataAffected = command.ExecuteNonQuery();
                if (Convert.ToBoolean(dataAffected))
                {
                    Console.WriteLine("Data successfully saved!");
                }
            }
            catch (NpgsqlException ex)
            {
                Debug.WriteLine(ex);
            }

            CloseConnection();
        }

        public List<Data> SelectAll()
        {
            List<Data> data = new List<Data>();

            OpenConnection();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tb_data ORDER BY id ASC", connection);
                command.Prepare();
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    data.Add(new Data(dr.GetInt32(0), dr.GetString(1)));
                }
            }
            catch (NpgsqlException ex)
            {
                Debug.WriteLine(ex);
            }

            CloseConnection();

            return data;
        }

        private void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (NpgsqlException ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (NpgsqlException ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }

    internal class Data
    {
        public Int32 id;
        public String data;

        public Data(Int32 id, String data)
        {
            this.id = id;
            this.data = data;
        }
    }
}