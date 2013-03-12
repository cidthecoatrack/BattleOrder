﻿using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemoveEnemyCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private RemoveEnemyCommand removeEnemyCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
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