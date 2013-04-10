using System;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core
{
    public class PlacementCalculator
    {
        private Double initiative;
        private Double maxActionsThisRound;
        private Double speed;
        private Double currentPartOfAction;
        
        public PlacementCalculator(BattleAction action)
        {
            maxActionsThisRound = action.ThisRound;
            speed = action.Speed;
            currentPartOfAction = action.Used + 1;
        }

        public Double ComputePlacement(Int32 initiative)
        {
            this.initiative = initiative;

            var fractionalTurn = (maxActionsThisRound - currentPartOfAction + 1) / maxActionsThisRound;
            return speed - initiative * fractionalTurn;
        }
    }
}