using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOrder.Repository.Gateways;
using NUnit.Framework;

namespace BattleOrder.Tests.Repository.Gateways
{
    [TestFixture]
    public class SaveDirectoryGatewayTests
    {
        private const String saveDirectory = "directory";

        private SaveDirectoryGateway gateway;

        [SetUp]
        public void Setup()
        {
            gateway = new SaveDirectoryGateway();
        }

        [Test]
        public void GetCorrectDirectoryFromSaveDirectoryFile()
        {
            File.WriteAllText("SaveDirectory", saveDirectory);
            var directory = gateway.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo(saveDirectory));
        }

        [Test]
        public void MakeSaveDirectory()
        {
            gateway.MakeSaveDirectoryFile(saveDirectory);
            Assert.That(File.Exists("SaveDirectory"), Is.True);
            var directory = gateway.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo(saveDirectory));
        }
    }
}