using ExtendedDatabase.Contracts;

namespace ExtendedDatabase.Models
{
    public class Person : IPerson 
    {
        public Person(string name, long id)
        {
            this.Name = name;
            this.Id = id;
        }
        
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}