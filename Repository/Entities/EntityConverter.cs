using System.Collections.Generic;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Repository.Entities
{
    public class EntityConverter
    {
        public ActionParticipant ConvertToActionParticipant(ParticipantEntity entity)
        {
            var participant = new ActionParticipant(entity.Name, entity.IsEnemy, entity.IsNpc);
            participant.Enabled = entity.Enabled;

            var actions = new List<BattleAction>();

            foreach (var savedAction in entity.Actions)
                actions.Add(ConvertToBattleAction(savedAction));

            participant.AddActions(actions);

            return participant;
        }

        public BattleAction ConvertToBattleAction(ActionEntity entity)
        {
            return new BattleAction(entity.Name, entity.PerRound, entity.Speed, entity.Prepped);
        }
    }
}