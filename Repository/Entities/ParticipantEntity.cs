using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Repository.Entities
{
    public class ParticipantEntity
    {
        public String Name { get; set; }
        public Boolean IsNpc { get; set; }
        public Boolean IsEnemy { get; set; }
        public Boolean Enabled { get; set; }
        public List<ActionEntity> Actions { get; set; }

        public ParticipantEntity()
        {
            Actions = new List<ActionEntity>();
        }

        public ParticipantEntity(ActionParticipant participant)
        {
            Name = participant.Name;
            IsNpc = participant.IsNpc;
            IsEnemy = participant.IsEnemy;
            Enabled = participant.Enabled;

            Actions = new List<ActionEntity>();

            foreach (var action in participant.Actions)
                Actions.Add(new ActionEntity(action));
        }
    }
}