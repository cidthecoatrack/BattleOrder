using System;
using BattleOrder.Models.Attacks;

namespace BattleOrder
{
    public class PlacementCalculator
    {
        private Double initiative;
        private Double maxAttacksThisRound;
        private Double speed;
        private Double currentPartOfAttack;
        
        public PlacementCalculator(Attack attack)
        {
            maxAttacksThisRound = attack.ThisRound;
            speed = attack.Speed;
            currentPartOfAttack = attack.AttacksUsed + 1;
        }

        public Double ComputePlacement(Int32 initiative)
        {
            this.initiative = initiative;

            var fractionalTurn = (maxAttacksThisRound - currentPartOfAttack + 1) / maxAttacksThisRound;
            return speed - initiative * fractionalTurn;
        }
    }
}