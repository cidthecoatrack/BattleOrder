using System;
using BattleOrder.Models;
using BattleOrder.Models.Participants;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.ViewModels
{
    [TestFixture]
    public class ParticipantViewModelTests
    {
        Participant participant;
        ParticipantViewModel participantViewModel;

        [SetUp]
        public void Setup()
        {
            participant = new Participant("name", false);
            participantViewModel = new ParticipantViewModel(participant);
        }

        [Test]
        public void CorrectLoad()
        {
            Assert.That(participantViewModel.Name, Is.EqualTo("name"));
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
    }
}