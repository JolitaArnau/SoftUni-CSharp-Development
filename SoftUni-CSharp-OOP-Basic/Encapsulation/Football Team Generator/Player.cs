using System;
using System.Collections.Generic;

public class Player
{
    private const string InvalidNameException = "A name should not be empty.";
    private const int MinStatsValue = 0;
    private const int MaxStatsValue = 100;
    private const string InvalidStatsException = "should be between 0 and 100.";

    private string name;
    private int endurance;
    private int sprint;
    private int dribble;
    private int passing;
    private int shooting;

    public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
    {
        this.Name = name;
        this.Endurance = endurance;
        this.Sprint = sprint;
        this.Dribble = dribble;
        this.Passing = passing;
        this.Shooting = shooting;
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

    public int Endurance
    {
        get { return this.endurance; }

        private set
        {
            if (value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException($"Endurance {InvalidStatsException}");
            }

            this.endurance = value;
        }
    }

    public int Sprint
    {
        get { return this.sprint; }

        private set
        {
            if (value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException($"Sprint {InvalidStatsException}");
            }

            this.sprint = value;
        }
    }

    public int Dribble
    {
        get { return this.dribble; }

        private set
        {
            if (value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException($"Dribble {InvalidStatsException}");
            }

            this.dribble = value;
        }
    }


    public int Passing
    {
        get { return this.passing; }

        private set
        {
            if (value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException($"Passing {InvalidStatsException}");
            }

            this.passing = value;
        }
    }


    public int Shooting
    {
        get { return this.shooting; }

        private set
        {
            if (value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException($"Shooting {InvalidStatsException}");
            }

            this.shooting = value;
        }
    }

    public int GetSkillLevel()
    {
        return (int) Math.Round((this.passing + this.shooting + this.sprint + this.dribble + this.endurance) / 5.0);
    }
}