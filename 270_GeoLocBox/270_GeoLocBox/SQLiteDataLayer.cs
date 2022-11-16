using System;
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
        private string connectionString = "";
        public static SqliteConnection Conn;       

        public SqlLiteDataLayer(string connectionString)
        {
            this.connectionString = connectionString;
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

        public void InsertRecord(SqliteCommand cmd, DateTime record_date, string location, string temp, string humidity, string light)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                cmd = new($"INSERT INTO SensorDetails VALUES ('{record_date.ToString("yyyy-MM-dd")}', '{location}', '{temp}', '{humidity}', '{light}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
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
    }
}
