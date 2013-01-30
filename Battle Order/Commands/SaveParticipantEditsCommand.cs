using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BattleOrder.ViewModels;

namespace BattleOrder.Commands
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

        public override void Execute(Object parameter)
        {
            participantViewModel.SaveChanges();
        }
    }
}