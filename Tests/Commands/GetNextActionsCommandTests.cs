using System;
using System.Collections.Generic;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class GetNextActionsCommandTests
    {
        private RoundViewModel roundViewModel;
        private GetNextActionsCommand getNextActionsCommand;

        [SetUp]
        public void Setup()
        {
            var name = new ActionParticipant("name");
            var otherName = new ActionParticipant("other name");
            var participants = new[] { name, otherName };

            var firstAction = new BattleAction("attack 1", 1, 1, true);
            var secondAction = new BattleAction("attack 2", 1, 1, true);

            name.AddAction(firstAction);
            otherName.AddAction(secondAction);

            name.Initiative = 2;
            otherName.Initiative = 1;


            roundViewModel = new RoundViewModel(1, participants);
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