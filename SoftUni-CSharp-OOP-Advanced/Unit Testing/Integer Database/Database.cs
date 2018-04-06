using System;
using System.Linq;

namespace IntegerDatabase
{
    public class Database
    {
        private const int CollectionCapacity = 16;

        private const string CollectionCapacityException =
            "You are trying to add more items than the collection capacity of ";

        private const string EmptyCollectionException = "Collection is already empty.";

        private readonly int[] numbersCollection;
        private int currentIndex;

        public int this[int index] => this.numbersCollection[index];

        public Database(params int[] numbersToStore)
        {
            this.numbersCollection = new int[CollectionCapacity];

            if (numbersToStore.Length > CollectionCapacity)
            {
                throw new InvalidOperationException(CollectionCapacityException + CollectionCapacity);
            }

            foreach (var number in numbersToStore)
            {
                this.numbersCollection[currentIndex] = number;
                this.currentIndex++;
            }
        }

        public void Add(int number)
        {
            if (this.currentIndex >= CollectionCapacity)
            {
                throw new InvalidOperationException(CollectionCapacityException + CollectionCapacity);
            }

            numbersCollection[currentIndex] = number;
            this.currentIndex++;
        }

        public void Remove()
        {
            if (this.currentIndex == 0)
            {
                throw new InvalidOperationException(EmptyCollectionException);
            }

            var lastNumber = this.numbersCollection[this.currentIndex - 1];

            this.numbersCollection[lastNumber] = 0;
            this.currentIndex--;
        }

        public int[] Fetch()
        {
            return this.numbersCollection.Take(currentIndex).ToArray();
        }
    }
}