using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class SaveActionEditsCommandTests
    {
        BattleAction action;
        ActionViewModel actionViewModel;
        SaveActionEditsCommand saveActionEditsCommand;
        
        [SetUp]
        public void Setup()
        {
            action = new BattleAction("name", .5, 1);
            actionViewModel = new ActionViewModel(action);
            saveActionEditsCommand = new SaveActionEditsCommand(actionViewModel);
        }
        
        [Test]
        public void InvalidPerRound()
        {
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.True);
            actionViewModel.DecrementPerRound();
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void InvalidName()
        {
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.True);
            actionViewModel.Name = String.Empty;
            Assert.That(saveActionEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            actionViewModel.Name = "New Name";
            actionViewModel.IncrementPerRound();
            actionViewModel.IncrementSpeed();
            
            saveActionEditsCommand.Execute(new Object());
            Assert.That(action.Name, Is.EqualTo("New Name"));
            Assert.That(action.PerRound, Is.EqualTo(1));
            Assert.That(action.Speed, Is.EqualTo(2));
        }
    }
}