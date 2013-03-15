using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class EditEnemyActionCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private EditEnemyActionCommand editEnemyActionCommand;
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
            editEnemyActionCommand = new EditEnemyActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentEnemy()
        {
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAction = action;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentEnemyAction()
        {
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemy = participant;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentEnemyAction = action;
            Assert.That(editEnemyActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}