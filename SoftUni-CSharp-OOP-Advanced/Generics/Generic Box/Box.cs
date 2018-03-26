public class Box<T>
{
    private T element;

    public Box(T element)
    {
        this.element = element;
    }

    public override string ToString()
    {
        return $"{element.GetType().FullName}: {element.ToString()}";
    }
}