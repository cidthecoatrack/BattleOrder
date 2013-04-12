using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Commands
{
    [TestFixture]
    public class AddPartyMemberActionCommandTests
    {
        private PartyViewModel partyViewModel;
        private AddPartyMemberActionCommand addPartyMemberActionCommand;
        private ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name");
            var participants = new[] { participant };

            partyViewModel = new PartyViewModel(participants);
            addPartyMemberActionCommand = new AddPartyMemberActionCommand(partyViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(addPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            partyViewModel.CurrentPartyMember = participant;
            Assert.That(addPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}