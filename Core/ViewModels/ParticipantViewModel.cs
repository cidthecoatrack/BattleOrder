using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class ParticipantViewModel : INotifyPropertyChanged
    {
        private Participant participant;
        private String name;
        private Boolean isNpc;
        private Boolean isEnemy;

        public Boolean Enabled { get; set; }
        public ObservableCollection<BattleAction> Actions { get; private set; }
        public BattleAction CurrentAction { get; set; }

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public Boolean IsNpc
        {
            get { return isNpc; }
            set
            {
                isNpc = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNpc"));
            }
        }

        public Boolean IsEnemy
        {
            get { return isEnemy; }
            set
            {
                isEnemy = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsEnemy"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SaveParticipantEditsCommand { get; set; }

        public ParticipantViewModel(Participant participant)
        {
            this.participant = participant;
            SaveParticipantEditsCommand = new SaveParticipantEditsCommand(this);

            Name = this.participant.Name;
            IsNpc = this.participant.IsNpc;
            IsEnemy = this.participant.IsEnemy;
            Enabled = this.participant.Enabled;

            Actions = new ObservableCollection<BattleAction>(participant.Actions);
        }

        public void SaveChanges()
        {
            participant.AlterInfo(Name, IsNpc, IsEnemy);
        }

        public void AddAction(BattleAction newAction)
        {
            participant.AddAction(newAction);
            Actions.Add(newAction);
        }

        public void RemoveAction()
        {
            participant.RemoveAction(CurrentAction);
            Actions.Remove(CurrentAction);
        }
    }
}