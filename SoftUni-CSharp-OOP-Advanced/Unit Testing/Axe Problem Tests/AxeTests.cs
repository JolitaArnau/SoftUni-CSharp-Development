using NUnit.Framework;

namespace LabAxeProblemTests
{
    public class AxeTests
    {
        private const int AttackPoints = 10;
        private const int Durability = 10;

        private const int DummyHealth = 10;
        private const int DummyExperience = 10;

        private const int ExpectedDurabilityValueAfterSingleAttack = 9;

        private Axe InitializeAxe()
        {
            return new Axe(AttackPoints, Durability);
        }

        private Dummy InitilizeDummy()
        {
            return new Dummy(DummyHealth, DummyExperience);
        }

        [Test]
        public void AxeLosesDurability()
        {
            var axe = InitializeAxe();
            var dummy = InitilizeDummy();

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(ExpectedDurabilityValueAfterSingleAttack),
                "Axe Durability doesn't change after attack.");
        }
    }
}