using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemovePartyMemberCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private RemovePartyMemberCommand removePartyMemberCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
            removePartyMemberCommand = new RemovePartyMemberCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(removePartyMemberCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberCommand.CanExecute(new Object()), Is.True);
        }
    }
}