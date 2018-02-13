using System;
using System.Linq;

public class DateModifier
{
    public static void DateDifference(string firstDate, string secondDate)
    {
        var d1 = firstDate.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var d2 = secondDate.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        DateTime first = new DateTime(d1[0], d1[1], d1[2]);
        DateTime second = new DateTime(d2[0], d2[1], d2[2]);

        TimeSpan difference = first.Subtract(second);
        Console.WriteLine(Math.Abs(difference.TotalDays));
    }
}