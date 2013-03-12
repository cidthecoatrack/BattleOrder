using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class IncrementInitiativeCommand : BaseCommand
    {
        private readonly SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;

        public IncrementInitiativeCommand(SetParticipantInitiativesViewModel setParticipantInitiativesViewModel)
        {
            this.setParticipantInitiativesViewModel = setParticipantInitiativesViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return setParticipantInitiativesViewModel.CurrentInitiative < 10;
        }

        public override void Execute(Object parameter)
        {
            setParticipantInitiativesViewModel.IncrementCurrentInitiative();
        }
    }
}