using System;
using System.Collections.Generic;

public class Box<T> where
    T : IComparable<T>
{
    private IList<T> sequence;

    public Box()
    {
        this.sequence = new List<T>();
    }

    public IList<T> Sequence => this.sequence;
    
    public void Add(T element)
    {
        this.sequence.Add(element);
    }

    public int GetElementsCountGreaterThanProvidedElement(IList<T> elements, T queryElement)
    {
        var counter = 0;
        
        foreach (var element in elements)
        {
            // 5.CompareTo(6) = -1      First value is smaller. / Second value is bigger -> increase counter

            if (queryElement.CompareTo(element) == -1)
            {
                counter++;
            }
        }
        
        return counter;
    }
}