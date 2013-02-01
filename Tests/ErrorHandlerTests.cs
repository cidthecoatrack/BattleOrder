using System;
using BattleOrder;
using BattleOrder.Core;
using NUnit.Framework;

namespace BattleOrder.Tests
{
    [TestFixture]
    public class ErrorHandlerTests
    {
        [Test]
        public void GetsCorrectDebugInfo()
        {
            var number = -1;
            var errorString = ErrorHandler.ShowDebugInfo(ErrorHandler.ERROR_TYPES.OUT_OF_RANGE, number);
            var expectedErrorString = "[ERROR: -1 OUT_OF_RANGE - ErrorHandlerTests.GetsCorrectDebugInfo.15]";
            Assert.That(errorString, Is.EqualTo(expectedErrorString));
        }
    }
}