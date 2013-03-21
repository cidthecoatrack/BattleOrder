using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class GetNextActionsCommand : BaseCommand
    {
        private readonly RoundViewModel roundViewModel;

        public GetNextActionsCommand(RoundViewModel roundViewModel)
        {
            this.roundViewModel = roundViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return roundViewModel.RoundIsActive;
        }

        public override void Execute(Object parameter)
        {
            roundViewModel.GetAttacks();
        }
    }
}