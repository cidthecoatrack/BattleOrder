using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class EditEnemyActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private EditEnemyActionCommand editEnemyActionCommand;
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
            editEnemyActionCommand = new EditEnemyActionCommand(partyViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemyAction = action;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAction()
        {
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentEnemyAction = action;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}