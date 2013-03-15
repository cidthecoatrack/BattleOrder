using System.Collections.Generic;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;
namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class RoundViewModelTests
    {
        RoundViewModel roundViewModel;

        [SetUp]
        public void Setup()
        {
            var attack = new BattleAction("attack", 1, 1, true);
            var attacks = new Queue<QueueableBattleAction>();
            attacks.Enqueue(new QueueableBattleAction("Name", attack, 2));
            attacks.Enqueue(new QueueableBattleAction("Other Name", attack, 1));

            roundViewModel = new RoundViewModel(attacks, 1);
        }
        
        [Test]
        public void CorrectLoad()
        {
            Assert.That(roundViewModel.CompletedActions, Is.EqualTo(1));
            Assert.That(roundViewModel.CurrentActions, Is.EqualTo("\nName's attack"));
            Assert.That(roundViewModel.RoundIsActive, Is.True);
            Assert.That(roundViewModel.RoundTitle, Is.EqualTo("Round 1: -1"));
            Assert.That(roundViewModel.TotalActions, Is.EqualTo(2));
        }

        [Test]
        public void GetAttacks()
        {
            roundViewModel.GetActions();
            
            Assert.That(roundViewModel.CompletedActions, Is.EqualTo(2));
            Assert.That(roundViewModel.CurrentActions, Is.EqualTo("\nOther Name's attack"));
            Assert.That(roundViewModel.RoundIsActive, Is.False);
            Assert.That(roundViewModel.RoundTitle, Is.EqualTo("Round 1: 0"));
        }
    }
}