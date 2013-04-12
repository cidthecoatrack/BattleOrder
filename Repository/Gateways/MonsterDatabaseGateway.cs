using System;
using System.Collections.Generic;
using System.IO;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Repository.Gateways
{
    public class MonsterDatabaseGateway : FileGateway
    {
        private const String MONSTER_DATABASE_FILENAME = "MonsterDatabase";

        public MonsterDatabaseGateway(String saveDirectory)
            : base(saveDirectory) { }

        public IEnumerable<ActionParticipant> LoadMonsterDatabase()
        {
            var path = Path.Combine(SaveDirectory, MONSTER_DATABASE_FILENAME);
            var pathWithExtension = path + RepositoryConstants.XML_EXTENSION;

            if (!File.Exists(pathWithExtension))
                File.Create(pathWithExtension);

            return LoadFile(MONSTER_DATABASE_FILENAME);
        }

        public void SaveMonsterDatabase(IEnumerable<ActionParticipant> dbToSave)
        {
            SaveFile(MONSTER_DATABASE_FILENAME, dbToSave);
        }
    }
}