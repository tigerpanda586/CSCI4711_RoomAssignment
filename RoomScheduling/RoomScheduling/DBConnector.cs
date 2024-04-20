using System;
using System.Collections.Generic;
using CarRentalSystem.Entity;
using CarRentalSystem.Boundary;
using System.Data.SQLite;

namespace CarRentalSystem.Controllers
{
    public static class DBConnector
    {
        public static void InitializeDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source = nCarDb.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    string strSql = @"BEGIN TRANSACTION; 
                    DROP TABLE IF EXISTS ACCOUNT;
                    DROP TABLE IF EXISTS LOGIN;
                    DROP TABLE IF EXISTS LOGOUT;
                    DROP TABLE IF EXISTS VEHICLE;
                    DROP TABLE IF EXISTS RESERVATION;
                    COMMIT;";
                    cmnd.CommandText = strSql;
                    cmnd.ExecuteNonQuery();
                    string table = @"CREATE TABLE [ACCOUNT] (
                                  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [username] INTEGER NOT NULL
                                , [password] INTEGER NOT NULL
                                , [type] TEXT NOT NULL
                                );";
                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGIN] (
                    [acctID] INTEGER NOT NULL
                    , [timestamp] TEXT NOT NULL
                    , CONSTRAINT [PK_LOGIN] PRIMARY KEY ([acctID],[timestamp])
                    , FOREIGN KEY([acctID]) REFERENCES [ACCOUNT]([id])
                    );";
                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGOUT] (
                    [acctID] INTEGER NOT NULL
                    , [timestamp] TEXT NOT NULL
                    , CONSTRAINT [PK_LOGOUT] PRIMARY KEY ([acctID],[timestamp])
                    , FOREIGN KEY([acctID]) REFERENCES [ACCOUNT]([id])
                    );";
                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [VEHICLE] (
                    [vid] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                    , [make] TEXT NOT NULL
                    , [model] TEXT NOT NULL
                    , [year] TEXT NOT NULL
                    );";
                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    table = @"CREATE TABLE [RESERVATION] (
                    [acctID] INTEGER NOT NULL
                    , [vid] INTEGER NOT NULL
                    , [startDate] INTEGER NOT NULL
                    , [endDate] INTEGER NOT NULL
                    , CONSTRAINT [PK_RESERVATION] PRIMARY KEY ([acctID],[vid],[startDate],[endDate])
                    , FOREIGN KEY([acctID]) REFERENCES [ACCOUNT]([id])
                    , FOREIGN KEY([vid]) REFERENCES [VEHICLE]([vid])
                    );";
                    cmnd.CommandText = table;
                    cmnd.ExecuteNonQuery();
                    strSql = @"BEGIN TRANSACTION; 
                    INSERT INTO ACCOUNT (username, password, type) VALUES ($hashusr1, $hashpwd1, 'customer');
                    INSERT INTO ACCOUNT (username, password, type) VALUES ($hashusr2, $hashpwd2, 'employee');
                    INSERT INTO VEHICLE (make, model, year) VALUES ('Honda', 'Civic', '2021');
                    INSERT INTO VEHICLE (make, model, year) VALUES ('Subaru', 'Outback', '2021');
                    INSERT INTO RESERVATION (acctID, vid, startDate, endDate) VALUES (1, 1, 20211201, 20211203);
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
		
        public static Account GetUser(string usr, string pwd)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source = nCarDb.db"))
            {
                conn.Open();
                int x = usr.GetHashCode();
                int y = pwd.GetHashCode();
                string stm = @"SELECT[Id]
                        ,[username]
                        ,[password]
                        ,[type]
                        FROM[ACCOUNT]
                        WHERE[username] == ($name)
                        AND[password] == ($pd);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", x);
                    cmnd.Parameters.AddWithValue("$pd", y);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Account acct = new Account(rdr.GetInt32(0), usr, rdr.GetString(3));
                            return acct;
                        }
                        Account act = new Account(0, null, null);
                        return act;
                    }
                }
            }
        }
		
        public static List<Vehicle> getVehicles()
        {
            List<Vehicle> vehicleInfoList = new List<Vehicle>();
            using (SQLiteConnection conn = new SQLiteConnection(@"data source = nCarDb.db"))
            {
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    conn.Open();
                    cmnd.Connection = conn;
                    cmnd.CommandText = "SELECT * FROM VEHICLE;";
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            vehicleInfoList.Add(new Vehicle(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3)));
                        }
                    }
                }
            }
            return vehicleInfoList;
        }
		
        public static void SaveLogin(string usr)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"data source = nCarDb.db"))
            {
                conn.Open();
                DateTime time = DateTime.Now;
                string t = time.ToString("s");
                int id = 0;
                int hash = usr.GetHashCode();
                string stm = "SELECT [id] FROM ACCOUNT WHERE username = ($name);";
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
