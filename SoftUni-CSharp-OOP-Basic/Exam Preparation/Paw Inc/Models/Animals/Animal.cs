namespace PawInc.Models.Animals
{
    public class Animal
    {
        private readonly int _age;

        protected Animal(string name, int age)
        {
            Name = name;
            _age = age;
            IsCleansed = false;
        }

        public string Name { get; }

        private int Age => _age;

        public bool IsCleansed { get; set; }

        public string AdoptionCenterName { get; set; }
    }
}