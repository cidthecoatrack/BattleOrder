using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class RemovePartyMemberActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private RemovePartyMemberActionCommand removePartyMemberActionCommand;
        private ActionParticipant participant;
        private BattleAction action;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack");
            participant = new ActionParticipant("name");
            participant.AddAction(action);
            var participants = new[] { participant };

            partyViewModel = new PartyViewModel(participants);
            removePartyMemberActionCommand = new RemovePartyMemberActionCommand(partyViewModel);
        }

        [Test]
        public void CantExecuteIfNoCurrentPartyMember()
        {
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMemberAction = action;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAction()
        {
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMemberAction = action;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}