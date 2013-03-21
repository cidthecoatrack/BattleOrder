using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class EditPartyMemberAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private EditPartyMemberActionCommand editPartyMemberAttackCommand;
        private Participant participant;
        private Attack attack;

        [SetUp]
        public void Setup()
        {
            attack = new Attack("attack");
            participant = new Participant("name");
            participant.AddAttack(attack);
            var participants = new[] { participant };

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
            editPartyMemberAttackCommand = new EditPartyMemberActionCommand(allParticipantsViewModel);
        }

        [Test]
        public void NoCurrentPartyMember()
        {
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAttack = attack;
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAttack()
        {
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAttack = attack;
            Assert.That(editPartyMemberAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}