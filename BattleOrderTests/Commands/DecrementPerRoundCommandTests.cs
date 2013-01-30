using System;
using BattleOrder.Commands;
using BattleOrder.Models.Attacks;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.Commands
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