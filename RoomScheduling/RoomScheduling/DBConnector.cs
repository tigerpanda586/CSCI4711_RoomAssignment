using System;
using System.Collections.Generic;
using RoomScheduling.Entity;
//using RoomScheduling.Boundary;
using System.Data.SQLite;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

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
                      [logNo] INTEGER PRIMARY KEY AUTOINCREMENT
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

                    //Starting here, a bunch of values are being added into the database manually
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
                    INSERT INTO SUBJECT(subject) VALUES ('Chemistry');
                    INSERT INTO SUBJECT(subject) VALUES ('Physics');
                    INSERT INTO SUBJECT(subject) VALUES ('Math');
                    INSERT INTO SUBJECT(subject) VALUES ('Spanish');
                    INSERT INTO SUBJECT(subject) VALUES ('English');
                    INSERT INTO SUBJECT(subject) VALUES ('Programming');
                    INSERT INTO SUBJECT(subject) VALUES ('Biology');
                    INSERT INTO SUBJECT(subject) VALUES ('Accounting');
                    INSERT INTO SUBJECT(subject) VALUES ('Finance');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr3, $hashpwd3, 'student');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr4, $hashpwd4, 'student');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr5, $hashpwd5, 'student');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr6, $hashpwd6, 'admin');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr7, $hashpwd7, 'admin');
                    INSERT INTO ACCOUNT (usn, pass, role) VALUES ($hashusr8, $hashpwd8, 'admin');
                    COMMIT;";
                    cmnd.CommandText = strSql;
                    string usrname1 = "admin@university.edu";
                    string pwd1 = "Password1!";
                    string usrname2 = "student@university.edu";
                    string pwd2 = "Password2!";
                    string usr3 = "bryangarris@university.edu";
                    string usr4 = "emmaboster@university.edu";
                    string usr5 = "mikehranica@university.edu";
                    string usr6 = "milkvosbein@university.edu";
                    string usr7 = "loverukk@university.edu";
                    string usr8 = "brightwin@university.edu";
                    string pwd3 = "H@rdcore99";
                    string pwd4 = "M@ddymor421";
                    string pwd5 = "Bigchair2!";
                    string pwd6 = "Blue5bl@nkets";
                    string pwd7 = "Tophats89!";
                    string pwd8 = "50$ecrets";
                    int x = usrname1.GetHashCode();
                    int y = pwd1.GetHashCode();
                    int x1 = usrname2.GetHashCode();
                    int y1 = pwd2.GetHashCode();
                    int a = usr3.GetHashCode();
                    int b = usr4.GetHashCode();
                    int c = usr5.GetHashCode();
                    int d = usr6.GetHashCode();
                    int e = usr7.GetHashCode();
                    int f = usr8.GetHashCode();
                    int g = pwd3.GetHashCode();
                    int h = pwd4.GetHashCode();
                    int i = pwd5.GetHashCode();
                    int j = pwd6.GetHashCode();
                    int k = pwd7.GetHashCode();
                    int l = pwd8.GetHashCode();
                    cmnd.Parameters.AddWithValue("$hashusr1", x);
                    cmnd.Parameters.AddWithValue("$hashpwd1", y);
                    cmnd.Parameters.AddWithValue("$hashusr2", x1);
                    cmnd.Parameters.AddWithValue("$hashpwd2", y1);
                    cmnd.Parameters.AddWithValue("$hashusr3", a);
                    cmnd.Parameters.AddWithValue("$hashpwd3", g);
                    cmnd.Parameters.AddWithValue("$hashusr4", b);
                    cmnd.Parameters.AddWithValue("$hashpwd4", h);
                    cmnd.Parameters.AddWithValue("$hashusr5", c);
                    cmnd.Parameters.AddWithValue("$hashpwd5", i);
                    cmnd.Parameters.AddWithValue("$hashpwd6", d);
                    cmnd.Parameters.AddWithValue("$hashusr6", j);
                    cmnd.Parameters.AddWithValue("$hashpwd7", e);
                    cmnd.Parameters.AddWithValue("$hashusr7", k);
                    cmnd.Parameters.AddWithValue("$hashpwd8", f);
                    cmnd.Parameters.AddWithValue("$hashusr8", l);
                    cmnd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public static Account GetUser(string usr)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
        {
            conn.Open();
            int x = usr.GetHashCode();
            string stm = @"SELECT[usn]
            ,[pass]
            ,[role]
            FROM[ACCOUNT]
            WHERE[usn] == ($name);";
            using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
            {
              cmnd.Parameters.AddWithValue("$name", x);
              using (SQLiteDataReader rdr = cmnd.ExecuteReader())
              {
                while (rdr.Read())
                {
                            Account acct = new Account(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
                            return acct;
                }
              Account act = new Account(null, null, null);
              return act;
            }
        }
    }
}

        public static List<Request> getRequests(string usr)
        {
            List<Request> requestInfoList = new List<Request>();
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    int x = usr.GetHashCode();
                    cmnd.CommandText = "SELECT * FROM REQUEST WHERE[usn] == ($name);";
                    using (SQLiteCommand cmnd2 = new SQLiteCommand(conn))
                    {
                        cmnd.Parameters.AddWithValue("$name", x);
                        using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                requestInfoList.Add(new Request(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetBoolean(3), rdr.GetDateTime(4), rdr.GetString(5)));
                            }
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

        private static int logcnt;
        public static void SaveLogs(string usr, string type)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source=..\..\Files\RoomSchedulingSystem.db"))
            {
                conn.Open();
                DateTime time = DateTime.Now;
                string t = time.ToString("s");
                string id = "0";
                int hash = usr.GetHashCode();
                string stm = "SELECT [usn] FROM ACCOUNT WHERE usn = ($name);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", hash);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            id = rdr.GetString(0);
                        }
                    }
                }
                stm = @"INSERT INTO LOGS VALUES($logNo, $type, $time, $usn);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    //[logNo] INTEGER PRIMARY KEY
                    //, [type] TEXT NOT NULL
                    //, [time] DATETIME NOT NULL
                    //, [usn] TEXT NOT NULL
                    //, FOREIGN KEY([usn]) REFERENCES[ACCOUNT]([usn])
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$usn", id);
                    cmnd.Parameters.AddWithValue("$time", t);
                    cmnd.Parameters.AddWithValue("$type", type);
                    logcnt++;
                    cmnd.Parameters.AddWithValue("$logNo", logcnt);
                    cmnd.ExecuteNonQuery();
                }
            }
        }
    }
}
