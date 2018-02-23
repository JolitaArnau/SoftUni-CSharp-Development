using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    public static void Main()
    {
        var line = Console.ReadLine();

        var teams = new List<Team>();

        while (!line.Equals("END"))
        {
            var tokens = line.Split(';');

            var command = tokens[0];

            switch (command)
            {
                case "Team":
                    teams.Add(new Team(tokens[1]));
                    break;
                case "Add":
                    var teamName = tokens[1];
                    var playerName = tokens[2];
                    var endurance = int.Parse(tokens[3]);
                    var sprint = int.Parse(tokens[4]);
                    var dribble = int.Parse(tokens[5]);
                    var passing = int.Parse(tokens[6]);
                    var shooting = int.Parse(tokens[7]);

                    bool teamExists = teams.Any(t => t.Name == teamName);
                    Team team;

                    if (!teamExists)
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    else
                    {
                        try
                        {
                            team = teams.Where(t => t.Name == teamName).First();
                            Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                            team.AddPlayer(player);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    break;
                case "Remove":
                    teamName = tokens[1];
                    team = teams.Where(t => t.Name == teamName).First();
                    playerName = tokens[2];

                    try
                    {
                        team.RemovePlayer(playerName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case "Rating":
                    teamName = tokens[1];
                    teamExists = teams.Any(t => t.Name == teamName);

                    if (!teamExists)
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    else
                    {
                        team = teams.Where(t => t.Name == teamName).First();

                        Console.WriteLine($"{team.Name} - {team.Rating()}");
                    }

                    break;
            }

            line = Console.ReadLine();
        }
    }
}