using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.ViewModels;

namespace BattleOrder.Commands
{
    public class DecrementInitiativeCommand : BaseCommand
    {
        private readonly SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;

        public DecrementInitiativeCommand(SetParticipantInitiativesViewModel setParticipantInitiativesViewModel)
        {
            this.setParticipantInitiativesViewModel = setParticipantInitiativesViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return setParticipantInitiativesViewModel.CurrentInitiative > 1;
        }

        public override void Execute(Object parameter)
        {
            setParticipantInitiativesViewModel.DecrementCurrentInitiative();
        }
    }
}