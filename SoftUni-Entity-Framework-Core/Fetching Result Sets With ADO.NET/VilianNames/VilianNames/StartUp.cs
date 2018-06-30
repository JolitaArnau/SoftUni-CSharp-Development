using System;
using System.Data.SqlClient;

namespace VilianNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();


                var villiansMinionsInfo = "SELECT v.Name, COUNT(mv.MinionId) AS Count " +
                                          "FROM Villains AS v " +
                                          "JOIN MinionsVillains AS mv " +
                                          "ON v.Id = mv.VillainId " +
                                          "GROUP BY v.Name HAVING COUNT(mv.MinionId) >= 3" +
                                          "ORDER BY COUNT(mv.MinionId) DESC;";

                var command = new SqlCommand(villiansMinionsInfo, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }

                connection.Close();
            }
        }
    }
}