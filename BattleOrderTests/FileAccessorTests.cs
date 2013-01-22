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
        [Test]
        public void GetsCorrectDirectoryFromSaveDirectoryFile()
        {
            File.WriteAllText("SaveDirectory", "directory");
            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo("directory"));
        }

        [Test]
        public void MakesSaveDirectory()
        {
            FileAccessor.MakeSaveDirectoryFile("directory");
            Assert.That(File.Exists("SaveDirectory"), Is.True);
            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo("directory"));
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

        private void SaveMonsterDatabaseToNewFile(IEnumerable<Participant> monsterDb)
        {
            File.Create("MonsterDatabase");
            var binary = new BinaryFormatter();

            using (var output = new FileStream("MonsterDatabase", FileMode.OpenOrCreate, FileAccess.Write))
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