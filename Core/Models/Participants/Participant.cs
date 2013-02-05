using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Attacks;

namespace BattleOrder.Core.Models.Participants
{
    [Serializable]
    public class Participant
    {
        private List<Attack> attacks;
        
        public String Name { get; private set; }
        public Int32 Initiative { get; set; }
        public Boolean IsNpc { get; private set; }
        public IEnumerable<Attack> Attacks { get { return attacks; } }
        public IEnumerable<Attack> CurrentAttacks { get { return attacks.Where(x => x.Prepped); } }

        public Participant() 
        {
            Name = String.Empty;
            attacks = new List<Attack>();
        }

        public Participant(String name, Boolean isNpc = true)
        {
            attacks = new List<Attack>();
            IsNpc = isNpc;
            Name = name;
        }

        public Participant(String name, IEnumerable<Attack> newAttacks) 
            : this(name)
        {
            attacks = new List<Attack>(newAttacks);
        }

        public Participant(String name, Attack newAttack, Boolean isNpc = true)
            : this(name, isNpc)
        {
            attacks = new List<Attack>();
            attacks.Add(newAttack);
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
            attacks.Add(newAttack);
        }

        public void RemoveAttack(Attack attack)
        {
            attacks.Remove(attack);
        }

        public void AlterInfo(String newName, Boolean npc)
        {
            Name = newName;
            IsNpc = npc;
        }

        public Boolean IsValid()
        {
            return !String.IsNullOrEmpty(Name) && attacks.Any();
        }
    }
}