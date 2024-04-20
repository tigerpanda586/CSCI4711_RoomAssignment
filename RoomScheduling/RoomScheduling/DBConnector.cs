using System;
using System.Collections.Generic;
using RoomScheduling.Entity;
//using RoomScheduling.Boundary;
using System.Data.SQLite;
using System.Security.Principal;

namespace RoomScheduling.Controllers
{
    public static class DBConnector
    {
        public static void InitializeDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    string strSql = @"BEGIN TRANSACTION; 
                    DROP TABLE IF EXISTS LOGS;
                    DROP TABLE IF EXISTS ACCOUNT;
                    DROP TABLE IF EXISTS REQUEST;
                    DROP TABLE IF EXISTS ROOM;
                    DROP TABLE IF EXISTS SUBJECT;
                    COMMIT;";
                    cmnd.CommandText = strSql;
                    cmnd.ExecuteNonQuery();
                    string table = @"CREATE TABLE [ACCOUNT] (
                                  [usn] TEXT PRIMARY KEY
                                , [pass] TEXT NOT NULL
                                , [role] TEXT NOT NULL
                                );";

                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [REQUEST] (
                      [reqNo] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                    , [status] TEXT NOT NULL
                    , [date] DATETIME NOT NULL
                    , [usn] TEXT NOT NULL
                    , [roomNo] INTEGER
                    , [subject] TEXT
                    , FOREIGN KEY([usn]) REFERENCES ACCOUNT([usn])
                    , FOREIGN KEY([roomNo]) REFERENCES ROOM([roomNo])
                    , FOREIGN KEY([subject]) REFERENCES SUBJECT([subject])
                    );";

                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGS] (
                      [logNo] TEXT PRIMARY KEY
                    , [type] TEXT NOT NULL
                    , [time] DATETIME NOT NULL
                    , [usn] TEXT NOT NULL
                    , FOREIGN KEY([usn]) REFERENCES [ACCOUNT]([usn])
                    );";

                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [ROOM] (
                      [roomNo] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                    , [date] DATETIME
                    , [maxcap] INTEGER NOT NULL
                    , [subject] TEXT
                    , FOREIGN KEY([subject]) REFERENCES SUBJECT([subject])
                    );";

                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [SUBJECT] (
                      [subject] TEXT PRIMARY KEY
                    );";

                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    strSql = @"BEGIN TRANSACTION; 
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr1, $hashpwd1, 'admin');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr2, $hashpwd2, 'student');
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (1, 20);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (2, 20);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (3, 20);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (4, 15);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (5, 15);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (6, 15);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (7, 10);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (8, 10);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (9, 5);
                    INSERT INTO ROOM (roomNo, maxcap) VALUES (10, 5);
                    COMMIT;";
                    cmnd.CommandText = strSql;
                    string usrname1 = "cus";
                    string pwd1 = "1qaz";
                    string usrname2 = "emp";
                    string pwd2 = "2wsx";
                    int x = usrname1.GetHashCode();
                    int y = pwd1.GetHashCode();
                    int x1 = usrname2.GetHashCode();
                    int y1 = pwd2.GetHashCode();
                    cmnd.Parameters.AddWithValue("$hashusr1", x);
                    cmnd.Parameters.AddWithValue("$hashpwd1", y);
                    cmnd.Parameters.AddWithValue("$hashusr2", x1);
                    cmnd.Parameters.AddWithValue("$hashpwd2", y1);
                    cmnd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        //public static Account GetUser(string usr, string pwd)
        // {
        //  using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
        //{
        //    conn.Open();
        //  int x = usr.GetHashCode();
        //int y = pwd.GetHashCode();
        //string stm = @"SELECT[Id]
        //      ,[usn]
        //    ,[pass]
        //  ,[role]
        //FROM[ACCOUNT]
        //WHERE[username] == ($name)
        //AND[password] == ($pd);";
        //using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
        //{
        //  cmnd.Parameters.AddWithValue("$name", x);
        //cmnd.Parameters.AddWithValue("$pd", y);
        //using (SQLiteDataReader rdr = cmnd.ExecuteReader())
        //{
        //  while (rdr.Read())
        //{
        //  Account acct = new Account(usr, pass, 0);
        //return acct;
        //}
        //Account act = new Account(null, null, null);
        //return act;
        //}
        //}
        //}
        //}

        public static List<Request> getRequests()
        {
            List<Request> requestInfoList = new List<Request>();
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    cmnd.CommandText = "SELECT * FROM REQUEST;";
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            requestInfoList.Add(new Request(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetBoolean(3), rdr.GetDateTime(4), rdr.GetString(5)));
                        }
                    }
                }
            }
            return requestInfoList;
        }

        public static List<Room> getRooms()
        {
            List<Room> roomInfoList = new List<Room>();
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    cmnd.CommandText = "SELECT * FROM ROOM;";
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            roomInfoList.Add(new Room(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetString(4)));
                        }
                    }
                }
            }
            return roomInfoList;
        }

        public static void SaveLogin(string usr)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                conn.Open();
                DateTime time = DateTime.Now;
                string t = time.ToString("s");
                int id = 0;
                int hash = usr.GetHashCode();
                string stm = "SELECT [usn] FROM ACCOUNT WHERE usn = ($name);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", hash);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            id = rdr.GetInt32(0);
                        }
                    }
                }
                stm = @"INSERT INTO LOGIN VALUES($id, $time);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$id", id);
                    cmnd.Parameters.AddWithValue("$time", t);
                    cmnd.ExecuteNonQuery();
                }
            }
        }
    }
}
