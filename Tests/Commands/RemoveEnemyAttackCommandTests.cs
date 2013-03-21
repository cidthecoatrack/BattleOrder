using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemoveEnemyAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private RemoveEnemyActionCommand removeEnemyAttackCommand;
        private Participant participant;
        private Attack attack;

        [SetUp]
        public void Setup()
        {
            attack = new Attack("attack");
            participant = new Participant("name");
            participant.AddAttack(attack);
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
            removeEnemyAttackCommand = new RemoveEnemyActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAttack = attack;
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAttack()
        {
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAttack = attack;
            Assert.That(removeEnemyAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}