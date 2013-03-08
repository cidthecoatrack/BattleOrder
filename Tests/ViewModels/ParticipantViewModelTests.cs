using System;
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
            Assert.That(participantViewModel.Name, Is.EqualTo("name"));
            Assert.That(participantViewModel.IsEnemy, Is.False);
            Assert.That(participantViewModel.IsNpc, Is.False);
        }

        [Test]
        public void CorrectSave()
        {
            participantViewModel.Name = "New Name";
            participantViewModel.IsNpc = true;

            participantViewModel.SaveChanges();

            Assert.That(participant.Name, Is.EqualTo("New Name"));
            Assert.That(participantViewModel.IsNpc, Is.True);
        }

        [Test]
        public void AddsAttack()
        {
            var attack = new Attack("attack name", 1, 1);
            participantViewModel.AddAttack(attack);

            Assert.That(participant.Attacks, Contains.Item(attack));
        }
    }
}