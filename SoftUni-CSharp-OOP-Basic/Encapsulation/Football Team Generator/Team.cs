using System;
using System.Collections.Generic;
using System.Linq;

public class Team
{
    private const string InvalidNameException = "A name should not be empty.";

    private string name;
    private List<Player> players;

    public Team(string name)
    {
        this.Name = name;
        this.players = new List<Player>();
    }

    public string Name
    {
        get { return this.name; }

        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(InvalidNameException);
            }

            this.name = value;
        }
    }

    public int Rating()
    {
        if (this.players.Count == 0)
        {
            return 0;
        }

        return (int) Math.Round(this.players.Select(p => p.GetSkillLevel()).Sum() / (double) this.players.Count);
    }

    public void AddPlayer(Player player)
    {        
        this.players.Add(player);
    }

    public void RemovePlayer(string playerName)
    {
        var playerToRemove = this.players.FirstOrDefault(p => p.Name == playerName);
        if (playerToRemove == null)
        {
            throw new ArgumentException($"Player {playerName} is not in {this.name} team.");
        }

        this.players.Remove(playerToRemove);
    }
}