using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChangeTownNamesCasing
{
    public class StartUp
    {
        public static void Main()
        {
            var connectionString = Configuration.ConnectionString;

            SqlConnection dbCon = new SqlConnection(connectionString);

            dbCon.Open();

            var desiredCountryName = Console.ReadLine();

            var townsAffectedCount = 0;

            var townAffectedNames = new List<string>();

            using (dbCon)
            {
                var findDesiredCountryIdCommand =
                    new SqlCommand($"SELECT Id FROM Countries WHERE Name = '{desiredCountryName}';", dbCon);

                if (findDesiredCountryIdCommand.ExecuteScalar() == null)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }


                var desiredCountryId = (int) findDesiredCountryIdCommand.ExecuteScalar();

                var reader =
                    new SqlCommand($"SELECT Name FROM Towns WHERE CountryCode = {desiredCountryId}", dbCon)
                        .ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No town names were affected.");
                        dbCon.Close();
                        reader.Close();
                        return;
                    }

                    while (reader.Read())
                    {
                        var currentTownName = (string) reader["Name"];

                        townAffectedNames.Add(currentTownName.ToUpper());

                        townsAffectedCount++;
                    }
                }
            }

            Console.WriteLine($"{townsAffectedCount} town names were affected.");

            Console.WriteLine($"[{string.Join(", ", townAffectedNames)}]");
        }
    }
}