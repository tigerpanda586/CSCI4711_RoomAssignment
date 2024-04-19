using System.IO;
using System.Data.SQLite;

public static class DatabaseHelper 
{ 
    private static string connectionString = @"Data Source=..\..\Files\LibraryManagementSystem.db;Version=3";
    public static void InitializeDatabase()
    {
        if (!File.Exists(@"..\..\Files\LibraryManagementSystem.db"))
        {
            SQLiteConnection.CreateFile(@"..\..\Files\LibraryManagementSystem.db");

            using (var connection = new SQLiteConnection(connectionString))
            {
                //Write queries for the tables
                string createLOGSTableQuery = @"
                    CREATE TABLE IF NOT EXISTS LOGS(
                        logNo INTEGER PRIMARY KEY AUTOINCREMENT
                        type TEXT NOT NULL
                        time DATETIME NOT NULL
                        FOREIGN KEY (usn) REFERENCES ACCOUNT(usn)
                    );";

                string createACCOUNTTableQuery = @"
                    CREATE TABLE IF NOT EXISTS ACCOUNT(
                        usn TEXT PRIMARY KEY
                        pass TEXT NOT NULL
                        role TEXT NOT NULL
                    );";

                string createREQUESTTableQuery = @"
                    CREATE TABLE IF NOT EXISTS REQUEST(
                        reqNo INTEGER PRIMARY KEY AUTOINCREMENT
                        status TEXT NOT NULL
                        date DATETIME NOT NULL
                        FOREIGN KEY (usn) REFERENCES ACCOUNT(usn)
                        FOREIGN KEY (roomNo) REFERENCES ROOM(roomNo)
                        FOREIGN KEY (subject) REFERENCES SUBJECT(subject)
                    );";

                string createROOMTableQuery = @"
                    CREATE TABLE IF NOT EXISTS ROOM(
                        roomNo INTEGER PRIMARY KEY AUTOINCREMENT
                        date DATETIME NOT NULL
                        maxcap INTEGER NOT NULL
                        FOREIGN KEY (subject) REFERENCES SUBJECT(subject)
                    );";

                string createSUBJECTTableQuery = @"
                    CREATE TABLE IF NOT EXISTS SUBJECT(
                        subject TEXT PRIMARY KEY
                    );";

                //This makes the actual tables in Database
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createLOGSTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createACCOUNTTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createREQUESTTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createROOMTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createSUBJECTTableQuery;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

