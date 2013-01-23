using BattleOrder.Attacks;
using NUnit.Framework;

namespace BattleOrderTests
{
    [TestFixture]
    public class QueueableAttackTests
    {
        QueueableAttack queueableAttack;
        
        [SetUp]
        public void Setup()
        {
            var attack = new Attack("bow", 2, 7);
            attack.FinishCurrentPartOfAttack();
            queueableAttack = new QueueableAttack("name", attack, 7);
        }
        
        [Test]
        public void ComputesPlacement()
        {
            Assert.That(queueableAttack.Placement, Is.EqualTo(3.5));
        }

        [Test]
        public void HasCorrectDescriptionForMultiPartAttack()
        {
            Assert.That(queueableAttack.Description, Is.EqualTo("name's second bow"));
        }

        [Test]
        public void HasCorrectDescription()
        {
            var attack = new Attack("attack", 1, 1);
            queueableAttack = new QueueableAttack("name", attack, 7);
            Assert.That(queueableAttack.Placement, Is.EqualTo(-6));
            Assert.That(queueableAttack.Description, Is.EqualTo("name's attack"));
        }
    }
}