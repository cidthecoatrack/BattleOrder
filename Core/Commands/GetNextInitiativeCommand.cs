﻿using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class GetNextInitiativeCommand : BaseCommand
    {
        private readonly SetInitiativesViewModel setParticipantInitiativesViewModel;

        public GetNextInitiativeCommand(SetInitiativesViewModel setParticipantInitiativesViewModel)
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