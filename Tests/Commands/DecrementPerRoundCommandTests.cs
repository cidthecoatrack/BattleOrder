using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class DecrementPerRoundCommandTests
    {
        AttackViewModel attackViewModel;
        DecrementPerRoundCommand decrementPerRoundCommand;
        
        [SetUp]
        public void Setup()
        {
            var attack = new Attack("name", 1, 1);
            attackViewModel = new AttackViewModel(attack);
            decrementPerRoundCommand = new DecrementPerRoundCommand(attackViewModel);
        }
        
        [Test]
        public void CantExecuteIfPerRoundTooLow()
        {
            Assert.That(decrementPerRoundCommand.CanExecute(new Object()), Is.True);
            decrementPerRoundCommand.Execute(new Object());
            Assert.That(decrementPerRoundCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void ExecuteDecrementsPerRound()
        {
            decrementPerRoundCommand.Execute(new Object());
            Assert.That(attackViewModel.PerRound, Is.EqualTo(.5));
        }
    }
}