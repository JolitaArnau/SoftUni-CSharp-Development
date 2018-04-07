namespace ExtendedDatabase.Contracts
{
    public interface IDatabase
    {
        void Add(IPerson person);

        void Remove(IPerson person);

        IPerson FindById(long id);

        IPerson FindByUsername(string name);

        int PeopleCount { get; }

    }
}