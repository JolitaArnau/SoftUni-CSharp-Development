using System.Collections.Generic;

public interface IMyList<T> : IEnumerable<T>
{
    void Add(T item);
    void Remove(int index);
    bool Contains(T item);
    void Swap(int firstIndex, int secondIndex);
    int CountGreaterThan(T item);
    T Max();
    T Min();
    IEnumerable<T> CustomList { get; }
    void Sort();
}