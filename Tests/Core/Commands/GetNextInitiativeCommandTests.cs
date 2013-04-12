using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;
using Moq;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class GetNextInitiativeCommandTests
    {
        private SetInitiativesViewModel setInitiativesViewModel;
        private GetNextInitiativeCommand getNextInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new ActionParticipant("name", isNpc: false);
            var participants = new[] { participant };
            var dice = DiceFactory.Create(new Random());

            setInitiativesViewModel = new SetInitiativesViewModel(participants, dice);
            participant.Initiative = 1;

            getNextInitiativeCommand = new GetNextInitiativeCommand(setInitiativesViewModel);
        }

        [Test]
        public void InitiativeTooLow()
        {
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void InitiativeTooHigh()
        {
            while (setInitiativesViewModel.CurrentInitiative < 10)
                setInitiativesViewModel.IncrementCurrentInitiative();

            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setInitiativesViewModel.IncrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            getNextInitiativeCommand.Execute(new Object());
            Assert.That(setInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}