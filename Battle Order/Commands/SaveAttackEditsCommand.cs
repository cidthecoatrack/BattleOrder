using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BattleOrder.ViewModels;

namespace BattleOrder.Commands
{
    public class SaveAttackEditsCommand : BaseCommand
    {
        private readonly AttackViewModel attackViewModel;
        
        public SaveAttackEditsCommand(AttackViewModel attackViewModel)
        {
            this.attackViewModel = attackViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return !String.IsNullOrEmpty(attackViewModel.AttackName) && attackViewModel.PerRound > 0;
        }

        public override void Execute(Object parameter)
        {
            attackViewModel.SaveChanges();
        }
    }
}