using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class PartyViewModel : INotifyPropertyChanged
    {
        private ActionParticipant currentPartyMember;
        private ActionParticipant currentEnemy;
        private List<ActionParticipant> allParticipants;

        public ObservableCollection<ActionParticipant> Party { get; private set; }
        public ObservableCollection<ActionParticipant> Enemies { get; private set; }
        public ObservableCollection<BattleAction> PartyMemberActions { get; private set; }
        public ObservableCollection<BattleAction> EnemyActions { get; private set; }
        public BattleAction CurrentPartyMemberAction { get; set; }
        public BattleAction CurrentEnemyAction { get; set; }

        public ActionParticipant CurrentPartyMember 
        {
            get { return currentPartyMember; }
            set
            {
                currentPartyMember = value;
                PartyMemberActions.Clear();

                if (currentPartyMember != null)
                    foreach (var a in currentPartyMember.Actions)
                        PartyMemberActions.Add(a);

                PropertyChanged(this, new PropertyChangedEventArgs("CurrentPartyMember"));
            }
        }

        public ActionParticipant CurrentEnemy 
        {
            get { return currentEnemy; }
            set
            {
                currentEnemy = value;
                EnemyActions.Clear();

                if (currentEnemy != null)
                    foreach (var a in currentEnemy.Actions)
                        EnemyActions.Add(a);

                PropertyChanged(this, new PropertyChangedEventArgs("CurrentEnemy"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand AddEnemyActionCommand { get; set; }
        public ICommand EditEnemyActionCommand { get; set; }
        public ICommand RemoveEnemyActionCommand { get; set; }
        public ICommand EditEnemyCommand { get; set; }
        public ICommand RemoveEnemyCommand { get; set; }
        public ICommand EditPartyMemberCommand { get; set; }
        public ICommand RemovePartyMemberCommand { get; set; }
        public ICommand AddPartyMemberActionCommand { get; set; }
        public ICommand EditPartyMemberActionCommand { get; set; }
        public ICommand RemovePartyMemberActionCommand { get; set; }

        public PartyViewModel(IEnumerable<ActionParticipant> participants)
        {
            allParticipants = new List<ActionParticipant>(participants);
            Party = new ObservableCollection<ActionParticipant>();
            Enemies = new ObservableCollection<ActionParticipant>();
            PartyMemberActions = new ObservableCollection<BattleAction>();
            EnemyActions = new ObservableCollection<BattleAction>();

            
            AddEnemyActionCommand = new AddEnemyActionCommand(this);
            EditEnemyActionCommand = new EditEnemyActionCommand(this);
            RemoveEnemyActionCommand = new RemoveEnemyActionCommand(this);
            EditEnemyCommand = new EditEnemyCommand(this);
            RemoveEnemyCommand = new RemoveEnemyCommand(this);
            EditPartyMemberCommand = new EditPartyMemberCommand(this);
            RemovePartyMemberCommand = new RemovePartyMemberCommand(this);
            AddPartyMemberActionCommand = new AddPartyMemberActionCommand(this);
            EditPartyMemberActionCommand = new EditPartyMemberActionCommand(this);
            RemovePartyMemberActionCommand = new RemovePartyMemberActionCommand(this);

            UpdateParties();
        }

        private void UpdateParties()
        {
            UpdatePartyMembers();
            UpdateEnemies();
        }

        private void UpdateEnemies()
        {
            var badGuys = allParticipants.Where(x => x.IsEnemy);

            var toAdd = badGuys.Except(Enemies);
            foreach (var p in toAdd)
                Enemies.Add(p);

            var toRemove = Enemies.Except(badGuys).ToList();
            foreach (var p in toRemove)
                Enemies.Remove(p);
        }

        private void UpdatePartyMembers()
        {
            var goodGuys = allParticipants.Where(x => !x.IsEnemy);

            var toAdd = goodGuys.Except(Party);
            foreach (var p in toAdd)
                Party.Add(p);

            var toRemove = Party.Except(goodGuys).ToList();
            foreach (var p in toRemove)
                Party.Remove(p);
        }

        public void AddParticipant(ActionParticipant newParticipant)
        {
            allParticipants.Add(newParticipant);
            UpdateParties();
        }

        public void RemoveParticipant(ActionParticipant participant)
        {
            allParticipants.Remove(participant);

            if (participant == CurrentPartyMember)
                CurrentPartyMember = null;
            else if (participant == CurrentEnemy)
                CurrentEnemy = null;

            UpdateParties();
        }

        public void RemoveAllEnemies()
        {
            foreach (var e in Enemies)
                allParticipants.Remove(e);

            CurrentEnemy = null;
            UpdateParties();
        }

        public void AddActionToPartyMember(BattleAction newAction)
        {
            CurrentPartyMember.AddAction(newAction);
            PartyMemberActions.Add(newAction);
        }

        public void AddActionToEnemy(BattleAction newAction)
        {
            CurrentEnemy.AddAction(newAction);
            EnemyActions.Add(newAction);
        }

        public void RemovePartyMemberAction()
        {
            CurrentPartyMember.RemoveAction(CurrentPartyMemberAction);
            PartyMemberActions.Remove(CurrentPartyMemberAction);
            CurrentPartyMemberAction = null;
        }

        public void RemoveEnemyAction()
        {
            CurrentEnemy.RemoveAction(CurrentEnemyAction);
            EnemyActions.Remove(CurrentEnemyAction);
            CurrentEnemyAction = null;
        }

        public IEnumerable<ActionParticipant> GetEnabledParticipants()
        {
            return allParticipants.Where(p => p.Enabled);
        }
    }
}