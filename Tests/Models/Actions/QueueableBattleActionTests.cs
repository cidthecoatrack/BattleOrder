using BattleOrder.Core.Models.Actions;
using NUnit.Framework;

namespace BattleOrder.Tests.Models.Actions
{
    [TestFixture]
    public class QueueableBattleActionTests
    {
        QueueableBattleAction queueableAction;
        
        [SetUp]
        public void Setup()
        {
            var attack = new BattleAction("bow", 2, 7);
            attack.FinishCurrentPartOfAction();
            queueableAction = new QueueableBattleAction("attacker", attack, 7);
        }
        
        [Test]
        public void ComputesPlacement()
        {
            Assert.That(queueableAction.Placement, Is.EqualTo(3.5));
        }

        [Test]
        public void HasCorrectDescriptionForMultiPartAction()
        {
            Assert.That(queueableAction.Description, Is.EqualTo("attacker's second bow"));
        }

        [Test]
        public void HasCorrectDescription()
        {
            var attack = new BattleAction("attack", 1, 1);
            queueableAction = new QueueableBattleAction("attacker", attack, 7);
            Assert.That(queueableAction.Placement, Is.EqualTo(-6));
            Assert.That(queueableAction.Description, Is.EqualTo("attacker's attack"));
        }
    }
}