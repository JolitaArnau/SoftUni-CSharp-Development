using System;
using System.Text;

public class Student : Person
{
    private const int MinFacultyNumberLentght = 5;
    private const int MaxFacultyNumberLength = 10;
    private const string FacultyNumberLenght = "Invalid faculty number!";

    private string facultyNumber;

    public Student(string firstName, string lastName, string facultyNumber) : base(firstName, lastName)
    {
        FacultyNumber = facultyNumber;
    }

    public string FacultyNumber
    {
        get => facultyNumber;

        private set
        {
            if (value.Length < MinFacultyNumberLentght || value.Length > MaxFacultyNumberLength || !FacultyNumberHasValidChars(value))
            {
                throw new ArgumentException(FacultyNumberLenght);
            }

            facultyNumber = value;
        }
    }

    public bool FacultyNumberHasValidChars(string value)
    {
        var facultyNumberHasValidChars = true;
        foreach (char ch in value)
        {
            if (!char.IsLetterOrDigit(ch))
            {
                facultyNumberHasValidChars = false;
            }
        }

        return facultyNumberHasValidChars;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("First Name: ").AppendLine(FirstName)
            .Append("Last Name: ").AppendLine(LastName)
            .Append("Faculty number: ").AppendLine(FacultyNumber);
        return sb.ToString();
    }
}