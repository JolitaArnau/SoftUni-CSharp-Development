public interface ICar
{
    string Model { get; set; }
    void Brake();
    void PushGasPedal();
    string DriverName { get; set; }
}