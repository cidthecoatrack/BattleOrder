using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.ViewModels
{
    [TestFixture]
    public class ActionViewModelTests
    {
        BattleAction action;
        ActionViewModel actionViewModel;
        
        [SetUp]
        public void Setup()
        {
            action = new BattleAction("name", 1.5, 2);
            actionViewModel = new ActionViewModel(action);
        }
        
        [Test]
        public void CorrectLoad()
        {
            Assert.That(actionViewModel.Name, Is.EqualTo("name"));
            Assert.That(actionViewModel.PerRound, Is.EqualTo(1.5));
            Assert.That(actionViewModel.Speed, Is.EqualTo(2));
        }

        [Test]
        public void IncrementPerRound()
        {
            actionViewModel.IncrementPerRound();
            Assert.That(actionViewModel.PerRound, Is.EqualTo(2));
        }

        [Test]
        public void IncrementSpeed()
        {
            actionViewModel.IncrementSpeed();
            Assert.That(actionViewModel.Speed, Is.EqualTo(3));
        }

        [Test]
        public void DecrementPerRound()
        {
            actionViewModel.DecrementPerRound();
            Assert.That(actionViewModel.PerRound, Is.EqualTo(1));
        }

        [Test]
        public void DecrementSpeed()
        {
            actionViewModel.DecrementSpeed();
            Assert.That(actionViewModel.Speed, Is.EqualTo(1));
        }

        [Test]
        public void CorrectSave()
        {
            actionViewModel.Name = "New Name";
            actionViewModel.IncrementPerRound();
            actionViewModel.IncrementPerRound();
            actionViewModel.IncrementSpeed();

            actionViewModel.SaveChanges();

            Assert.That(action.Name, Is.EqualTo("New Name"));
            Assert.That(action.PerRound, Is.EqualTo(2.5));
            Assert.That(action.Speed, Is.EqualTo(3));
        }
    }
}