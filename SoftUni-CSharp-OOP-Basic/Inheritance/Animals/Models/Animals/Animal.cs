using System;

public abstract class Animal
{
    private const string InvalidInputMessage = "Invalid input!";

    private string name;
    private int age;
    private string gender;

    public Animal(string name, int age, string gender)
    {
        Name = name;
        Age = age;
        Gender = gender;
    }

    public string Name
    {
        get => name;

        private set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(InvalidInputMessage);
            }

            name = value;
        }
    }


    public int Age
    {
        get => age;

        private set
        {
            if (value < 0)
            {
                throw new ArgumentException(InvalidInputMessage);
            }

            age = value;
        }
    }

    public string Gender
    {
        get => gender;

        private set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(InvalidInputMessage);
            }

            gender = value;
        }
    }

    public abstract string ProduceSound();

    public override string ToString()
    {
        return $"{GetType().Name}{Environment.NewLine}{Name} {Age} {Gender}{Environment.NewLine}{ProduceSound()}";
    }
}