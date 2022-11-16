﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phidget22;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Configuration;

namespace _270_GeoLocBox
{
    public class SqlLiteDataLayer
    {
        private static string connectionString;
        public static SqliteConnection Conn;

        static public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                ResetConnection();
            }
        }

        static SqlLiteDataLayer()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            Conn = new SqliteConnection(ConnectionString);
            SetUpDB();
        }

        private static void SetUpDB()
        {
            //These need to be altered to match the geolocDB
            ExecuteNonQuery(new SqliteCommand(@"USING GeoBox
                                                CREATE TABLE 'SensorDetails' (
                                                    'Time' TEXT NOT NULL,
                                                    'Location' TEXT NOT NULL,
                                                    'Temp' TEXT,
                                                    'Humidity' TEXT,
                                                    'Light' TEXT,
                                                    PRIMARY KEY('Time')
                                                    )", Conn));
        }

        private static void ResetConnection()
        {
            Conn = new SqliteConnection(ConnectionString);
        }

        public static bool ExecuteNonQuery(SqliteCommand cmd)
        {
            try
            {
                Conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Conn.Close();
            }
        }

        public static object? ExecuteScaler(SqliteCommand cmd)
        {
            try
            {
                Conn.Open();
                return cmd.ExecuteScalar();
            }
            catch
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }
        }

        public void UpdateRecord(SqliteCommand cmd, int id, string test_data, DateTime record_date)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new($"UPDATE SensorDetails SET test_data = '{test_data}', record_date = '{record_date}' WHERE id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecord(SqliteCommand cmd, int id)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new($"DELETE FROM SensorDetails WHERE id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetRecords(SqliteCommand cmd)
        {
            DataTable table = new();
            table.Columns.Add("id");
            table.Columns.Add("test_data"); ;
            table.Columns.Add("record_date");

            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new("SELECT * FROM SensorDetails", conn);
                SqliteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    table.Rows.Add(dr[0], dr[1], dr[2]);
                }
            }

            return table;
        }

        public void InsertRecord(SqliteCommand cmd, string test_data, DateTime record_date)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                cmd = new($"INSERT INTO SensorDetails (test_data, record_date) VALUES ('{test_data}', '{record_date.ToString("yyyy-MM-dd")}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
