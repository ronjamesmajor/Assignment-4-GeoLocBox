using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phidget22;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;

namespace _270_GeoLocBox
{
    public class SQLDataLayer
    {
        SqliteConnection conn;
        SqliteCommand cmd;
        string connectionString = "";

        static SQLDataLayer()
        {

        }

        public SQLDataLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UpdateRecord(int id, string test_data, DateTime record_date)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new($"UPDATE Table1 SET test_data = '{test_data}', record_date = '{record_date}' WHERE id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecord(int id)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new($"DELETE FROM Table1 WHERE id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetRecords()
        {
            DataTable table = new();
            table.Columns.Add("id");
            table.Columns.Add("test_data"); ;
            table.Columns.Add("record_date");

            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new("SELECT * FROM Table1", conn);
                SqliteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    table.Rows.Add(dr[0], dr[1], dr[2]);
                }
            }

            return table;
        }

        public void InsertRecord(string test_data, DateTime record_date)
        {
            //Come back here and replace with our connection string
            using (SqliteConnection conn = new(connectionString))
            {
                cmd = new($"INSERT INTO Table1 (test_data, record_date) VALUES ('{test_data}', '{record_date.ToString("yyyy-MM-dd")}')", conn);
                conn.Open();
                //Execute for no results, create, update, delete
                cmd.ExecuteNonQuery();
            }
        }
    }
}
