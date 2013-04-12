using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class IncrementInitiativeCommandTests
    {
        private SetInitiativesViewModel setInitiativesViewModel;
        private IncrementInitiativeCommand incrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new ActionParticipant("name", isNpc: false);
            var participants = new[] { participant };
            var dice = DiceFactory.Create(new Random());

            setInitiativesViewModel = new SetInitiativesViewModel(participants, dice);
            participant.Initiative = 9;

            incrementInitiativeCommand = new IncrementInitiativeCommand(setInitiativesViewModel);
        }

        [Test]
        public void InitiativeTooHigh()
        {
            Assert.That(incrementInitiativeCommand.CanExecute(new Object()), Is.True);
            incrementInitiativeCommand.Execute(new Object());
            Assert.That(incrementInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            incrementInitiativeCommand.Execute(new Object());
            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(10));
        }
    }
}