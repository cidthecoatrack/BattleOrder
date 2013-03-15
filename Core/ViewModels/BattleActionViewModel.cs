using System;
using System.ComponentModel;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core.ViewModels
{
    public class BattleActionViewModel : INotifyPropertyChanged
    {
        private BattleAction action;
        private String name;
        private Double perRound;
        private Int32 speed;

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        
        public Double PerRound
        {
            get { return perRound; }
            private set
            {
                perRound = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PerRound"));
            }
        }
        
        public Int32 Speed
        {
            get { return speed; }
            private set
            {
                speed = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Speed"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ICommand SaveActionEditsCommand { get; set; }
        public ICommand DecrementPerRoundCommand { get; set; }

        public BattleActionViewModel(BattleAction action)
        {
            this.action = action;
            Name = this.action.Name;
            PerRound = this.action.PerRound;
            Speed = this.action.Speed;

            SaveActionEditsCommand = new SaveActionEditsCommand(this);
            DecrementPerRoundCommand = new DecrementPerRoundCommand(this);
        }

        public void SaveChanges()
        {
            action.AlterInfo(Name, PerRound, Speed);
        }

        public void IncrementPerRound()
        {
            PerRound += .5;
        }

        public void DecrementPerRound()
        {
            PerRound -= .5;
        }

        public void IncrementSpeed()
        {
            Speed++;
        }

        public void DecrementSpeed()
        {
            Speed--;
        }
    }
}