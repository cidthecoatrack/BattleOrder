using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Battle_Order;
using BattleOrder.Core;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using NUnit.Framework;

namespace BattleOrder.Tests
{
    [TestFixture]
    public class FileAccessorTests
    {
        private const String saveDirectory = "directory";
        
        public FileAccessorTests()
        { 
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            return typeof(Attack).Assembly;
        }

        [Test]
        public void GetsCorrectDirectoryFromSaveDirectoryFile()
        {
            File.WriteAllText("SaveDirectory", saveDirectory);
            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo(saveDirectory));
        }

        [Test]
        public void MakesSaveDirectory()
        {
            FileAccessor.MakeSaveDirectoryFile(saveDirectory);
            Assert.That(File.Exists("SaveDirectory"), Is.True);
            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo(saveDirectory));
        }

        [Test]
        public void LoadsMonsterDatabase()
        {
            var monsterDb = CreateDatabase();
            SaveDatabaseToNewFile(monsterDb);

            var accessor = new FileAccessor(saveDirectory);

            monsterDb = accessor.LoadMonsterDatabase();
            Assert.That(monsterDb.Count(), Is.EqualTo(4));
        }

        [Test]
        public void LoadsParty()
        {
            var party = CreateDatabase();
            SaveDatabaseToNewFile(party);

            var accessor = new FileAccessor(saveDirectory);

            party = accessor.LoadParty("MonsterDatabase");
            Assert.That(party.Count(), Is.EqualTo(4));
        }

        [Test]
        public void SavesMonsterDatabase()
        {
            var monsterDb = CreateDatabase();
            var accessor = new FileAccessor(saveDirectory);

            accessor.SaveMonsterDatabase(monsterDb);
            var path = Path.Combine(saveDirectory, "MonsterDatabase"); 
            Assert.That(File.Exists(path), Is.True);

            monsterDb = accessor.LoadMonsterDatabase();
            Assert.That(monsterDb.Count(), Is.EqualTo(4));
        }

        [Test]
        public void SavesParty()
        {
            var party = CreateDatabase();
            var accessor = new FileAccessor(saveDirectory);
            accessor.SaveParty(party, "newParty");

            var path = Path.Combine(saveDirectory, "newParty");
            Assert.That(File.Exists(path), Is.True);

            var loadedParty = accessor.LoadParty("newParty");
            Assert.That(party.Count(), Is.EqualTo(loadedParty.Count()));
        }

        [Test]
        public void LoadsOldMonsterDatabase()
        {
            var accessor = new FileAccessor(@"Files\Old");
            var newMonsterDb = accessor.LoadMonsterDatabase();

            Assert.That(newMonsterDb.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LoadsOldParty()
        {
            var accessor = new FileAccessor(@"Files\Old");
            var newParty = accessor.LoadParty("OldParty");

            Assert.That(newParty.Count(), Is.EqualTo(2));
        }

        private void SaveDatabaseToNewFile(IEnumerable<ActionParticipant> monsterDb)
        {
            var binary = new BinaryFormatter();
            var path = Path.Combine(saveDirectory, "MonsterDatabase");

            using (var output = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                foreach (var databaseEntry in monsterDb)
                    binary.Serialize(output, databaseEntry);
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