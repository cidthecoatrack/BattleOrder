using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class AddEnemyActionCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private AddEnemyActionCommand addEnemyActionCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            addEnemyActionCommand = new AddEnemyActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(addEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(addEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}