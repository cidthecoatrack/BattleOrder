using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemoveEnemyAttackCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public RemoveEnemyAttackCommand(AllParticipantsViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentEnemy != null
                && allParticipantsViewModel.CurrentEnemyAttack != null;
        }

        public override void Execute(Object parameter)
        {
            allParticipantsViewModel.RemoveEnemyAttack();
        }
    }
}