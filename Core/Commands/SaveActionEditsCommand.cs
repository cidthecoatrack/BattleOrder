using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class SaveActionEditsCommand : BaseCommand
    {
        private readonly AttackViewModel attackViewModel;
        
        public SaveActionEditsCommand(AttackViewModel attackViewModel)
        {
            this.attackViewModel = attackViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return !String.IsNullOrEmpty(attackViewModel.Name) && attackViewModel.PerRound > 0;
        }

        public override void Execute(Object parameter)
        {
            attackViewModel.SaveChanges();
        }
    }
}