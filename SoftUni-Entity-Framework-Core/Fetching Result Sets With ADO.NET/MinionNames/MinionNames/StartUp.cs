using System;
using System.Data.SqlClient;

namespace MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection(Configuration.ConnectionString);

            var villianId = int.Parse(Console.ReadLine());

            using (connection)
            {
                connection.Open();

                var villianName = GetVillianName(villianId, connection);

                if (villianName == null)
                {
                    Console.WriteLine($"No villain with ID {villianId} exists in the database.");
                }
                
                else
                {
                    Console.WriteLine($"Villian: {villianName}");

                    var minionsInfo = "SELECT m.Name, m.Age " +
                                      "FROM Villains AS v " +
                                      "JOIN MinionsVillains AS mv " +
                                      "ON v.Id = mv.VillainId " +
                                      "JOIN Minions AS m " +
                                      "ON mv.MinionId = m.Id " +
                                      "WHERE v.Id = @id " +
                                      "GROUP BY m.Name, m.Age " +
                                      "ORDER BY m.Name";
                    
                    var command = new SqlCommand(minionsInfo, connection);

                    command.Parameters.AddWithValue("@id", villianId);

                    SqlDataReader reader = command.ExecuteReader();

                    var counter = 1;

                    while (reader.Read())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("(no minions)");
                        }
                        
                        Console.WriteLine($"{counter}. {reader[0]} {reader[1]}");

                        counter++;
                    }
                }
                
                connection.Close();
            }
        }

        private static string GetVillianName(int villianId, SqlConnection connection)
        {
            var villianNameCommand = "SELECT Name FROM Villains WHERE Id = @id";
            
            var command = new SqlCommand(villianNameCommand, connection);

            command.Parameters.AddWithValue("@id", villianId);
            
            return (string)command.ExecuteScalar();
        }
    }
}