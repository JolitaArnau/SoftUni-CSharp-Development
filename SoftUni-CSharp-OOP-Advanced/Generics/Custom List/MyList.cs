using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyList<T> : IMyList<T>, IEnumerable<T> where T : IComparable<T>
{
    private IList<T> sequence;

    public MyList()
    {
        this.sequence = new List<T>();
    }

    public IEnumerable<T> CustomList => sequence;


    public void Add(T item)
    {
        this.sequence.Add(item);
    }

    public void Remove(int index)
    {
        this.sequence.RemoveAt(index);
    }

    public bool Contains(T item)
    {
        return this.sequence.Contains(item);
    }

    public void Swap(int firstIndex, int secondIndex)
    {
        var firstElement = this.sequence[firstIndex];
        sequence[firstIndex] = sequence[secondIndex];
        sequence[secondIndex] = firstElement;
    }

    public int CountGreaterThan(T item)
    {
        throw new NotImplementedException();
    }

    public int CountGraterThan(T item)
    {
        var counter = 0;

        foreach (var el in this.sequence)
        {
            if (item.CompareTo(el) == -1)
            {
                counter++;
            }
        }

        return counter;
    }

    public T Max()
    {
        return sequence.Max();
    }

    public T Min()
    {
        return sequence.Min();
    }

    public void Sort()
    {
        var orderedList = new MyList<T>();

        foreach (var item in this.sequence.OrderBy(i => i).ToList())
        {
            orderedList.Add(item);
            sequence = orderedList.ToList();
        }
    }


    public IEnumerator<T> GetEnumerator()
    {
        return this.sequence.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.sequence.GetEnumerator();
    }

    public override string ToString()
    {
        var resultString = new StringBuilder();

        foreach (var el in sequence)
        {
            resultString.AppendLine(el.ToString());
        }

        return resultString.ToString();
    }
}