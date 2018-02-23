public abstract class Mood
{
    private int happinessPoints;

    public Mood(int happinessPoints)
    {
        this.happinessPoints = happinessPoints;
    }

    public int HappinessPoints { get; private set; }
}