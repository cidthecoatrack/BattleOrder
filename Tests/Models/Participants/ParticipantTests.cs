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
        ActionParticipant participant;

        [SetUp]
        public void Setup()
        {
            var actions = new List<BattleAction>();
            for (var i = 0; i < 3; i++)
                actions.Add(new BattleAction("name " + i, i, i, (i % 2 == 0)));

            participant = new ActionParticipant("name");
            participant.AddActions(actions);
        }

        [Test]
        public void Constructor()
        {
            participant = new ActionParticipant("name");
            Assert.That(participant.Name, Is.EqualTo("name"));
            Assert.That(participant.IsNpc, Is.True);
            Assert.That(participant.IsEnemy, Is.True);
            Assert.That(participant.Actions, Is.EqualTo(Enumerable.Empty<BattleAction>()));
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
        public void AddAction()
        {
            var action = new BattleAction("attack name", 1, 2);
            participant.AddAction(action);
            var hasBeenAdded = participant.Actions.Contains(action);
            Assert.That(hasBeenAdded, Is.True);
        }

        [Test]
        public void AddActions()
        {
            var actions = new List<BattleAction>();
            for (var i = 0; i < 2; i++)
                actions.Add(new BattleAction("new attack " + i, i, i));

            participant.AddActions(actions);

            foreach (var action in actions)
            {
                var hasBeenAdded = participant.Actions.Contains(action);
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
        public void IsValid()
        {
            participant = new ActionParticipant(String.Empty);
            Assert.That(participant.IsValid(), Is.False, "No name");

            participant.AlterInfo("name", false, false);
            Assert.That(participant.IsValid(), Is.True, "Has name");
        }
    }
}