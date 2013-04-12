using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Battle_Order;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Repository.Gateways;
using NUnit.Framework;

namespace BattleOrder.Tests.Repository.Gateways
{
    [TestFixture]
    public class FileGatewayTests
    {
        private const String saveDirectory = "directory";

        private FileGateway gateway;
        private IEnumerable<ActionParticipant> party;

        [SetUp]
        public void Setup()
        {
            gateway = new FileGateway(saveDirectory);
            party = CreateDatabase();

            gateway.SaveFile("newParty", party);
        }

        [Test]
        public void LoadFile()
        {
            var loadedParty = gateway.LoadFile("newParty");
            Assert.That(loadedParty.Count(), Is.EqualTo(party.Count()));
        }

        [Test]
        public void SaveFile()
        {
            var path = Path.Combine(saveDirectory, "newParty.xml");
            Assert.That(File.Exists(path), Is.True);
        }

        private IEnumerable<ActionParticipant> CreateDatabase()
        {
            var db = new List<ActionParticipant>();
            db.Add(new ActionParticipant("monster 1"));
            db.Add(new ActionParticipant("monster 2"));
            db.Add(new ActionParticipant("player 1"));

            var withActions = new ActionParticipant("Has actions");
            withActions.AddAction(new BattleAction("action"));
            db.Add(withActions);

            return db;
        }
    }
}