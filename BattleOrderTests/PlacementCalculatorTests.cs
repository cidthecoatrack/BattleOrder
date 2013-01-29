using BattleOrder;
using BattleOrder.Models.Attacks;
using NUnit.Framework;

namespace BattleOrderTests
{
    [TestFixture]
    public class PlacementCalculatorTests
    {
        Attack attack;
        PlacementCalculator calculator;

        [SetUp]
        public void Setup()
        {
            attack = new Attack("attack", 2, 7);
            calculator = new PlacementCalculator(attack);
        }

        [Test]
        public void SetPlacementByInitiative()
        {
            attack = new Attack("attack", 1, 1);
            calculator = new PlacementCalculator(attack);
            Assert.That(calculator.ComputePlacement(10), Is.EqualTo(-9));
        }

        [Test]
        public void SetPlacementByInitiativeForMultiPartAttack()
        {
            attack.FinishCurrentPartOfAttack();
            calculator = new PlacementCalculator(attack);
            Assert.That(calculator.ComputePlacement(7), Is.EqualTo(3.5));
        }
    }
}