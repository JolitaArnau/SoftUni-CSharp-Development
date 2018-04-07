namespace ListIterator
{
    using System;
    using System.Collections.Generic;


    public class Iterator : IIterator
    {
        private readonly IList<string> data;
        private int index;

        public Iterator(params string[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            this.data = array;
            this.index = 0;
        }

        public bool Move()
        {
            if (this.HasNext())
            {
                this.index++;
                return true;
            }

            return false;
        }

        public bool HasNext()
        {
            int nextIndex = this.index + 1;
            if (nextIndex > this.data.Count)
            {
                return false;
            }

            return true;
        }

        public string Print()
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.data[this.index];
        }
    }
}