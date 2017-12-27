using System;
using System.Data.SQLite;
using System.IO;

namespace testsqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (!File.Exists(@".\test.sqlite"))
            {
                Console.WriteLine("creating a new db named test.sqlite");
                SQLiteConnection.CreateFile("test.sqlite");
                using (var db = new SQLiteConnection(@"Data Source=.\test.sqlite"))
                {
                    db.Open();
                    var sql = "create table scores (name varchar(20), score int)";
                    var cmd = new SQLiteCommand(sql, db);
                    cmd.ExecuteNonQuery();
                    sql = "insert into scores values('张三',100);insert into scores values('李四',100)";
                    cmd = new SQLiteCommand(sql, db);
                    cmd.ExecuteNonQuery();
                }
            }
            using (var db = new SQLiteConnection(@"Data Source=.\test.sqlite"))
            {
                db.Open();
                var sql = "select * from scores";
                var cmd = new SQLiteCommand(sql, db);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                }
            }
        }
    }
}
