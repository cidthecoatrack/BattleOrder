using System;
using BattleOrder;
using NUnit.Framework;

namespace BattleOrderTests
{
    [TestFixture]
    public class ErrorHandlerTests
    {
        [Test]
        public void GetsCorrectDebugInfo()
        {
            var number = -1;
            var errorString = ErrorHandler.ShowDebugInfo(ErrorHandler.ERROR_TYPES.OUT_OF_RANGE, number);
            var expectedErrorString = String.Format("[ERROR: {0} OUT_OF_RANGE - ErrorHandlerTests.GetsCorrectDebugInfo.14]", number);
            Assert.That(errorString, Is.EqualTo(expectedErrorString));
        }
    }
}