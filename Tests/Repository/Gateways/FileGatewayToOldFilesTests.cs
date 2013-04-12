using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Battle_Order;
using BattleOrder.Repository.Gateways;
using NUnit.Framework;

namespace BattleOrder.Tests.Repository.Gateways
{
    [TestFixture]
    public class FileGatewayToOldFilesTests
    {
        private const String saveDirectory = @"Files\Old";

        private FileGateway gateway;

        public FileGatewayToOldFilesTests()
        { 
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            return typeof(Attack).Assembly;
        }

        [SetUp]
        public void Setup()
        {
            gateway = new FileGateway(saveDirectory);
        }

        [Test]
        public void LoadOldFile()
        {
            var newParty = gateway.LoadFile("OldParty");
            Assert.That(newParty.Count(), Is.EqualTo(2));
        }
    }
}