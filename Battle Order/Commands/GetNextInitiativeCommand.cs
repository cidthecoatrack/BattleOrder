using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.ViewModels;

namespace BattleOrder.Commands
{
    public class GetNextInitiativeCommand : BaseCommand
    {
        private readonly SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;

        public GetNextInitiativeCommand(SetParticipantInitiativesViewModel setParticipantInitiativesViewModel)
        {
            this.setParticipantInitiativesViewModel = setParticipantInitiativesViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            var initiative = setParticipantInitiativesViewModel.CurrentInitiative;
            return initiative > 0 && initiative < 11;
        }

        public override void Execute(Object parameter)
        {
            setParticipantInitiativesViewModel.GetNextParticipantToSetInitiative();
        }
    }
}