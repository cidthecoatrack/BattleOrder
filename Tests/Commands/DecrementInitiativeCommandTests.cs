using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class DecrementInitiativeCommandTests
    {
        private SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        private DecrementInitiativeCommand decrementInitiativeCommand;

        [SetUp]
        public void Setup()
        {
            var participant = new Participant("name");
            var participants = new[] { participant };

            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            participant.Initiative = 2;

            decrementInitiativeCommand = new DecrementInitiativeCommand(setParticipantInitiativesViewModel);
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
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(1));
        }
    }
}