using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileMover.Classes
{
    internal class PostgreSqlManager
    {
        private string connactionString;
        private NpgsqlConnection Connector;
        public PostgreSqlManager(string connactionString) 
        {
            this.connactionString = connactionString;
            Connector = new NpgsqlConnection(connactionString);
            Connector.Open();
        }

        public void CreateTable() 
        {
            using (var cmd = new NpgsqlCommand()) 
            { 
                cmd.Connection = Connector;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS search_result" +
                    " (Id SEREAL PRIMERY KEY, " +
                    "file_name VARCHAR(255)," +
                    " dir_in VARCHAR(255)," +
                    " key_word VARCHAR(255)," +
                    " match BOOLEAN, " +
                    "dir_out VARCHAR(255), " +
                    "day_time VARCHAR(255));";
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertData(string file_name, string dir_in, string key_word, bool match,string dir_out, string day_time) 
        {
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = Connector;
                cmd.CommandText = "INSERT INTO search_result (file_name, dir_in, key_word, match, dir_out, day_time ) VALUES " +
                    "(@val1, @val2, @val3, @val4, @val5, @val6) ";
                cmd.Parameters.AddWithValue("val1", file_name);
                cmd.Parameters.AddWithValue("val2", dir_in);
                cmd.Parameters.AddWithValue("val3", key_word);
                cmd.Parameters.AddWithValue("val4", match);
                cmd.Parameters.AddWithValue("val5", dir_out);
                cmd.Parameters.AddWithValue("val6", day_time);
                cmd.ExecuteNonQuery();
            }
        }

        public void CloseConnection()
        { 
            Connector.Close();
        }
    }
}
