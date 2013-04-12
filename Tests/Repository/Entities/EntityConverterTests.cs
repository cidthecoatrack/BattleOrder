using System;
using BattleOrder.Repository.Entities;
using NUnit.Framework;

namespace BattleOrder.Tests.Repository.Entities
{
    [TestFixture]
    public class EntityConverterTests
    {
        private EntityConverter converter;

        [SetUp]
        public void Setup()
        {
            converter = new EntityConverter();
        }

        [Test]
        public void ConvertAction()
        {
            Assert.Fail();
        }

        [Test]
        public void ConvertParticipant()
        {
            Assert.Fail();
        }
    }
}