using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        
        MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
        MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
        if(oldestMemberMethod == null || addMemberMethod == null)
        {
            throw new Exception();
        }
        
        int countOfFamilyMembers = int.Parse(Console.ReadLine());
        Family family = new Family();

        for (int i = 0; i < countOfFamilyMembers; i++)
        {
            var members = Console.ReadLine().Split(' ').ToList();

            Person member = new Person()
            {
                Name = members[0],
                Age = int.Parse(members[1])
            };

            family.AddMember(member);
        }

        Person oldestMember = family.GetOldestMember();

        Console.Write(string.Join(" ", oldestMember.Name, oldestMember.Age));
    }
}