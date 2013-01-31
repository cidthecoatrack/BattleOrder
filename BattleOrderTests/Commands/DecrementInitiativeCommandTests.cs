using System;
using System.Collections.Generic;
using BattleOrder.Commands;
using BattleOrder.Models.Participants;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.Commands
{
    [TestFixture]
    public class DecrementInitiativeCommandTests
    {
        SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        DecrementInitiativeCommand decrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            participant.Initiative = 2;

            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            decrementInitiativeCommand = new DecrementInitiativeCommand(setParticipantInitiativesViewModel);
        }

        [Test]
        public void CantExecuteIfInitiativeTooLow()
        {
            Assert.That(decrementInitiativeCommand.CanExecute(new Object()), Is.True);
            decrementInitiativeCommand.Execute(new Object());
            Assert.That(decrementInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void ExecuteDecrementsPerRound()
        {
            decrementInitiativeCommand.Execute(new Object());
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(1));
        }
    }
}