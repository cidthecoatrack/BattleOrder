using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class EditPartyMemberActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private EditPartyMemberActionCommand editPartyMemberActionCommand;
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
            editPartyMemberActionCommand = new EditPartyMemberActionCommand(partyViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMemberAction = action;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAction()
        {
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMemberAction = action;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}