using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BattleOrder.Models;
using BattleOrder.Models.Participants;
using NUnit.Framework;

namespace BattleOrder.Tests
{
    [TestFixture]
    public class FileAccessorTests
    {
        private const String saveDirectory = "directory";
        
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
            SaveMonsterDatabaseToNewFile(monsterDb);

            var accessor = new FileAccessor(saveDirectory);

            monsterDb = accessor.LoadMonsterDatabase();
            Assert.That(monsterDb.Count(), Is.EqualTo(3));
        }

        [Test]
        public void LoadsParty()
        {
            var party = CreateDatabase();
            SaveMonsterDatabaseToNewFile(party);

            var accessor = new FileAccessor(saveDirectory);

            party = accessor.LoadParty("MonsterDatabase");
            Assert.That(party.Count(), Is.EqualTo(3));
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
            Assert.That(monsterDb.Count(), Is.EqualTo(3));
        }

        [Test]
        public void SavesParty()
        {
            var party = CreateDatabase();
            var accessor = new FileAccessor(saveDirectory);

            accessor.SaveParty(party, "newParty");
            var path = Path.Combine(saveDirectory, "newParty");
            Assert.That(File.Exists(path), Is.True);

            party = accessor.LoadParty("newParty");
            Assert.That(party.Count(), Is.EqualTo(3));
        }

        private void SaveMonsterDatabaseToNewFile(IEnumerable<Participant> monsterDb)
        {
            var binary = new BinaryFormatter();
            var path = Path.Combine(saveDirectory, "MonsterDatabase");

            using (var output = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                foreach (var databaseEntry in monsterDb)
                    binary.Serialize(output, databaseEntry);
        }

        private IEnumerable<Participant> CreateDatabase()
        {
            var monsterDb = new List<Participant>();
            monsterDb.Add(new Participant("monster 1"));
            monsterDb.Add(new Participant("monster 2"));
            monsterDb.Add(new Participant("player 1"));

            return monsterDb;
        }
    }
}