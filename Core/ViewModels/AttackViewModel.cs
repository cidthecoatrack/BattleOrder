using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;

namespace BattleOrder.Core.ViewModels
{
    public class AttackViewModel
    {
        private Attack attack;

        public String AttackName { get; set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }

        public ICommand SaveAttackEditsCommand { get; set; }
        public ICommand DecrementPerRoundCommand { get; set; }

        public AttackViewModel(Attack attack)
        {
            this.attack = attack;
            SaveAttackEditsCommand = new SaveAttackEditsCommand(this);
            DecrementPerRoundCommand = new DecrementPerRoundCommand(this);
            SetToUneditedVariables();
        }

        private void SetToUneditedVariables()
        {
            AttackName = attack.Name;
            PerRound = attack.PerRound;
            Speed = attack.Speed;
        }

        public void SaveChanges()
        {
            attack.AlterInfo(AttackName, PerRound, Speed);
        }

        public void IncrementPerRound()
        {
            PerRound += .5;
        }

        public void DecrementPerRound()
        {
            PerRound -= .5;
        }

        public void IncrementSpeed()
        {
            Speed++;
        }

        public void DecrementSpeed()
        {
            Speed--;
        }
    }
}