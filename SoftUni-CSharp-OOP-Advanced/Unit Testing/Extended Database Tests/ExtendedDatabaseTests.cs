
using System;
using System.Collections.Generic;
using ExtendedDatabase.Contracts;
using ExtendedDatabase.Models;
using NUnit.Framework;
using System.Linq;

namespace DatabaseTester
{
    public class ExtendedDatabaseTests
    {
        private const string UsernameToCheckAgainst = "Alex";
        private const long IdToCheckAgainst = 123987456;

        private IDatabase database;
        private readonly IPerson firstTestPerson = new Person("Alex", 123987456);
        private readonly IPerson secondTestPerson = new Person("Peter", 73651827);

        [SetUp]
        public void InitializeDb()
        {
            IList<IPerson> people = new List<IPerson>() { firstTestPerson, secondTestPerson };
            this.database = new Database(people);
        }

        [Test]
        public void InitializeDbWithEmptyConstructorShouldNotThrowError()
        {
            Assert.DoesNotThrow(() => this.database = new Database());
        }

        [Test]
        public void InitializeDbWithParamsWorks()
        {
            Assert.AreEqual(2, this.database.PeopleCount);
        }

        [Test]
        public void AddThrowsExceptionIfUsernameIsNotUnique()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person("Alex", 12)));
        }

        [Test]
        public void AddThrowsExceptionIfIdIsNotUnique()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person("Alex", 123987456)));
        }

        [Test]
        public void AddShouldAddNewPersonToDatabase()
        {
            this.database.Add(new Person("Alan", 192));
            
            Assert.AreEqual(3, this.database.PeopleCount);
        }

        [Test]
        public void RemoveShouldRemoveOnlyExistantPerson()
        {
            var personToRemove = new Person("Dominik", 123);
            
            database.Remove(personToRemove);
            
            Assert.AreNotEqual(personToRemove, firstTestPerson);
        }

        [Test]
        public void RemoveShouldRemovePersonFromDb()
        {
            database.Remove(firstTestPerson);
            
            Assert.AreEqual(1, database.PeopleCount);
        }

        [Test]
        public void FindByIdShouldFindPersonWithProvidedId()
        {            
            Assert.That(() => database.FindById(IdToCheckAgainst), Is.EqualTo(firstTestPerson));
        }
        
        [Test]
        public void FindByUsernameShouldFindPersonWithProvidedName()
        {            
            Assert.That(() => database.FindByUsername(UsernameToCheckAgainst), Is.EqualTo(firstTestPerson));
        }

        [Test]
        public void FindByIdThrowsExceptionWhenIdDoesNotMatch()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(2));
        }

        [Test]
        public void FindByIdThrowsExceptionWhenIdIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-2));
        }

        [Test]
        public void FindByUsernameThrowsExceptionWhenNameDoesNotMatch()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("Dominik"));
        }

        [Test]
        public void FindByUsernameThrowsExceptionWhenNameIsNull()
        {
            Assert.That(() => this.database.FindByUsername(null), Throws.Exception);
        }
    }
}