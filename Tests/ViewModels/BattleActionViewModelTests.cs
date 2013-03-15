using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class BattleActionViewModelTests
    {
        BattleAction action;
        BattleActionViewModel battleActionViewModel;
        
        [SetUp]
        public void Setup()
        {
            action = new BattleAction("name", 1.5, 2);
            battleActionViewModel = new BattleActionViewModel(action);
        }
        
        [Test]
        public void CorrectLoad()
        {
            Assert.That(battleActionViewModel.Name, Is.EqualTo("name"));
            Assert.That(battleActionViewModel.PerRound, Is.EqualTo(1.5));
            Assert.That(battleActionViewModel.Speed, Is.EqualTo(2));
        }

        [Test]
        public void IncrementPerRound()
        {
            battleActionViewModel.IncrementPerRound();
            Assert.That(battleActionViewModel.PerRound, Is.EqualTo(2));
        }

        [Test]
        public void IncrementSpeed()
        {
            battleActionViewModel.IncrementSpeed();
            Assert.That(battleActionViewModel.Speed, Is.EqualTo(3));
        }

        [Test]
        public void DecrementPerRound()
        {
            battleActionViewModel.DecrementPerRound();
            Assert.That(battleActionViewModel.PerRound, Is.EqualTo(1));
        }

        [Test]
        public void DecrementSpeed()
        {
            battleActionViewModel.DecrementSpeed();
            Assert.That(battleActionViewModel.Speed, Is.EqualTo(1));
        }

        [Test]
        public void CorrectSave()
        {
            battleActionViewModel.Name = "New Name";
            battleActionViewModel.IncrementPerRound();
            battleActionViewModel.IncrementPerRound();
            battleActionViewModel.IncrementSpeed();

            battleActionViewModel.SaveChanges();

            Assert.That(action.Name, Is.EqualTo("New Name"));
            Assert.That(action.PerRound, Is.EqualTo(2.5));
            Assert.That(action.Speed, Is.EqualTo(3));
        }
    }
}