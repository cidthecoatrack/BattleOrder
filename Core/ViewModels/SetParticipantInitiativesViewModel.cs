using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class SetParticipantInitiativesViewModel
    {
        private IEnumerable<Participant> participants;
        private Participant currentParticipant;

        public String InitiativeString { get { return String.Format("{0}'s initiative", currentParticipant.Name); } }
        public Int32 CurrentInitiative { get { return currentParticipant.Initiative; } }
        public Boolean AllInitiativesSet { get { return currentParticipant == null; } }

        public ICommand DecrementInitiativeCommand { get; set; }
        public ICommand IncrementInitiativeCommand { get; set; }
        public ICommand GetNextInitiativeCommand { get; set; }

        public SetParticipantInitiativesViewModel(IEnumerable<Participant> participants)
        {
            this.participants = participants;
            currentParticipant = participants.First();

            DecrementInitiativeCommand = new DecrementInitiativeCommand(this);
            IncrementInitiativeCommand = new IncrementInitiativeCommand(this);
            GetNextInitiativeCommand = new GetNextInitiativeCommand(this);
        }

        public void GetNextParticipantToSetInitiative()
        {
            currentParticipant = participants.FirstOrDefault(x => x.Initiative == 0);
        }

        public void DecrementCurrentInitiative()
        {
            currentParticipant.Initiative--;
        }

        public void IncrementCurrentInitiative()
        {
            currentParticipant.Initiative++;
        }
    }
}