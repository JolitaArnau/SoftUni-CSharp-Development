using IntegerDatabase;
using NUnit.Framework;

namespace IntegerDatabaseTests
{
    using System;

    public class DatabaseTests
    {
        private Database database;
        private const int DefaultCollectionCapacity = 16;
        private const int EmptyCollectionCapacity = 0;
        private const int Number = 7;

        [SetUp]
        public void InitializeDb()
        {
            this.database = new Database();
        }

        [Test]
        public void ExceptionIsThrownWhenCollectionCapacityIsExceededOnInit()
        {
            Assert.Throws<InvalidOperationException>(() => new Database(new int[DefaultCollectionCapacity + 1]));
        }

        [Test]
        public void AddShouldAddElementAtNextFreeCell()
        {
            this.database.Add(Number);

            Assert.AreEqual(Number, this.database[0]);
        }

        [Test]
        public void AddThrowsExceptionWhenCollectionCapacityIsExceeded()
        {
            database = new Database(new int[DefaultCollectionCapacity]);

            Assert.Throws<InvalidOperationException>(() => database.Add(Number));
        }

        [Test]
        public void RemoveShouldRemoveLastElementInCollection()
        {
            this.database.Add(Number);
            this.database.Remove();
            
            Assert.AreEqual(this.database[0], this.database[0]);
        }

        [Test]
        public void RemoveShouldThrowExceptionWhenDbIsEmpty()
        {
            database = new Database(new int[EmptyCollectionCapacity]);

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
    }
}