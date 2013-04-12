using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Battle_Order;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Repository.Entities;

namespace BattleOrder.Repository.Gateways
{
    public class FileGateway
    {
        public String SaveDirectory { get; private set; }

        public FileGateway(String saveDirectory)
        {
            this.SaveDirectory = saveDirectory;

            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
        }

        public IEnumerable<ActionParticipant> LoadFile(String fileName)
        {
            var path = Path.Combine(SaveDirectory, fileName);
            var pathWithExtension = path + RepositoryConstants.XML_EXTENSION;

            if (File.Exists(pathWithExtension))
                return LoadXmlFile(pathWithExtension);

            return LoadBinaryFile(path);
        }

        private IEnumerable<ActionParticipant> LoadXmlFile(String path)
        {
            var deserializer = new XmlSerializer(typeof(List<ParticipantEntity>));

            List<ParticipantEntity> savedDb;

            using (var reader = new StreamReader(path))
                savedDb = (List<ParticipantEntity>)deserializer.Deserialize(reader);

            var db = new List<ActionParticipant>();

            foreach (var entry in savedDb)
                db.Add(entry.GetActionParticipant());

            return db;
        }

        private IEnumerable<ActionParticipant> LoadBinaryFile(String path)
        {
            var db = new List<ActionParticipant>();
            var binary = new BinaryFormatter();

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

        public void SaveFile(String fileName, IEnumerable<ActionParticipant> db)
        {
            var saveableDb = new List<ParticipantEntity>();

            foreach (var participant in db)
                saveableDb.Add(new ParticipantEntity(participant));

            fileName += RepositoryConstants.XML_EXTENSION;
            var path = Path.Combine(SaveDirectory, fileName);
            var serializer = new XmlSerializer(typeof(List<ParticipantEntity>));

            using (var writer = new StreamWriter(path))
                serializer.Serialize(writer, saveableDb);
        }
    }
}