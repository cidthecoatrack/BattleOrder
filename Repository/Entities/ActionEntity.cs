using System;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Repository.Entities
{
    public class ActionEntity
    {
        public String Name { get; set; }
        public Double PerRound { get; set; }
        public Int32 Speed { get; set; }
        public Boolean Prepped { get; set; }

        public ActionEntity() { }

        public ActionEntity(BattleAction action)
        {
            Name = action.Name;
            PerRound = action.PerRound;
            Speed = action.Speed;
            Prepped = action.Prepped;
        }
    }
}