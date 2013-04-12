using BattleOrder.Core.Models.Actions;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.Models.Actions
{
    [TestFixture]
    public class QueueableActionTests
    {
        QueueableAction queueableAction;
        
        [SetUp]
        public void Setup()
        {
            var action = new BattleAction("bow", 2, 7);
            action.FinishCurrentPartOfAction();
            queueableAction = new QueueableAction("attacker", action, 7);
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
            var action = new BattleAction("attack", 1, 1);
            queueableAction = new QueueableAction("attacker", action, 7);
            Assert.That(queueableAction.Placement, Is.EqualTo(-6));
            Assert.That(queueableAction.Description, Is.EqualTo("attacker's attack"));
        }
    }
}