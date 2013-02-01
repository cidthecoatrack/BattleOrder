using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class ParticipantViewModel
    {
        private Participant participant;

        public String Name { get; set; }
        public Boolean IsNpc { get; set; }

        public ICommand SaveParticipantEditsCommand { get; set; }

        public ParticipantViewModel(Participant participant)
        {
            this.participant = participant;
            SaveParticipantEditsCommand = new SaveParticipantEditsCommand(this);
            SetToUneditedVariables();
        }

        private void SetToUneditedVariables()
        {
            Name = participant.Name;
            IsNpc = participant.IsNpc;
        }

        public void SaveChanges()
        {
            participant.AlterInfo(Name, IsNpc);
        }
    }
}