using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class DecrementInitiativeCommandTests
    {
        private SetInitiativesViewModel setInitiativesViewModel;
        private DecrementInitiativeCommand decrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new ActionParticipant("name", isNpc: false);
            var participants = new[] { participant };
            var dice = DiceFactory.Create(new Random());

            setInitiativesViewModel = new SetInitiativesViewModel(participants, dice);
            participant.Initiative = 2;

            decrementInitiativeCommand = new DecrementInitiativeCommand(setInitiativesViewModel);
        }

        [Test]
        public void InitiativeTooLow()
        {
            Assert.That(decrementInitiativeCommand.CanExecute(new Object()), Is.True);
            decrementInitiativeCommand.Execute(new Object());
            Assert.That(decrementInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            decrementInitiativeCommand.Execute(new Object());
            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(1));
        }
    }
}