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
            ExecuteNonQuery(new SqliteCommand("create table Users(Username varchar(30) primary key, [Password] nvarchar(50) not null, Register_Date datetime not null, Profile_Picture varbinary(MAX) null, Online bit null, Status nvarchar(30) null, Biography nvarchar(MAX) null)", Conn));
            ExecuteNonQuery(new SqliteCommand("create table Chat(username varchar(30), message_date datetime primary key(username,message_date), [message] varchar(150), foreign key (username) references users(username))", Conn));
            ExecuteNonQuery(new SqliteCommand(@"create table DirectMessages (
	                        Sender varchar(30) foreign key references Users(Username),
	                        Receiver varchar(30) foreign key references Users(Username),
	                        [TimeStamp] datetime,
	                        Body varchar(255),
                            [Read] bit,
	                        primary key (Sender, Receiver, [TimeStamp])
                            );", Conn));
            ExecuteNonQuery(new SqliteCommand(@"create table Groups
                            (
	                            GroupId int identity(1,1) primary key not null,
	                            GroupName varchar(25) not null,
	                            GroupCreator varchar(25) not null
                            );", Conn));
            ExecuteNonQuery(new SqliteCommand(@"create table GroupUsers
                            (
                                GroupID int foreign key references Groups(GroupID) not null,
	                            GroupUser varchar(30) foreign key references Users(username) not null,
	                            primary key (GroupID, GroupUser)
                            );", Conn));
            ExecuteNonQuery(new SqliteCommand(@"create table GroupChat(
	                            GroupID int foreign key references Groups(GroupID) not null,
	                            Sender varchar(30) foreign key references Users(username) not null,
	                            [TimeStamp] datetime not null,
	                            Body varchar(255),
	                            primary key (GroupId, [TimeStamp])
                            );", Conn));
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
                cmd = new($"UPDATE Table1 SET test_data = '{test_data}', record_date = '{record_date}' WHERE id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecord(SqliteCommand cmd, int id)
        {
            using (SqliteConnection conn = new(connectionString))
            {
                conn.Open();
                cmd = new($"DELETE FROM Table1 WHERE id = '{id}'", conn);
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
                cmd = new("SELECT * FROM Table1", conn);
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
                cmd = new($"INSERT INTO Table1 (test_data, record_date) VALUES ('{test_data}', '{record_date.ToString("yyyy-MM-dd")}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
