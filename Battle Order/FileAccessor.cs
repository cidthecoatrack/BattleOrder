using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BattleOrder.Models;

namespace BattleOrder
{
    public class FileAccessor
    {
        public String SaveDirectory { get; private set; }
        private String partyFileName;
        private const String monsterDbFileName = "MonsterDatabase";
        private String monsterDbLocation { get { return Path.Combine(SaveDirectory, monsterDbFileName); } }
        
        public FileAccessor(String saveDirectory)
        {
            this.SaveDirectory = saveDirectory;
            VerifyExistenceOfSaveDirectory();
        }

        private void VerifyExistenceOfSaveDirectory()
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
        }

        public static String GetSaveDirectoryFromWorkingDirectory()
        {
            return File.ReadAllText("SaveDirectory");
        }

        public static void MakeSaveDirectoryFile(String saveDirectory)
        {
            File.WriteAllText("SaveDirectory", saveDirectory);
        }

        public IEnumerable<Participant> LoadMonsterDatabase()
        {
            if (!File.Exists(monsterDbLocation))
                File.Create(monsterDbLocation);

            var monsterDb = new List<Participant>();
            var binary = new BinaryFormatter();

            using (var input = new FileStream(monsterDbLocation, FileMode.Open, FileAccess.Read))
            {
                while (true)
                {
                    try
                    {
                        var newParticipant = (Participant)binary.Deserialize(input);
                        monsterDb.Add(newParticipant);
                    }
                    catch (SerializationException)
                    {
                        break;
                    }
                }
            }

            return monsterDb;
        }

        public IEnumerable<Participant> LoadParty(String partyFileName)
        {
            var party = new List<Participant>();
            var binary = new BinaryFormatter();
            var path = Path.Combine(SaveDirectory, partyFileName);

            using (var input = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                while (true)
                {
                    try
                    {
                        var newParticipant = (Participant)binary.Deserialize(input);
                        party.Add(newParticipant);
                    }
                    catch (SerializationException)
                    {
                        break;
                    }
                }
            }

            return party;
        }

        public void SaveMonsterDatabase(IEnumerable<Participant> dbToSave)
        {
            var binary = new BinaryFormatter();
            
            using (var output = new FileStream(monsterDbLocation, FileMode.OpenOrCreate, FileAccess.Write))
                foreach (var databaseEntry in dbToSave)
                    binary.Serialize(output, databaseEntry);
        }

        public void SaveParty(IEnumerable<Participant> partyToSave)
        {
            var binary = new BinaryFormatter();
            var path = Path.Combine(SaveDirectory, partyFileName);

            using (var output = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                foreach (var goodguy in partyToSave)
                    binary.Serialize(output, goodguy);
        }

        public void SaveParty(IEnumerable<Participant> partyToSave, String newPartyFileName)
        {
            if (!String.IsNullOrEmpty(newPartyFileName)) 
                partyFileName = newPartyFileName;

            SaveParty(partyToSave);
        }
    }
}