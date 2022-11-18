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

        public SqlLiteDataLayer(string connectionString)
        {
            ConnectionString = connectionString;
            SetUpDB();
        }

        private static void SetUpDB()
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                ExecuteNonQuery(@"CREATE TABLE 'GeoLocation' (
                                                    'Time' TEXT NOT NULL,
                                                    'Latitude' TEXT NOT NULL,
                                                    'Longitude' TEXT NOT NULL,
                                                    'Altitude' TEXT NOT NULL,
                                                    PRIMARY KEY('Time')
                                                    )");

                ExecuteNonQuery(@"CREATE TABLE 'SensorData' (
                                                    'Time' TEXT NOT NULL,
                                                    'Temp' TEXT,
                                                    'Humidity' TEXT,
                                                    'Light' TEXT,
                                                    PRIMARY KEY('Time')
                                                    )");
            }
        }

        public void InsertSensorData(DateTime record_date, string temp, string humidity, string light)
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                SqliteCommand cmd = new(@$"INSERT INTO SensorData VALUES (@Date,@Temp,@Hum,@Light)", conn);
                cmd.Parameters.AddWithValue("@Date", record_date.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Temp", temp);
                cmd.Parameters.AddWithValue("@Hum", humidity);
                cmd.Parameters.AddWithValue("@Light", light);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertGeoData(DateTime record_date, string latitude, string longitude, string altitude)
        {
            using (SqliteConnection conn = new(ConnectionString))
            {
                SqliteCommand cmd = new();
                cmd = new(@$"INSERT INTO GeoLocation VALUES (@Date,@Lat, @Long, @Alt)", conn);
                cmd.Parameters.AddWithValue("@Date", record_date.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Lat", latitude);
                cmd.Parameters.AddWithValue("@Long", longitude);
                cmd.Parameters.AddWithValue("@Alt", altitude);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                using (SqliteConnection conn = new(ConnectionString))
                {
                    conn.Open();
                    SqliteCommand cmd = new(query, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
