using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class AddPartyMemberActionCommandTests
    {
        private PartyViewModel allParticipantsViewModel;
        private AddPartyMemberActionCommand addPartyMemberActionCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new PartyViewModel(participants);
            addPartyMemberActionCommand = new AddPartyMemberActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(addPartyMemberActionCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(addPartyMemberActionCommand.CanExecute(new Object()), Is.True);
        }
    }
}