namespace ListIterator
{
    public interface IIterator
    {
        bool Move();
        bool HasNext();
        string Print();
    }
}