using System;
using System.Linq;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class ParticipantViewModelTests
    {
        Participant participant;
        ParticipantViewModel participantViewModel;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name", false, false);
            participantViewModel = new ParticipantViewModel(participant);
        }

        [Test]
        public void CorrectLoad()
        {
            Assert.That(participantViewModel.Name, Is.EqualTo(participant.Name));
            Assert.That(participantViewModel.IsEnemy, Is.EqualTo(participant.IsEnemy), "IsEnemy");
            Assert.That(participantViewModel.IsNpc, Is.EqualTo(participant.IsNpc), "IsNPC");
        }

        [Test]
        public void CorrectSave()
        {
            participantViewModel.Name = "New Name";
            participantViewModel.IsNpc = true;

            participantViewModel.SaveChanges();

            Assert.That(participant.Name, Is.EqualTo(participantViewModel.Name));
            Assert.That(participantViewModel.IsNpc, Is.EqualTo(participantViewModel.IsNpc));
        }

        [Test]
        public void AddsAttack()
        {
            var attack = new Attack("attack name", 1, 1);
            var attackCount = participant.Attacks.Count();
            participantViewModel.AddAttack(attack);

            Assert.That(participant.Attacks, Contains.Item(attack));
            Assert.That(participant.Attacks.Count(), Is.EqualTo(attackCount + 1));
        }
    }
}