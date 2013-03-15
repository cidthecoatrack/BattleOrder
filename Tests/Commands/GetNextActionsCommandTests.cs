using System;
using System.Collections.Generic;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class GetNextActionsCommandTests
    {
        private RoundViewModel roundViewModel;
        private ICommand getNextActionsCommand;

        [SetUp]
        public void Setup()
        {
            var attacks = new Queue<QueueableBattleAction>();
            var firstAttack = new BattleAction("attack 1", 1, 1, true);
            var secondAttack = new BattleAction("attack 2", 1, 1, true);
            attacks.Enqueue(new QueueableBattleAction("name", firstAttack, 2));
            attacks.Enqueue(new QueueableBattleAction("other name", secondAttack, 1));

            roundViewModel = new RoundViewModel(attacks, 1);
            getNextActionsCommand = new GetNextActionsCommand(roundViewModel);
        }

        [Test]
        public void RoundComplete()
        {
            Assert.That(getNextActionsCommand.CanExecute(new Object()), Is.True);
            getNextActionsCommand.Execute(new Object());
            Assert.That(getNextActionsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            Assert.That(roundViewModel.CurrentActions, Is.EqualTo("\nname's attack 1"));
            getNextActionsCommand.Execute(new Object());
            Assert.That(roundViewModel.CurrentActions, Is.EqualTo("\nother name's attack 2"));
        }
    }
}