namespace PawInc.Models.Animals
{
    public class Dog : Animal
    {
        private readonly int _commands;

        public Dog(string name, int age, int commands) : base(name, age)
        {
            _commands = commands;
        }

        private int Commands => _commands;
    }
}