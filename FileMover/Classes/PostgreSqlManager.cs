using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.IO;
using System.Collections;


namespace FileMover.Classes
{
    internal class PostgreSqlManager
    {
        private string DbName = ConfigurationManager.AppSettings["Database"];
        private string TableName = ConfigurationManager.AppSettings["tableName"];
        private readonly string ConnactionString = ConfigurationManager.ConnectionStrings["conDb"].ConnectionString;
        private NpgsqlConnection Connector; 
        public string ErrorsMessage { get; private set; } = "";

        public static async Task<PostgreSqlManager> CreateObjectAsync()
        {
            var postgreSqlManager = new PostgreSqlManager();

            await postgreSqlManager.InitializeAsync();

            return postgreSqlManager;
        }
        private PostgreSqlManager()
        {
        }
        private async Task InitializeAsync()
        {
            try
            {
                await CreateDataBaseAsync();

                Connector = new NpgsqlConnection(ConnactionString);
                Connector.Open();

                await CreateTableAsync(TableName);

            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }
        }
        public async Task CreateDataBaseAsync()
        {
            try
            {
                string connactionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (var connection = new NpgsqlConnection(connactionString))
                {
                    connection.Open();
                    
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = $"SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = '{DbName}');";
                        var result = await cmd.ExecuteScalarAsync();

                        if ((bool)result == false)
                        {
                            cmd.CommandText = $"CREATE DATABASE  {DbName}";
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }

        }

        public async Task CreateTableAsync(string TableName)
        {
            try
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = Connector;

                    cmd.CommandText = $"SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = '{TableName}')";
                    var result = await cmd.ExecuteScalarAsync();
                    if ((bool)result == false)
                    {
                        cmd.CommandText = $"CREATE TABLE {TableName}" +
                                      "(id SERIAL PRIMARY KEY, file_name VARCHAR(255)," +
                                      "dir_in VARCHAR(255), key_word VARCHAR(255)," +
                                      "match BOOLEAN, dir_out VARCHAR(255)," +
                                      "day_time VARCHAR(255));";
                        await cmd.ExecuteNonQueryAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }


        }
        public async Task InsertDataAsync(SearchResult sr)//(string file_name, string dir_in, string key_word, bool match, string dir_out, string day_time)
        {
            try
            {
                string sqlCommand = $"INSERT INTO {TableName} (file_name, dir_in, key_word, match, dir_out, day_time) " +
                                    $"VALUES (@file_name, @dir_in, @key_word, @match, @dir_out, @day_time)";
                var queryArgyments = new
                {
                    file_name = sr.file_name,
                    dir_in=sr.dir_in,
                    key_word=sr.key_word,
                    match=sr.match,
                    dir_out=sr.dir_out,
                    day_time=sr.day_time,
                };
                await Connector.ExecuteAsync(sqlCommand, queryArgyments);
                /*
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = Connector;

                    cmd.CommandText = $"INSERT INTO {TableName} (file_name, dir_in, key_word, match, dir_out, day_time ) " +
                                      "VALUES (@val1, @val2, @val3, @val4, @val5, @val6) ";
                    cmd.Parameters.AddWithValue("val1", file_name);
                    cmd.Parameters.AddWithValue("val2", dir_in);
                    cmd.Parameters.AddWithValue("val3", key_word);
                    cmd.Parameters.AddWithValue("val4", match);
                    cmd.Parameters.AddWithValue("val5", dir_out);
                    cmd.Parameters.AddWithValue("val6", day_time);

                    await cmd.ExecuteNonQueryAsync();
                }
                */
            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }

        }

        public async Task<int> CountMatchesAsync()
        {
            int rezult = -1;
            try
            {
                string sqlCommand = $"SELECT count(*) FROM {TableName} " +
                                    $"WHERE day_time = (SELECT day_time FROM {TableName} ORDER BY id DESC LIMIT 1)";

                rezult = Convert.ToInt32(await Connector.ExecuteScalarAsync(sqlCommand));
                /*
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = Connector;

                    cmd.CommandText = $"SELECT count(*) FROM {TableName} " +
                                      $"where day_time = (SELECT day_time FROM {TableName} ORDER BY id DESC LIMIT 1)";

                    var reader = await cmd.ExecuteScalarAsync();

                    rezult = Convert.ToInt32(reader);
                }
                */
            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }
            return rezult;
        }
        public async Task<IEnumerable<SearchResult>> CountFilesByMatchesAsync()
        {
            string sqlCommand = $"SELECT key_word, count(*)  FROM {TableName} " +
                                $"where day_time = (SELECT day_time FROM {TableName} ORDER BY id DESC LIMIT 1) " +
                                $"GROUP by key_word";
            return await Connector.QueryAsync<SearchResult>(sqlCommand);
        }

        /*
        public async Task<List<string>> GetFindedWordsCount()
        {
            var result = new List<string>();
            try
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = Connector;

                    cmd.CommandText = $"SELECT key_word, count(*)  FROM {TableName} " +
                                      $"where day_time = (SELECT day_time FROM {TableName} ORDER BY day_time desc LIMIT 1) " +
                                      $"GROUP by key_word;";
                    var reader = await cmd.ExecuteReaderAsync();
                    
                    while (await reader.ReadAsync())
                    {
                        result.Add(reader[0] as string + " " + reader[1]);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorsMessage = ex.Message;
            }
            return result;
        }
        */
        public void CloseConnection()
        {
            Connector.Close();
        }

        public async Task<IEnumerable<SearchResult>> GetAllRowsLastSearchAsync()//выдает все записи из последнего поиска совпадений
        {
            string sql = $"SELECT * FROM {TableName} WHERE day_time = (SELECT day_time FROM {TableName} ORDER BY id DESC LIMIT 1)";
                         
            return await Connector.QueryAsync<SearchResult>(sql);
        }
        public IEnumerable<SearchResult> TotalRequests()
        {
            string sql = $"SELECT day_time, count(id) FROM {TableName} GROUP by day_time";
            
            return Connector.Query<SearchResult>(sql);
        }
        public int TotalRows()
        {
            string slq = $"SELECT count(*) FROM {TableName}";

            return Connector.ExecuteScalar<int>(slq);
        }

    }
    
}