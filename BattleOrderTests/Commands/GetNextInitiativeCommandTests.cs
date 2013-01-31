using System;
using BattleOrder.Commands;
using BattleOrder.Models.Participants;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.Commands
{
    [TestFixture]
    public class GetNextInitiativeCommandTests
    {
        SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        GetNextInitiativeCommand getNextInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            participant.Initiative = 1;

            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            getNextInitiativeCommand = new GetNextInitiativeCommand(setParticipantInitiativesViewModel);
        }

        [Test]
        public void CantExecuteIfInitiativeTooLow()
        {
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setParticipantInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void CantExecuteIfInitiativeTooHigh()
        {
            while (setParticipantInitiativesViewModel.CurrentInitiative < 10)
                setParticipantInitiativesViewModel.IncrementCurrentInitiative();

            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.True);
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            Assert.That(getNextInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void ExecuteGetsNextParticipantWithInitiativeToSet()
        {
            getNextInitiativeCommand.Execute(new Object());
            Assert.That(setParticipantInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}