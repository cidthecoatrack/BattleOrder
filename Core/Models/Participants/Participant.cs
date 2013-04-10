using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core.Models.Participants
{
    public class Participant
    {
        public String Name { get; set; }
        public Boolean IsNpc { get; set; }
        public Boolean IsEnemy { get; set; }
        public Boolean Enabled { get; set; }
        public IEnumerable<Attack> Attacks { get; set; }
    }
}