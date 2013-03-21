using System;
using System.ComponentModel;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;

namespace BattleOrder.Core.ViewModels
{
    public class ActionViewModel : INotifyPropertyChanged
    {
        private Attack attack;
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

        public ICommand SaveAttackEditsCommand { get; set; }
        public ICommand DecrementPerRoundCommand { get; set; }

        public ActionViewModel(Attack attack)
        {
            this.attack = attack;
            SaveAttackEditsCommand = new SaveActionEditsCommand(this);
            DecrementPerRoundCommand = new DecrementPerRoundCommand(this);
            SetToUneditedVariables();
        }

        private void SetToUneditedVariables()
        {
            Name = attack.Name;
            PerRound = attack.PerRound;
            Speed = attack.Speed;
        }

        public void SaveChanges()
        {
            attack.AlterInfo(Name, PerRound, Speed);
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