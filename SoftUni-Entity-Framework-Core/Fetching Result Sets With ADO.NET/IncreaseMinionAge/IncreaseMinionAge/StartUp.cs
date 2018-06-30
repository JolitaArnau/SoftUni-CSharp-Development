using System;
using System.Data.SqlClient;
using System.Linq;

namespace IncreaseMinionAge
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = Configuration.ConnectionString;

            var dbCon = new SqlConnection(connectionString);

            dbCon.Open();

            var ids = Console.ReadLine().Split().Select(int.Parse).ToArray();

            using (dbCon)
            {
                foreach (var currentId in ids)
                {
                    var updateMinionAgeCommand = new SqlCommand("UPDATE Minions " +
                                                                "SET Age += 1 " +
                                                                $"WHERE Id = {currentId};", dbCon);

                    updateMinionAgeCommand.ExecuteNonQuery();

                    var minionInfo = new SqlCommand($"SELECT Name, Age FROM Minions WHERE Id = {currentId}", dbCon);
                    
                    SqlDataReader reader = minionInfo.ExecuteReader();

                    using (reader)
                    {
                        if (!reader.HasRows)
                        {
                            reader.Close();
                            dbCon.Close();
                            return;
                        }
                        
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Name"] + " " + reader["Age"]);
                        }
                    }
                }
            }
        }
    }
}