using NUnit.Framework;

namespace LabAxeProblemTests
{
    public class DummyTests
    {
        private const int Health = 10;
        private const int Experience = 20;

        private const int AttackPointsAxe = 5;

        private const int ExpectedHealthDamage = 5;

        private Dummy InitializeDummy()
        {
            return new Dummy(Health, Experience);
        }

        [Test]
        public void DummyLosesHealthAfterAttack()
        {
            var dummy = InitializeDummy();

            dummy.TakeAttack(AttackPointsAxe);

            Assert.That(dummy.Health, Is.EqualTo(ExpectedHealthDamage));
        }
    }
}