using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemoveEnemyActionCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private RemoveEnemyActionCommand removeEnemyActionCommand;
        private Participant participant;
        private BattleAction action;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack");
            participant = new Participant("name");
            participant.AddAction(action);
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            removeEnemyActionCommand = new RemoveEnemyActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAction = action;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAction()
        {
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAction = action;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}