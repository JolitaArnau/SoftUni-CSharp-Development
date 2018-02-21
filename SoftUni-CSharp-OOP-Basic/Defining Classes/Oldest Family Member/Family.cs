using System.Collections.Generic;
using System.Linq;

public class Family
{
    public Family()
    {
        this.people = new List<Person>();
    }
    
    List<Person> people;

    public void AddMember(Person member)
    {
        this.people.Add(member);
    }

    public Person GetOldestMember()
    {
        return this.people.OrderByDescending(a => a.Age).First();
    }
}