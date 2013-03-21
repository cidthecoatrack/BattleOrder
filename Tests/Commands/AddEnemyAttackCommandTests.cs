using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class AddEnemyAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private AddEnemyAttackCommand addEnemyAttackCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
            addEnemyAttackCommand = new AddEnemyAttackCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(addEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(addEnemyAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}