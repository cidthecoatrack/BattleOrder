using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using NUnit.Framework;

namespace BattleOrder.Tests.Models.Participants
{
    [TestFixture]
    public class ParticipantTests
    {
        Participant participant;

        [SetUp]
        public void Setup()
        {
            var attacks = new List<Attack>();
            for (var i = 0; i < 3; i++)
                attacks.Add(new Attack("name " + i, i, i, (i % 2 == 0))); 
            
            participant = new Participant("name", attacks);
        }

        [Test]
        public void DefaultParticipantConstructor()
        {
            participant = new Participant();
            Assert.That(participant.Name, Is.EqualTo(String.Empty));
            Assert.That(participant.IsNpc, Is.EqualTo(false));
            Assert.That(participant.Attacks, Is.EqualTo(Enumerable.Empty<Attack>()));
            Assert.That(participant.Initiative, Is.EqualTo(0));
        }
        
        [Test]
        public void IsNpcDefaultsToTrue()
        {
            Assert.That(participant.IsNpc, Is.True);
        }

        [Test]
        public void ManuallySetIsNpc()
        {
            participant = new Participant("name", true);
            Assert.That(participant.IsNpc, Is.True);

            participant = new Participant("name", false);
            Assert.That(participant.IsNpc, Is.False);
        }

        [Test]
        public void NameCorrectlyPassedIn()
        {
            Assert.That(participant.Name, Is.EqualTo("name"));
        }

        [Test]
        public void PassInIEnumerableOfAttacks()
        {
            var count = participant.Attacks.Count();
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void CurrentAttacksIsAccurate()
        {
            var attacks = new List<Attack>();
            for (var i = 0; i < 3; i++)
                attacks.Add(new Attack("name " + i, i, i, (i % 2 == 0)));

            participant = new Participant("name", attacks); 
            var count = participant.CurrentAttacks.Count();
            Assert.That(count, Is.EqualTo(2));

            foreach (var attack in participant.CurrentAttacks)
                Assert.That(attack.PerRound % 2, Is.EqualTo(0));
        }

        [Test]
        public void AddAttackWhenConstructingParticipant()
        {
            var attack = new Attack("attack name", 1, 2);
            participant = new Participant("name", attack);

            Assert.That(participant.Attacks.Count(), Is.EqualTo(1));
            var passedAttack = participant.Attacks.First();
            Assert.That(passedAttack.Name, Is.EqualTo("attack name"));
        }

        [Test]
        public void CurrentAttackStringIsCorrect()
        {
            var currentAttacks = participant.CurrentAttacksToString();
            var expectedString = "name 0 and name 2";
            Assert.That(currentAttacks, Is.EqualTo(expectedString));
        }

        [Test]
        public void AddAttack()
        {
            var attack = new Attack("attack name", 1, 2);
            participant.AddAttack(attack);
            var hasBeenAdded = participant.Attacks.Contains(attack);
            Assert.That(hasBeenAdded, Is.True);
        }

        [Test]
        public void RemoveAttack()
        {
            var attack = participant.Attacks.First(x => x.Name == "name 0");
            participant.RemoveAttack(attack);
            var hasBeenRemoved = !participant.Attacks.Contains(attack);
            Assert.That(hasBeenRemoved, Is.True);
        }

        [Test]
        public void ParticipantIsValid()
        {
            participant = new Participant();
            Assert.That(participant.IsValid(), Is.False);

            participant.AlterInfo("name", false);
            var attack = new Attack();
            participant.AddAttack(attack);
            Assert.That(participant.IsValid(), Is.True);

            participant.AlterInfo(String.Empty, false);
            Assert.That(participant.IsValid(), Is.False);

            participant.AlterInfo("name", false);
            participant.RemoveAttack(attack);
            Assert.That(participant.IsValid(), Is.False);
        }
    }
}