using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleOrder
{
    public class QueueableParticipant
    {
        public String Name { get; private set; }
        public Attack Attack { get; private set; }

        public QueueableParticipant(String name, Attack attack)
        {
            Name = name;
            Attack = attack;
        }
    }
}