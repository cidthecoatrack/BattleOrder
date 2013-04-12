using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class EditEnemyCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private EditEnemyCommand editEnemyCommand;
        private ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            editEnemyCommand = new EditEnemyCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(editEnemyCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyCommand.CanExecute(new Object()), Is.True);
        }
    }
}