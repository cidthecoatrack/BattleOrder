using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class EditPartyMemberCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private EditPartyMemberCommand editPartyMemberCommand;
        private ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            editPartyMemberCommand = new EditPartyMemberCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(editPartyMemberCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberCommand.CanExecute(new Object()), Is.True);
        }
    }
}