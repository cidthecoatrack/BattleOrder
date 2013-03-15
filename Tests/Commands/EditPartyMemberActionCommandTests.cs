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
        private PartyViewModel allParticipantsViewModel;
        private EditPartyMemberActionCommand editPartyMemberActionCommand;
        private Participant participant;
        private BattleAction action;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack");
            participant = new Participant("name");
            participant.AddAction(action);
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            editPartyMemberActionCommand = new EditPartyMemberActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAction = action;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAction()
        {
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAction = action;
            Assert.That(editPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}