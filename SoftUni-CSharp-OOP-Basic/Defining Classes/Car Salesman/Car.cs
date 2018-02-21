public class Car
{
    public string Model { get; set; }
    public Engine Engine { get; set; }
    public string Weight { get; set; }
    public string Color { get; set; }

    public Car(string model, Engine engine)
    {
        Model = model;
        Engine = engine;
        Weight = "n/a";
        Color = "n/a";
    }
}