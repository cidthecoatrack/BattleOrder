using System;
using BattleOrder.Models.Attacks;
using NUnit.Framework;

namespace BattleOrderTests.Models.Attacks
{
    [TestFixture]
    public class AttackTests
    {
        Attack attack;

        [Test]
        public void PreppedDefaultedToTrue()
        {
            attack = new Attack("attack", 1, 1);
            Assert.That(attack.Prepped, Is.True);
        }

        [Test]
        public void PreppedSetToFalse()
        {
            attack = new Attack("attack", 1, 1, false);
            Assert.That(attack.Prepped, Is.False);
        }

        [Test]
        public void ThisRoundShowsPerRound()
        {
            attack = new Attack("attack", 1, 2);
            Assert.That(attack.ThisRound, Is.EqualTo(1));
        }

        [Test]
        public void ThisRoundShowsLowerPerRoundIfNotAllUsable()
        {
            attack = new Attack("attack", 1.5, 3);
            attack.FinishCurrentPartOfAttack();
            attack.FinishCurrentPartOfAttack();
            Assert.That(attack.ThisRound, Is.EqualTo(1));
        }

        [Test]
        public void ThisRoundShowsUpperPerRoundIfAllUsable()
        {
            attack = new Attack("attack", 1.5, 3);
            Assert.That(attack.ThisRound, Is.EqualTo(2));
        }

        [Test]
        public void AttacksUsedGivesAccurateCount()
        {
            attack = new Attack("attack", 5, 1);
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
            attack = new Attack("attack", 5, 1);
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(2));
        }

        [Test]
        public void AttacksLeftShowUpperPerRoundIfAllUsable()
        {
            attack = new Attack("attack", 4.5, 1);
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(2));
        }

        [Test]
        public void AttacksLeftShowLowerPerRoundIfNotAllUsable()
        {
            attack = new Attack("attack", 4.5, 1);
            FinishAttacks(5);
            attack.ResetPartial();
            FinishAttacks(3);
            Assert.That(attack.AttacksLeft, Is.EqualTo(1));
        }
    }
}