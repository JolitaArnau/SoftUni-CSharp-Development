using System;
using System.Collections.Generic;
using ExtendedDatabase.Contracts;
using System.Linq;

namespace ExtendedDatabase.Models
{
    public class Database : IDatabase
    {
        private const string UsernameAlreadyExistsException = "A user with this Username already exists";
        private const string IdAlreadyExistsException = "A user with this Id already exists";
        private const string IdMustBeNonNegativeException = "Id must be a non-negative number";
        private const string NoMatchingUserForProvidedIdException = "A user with this Id does not exist";
        private const string NoMatchingUserForProvidedNameException = "A user with this Name does not exist";
        private const string ProvidedQueryNameCannotBeNullException =
            "The provided query Name must be different than null";
        
        private readonly IList<IPerson> people;

        public int PeopleCount => this.people.Count;

        public Database()
        {
            this.people = new List<IPerson>();
        }
        
        public Database(IEnumerable<IPerson> people)
            :this()
        {
            foreach (var person in people)
            {
                this.people.Add(person);
            }
        }

        public void Add(IPerson personToAdd)
        {
            if (this.people.Any(p => p.Name.ToLower().Equals(personToAdd.Name.ToLower())))
            {
                throw new InvalidOperationException(UsernameAlreadyExistsException);
            }

            if (this.people.Any(p => p.Id.Equals(personToAdd.Id)))
            {
                throw new InvalidOperationException(IdAlreadyExistsException);
            }
            
            this.people.Add(personToAdd);
        }

        public void Remove(IPerson personToRemove)
        {
            if (this.people.Contains(personToRemove))
            {
                this.people.Remove(personToRemove);
            }
        }

        public IPerson FindById(long id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(IdMustBeNonNegativeException);
            }

            if (!this.people.Any(p => p.Id.Equals(id)))
            {
                throw new InvalidOperationException(NoMatchingUserForProvidedIdException);
            }

            return this.people.FirstOrDefault(p => p.Id.Equals(id));
        }

        public IPerson FindByUsername(string name)
        {
            if (!this.people.Any(p => p.Name.ToLower().Equals(name.ToLower())))
            {
                throw new InvalidOperationException(NoMatchingUserForProvidedNameException);
            }

            if (name == null)
            {
                throw new ArgumentNullException(ProvidedQueryNameCannotBeNullException);
            }

            return this.people.FirstOrDefault(p => p.Name.ToLower().Equals(name.ToLower()));
        }

    }
}