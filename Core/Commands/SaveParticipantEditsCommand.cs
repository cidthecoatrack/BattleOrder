using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class SaveParticipantEditsCommand : BaseCommand
    {
        private readonly ParticipantViewModel participantViewModel;

        public SaveParticipantEditsCommand(ParticipantViewModel participantViewModel)
        {
            this.participantViewModel = participantViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return !String.IsNullOrEmpty(participantViewModel.Name);
        }

        public override void Execute(Object parameter) { }
    }
}