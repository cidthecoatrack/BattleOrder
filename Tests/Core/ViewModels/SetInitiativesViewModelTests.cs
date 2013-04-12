using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;
using Moq;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.ViewModels
{
    [TestFixture]
    public class SetInitiativesViewModelTests
    {
        private ActionParticipant npc;
        private IEnumerable<ActionParticipant> participants;
        private SetInitiativesViewModel setInitiativesViewModel;

        [SetUp]
        public void Setup()
        {
            npc = new ActionParticipant("3", isNpc: true);

            participants = new[] 
            {
                new ActionParticipant("1", isNpc: false),
                new ActionParticipant("2", isNpc: false),
                npc
            };

            var participant = participants.First();
            participant.Initiative = 9;

            var dice = DiceFactory.Create(new Random());

            setInitiativesViewModel = new SetInitiativesViewModel(participants, dice);
        }

        [Test]
        public void CorrectLoad()
        {
            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(0));
            Assert.That(setInitiativesViewModel.InitiativeString, Is.EqualTo("1's initiative"));
        }

        [Test]
        public void NpcInitiativeSet()
        {
            Assert.That(npc.Initiative, Is.InRange<Int32>(1, 10));
        }

        [Test]
        public void DiceSetNpcInitiatives()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.d10(It.IsAny<Int32>(), It.IsAny<Int32>())).Returns(5);

            setInitiativesViewModel = new SetInitiativesViewModel(participants, mockDice.Object);

            mockDice.Verify(d => d.d10(1, 0), Times.Once());
            Assert.That(npc.Initiative, Is.EqualTo(5));
        }

        [Test]
        public void IncrementInitiative()
        {
            setInitiativesViewModel.IncrementCurrentInitiative();
            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(1));

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));
        }

        [Test]
        public void DecrementInitiative()
        {
            setInitiativesViewModel.DecrementCurrentInitiative();
            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(-1));

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(-1));
        }

        [Test]
        public void GetsNextParticipant()
        {
            setInitiativesViewModel.IncrementCurrentInitiative();
            setInitiativesViewModel.GetNextParticipantToSetInitiative();

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            Assert.That(setInitiativesViewModel.CurrentInitiative, Is.EqualTo(0));
            Assert.That(setInitiativesViewModel.InitiativeString, Is.EqualTo("2's initiative"));
        }

        [Test]
        public void AllInitiativesSetIfNoMoreToSet()
        {
            setInitiativesViewModel.IncrementCurrentInitiative();
            setInitiativesViewModel.GetNextParticipantToSetInitiative();
            setInitiativesViewModel.IncrementCurrentInitiative();
            setInitiativesViewModel.GetNextParticipantToSetInitiative();

            var participant = participants.First(x => x.Name == "1");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            participant = participants.First(x => x.Name == "2");
            Assert.That(participant.Initiative, Is.EqualTo(1));

            Assert.That(setInitiativesViewModel.AllInitiativesSet, Is.True);
        }

        [Test]
        public void AllInitiativesSetIfAllNpcs()
        {
            participants = new[] { npc };
            var dice = DiceFactory.Create(new Random());

            setInitiativesViewModel = new SetInitiativesViewModel(participants, dice);

            Assert.That(setInitiativesViewModel.AllInitiativesSet, Is.True);
        }
    }
}