using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class SaveParticipantEditsCommandTests
    {
        private ActionParticipant participant;
        private ParticipantViewModel participantViewModel;
        private SaveParticipantEditsCommand saveParticipantEditsCommand;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name", true);
            participantViewModel = new ParticipantViewModel(participant);
            saveParticipantEditsCommand = new SaveParticipantEditsCommand(participantViewModel);
        }

        [Test]
        public void EmptyName()
        {
            Assert.That(saveParticipantEditsCommand.CanExecute(new Object()), Is.True);
            participantViewModel.Name = String.Empty;
            Assert.That(saveParticipantEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            participantViewModel.Name = "New Name";
            participantViewModel.IsNpc = false;
            
            saveParticipantEditsCommand.Execute(new Object());

            Assert.That(participant.Name, Is.EqualTo("New Name"));
            Assert.That(participant.IsNpc, Is.False);
        }
    }
}