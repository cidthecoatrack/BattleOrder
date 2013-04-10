using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class DecrementPerRoundCommand : BaseCommand
    {
        private readonly ActionViewModel actionViewModel;

        public DecrementPerRoundCommand(ActionViewModel actionViewModel)
        {
            this.actionViewModel = actionViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return actionViewModel.PerRound > .5;
        }

        public override void Execute(Object parameter)
        {
            actionViewModel.DecrementPerRound();
        }
    }
}