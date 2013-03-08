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
        public Boolean IsEnemy { get; private set; }
        public IEnumerable<Attack> Attacks { get { return attacks; } }
        public IEnumerable<Attack> CurrentAttacks { get { return attacks.Where(x => x.Prepped); } }

        public Participant(String name, Boolean isEnemy = true, Boolean isNpc = true)
        {
            Name = name;
            IsNpc = isNpc;
            IsEnemy = isEnemy;
            attacks = new List<Attack>();
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

        public void AddAttacks(IEnumerable<Attack> newAttacks)
        {
            attacks.AddRange(newAttacks);
        }

        public void RemoveAttack(Attack attack)
        {
            attacks.Remove(attack);
        }

        public void AlterInfo(String newName, Boolean npc, Boolean isEnemy)
        {
            Name = newName;
            IsNpc = npc;
            IsEnemy = isEnemy;
        }

        public Boolean IsValid()
        {
            return !String.IsNullOrEmpty(Name) && attacks.Any();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}