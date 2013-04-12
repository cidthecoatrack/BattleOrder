using BattleOrder.Core;
using NUnit.Framework;

namespace BattleOrder.Tests.Core
{
    [TestFixture]
    public class ErrorHandlerTests
    {
        [Test]
        public void GetsCorrectDebugInfo()
        {
            var number = -1;
            var errorString = ErrorHandler.ShowDebugInfo(ErrorHandler.ERROR_TYPES.OUT_OF_RANGE, number);
            var expectedErrorString = "[ERROR: -1 OUT_OF_RANGE - ErrorHandlerTests.GetsCorrectDebugInfo.13]";
            Assert.That(errorString, Is.EqualTo(expectedErrorString));
        }
    }
}