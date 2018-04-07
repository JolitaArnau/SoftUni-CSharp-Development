namespace ExtendedDatabase.Contracts
{
    public interface IPerson
    {
        long Id { get; set; }
        
        string Name { get; set; }
    }
}