using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Attacks;

namespace BattleOrder.Core.Models.Participants
{
    [Serializable]
    public class Participant
    {
        public String Name { get; private set; }
        public Int32 Initiative { get; set; }
        public IEnumerable<Attack> Attacks { get; private set; }
        public Boolean IsNpc { get; private set; }
        public IEnumerable<Attack> CurrentAttacks { get { return Attacks.Where(x => x.Prepped); } }

        public Participant() 
        {
            Name = String.Empty;
            Attacks = Enumerable.Empty<Attack>();
        }

        public Participant(String name, Boolean isNpc = true)
        {
            Attacks = Enumerable.Empty<Attack>();
            IsNpc = isNpc;
            Name = name;
        }

        public Participant(String name, IEnumerable<Attack> attacks) 
            : this(name)
        {
            Attacks = attacks;
        }

        public Participant(String name, Attack newAttack, Boolean isNpc = true)
            : this(name, isNpc)
        {
            Attacks = new[] { newAttack };
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

        public void PrepareForNextRound()
        {
            foreach (var attack in Attacks)
                attack.PrepareForNextRound();

            Initiative = 0;
        }

        public void PrepareForNextBattle()
        {
            foreach (var attack in Attacks)
                attack.PrepareForNextBattle();

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