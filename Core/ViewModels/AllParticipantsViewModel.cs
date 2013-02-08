using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class AllParticipantsViewModel
    {
        private List<Participant> participants;

        public IEnumerable<Participant> AllParticipants { get { return participants; } }
        public IEnumerable<Participant> PartyMembers { get { return participants.Where(x => !x.IsEnemy); } }
        public IEnumerable<Participant> Enemies { get { return participants.Where(x => x.IsEnemy); } }
        public Participant CurrentPartyMember { get; set; }
        public Participant CurrentEnemy { get; set; }
        public IEnumerable<Attack> PartyMemberAttacks { get { return CurrentPartyMember.Attacks; } }
        public IEnumerable<Attack> EnemyAttacks { get { return CurrentEnemy.Attacks; } }

        public AllParticipantsViewModel(List<Participant> participants)
        {
            this.participants = participants;
        }

        public void AddParticipant(Participant newParticipant)
        {
            participants.Add(newParticipant);
        }

        public void RemoveParticipant(Participant participant)
        {
            participants.Remove(participant);
        }

        public void RemoveAllEnemies()
        {
            foreach (var enemy in Enemies)
                participants.Remove(enemy);
        }

        public void AddAttackToPartyMember(Attack newAttack)
        {
            CurrentPartyMember.AddAttack(newAttack);
        }

        public void AddAttackToEnemy(Attack newAttack)
        {
            CurrentEnemy.AddAttack(newAttack);
        }
    }
}