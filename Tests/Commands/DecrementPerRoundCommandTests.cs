using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class DecrementPerRoundCommandTests
    {
        BattleActionViewModel battleActionViewModel;
        DecrementPerRoundCommand decrementPerRoundCommand;
        
        [SetUp]
        public void Setup()
        {
            var attack = new BattleAction("name", 1, 1);
            battleActionViewModel = new BattleActionViewModel(attack);
            decrementPerRoundCommand = new DecrementPerRoundCommand(battleActionViewModel);
        }
        
        [Test]
        public void PerRoundTooLow()
        {
            Assert.That(decrementPerRoundCommand.CanExecute(new Object()), Is.True);
            decrementPerRoundCommand.Execute(new Object());
            Assert.That(decrementPerRoundCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            decrementPerRoundCommand.Execute(new Object());
            Assert.That(battleActionViewModel.PerRound, Is.EqualTo(.5));
        }
    }
}