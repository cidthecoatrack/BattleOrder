using System;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class ParticipantViewModel
    {
        private Participant participant;

        public String Name { get; set; }
        public Boolean IsNpc { get; set; }
        public Boolean IsEnemy { get; set; }

        public ICommand SaveParticipantEditsCommand { get; set; }

        public ParticipantViewModel(Participant participant)
        {
            this.participant = participant;
            SaveParticipantEditsCommand = new SaveParticipantEditsCommand(this);

            Name = participant.Name;
            IsNpc = participant.IsNpc;
            IsEnemy = participant.IsEnemy;
        }

        public void SaveChanges()
        {
            participant.AlterInfo(Name, IsNpc, IsEnemy);
        }

        public void AddAttack(Attack attack)
        {
            participant.AddAttack(attack);
        }
    }
}