using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListyIterator
{
    public class Iterator<T> : IEnumerable<T>
    {
        private const int CurrentInternalInitialIndex = 0;

        private readonly List<T> data;
        private int currentInternalIndex;
        
        public Iterator()
        {
            this.data = new List<T>();
            this.currentInternalIndex = CurrentInternalInitialIndex;
        }

        public Iterator(IEnumerable<T> sequence)
        {
            this.data = new List<T>(sequence);
            this.currentInternalIndex = CurrentInternalInitialIndex;
        }

        public bool Move()
        {
            if (this.currentInternalIndex < this.data.Count - 1)
            {
                currentInternalIndex++;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool HasNext() => this.currentInternalIndex < this.data.Count - 1;

        public T Print()
        {
            if (!this.data.Any())
            {
                throw new ArgumentException("Invalid Operation!");
            }

            return this.data[this.currentInternalIndex];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Join(" ", this.data));

            return sb.ToString();
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.data.Count; i++)
            {
                yield return this.data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}