﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileMover.Classes
{
    internal class PostgreSqlManager
    {
        private string DbName = "mytest9";
        private string TableName = "search_match";

        private NpgsqlConnection Connector;
        public PostgreSqlManager()
        {

        }
        public async Task InitializeAsync() 
        {
            await CreateDataBaseAsync(DbName);

            await CreateTableAsync(DbName, TableName);
            
            Connector = new NpgsqlConnection($"Host=localhost;Port=5432;Database={DbName};Username=postgres;Password=Sur999");
            Connector.Open();
        }
        public async Task CreateDataBaseAsync(string DbName)
        {
            string connactionString = "Host=localhost;Port=5432;Username=postgres;Password=Sur999";
            using (var con = new NpgsqlConnection(connactionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

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

        public async Task CreateTableAsync(string DbName, string TableName)
        {
            string conString = $"Host=localhost;Port=5432;Database={DbName};Username=postgres;Password=Sur999";
            using (var Connector = new NpgsqlConnection(conString))
            {
                Connector.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = Connector;
                    
                    cmd.CommandText = $"SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = '{TableName}')";
                    var result = await cmd.ExecuteScalarAsync();
                    if ((bool)result == false)
                    {
                        cmd.CommandText = $"CREATE TABLE {TableName}" +
                                      "(Id SERIAL PRIMARY KEY, file_name VARCHAR(255)," +
                                      "dir_in VARCHAR(255), key_word VARCHAR(255)," +
                                      "match BOOLEAN, dir_out VARCHAR(255)," +
                                      "day_time VARCHAR(255));";
                        await cmd.ExecuteNonQueryAsync();
                    }

                }

            }
        }
        public async Task InsertDataAsync(string file_name, string dir_in, string key_word, bool match, string dir_out, string day_time)
        {
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
        }

        public void CloseConnection()
        {
            Connector.Close();
        }
    }    
}