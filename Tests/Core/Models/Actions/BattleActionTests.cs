using System;
using BattleOrder.Core.Models.Actions;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Models.Actions
{
    [TestFixture]
    public class BattleActionTests
    {
        BattleAction action;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack", 5, 3);
        }

        [Test]
        public void Constructor()
        {
            Assert.That(action.Name, Is.EqualTo("attack"));
            Assert.That(action.PerRound, Is.EqualTo(5));
            Assert.That(action.Speed, Is.EqualTo(3));
            Assert.That(action.Prepped, Is.EqualTo(false));
        }

        [Test]
        public void AlterInfo()
        {
            action.AlterInfo("new name", 1.5, 5);
            Assert.That(action.Name, Is.EqualTo("new name"));
            Assert.That(action.PerRound, Is.EqualTo(1.5));
            Assert.That(action.Speed, Is.EqualTo(5));
        }
        
        [Test]
        public void PreppedDefaultedToFalse()
        {
            Assert.That(action.Prepped, Is.False);
        }

        [Test]
        public void PreppedCanBeSet()
        {
            action = new BattleAction(action.Name, action.PerRound, action.Speed, false);
            Assert.That(action.Prepped, Is.False);

            action = new BattleAction(action.Name, action.PerRound, action.Speed, true);
            Assert.That(action.Prepped, Is.True);
        }

        [Test]
        public void ThisRoundShowsPerRound()
        {
            Assert.That(action.ThisRound, Is.EqualTo(5));
        }

        [Test]
        public void ThisRoundShowsLowerPerRoundIfNotAllUsable()
        {
            action.AlterInfo(action.Name, 1.5, action.Speed);
            action.FinishCurrentPartOfAction();
            action.FinishCurrentPartOfAction();
            Assert.That(action.ThisRound, Is.EqualTo(1));
        }

        [Test]
        public void ThisRoundShowsUpperPerRoundIfAllUsable()
        {
            action.AlterInfo(action.Name, 1.5, action.Speed);
            Assert.That(action.ThisRound, Is.EqualTo(2));
        }

        [Test]
        public void UsedGivesAccurateCount()
        {
            FinishActions(3);
            Assert.That(action.Used, Is.EqualTo(3));
        }

        private void FinishActions(Int32 quantity)
        {
            while (quantity-- > 0)
                action.FinishCurrentPartOfAction();
        }

        [Test]
        public void LeftGivesAccurateCount()
        {
            FinishActions(3);
            Assert.That(action.Left, Is.EqualTo(2));
        }

        [Test]
        public void LeftShowUpperPerRoundIfAllUsable()
        {
            action.AlterInfo(action.Name, 4.5, action.Speed);
            FinishActions(3);
            Assert.That(action.Left, Is.EqualTo(2));
        }

        [Test]
        public void LeftShowLowerPerRoundIfNotAllUsable()
        {
            action.AlterInfo(action.Name, 4.5, action.Speed);
            FinishActions(5);
            action.PrepareForNextRound();
            FinishActions(3);
            Assert.That(action.Left, Is.EqualTo(1));
        }

        [Test]
        public void IsValid()
        {
            action = new BattleAction(String.Empty);
            Assert.That(action.IsValid(), Is.False);

            action.AlterInfo("name", .5, 0);
            Assert.That(action.IsValid(), Is.True);

            action.AlterInfo(String.Empty, .5, 0);
            Assert.That(action.IsValid(), Is.False);

            action.AlterInfo("name", 0, 0);
            Assert.That(action.IsValid(), Is.False);
        }
    }
}