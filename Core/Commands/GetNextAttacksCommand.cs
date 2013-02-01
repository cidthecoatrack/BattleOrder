using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class GetNextAttacksCommand : BaseCommand
    {
        private readonly RoundViewModel roundViewModel;

        public GetNextAttacksCommand(RoundViewModel roundViewModel)
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