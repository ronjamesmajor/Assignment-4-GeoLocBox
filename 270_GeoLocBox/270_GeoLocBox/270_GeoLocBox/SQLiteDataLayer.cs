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
        private static string ConnectionString = "";
        public static SqliteConnection Conn;

        public SqlLiteDataLayer(string connectionString)
        {
            ConnectionString = connectionString;
            SetUpDB();
        }

        private static void SetUpDB()
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                ExecuteNonQuery(new SqliteCommand(@"USING GeoBox
                                                CREATE TABLE 'GeoLocation' (
                                                    'Time' TEXT NOT NULL,
                                                    'Latitude' TEXT NOT NULL,
                                                    'Longitude' TEXT NOT NULL,
                                                    'Altitude' TEXT NOT NULL,
                                                    PRIMARY KEY('Time')
                                                    )", Conn));

                ExecuteNonQuery(new SqliteCommand(@"USING GeoBox
                                                CREATE TABLE 'SensorData' (
                                                    'Time' TEXT NOT NULL,
                                                    'Temp' TEXT,
                                                    'Humidity' TEXT,
                                                    'Light' TEXT,
                                                    PRIMARY KEY('Time')
                                                    )", Conn));
            }
        }

        public void InsertSensorData(DateTime record_date, string temp, string humidity, string light)
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                SqliteCommand cmd = new();

                cmd = new(@$"INSERT INTO SensorData 
                            VALUES ('{record_date.ToString("yyyy-MM-dd")}','{temp}', '{humidity}', '{light}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertGeoData(DateTime record_date, string latitude, string longitude, string altitude)
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                SqliteCommand cmd = new();

                cmd = new(@$"INSERT INTO GeoLocation 
                            VALUES ('{record_date.ToString("yyyy-MM-dd")}','{latitude}', '{longitude}', '{altitude}')", conn);
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
