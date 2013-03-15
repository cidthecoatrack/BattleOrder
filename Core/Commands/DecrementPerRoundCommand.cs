using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class DecrementPerRoundCommand : BaseCommand
    {
        private readonly BattleActionViewModel battleActionViewModel;

        public DecrementPerRoundCommand(BattleActionViewModel battleActionViewModel)
        {
            this.battleActionViewModel = battleActionViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return battleActionViewModel.PerRound > .5;
        }

        public override void Execute(Object parameter)
        {
            battleActionViewModel.DecrementPerRound();
        }
    }
}