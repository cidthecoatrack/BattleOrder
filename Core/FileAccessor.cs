using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using ProtoBuf;

namespace BattleOrder.Core
{
    public class FileAccessor
    {
        private const String monsterDbFileName = "MonsterDatabase";

        public String SaveDirectory { get; private set; }
        private String partyFileName;
        
        public FileAccessor(String saveDirectory)
        {
            this.SaveDirectory = saveDirectory;

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

        public IEnumerable<ActionParticipant> LoadMonsterDatabase()
        {
            var path = Path.Combine(SaveDirectory, monsterDbFileName);

            if (!File.Exists(path))
                File.Create(path);

            return LoadFile(monsterDbFileName);
        }

        public IEnumerable<ActionParticipant> LoadParty(String partyFileName)
        {
            this.partyFileName = partyFileName;
            return LoadFile(partyFileName);
        }

        private IEnumerable<ActionParticipant> LoadFile(String fileName)
        {
            var db = new List<ActionParticipant>();
            var binary = new BinaryFormatter();

            var path = Path.Combine(SaveDirectory, fileName);
            using (var input = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                while (true)
                {
                    Object deserialized;

                    try
                    {
                        deserialized = binary.Deserialize(input);
                    }
                    catch (SerializationException ex)
                    {
                        if (ex.Message.Contains("End of Stream"))
                            break;

                        throw;
                    }

                    var participant = GetActionParticipant(deserialized);

                    db.Add(participant);
                }
            }

            return db;
        }

        private ActionParticipant GetActionParticipant(Object rawObject)
        {
            try
            {
                return (ActionParticipant)rawObject;
            }
            catch (InvalidCastException)
            {
                var oldParticipant = (Participant)rawObject;
                var participant = new ActionParticipant(oldParticipant.Name, oldParticipant.IsEnemy, oldParticipant.IsNpc);
                participant.Enabled = oldParticipant.Enabled;

                var actions = ConvertAttacks(oldParticipant.Attacks);
                participant.AddActions(actions);

                return participant;
            }
        }

        private IEnumerable<BattleAction> ConvertAttacks(IEnumerable<Attack> attacks)
        {
            var actions = new List<BattleAction>();

            foreach (var attack in attacks)
            {
                var action = new BattleAction(attack.Name, attack.PerRound, attack.Speed, attack.Prepped);
                actions.Add(action);
            }

            return actions;
        }

        public void SaveMonsterDatabase(IEnumerable<ActionParticipant> dbToSave)
        {
            SaveFile(monsterDbFileName, dbToSave);
        }

        public void SaveParty(IEnumerable<ActionParticipant> partyToSave, String newPartyFileName = "")
        {
            if (!String.IsNullOrEmpty(newPartyFileName)) 
                partyFileName = newPartyFileName;

            SaveFile(partyFileName, partyToSave);
        }

        private void SaveFile(String fileName, IEnumerable<ActionParticipant> db)
        {
            var binary = new BinaryFormatter();
            var path = Path.Combine(SaveDirectory, fileName);

            using (var output = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                foreach (var entry in db)
                    binary.Serialize(output, entry);
        }
    }
}