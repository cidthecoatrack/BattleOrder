using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class SetParticipantInitiativesViewModelTests
    {
        IEnumerable<Participant> participants;
        SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;

        [SetUp]
        public void Setup()
        {
            participants = new[] { new Participant("1"), new Participant("2") };
            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
        }

        [Test]
        public void CorrectLoad()
        {
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(0));
            Assert.That(setParticipantInitiativesViewModel.InitiativeString, Is.EqualTo("1's initiative"));
        }

        [Test]
        public void IncrementInitiative()
        {
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(1));

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));
        }

        [Test]
        public void DecrementInitiative()
        {
            setParticipantInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(-1));

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(-1));
        }

        [Test]
        public void GetsNextParticipant()
        {
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            setParticipantInitiativesViewModel.GetNextParticipantToSetInitiative();

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(0));
            Assert.That(setParticipantInitiativesViewModel.InitiativeString, Is.EqualTo("2's initiative"));
        }

        [Test]
        public void CurrentIsNullIfNoMoreToSet()
        {
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            setParticipantInitiativesViewModel.GetNextParticipantToSetInitiative();
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            setParticipantInitiativesViewModel.GetNextParticipantToSetInitiative();

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            participant = participants.First(x => x.Name == "2");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            Assert.That(setParticipantInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}