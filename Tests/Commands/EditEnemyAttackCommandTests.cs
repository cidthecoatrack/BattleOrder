using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class EditEnemyAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private EditEnemyActionCommand editEnemyAttackCommand;
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
            editEnemyAttackCommand = new EditEnemyActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAttack = attack;
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAttack()
        {
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAttack = attack;
            Assert.That(editEnemyAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}