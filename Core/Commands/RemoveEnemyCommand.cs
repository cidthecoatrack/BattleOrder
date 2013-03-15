using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemoveEnemyCommand : BaseCommand
    {
        private readonly PartyViewModel allParticipantsViewModel;

        public RemoveEnemyCommand(PartyViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentEnemy != null;
        }

        public override void Execute(Object parameter) { }
    }
}