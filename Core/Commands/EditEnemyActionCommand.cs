using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class EditEnemyActionCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public EditEnemyActionCommand(AllParticipantsViewModel allParticipantsViewModel)
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

        }
    }
}