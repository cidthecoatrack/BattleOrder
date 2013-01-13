using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battle_Order
{
    [Serializable]
    class Participant
    {
        public String Name { get; set; }
        public Int32 Initiative { get; private set; }
        public List<Attack> Attacks { get; private set; }
        public Attack SingleAttack { get; private set; }
        public Boolean NPC { get; private set; }

        public Attack[] CurrentAttacks
        {
            get
            {
                var currentAttacks = Attacks.Where(x => x.Prepped);
                return currentAttacks as Attack[];
            }
        }

        public Participant(String name, Boolean npc, Attack currentAttack, Int32 initiative = 0)
        {
            Attacks = new List<Attack>();
            NPC = npc;
            Name = name;
            Initiative = initiative;
            SingleAttack = currentAttack;
        }

        public Participant(String name, List<Attack> attacks)
        {
            Attacks = new List<Attack>();
            NPC = true;
            Name = name;
            foreach (var attack in attacks)
                Attacks.Add(new Attack(attack.Name, attack.PerRound, attack.Speed, attack.Prepped));
        }

        public Participant(String name, Attack NewAttack)
        {
            Attacks = new List<Attack>();
            NPC = true;
            Name = name;
            NewAttack.Prepped = true;
            Attacks.Add(NewAttack);
        }

        public Participant(String name, String attackName, Int32 attackSpeed, Double perRound)
        {
            Attacks = new List<Attack>();
            NPC = true;
            Name = name;
            var newAttack = new Attack(attackName, perRound, attackSpeed);
            Attacks.Add(newAttack);
        }

        public String CurrentAttacksToString()
        {
            String output = String.Empty;
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
            Attacks.Add(newAttack);
        }
    }
}