using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemoveEnemyActionCommand : BaseCommand
    {
        private readonly PartyViewModel allParticipantsViewModel;

        public RemoveEnemyActionCommand(PartyViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentEnemy != null
                && allParticipantsViewModel.CurrentEnemyAction != null;
        }

        public override void Execute(Object parameter) { }
    }
}