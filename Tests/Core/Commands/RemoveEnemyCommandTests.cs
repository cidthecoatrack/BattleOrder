using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class RemoveEnemyCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private RemoveEnemyCommand removeEnemyCommand;
        private ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            removeEnemyCommand = new RemoveEnemyCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(removeEnemyCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyCommand.CanExecute(new Object()), Is.True);
        }
    }
}