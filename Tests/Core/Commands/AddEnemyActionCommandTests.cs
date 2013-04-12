using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class AddEnemyActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private AddEnemyActionCommand addEnemyActionCommand;
        private ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name");
            var participants = new[] { participant };

            partyViewModel = new PartyViewModel(participants);
            addEnemyActionCommand = new AddEnemyActionCommand(partyViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(addEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemy = participant;
            Assert.That(addEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}