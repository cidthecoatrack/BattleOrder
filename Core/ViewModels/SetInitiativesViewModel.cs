using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Participants;
using D20Dice.Dice;

namespace BattleOrder.Core.ViewModels
{
    public class SetInitiativesViewModel
    {
        private IEnumerable<ActionParticipant> participants;
        private ActionParticipant currentParticipant;
        private IDice dice;

        public String InitiativeString { get { return String.Format("{0}'s initiative", currentParticipant.Name); } }
        public Int32 CurrentInitiative { get { return currentParticipant.Initiative; } }
        public Boolean AllInitiativesSet { get { return currentParticipant == null; } }

        public ICommand DecrementInitiativeCommand { get; set; }
        public ICommand IncrementInitiativeCommand { get; set; }
        public ICommand GetNextInitiativeCommand { get; set; }

        public SetInitiativesViewModel(IEnumerable<ActionParticipant> participants, IDice dice)
        {
            this.participants = participants;
            this.dice = dice;

            DecrementInitiativeCommand = new DecrementInitiativeCommand(this);
            IncrementInitiativeCommand = new IncrementInitiativeCommand(this);
            GetNextInitiativeCommand = new GetNextInitiativeCommand(this);

            ZeroOutAllInitiatives();
            SetNpcInitiatives();

            currentParticipant = participants.FirstOrDefault(p => p.Initiative == 0);
        }

        private void SetNpcInitiatives()
        {
            var npcs = participants.Where(p => p.IsNpc);

            foreach (var npc in npcs)
                npc.Initiative = dice.d10();
        }

        private void ZeroOutAllInitiatives()
        {
            foreach (var participant in participants)
                participant.Initiative = 0;
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