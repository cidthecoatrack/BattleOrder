using System;
using System.Collections.Generic;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class IncrementInitiativeCommandTests
    {
        SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        IncrementInitiativeCommand incrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            participant.Initiative = 9;

            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            incrementInitiativeCommand = new IncrementInitiativeCommand(setParticipantInitiativesViewModel);
        }

        [Test]
        public void CantExecuteIfInitiativeTooHigh()
        {
            Assert.That(incrementInitiativeCommand.CanExecute(new Object()), Is.True);
            incrementInitiativeCommand.Execute(new Object());
            Assert.That(incrementInitiativeCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void ExecuteDecrementsPerRound()
        {
            incrementInitiativeCommand.Execute(new Object());
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(10));
        }
    }
}
