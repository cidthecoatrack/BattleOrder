using System;
using System.Collections.Generic;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Repository.Gateways
{
    public class PartyGateway : FileGateway
    {
        private String partyFileName;

        public PartyGateway(String saveDirectory)
            : base(saveDirectory) { }

        public IEnumerable<ActionParticipant> LoadParty(String partyFileName)
        {
            this.partyFileName = partyFileName;
            return LoadFile(partyFileName);
        }

        public void SaveParty(IEnumerable<ActionParticipant> partyToSave, String newPartyFileName = "")
        {
            if (!String.IsNullOrEmpty(newPartyFileName))
                partyFileName = newPartyFileName;

            SaveFile(partyFileName, partyToSave);
        }
    }
}