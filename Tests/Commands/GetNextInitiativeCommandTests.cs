using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class GetNextInitiativeCommandTests
    {
        private SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        private GetNextInitiativeCommand getNextInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            participant.Initiative = 1;

            getNextInitiativeCommand = new GetNextInitiativeCommand(setParticipantInitiativesViewModel);
        }

        [Test]
        public void InitiativeTooLow()
        {
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setParticipantInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void InitiativeTooHigh()
        {
            while (setParticipantInitiativesViewModel.CurrentInitiative < 10)
                setParticipantInitiativesViewModel.IncrementCurrentInitiative();

            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            getNextInitiativeCommand.Execute(new Object());
            Assert.That(setParticipantInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}