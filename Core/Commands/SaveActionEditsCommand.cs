using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class SaveActionEditsCommand : BaseCommand
    {
        private readonly BattleActionViewModel battleActionViewModel;

        public SaveActionEditsCommand(BattleActionViewModel battleActionViewModel)
        {
            this.battleActionViewModel = battleActionViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return !String.IsNullOrEmpty(battleActionViewModel.Name) && battleActionViewModel.PerRound > 0;
        }

        public override void Execute(Object parameter)
        {
            battleActionViewModel.SaveChanges();
        }
    }
}