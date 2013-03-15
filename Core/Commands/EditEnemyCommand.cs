using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class EditEnemyCommand : BaseCommand
    {
        private readonly PartyViewModel allParticipantsViewModel;

        public EditEnemyCommand(PartyViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentEnemy != null;
        }

        public override void Execute(Object parameter)
        {

        }
    }
}