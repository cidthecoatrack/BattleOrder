using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.Models.Attacks;

namespace BattleOrder.ViewModels
{
    public class AttackViewModel
    {
        private Attack attack;

        public String AttackName { get; set; }
        public Double PerRound { get; set; }
        public Int32 Speed { get; set; }

        public AttackViewModel(Attack attack)
        {
            this.attack = attack;
        }

        public void SaveChanges()
        {
            attack = new Attack(AttackName, PerRound, Speed);
        }
    }
}