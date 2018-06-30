using System;
using System.Data.SqlClient;

namespace RemoveVillain
{
    public class StartUp
    {
        public static void Main()
        {
            var connectionString = Configuration.ConnectionString;

            var dbCon = new SqlConnection(connectionString);

            dbCon.Open();

            var id = int.Parse(Console.ReadLine());

            using (dbCon)
            {
                var villainNameCommand =
                    new SqlCommand($"SELECT Name FROM Villains WHERE Id = {id};", dbCon);

                if (villainNameCommand.ExecuteScalar() == null)
                {
                    Console.WriteLine("No such villain was found.");

                    return;
                }

                var minionsCountToRealeseCommand = new SqlCommand("SELECT COUNT(MinionId) " +
                                                           "FROM MinionsVillains " +
                                                           $"WHERE VillainId = {id};", dbCon);

                minionsCountToRealeseCommand.ExecuteScalar();

                var minionsReleased = (int) minionsCountToRealeseCommand.ExecuteScalar();
                
                var retrievedVillainName = villainNameCommand.ExecuteScalar().ToString();

                var deleteVillainAndMinionsCommand = new SqlCommand($"DELETE FROM MinionsVillains WHERE VillainId = {id};", dbCon);

                deleteVillainAndMinionsCommand.ExecuteNonQuery();
                
                var deleteVillainFromDb = new SqlCommand($"DELETE FROM Villains WHERE Id = {id};", dbCon);

                deleteVillainFromDb.ExecuteNonQuery();

                Console.WriteLine($"{retrievedVillainName} was deleted.");

                Console.WriteLine($"{minionsReleased} minions were released.");
                               
            }
        }
    }
}