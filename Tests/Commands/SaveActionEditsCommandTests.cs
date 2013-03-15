using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class SaveActionEditsCommandTests
    {
        BattleAction action;
        BattleActionViewModel battleActionViewModel;
        SaveActionEditsCommand saveActionEditsCommand;
        
        [SetUp]
        public void Setup()
        {
            action = new BattleAction("name", .5, 1);
            battleActionViewModel = new BattleActionViewModel(action);
            saveActionEditsCommand = new SaveActionEditsCommand(battleActionViewModel);
        }
        
        [Test]
        public void InvalidPerRound()
        {
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.True);
            battleActionViewModel.DecrementPerRound();
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void InvalidActionkName()
        {
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.True);
            battleActionViewModel.Name = String.Empty;
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            battleActionViewModel.Name = "New Name";
            battleActionViewModel.IncrementPerRound();
            battleActionViewModel.IncrementSpeed();
            
            saveActionEditsCommand.Execute(new Object());
            Assert.That(action.Name, Is.EqualTo("New Name"));
            Assert.That(action.PerRound, Is.EqualTo(1));
            Assert.That(action.Speed, Is.EqualTo(2));
        }
    }
}