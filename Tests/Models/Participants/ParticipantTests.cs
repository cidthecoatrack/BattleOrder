using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using NUnit.Framework;

namespace BattleOrder.Tests.Models.Participants
{
    [TestFixture]
    public class ParticipantTests
    {
        private Participant participant;

        [SetUp]
        public void Setup()
        {
            var attacks = new List<BattleAction>();
            for (var i = 0; i < 3; i++)
                attacks.Add(new BattleAction("name " + i, i, i, (i % 2 == 0))); 
            
            participant = new Participant("name");
            participant.AddActions(attacks);
        }

        [Test]
        public void Constructor()
        {
            participant = new Participant("name");
            Assert.That(participant.Name, Is.EqualTo("name"));
            Assert.That(participant.IsNpc, Is.True);
            Assert.That(participant.IsEnemy, Is.True);
            Assert.That(participant.Actions, Is.EqualTo(Enumerable.Empty<Action>()));
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
        public void PassInIEnumerableOfActions()
        {
            var count = participant.Actions.Count();
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void CurrentActionsIsAccurate()
        {
            var count = participant.CurrentActions.Count();
            Assert.That(count, Is.EqualTo(2));

            foreach (var action in participant.CurrentActions)
                Assert.That(action.PerRound % 2, Is.EqualTo(0));
        }

        [Test]
        public void AddAction()
        {
            var attack = new BattleAction("attack name", 1, 2);
            participant.AddAction(attack);
            var hasBeenAdded = participant.Actions.Contains(attack);
            Assert.That(hasBeenAdded, Is.True);
        }

        [Test]
        public void AddActions()
        {
            var attacks = new List<BattleAction>();
            for (var i = 0; i < 2; i++)
                attacks.Add(new BattleAction("new attack " + i, i, i));

            participant.AddActions(attacks);

            foreach (var attack in attacks)
            {
                var hasBeenAdded = participant.Actions.Contains(attack);
                Assert.That(hasBeenAdded, Is.True);
            }
        }

        [Test]
        public void RemoveAction()
        {
            var action = participant.Actions.First(x => x.Name == "name 0");
            participant.RemoveAction(action);
            var hasBeenRemoved = !participant.Actions.Contains(action);
            Assert.That(hasBeenRemoved, Is.True);
        }

        [Test]
        public void ParticipantIsValid()
        {
            participant = new Participant(String.Empty);
            Assert.That(participant.IsValid(), Is.False);

            participant.AlterInfo("name", false, false);
            Assert.That(participant.IsValid(), Is.True);
        }
    }
}