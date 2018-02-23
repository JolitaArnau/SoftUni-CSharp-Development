using System;

public class Person
{
    private const string FirstLetterUpperCase = "Expected upper case letter! Argument:";
    private const string FirstNameLength = "Expected length at least 4 symbols! Argument: firstName";
    private const string LastNameLength = "Expected length at least 3 symbols! Argument: lastName";

    private string firstName;
    private string lastName;

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName
    {
        get => firstName;
        
        private set
        {
            if (Char.IsLower(value[0]))
            {
                throw new ArgumentException($"{FirstLetterUpperCase} firstName");
            }

            if (value.Length < 4)
            {
                throw new ArgumentException(FirstNameLength);
            }

            firstName = value;
        }
    }

    public string LastName
    {
        get => lastName;

        private set
        {
            if (Char.IsLower(value[0]))
            {
                throw new ArgumentException($"{FirstLetterUpperCase} lastName");
            }
            
            if (value.Length < 3)
            {
                throw new ArgumentException(LastNameLength);
            }

            lastName = value;
        }
    }
}