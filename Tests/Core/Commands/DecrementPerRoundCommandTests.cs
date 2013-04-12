using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class DecrementPerRoundCommandTests
    {
        ActionViewModel actionViewModel;
        DecrementPerRoundCommand decrementPerRoundCommand;
        
        [SetUp]
        public void Setup()
        {
            var action = new BattleAction("name", 1, 1);
            actionViewModel = new ActionViewModel(action);
            decrementPerRoundCommand = new DecrementPerRoundCommand(actionViewModel);
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
            Assert.That(actionViewModel.PerRound, Is.EqualTo(.5));
        }
    }
}