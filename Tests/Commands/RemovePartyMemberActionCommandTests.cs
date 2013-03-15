using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemovePartyMemberActionCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private RemovePartyMemberActionCommand removePartyMemberActionCommand;
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
            removePartyMemberActionCommand = new RemovePartyMemberActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void CantExecuteIfNoCurrentPartyMember()
        {
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAction = action;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAction()
        {
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAction = action;
            Assert.That(removePartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}