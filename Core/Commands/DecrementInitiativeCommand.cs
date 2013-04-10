using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class DecrementInitiativeCommand : BaseCommand
    {
        private readonly SetInitiativesViewModel setParticipantInitiativesViewModel;

        public DecrementInitiativeCommand(SetInitiativesViewModel setParticipantInitiativesViewModel)
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