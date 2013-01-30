using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Models.Attacks;

namespace BattleOrder.Models
{
    [Serializable]
    public class Participant
    {
        public String Name { get; private set; }
        public Int32 Initiative { get; set; }
        public IEnumerable<Attack> Attacks { get; private set; }
        public Boolean IsNpc { get; private set; }
        public IEnumerable<Attack> CurrentAttacks { get { return Attacks.Where(x => x.Prepped); } }

        public Participant(String name, Boolean isNpc = true)
        {
            Attacks = Enumerable.Empty<Attack>();
            IsNpc = isNpc;
            Name = name;
        }

        public Participant(String name, IEnumerable<Attack> attacks) 
            : this(name)
        {
            Attacks = Attacks.Union(attacks);
        }

        public Participant(String name, Attack newAttack, Boolean isNpc = false)
            : this(name, isNpc)
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

        public void AlterInfo(String newName, Boolean npc)
        {
            Name = newName;
            IsNpc = npc;
        }
    }
}