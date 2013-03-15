using BattleOrder.Core;
using BattleOrder.Core.Models.Actions;
using NUnit.Framework;

namespace BattleOrder.Tests
{
    [TestFixture]
    public class PlacementCalculatorTests
    {
        BattleAction action;
        PlacementCalculator calculator;

        [SetUp]
        public void Setup()
        {
            action = new BattleAction("attack", 2, 7);
            calculator = new PlacementCalculator(action);
        }

        [Test]
        public void SetPlacementByInitiative()
        {
            action = new BattleAction("attack", 1, 1);
            calculator = new PlacementCalculator(action);
            Assert.That(calculator.ComputePlacement(10), Is.EqualTo(-9));
        }

        [Test]
        public void SetPlacementByInitiativeForMultiPartAction()
        {
            action.FinishCurrentPartOfAction();
            calculator = new PlacementCalculator(action);
            Assert.That(calculator.ComputePlacement(7), Is.EqualTo(3.5));
        }
    }
}