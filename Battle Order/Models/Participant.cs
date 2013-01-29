using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Models.Attacks;

namespace BattleOrder.Models
{
    [Serializable]
    public class Participant
    {
        public String Name { get; set; }
        public Int32 Initiative { get; set; }
        public IEnumerable<Attack> Attacks { get; private set; }
        public Boolean NPC { get; set; }
        public IEnumerable<Attack> CurrentAttacks { get { return Attacks.Where(x => x.Prepped); } }

        public Participant(String name, Boolean npc = true, Int32 initiative = 0)
        {
            Attacks = Enumerable.Empty<Attack>();
            NPC = npc;
            Name = name;
            Initiative = initiative;
        }

        public Participant(String name, IEnumerable<Attack> attacks) 
            : this(name)
        {
            Attacks = Attacks.Union(attacks);
        }

        public Participant(String name, Attack newAttack, Boolean npc = false)
            : this(name, npc)
        {
            newAttack.Prepped = true;
            AddAttack(newAttack);
        }

        public String CurrentAttacksToString()
        {
            var output = String.Empty;
            foreach (var attack in CurrentAttacks)
            {
                if (!String.IsNullOrEmpty(output))
                    output += " and ";
                output += String.Format("{0}", attack.Name);
            }

            return output;
        }

        public void Reset()
        {
            foreach (var attack in Attacks)
            {
                if (attack.Prepped)
                    attack.ResetPartial();
                else
                    attack.Reset();
            }

            Initiative = 0;
        }

        public void TotalReset()
        {
            foreach (var attack in Attacks)
                attack.Reset();

            Initiative = 0;
        }

        public void AddAttack(Attack newAttack)
        {
            var tempList = Attacks as List<Attack>;
            tempList.Add(newAttack);
            Attacks = tempList;
        }

        public void RemoveAttack(Attack attack)
        {
            var tempList = Attacks as List<Attack>;
            tempList.Remove(attack);
            Attacks = tempList;
        }
    }
}