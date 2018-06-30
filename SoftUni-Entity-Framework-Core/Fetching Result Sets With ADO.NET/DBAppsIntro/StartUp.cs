using System.Data.SqlClient;

namespace DBAppsIntro
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                var createCommand = "CREATE DATABASE MinionsDB";

                var createTableCountriesCommand =
                    "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";

                var createTableTownsCommand =
                    "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY " +
                    "REFERENCES Countries(Id))";

                var createTableMinionsCommand =
                    "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY " +
                    "REFERENCES Towns(Id))";

                var createTableEvilnessFactorsCommand =
                    "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))";

                var createTableVilliansCommand =
                    "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN " +
                    "KEY REFERENCES EvilnessFactors(Id))";

                var createTableMinionsVillians = "CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES " +
                                                 "Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id)," +
                                                 "CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";

                ExecuteNonQuery(createCommand, connection);

                ExecuteNonQuery(createTableCountriesCommand, connection);

                ExecuteNonQuery(createTableTownsCommand, connection);

                ExecuteNonQuery(createTableMinionsCommand, connection);

                ExecuteNonQuery(createTableEvilnessFactorsCommand, connection);

                ExecuteNonQuery(createTableVilliansCommand, connection);

                ExecuteNonQuery(createTableMinionsVillians, connection);

                var insertIntoCountriesCommand = "INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England')," +
                                                 "('Cyprus'),('Germany'),('Norway')";

                var insertIntoTownsCommand =
                    "INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1)," +
                    "('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2)," +
                    "('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";

                var insertIntoMinionsCommand = "INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3)," +
                                               "('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2)," +
                                               "('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10)," +
                                               "('Zoe', 125, 5),('Json', 21, 1)";

                var insertIntoEvilnessFactorsCommand =
                    "INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good')," +
                    "('Bad'), ('Evil'),('Super evil')";

                var insertIntoVilliansCommand = "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2)," +
                                                "('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1)," +
                                                "('Dobromir',2)";

                var insertIntoMinionsVillansCommand =
                    "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2)," +
                    "(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3)," +
                    "(5,3),(4,3),(1,2),(2,1),(2,7)";
                
                
                ExecuteNonQuery(insertIntoCountriesCommand, connection);

                ExecuteNonQuery(insertIntoTownsCommand, connection);

                ExecuteNonQuery(insertIntoMinionsCommand, connection);

                ExecuteNonQuery(insertIntoEvilnessFactorsCommand, connection);

                ExecuteNonQuery(insertIntoVilliansCommand, connection);

                ExecuteNonQuery(insertIntoMinionsVillansCommand, connection);

                connection.Close();
                
            }
        }

        private static void ExecuteNonQuery(string createCommand, SqlConnection connection)
        {
            var command = new SqlCommand(createCommand, connection);

            command.ExecuteNonQuery();
        }
    }
}