using System;
using System.Collections.Generic;
using BattleOrder.Models.Participants;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.ViewModels
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
        }

        [Test]
        public void DecrementInitiative()
        {
            setParticipantInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(setParticipantInitiativesViewModel.CurrentInitiative, Is.EqualTo(-1));
        }

        [Test]
        public void GetsNextParticipant()
        {
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
            setParticipantInitiativesViewModel.GetNextParticipantToSetInitiative();

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

            Assert.That(setParticipantInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}