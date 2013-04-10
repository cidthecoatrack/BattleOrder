using System.Collections.Generic;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;
namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class RoundViewModelTests
    {
        private RoundViewModel roundViewModel;

        [SetUp]
        public void Setup()
        {
            var name = new ActionParticipant("Name");
            var otherName = new ActionParticipant("Other Name");
            var participants = new[] { name, otherName };

            var action = new BattleAction("attack", 1, 1, true);
            name.Initiative = 2;
            otherName.Initiative = 1;
            name.AddAction(action);
            otherName.AddAction(action);

            roundViewModel = new RoundViewModel(1, participants);
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
        public void GetActions()
        {
            roundViewModel.GetActions();
            
            Assert.That(roundViewModel.CompletedActions, Is.EqualTo(2));
            Assert.That(roundViewModel.CurrentActions, Is.EqualTo("\nOther Name's attack"));
            Assert.That(roundViewModel.RoundIsActive, Is.False);
            Assert.That(roundViewModel.RoundTitle, Is.EqualTo("Round 1: 0"));
        }
    }
}