using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PrintAllMinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var dbCon = new SqlConnection(Configuration.ConnectionString);
            dbCon.Open();

            var minionsInitial = new List<string>();
            var minionsArranged = new List<string>();

            using (dbCon)
            {
                SqlCommand command = new SqlCommand("SELECT Name FROM Minions", dbCon);

                SqlDataReader reader = command.ExecuteReader();

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
                        minionsInitial.Add((string) reader["Name"]);
                    }
                }
            }

            while (minionsInitial.Count > 0)
            {
                minionsArranged.Add(minionsInitial[0]);
                minionsInitial.RemoveAt(0);

                if (minionsInitial.Count > 0)
                {
                    minionsArranged.Add(minionsInitial[minionsInitial.Count - 1]);
                    minionsInitial.RemoveAt(minionsInitial.Count - 1);
                }
            }

            minionsArranged.ForEach(Console.WriteLine);
        }
    }
}