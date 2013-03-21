using System;
using System.Collections.Generic;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class GetNextAttacksCommandTests
    {
        RoundViewModel roundViewModel;
        GetNextAttacksCommand getNextAttacksCommand;

        [SetUp]
        public void Setup()
        {
            var attacks = new Queue<QueueableAction>();
            var firstAttack = new Attack("attack 1", 1, 1, true);
            var secondAttack = new Attack("attack 2", 1, 1, true);
            attacks.Enqueue(new QueueableAction("name", firstAttack, 2));
            attacks.Enqueue(new QueueableAction("other name", secondAttack, 1));

            roundViewModel = new RoundViewModel(attacks, 1);
            getNextAttacksCommand = new GetNextAttacksCommand(roundViewModel);
        }

        [Test]
        public void RoundComplete()
        {
            Assert.That(getNextAttacksCommand.CanExecute(new Object()), Is.True);
            getNextAttacksCommand.Execute(new Object());
            Assert.That(getNextAttacksCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            Assert.That(roundViewModel.CurrentAttacks, Is.EqualTo("\nname's attack 1"));
            getNextAttacksCommand.Execute(new Object());
            Assert.That(roundViewModel.CurrentAttacks, Is.EqualTo("\nother name's attack 2"));
        }
    }
}