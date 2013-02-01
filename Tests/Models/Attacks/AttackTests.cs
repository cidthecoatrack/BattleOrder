using System;
using BattleOrder.Core.Models.Attacks;
using NUnit.Framework;

namespace BattleOrder.Tests.Models.Attacks
{
    [TestFixture]
    public class AttackTests
    {
        Attack attack;

        [SetUp]
        public void Setup()
        {
            attack = new Attack("attack", 5, 3);
        }

        [Test]
        public void DefaultAttackConstructor()
        {
            attack = new Attack();
            Assert.That(attack.Name, Is.EqualTo(String.Empty));
            Assert.That(attack.PerRound, Is.EqualTo(0));
            Assert.That(attack.Speed, Is.EqualTo(0));
            Assert.That(attack.Prepped, Is.EqualTo(false));
        }

        [Test]
        public void AlterAttackInfo()
        {
            attack.AlterInfo("new name", 1.5, 5);
            Assert.That(attack.Name, Is.EqualTo("new name"));
            Assert.That(attack.PerRound, Is.EqualTo(1.5));
            Assert.That(attack.Speed, Is.EqualTo(5));
        }
        
        [Test]
        public void PreppedDefaultedToFalse()
        {
            Assert.That(attack.Prepped, Is.False);
        }

        [Test]
        public void PreppedCanBeSet()
        {
            attack = new Attack(attack.Name, attack.PerRound, attack.Speed, false);
            Assert.That(attack.Prepped, Is.False);

            attack = new Attack(attack.Name, attack.PerRound, attack.Speed, true);
            Assert.That(attack.Prepped, Is.True);
        }

        [Test]
        public void ThisRoundShowsPerRound()
        {
            Assert.That(attack.ThisRound, Is.EqualTo(5));
        }

        [Test]
        public void ThisRoundShowsLowerPerRoundIfNotAllUsable()
        {
            attack.AlterInfo(attack.Name, 1.5, attack.Speed);
            attack.FinishCurrentPartOfAttack();
            attack.FinishCurrentPartOfAttack();
            Assert.That(attack.ThisRound, Is.EqualTo(1));
        }

        [Test]
        public void ThisRoundShowsUpperPerRoundIfAllUsable()
        {
            attack.AlterInfo(attack.Name, 1.5, attack.Speed);
            Assert.That(attack.ThisRound, Is.EqualTo(2));
        }

        [Test]
        public void AttacksUsedGivesAccurateCount()
        {
            FinishAttacks(3);
            Assert.That(attack.AttacksUsed, Is.EqualTo(3));
        }

        private void FinishAttacks(Int32 quantity)
        {
            while (quantity-- > 0)
                attack.FinishCurrentPartOfAttack();
        }

        [Test]
        public void AttacksLeftGivesAccurateCount()
        {
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(2));
        }

        [Test]
        public void AttacksLeftShowUpperPerRoundIfAllUsable()
        {
            attack.AlterInfo(attack.Name, 4.5, attack.Speed);
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(2));
        }

        [Test]
        public void AttacksLeftShowLowerPerRoundIfNotAllUsable()
        {
            attack.AlterInfo(attack.Name, 4.5, attack.Speed);
            FinishAttacks(5);
            attack.PrepareForNextRound();
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(1));
        }
    }
}