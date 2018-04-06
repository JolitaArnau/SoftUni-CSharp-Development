using Moq;
using NUnit.Framework;

namespace LabAxeProblemTests
{
    public class HeroTests
    {
        [Test]
        public void HeroGainsExperienceAfterAttackIfTargetDies()
        {
            const int expeprienceGained = 5;
            
            var target = new Mock<ITarget>();
            target.Setup(t => t.IsDead()).Returns(true);
            target.Setup(t => t.GiveExperience()).Returns(expeprienceGained);
            
            var weapon = new Mock<IWeapon>();
            weapon.Verify(w => w.Attack(target.Object));
            
            var hero = new Hero("Superman", weapon.Object);
            hero.Attack(target.Object);
            Assert.That(hero.Experience, Is.EqualTo(expeprienceGained));
        }
    }
}