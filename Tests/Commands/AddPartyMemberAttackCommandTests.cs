using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class AddPartyMemberAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private AddPartyMemberActionCommand addPartyMemberAttackCommand;
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name");
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
            addPartyMemberAttackCommand = new AddPartyMemberActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(addPartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(addPartyMemberAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}