using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class RemoveEnemyActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private RemoveEnemyActionCommand removeEnemyActionCommand;
        private ActionParticipant participant;
        private BattleAction action;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack");
            participant = new ActionParticipant("name");
            participant.AddAction(action);
            var participants = new[] { participant };

            partyViewModel = new PartyViewModel(participants);
            removeEnemyActionCommand = new RemoveEnemyActionCommand(partyViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemyAction = action;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAction()
        {
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemy = participant;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemyAction = action;
            Assert.That(removeEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}