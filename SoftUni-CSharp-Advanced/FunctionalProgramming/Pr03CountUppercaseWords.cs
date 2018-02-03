namespace Pr03CountUppercaseWords
{
    using System;
    using System.Linq;

    class Pr03CountUppercaseWords
    {
        static void Main()
        {
            Func<string, bool> checker = s => char.IsUpper(s[0]);

                 Console
                .ReadLine()
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Where(checker)
                .ToList()
                .ForEach(s => Console.WriteLine(s));
            
            
        }
    }
}