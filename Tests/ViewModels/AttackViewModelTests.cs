using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class AttackViewModelTests
    {
        Attack attack;
        AttackViewModel attackViewModel;
        
        [SetUp]
        public void Setup()
        {
            attack = new Attack("name", 1.5, 2);
            attackViewModel = new AttackViewModel(attack);
        }
        
        [Test]
        public void CorrectLoad()
        {
            Assert.That(attackViewModel.AttackName, Is.EqualTo("name"));
            Assert.That(attackViewModel.PerRound, Is.EqualTo(1.5));
            Assert.That(attackViewModel.Speed, Is.EqualTo(2));
        }

        [Test]
        public void IncrementPerRound()
        {
            attackViewModel.IncrementPerRound();
            Assert.That(attackViewModel.PerRound, Is.EqualTo(2));
        }

        [Test]
        public void IncrementSpeed()
        {
            attackViewModel.IncrementSpeed();
            Assert.That(attackViewModel.Speed, Is.EqualTo(3));
        }

        [Test]
        public void DecrementPerRound()
        {
            attackViewModel.DecrementPerRound();
            Assert.That(attackViewModel.PerRound, Is.EqualTo(1));
        }

        [Test]
        public void DecrementSpeed()
        {
            attackViewModel.DecrementSpeed();
            Assert.That(attackViewModel.Speed, Is.EqualTo(1));
        }

        [Test]
        public void CorrectSave()
        {
            attackViewModel.AttackName = "New Name";
            attackViewModel.IncrementPerRound();
            attackViewModel.IncrementPerRound();
            attackViewModel.IncrementSpeed();

            attackViewModel.SaveChanges();

            Assert.That(attack.Name, Is.EqualTo("New Name"));
            Assert.That(attack.PerRound, Is.EqualTo(2.5));
            Assert.That(attack.Speed, Is.EqualTo(3));
        }
    }
}