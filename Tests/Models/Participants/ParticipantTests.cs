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
            
            participant = new Participant("name");
            participant.AddAttacks(attacks);
        }

        [Test]
        public void Constructor()
        {
            participant = new Participant("name");
            Assert.That(participant.Name, Is.EqualTo("name"));
            Assert.That(participant.IsNpc, Is.True);
            Assert.That(participant.IsEnemy, Is.True);
            Assert.That(participant.Attacks, Is.EqualTo(Enumerable.Empty<Attack>()));
            Assert.That(participant.Initiative, Is.EqualTo(0));
            Assert.That(participant.Enabled, Is.True);
        }

        [Test]
        public void ManuallySetInitialValues()
        {
            Assert.That(participant.Name, Is.EqualTo("name"));
            Assert.That(participant.IsNpc, Is.True);
            Assert.That(participant.IsEnemy, Is.True);
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
            var count = participant.CurrentAttacks.Count();
            Assert.That(count, Is.EqualTo(2));

            foreach (var attack in participant.CurrentAttacks)
                Assert.That(attack.PerRound % 2, Is.EqualTo(0));
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
        public void AddAttacks()
        {
            var attacks = new List<Attack>();
            for (var i = 0; i < 2; i++)
                attacks.Add(new Attack("new attack " + i, i, i));

            participant.AddAttacks(attacks);

            foreach (var attack in attacks)
            {
                var hasBeenAdded = participant.Attacks.Contains(attack);
                Assert.That(hasBeenAdded, Is.True);
            }
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
            participant = new Participant(String.Empty);
            Assert.That(participant.IsValid(), Is.False);

            participant.AlterInfo("name", false, false);
            var attack = new Attack(String.Empty);
            participant.AddAttack(attack);
            Assert.That(participant.IsValid(), Is.True);

            participant.AlterInfo(String.Empty, false, false);
            Assert.That(participant.IsValid(), Is.False);

            participant.AlterInfo("name", false, false);
            participant.RemoveAttack(attack);
            Assert.That(participant.IsValid(), Is.False);
        }
    }
}