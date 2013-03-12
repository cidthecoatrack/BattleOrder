using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class RemovePartyMemberAttackCommandTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private RemovePartyMemberAttackCommand removePartyMemberAttackCommand;
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
            removePartyMemberAttackCommand = new RemovePartyMemberAttackCommand(allParticipantsViewModel);
        }

        [Test]
        public void CantExecuteIfNoCurrentPartyMember()
        {
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAttack = attack;
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.True);
        }

        [Test]
        public void NoCurrentPartyMemberAttack()
        {
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMember = participant;
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.False);
            allParticipantsViewModel.CurrentPartyMemberAttack = attack;
            Assert.That(removePartyMemberAttackCommand.CanExecute(new Object()), Is.True);
        }
    }
}