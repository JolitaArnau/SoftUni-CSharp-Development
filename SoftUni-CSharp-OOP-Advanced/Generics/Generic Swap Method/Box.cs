using System.Collections.Generic;
using System.Text;

public class Box<T>
{
    private readonly IList<T> elements;
    private T element { get; }

    public Box()
    {
        this.elements = new List<T>();
    }

    public Box(T element)
    {
        this.element = element;
        this.elements = new List<T>();
    }

    public void Add(T item)
    {
        this.elements.Add(item);
    }

    public void Swap(int firstIndex, int secondIndex)
    {
        var firstElement = this.elements[firstIndex];
        elements[firstIndex] = elements[secondIndex];
        elements[secondIndex] = firstElement;
    }


    public override string ToString()
    {
        var sb = new StringBuilder();
        
        foreach (var piece in this.elements)
        {
            sb.AppendLine($"{piece.GetType().FullName}: {piece}");
        }

        return sb.ToString().Trim();
    }
}