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
        private SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        private IncrementInitiativeCommand incrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            participant.Initiative = 9;

            incrementInitiativeCommand = new IncrementInitiativeCommand(setParticipantInitiativesViewModel);
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
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(10));
        }
    }
}