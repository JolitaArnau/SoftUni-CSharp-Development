namespace PawInc.Models.Animals
{
    public class Cat : Animal
    {
        private readonly int _iq;

        public Cat(string name, int age, int iq) : base(name, age)
        {
            _iq = iq;
        }

        private int Iq => _iq;
    }
}