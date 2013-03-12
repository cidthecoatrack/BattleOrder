using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class AllParticipantsViewModel : INotifyPropertyChanged
    {
        private Participant currentPartyMember;
        private Participant currentEnemy;
        private List<Participant> allParticipants;

        public ObservableCollection<Participant> PartyMembers { get; private set; }
        public ObservableCollection<Participant> Enemies { get; private set; }
        public ObservableCollection<Attack> PartyMemberAttacks { get; private set; }
        public ObservableCollection<Attack> EnemyAttacks { get; private set; }
        public Attack CurrentPartyMemberAttack { get; set; }
        public Attack CurrentEnemyAttack { get; set; }

        public Participant CurrentPartyMember 
        {
            get { return currentPartyMember; }
            set
            {
                currentPartyMember = value;
                PartyMemberAttacks.Clear();

                if (currentPartyMember != null)
                    foreach (var a in currentPartyMember.Attacks)
                        PartyMemberAttacks.Add(a);

                PropertyChanged(this, new PropertyChangedEventArgs("CurrentPartyMember"));
            }
        }

        public Participant CurrentEnemy 
        {
            get { return currentEnemy; }
            set
            {
                currentEnemy = value;
                EnemyAttacks.Clear();

                if (currentEnemy != null)
                    foreach (var a in currentEnemy.Attacks)
                        EnemyAttacks.Add(a);

                PropertyChanged(this, new PropertyChangedEventArgs("CurrentEnemy"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand AddEnemyAttackCommand { get; set; }
        public ICommand EditEnemyAttackCommand { get; set; }
        public ICommand RemoveEnemyAttackCommand { get; set; }
        public ICommand EditEnemyCommand { get; set; }
        public ICommand RemoveEnemyCommand { get; set; }
        public ICommand EditPartyMemberCommand { get; set; }
        public ICommand RemovePartyMemberCommand { get; set; }
        public ICommand AddPartyMemberAttackCommand { get; set; }
        public ICommand EditPartyMemberAttackCommand { get; set; }
        public ICommand RemovePartyMemberAttackCommand { get; set; }

        public AllParticipantsViewModel(IEnumerable<Participant> participants)
        {
            allParticipants = new List<Participant>(participants);
            PartyMembers = new ObservableCollection<Participant>();
            Enemies = new ObservableCollection<Participant>();
            PartyMemberAttacks = new ObservableCollection<Attack>();
            EnemyAttacks = new ObservableCollection<Attack>();

            
            AddEnemyAttackCommand = new AddEnemyAttackCommand(this);
            EditEnemyAttackCommand = new EditEnemyAttackCommand(this);
            RemoveEnemyAttackCommand = new RemoveEnemyAttackCommand(this);
            EditEnemyCommand = new EditEnemyCommand(this);
            RemoveEnemyCommand = new RemoveEnemyCommand(this);
            EditPartyMemberCommand = new EditPartyMemberCommand(this);
            RemovePartyMemberCommand = new RemovePartyMemberCommand(this);
            AddPartyMemberAttackCommand = new AddPartyMemberAttackCommand(this);
            EditPartyMemberAttackCommand = new EditPartyMemberAttackCommand(this);
            RemovePartyMemberAttackCommand = new RemovePartyMemberAttackCommand(this);

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

            var toAdd = goodGuys.Except(PartyMembers);
            foreach (var p in toAdd)
                PartyMembers.Add(p);

            var toRemove = PartyMembers.Except(goodGuys).ToList();
            foreach (var p in toRemove)
                PartyMembers.Remove(p);
        }

        public void AddParticipant(Participant newParticipant)
        {
            allParticipants.Add(newParticipant);
            UpdateParties();
        }

        public void RemoveParticipant(Participant participant)
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

        public void AddAttackToPartyMember(Attack newAttack)
        {
            CurrentPartyMember.AddAttack(newAttack);
            PartyMemberAttacks.Add(newAttack);
        }

        public void AddAttackToEnemy(Attack newAttack)
        {
            CurrentEnemy.AddAttack(newAttack);
            EnemyAttacks.Add(newAttack);
        }

        public void RemovePartyMemberAttack()
        {
            CurrentPartyMember.RemoveAttack(CurrentPartyMemberAttack);
            PartyMemberAttacks.Remove(CurrentPartyMemberAttack);
            CurrentPartyMemberAttack = null;
        }

        public void RemoveEnemyAttack()
        {
            CurrentEnemy.RemoveAttack(CurrentEnemyAttack);
            EnemyAttacks.Remove(CurrentEnemyAttack);
            CurrentEnemyAttack = null;
        }
    }
}