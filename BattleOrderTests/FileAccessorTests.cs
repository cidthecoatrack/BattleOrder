using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Moq;
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
            var monsterDb = CreateMonsterDatabase();
            SaveMonsterDatabaseToNewFile(monsterDb);

            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            var accessor = new FileAccessor(directory);

            monsterDb = accessor.LoadMonsterDatabase();
            Assert.That(monsterDb.Count(), Is.EqualTo(3));
        }

        [Test]
        public void LoadsParty()
        {
            var party = CreateMonsterDatabase();
            SaveMonsterDatabaseToNewFile(party);

            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            var accessor = new FileAccessor(directory);

            party = accessor.LoadParty("MonsterDatabase");
            Assert.That(party.Count(), Is.EqualTo(3));
        }

        [Test]
        public void SavesMonsterDatabase()
        {
            var monsterDb = CreateMonsterDatabase();

            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            var accessor = new FileAccessor(directory);

            accessor.SaveMonsterDatabase(monsterDb);
            monsterDb = accessor.LoadMonsterDatabase();
            Assert.That(monsterDb.Count(), Is.EqualTo(3));
        }

        [Test]
        public void SavesParty()
        {
            var party = CreateMonsterDatabase();

            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            var accessor = new FileAccessor(directory);

            accessor.SaveParty(party, "newParty");
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

        private IEnumerable<Participant> CreateMonsterDatabase()
        {
            var monsterDb = new List<Participant>();
            monsterDb.Add(new Participant("monster 1"));
            monsterDb.Add(new Participant("monster 2"));
            monsterDb.Add(new Participant("monster 3"));

            return monsterDb;
        }
    }
}