using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;

namespace BattleOrder.Core.ViewModels
{
    public class RoundViewModel
    {
        private Queue<QueueableAction> attacks;
        private readonly Int32 round;

        public String RoundTitle { get; private set; }
        public String CurrentAttacks { get; private set; }
        public Int32 TotalAttacks { get; private set; }
        public Int32 CompletedAttacks { get { return TotalAttacks - attacks.Count; } }
        public Boolean RoundIsActive { get { return attacks.Any(); } }

        public ICommand GetNextAttacksCommand { get; set; }

        public RoundViewModel(Queue<QueueableAction> attacks, Int32 round)
        {
            this.attacks = attacks;
            this.round = round;
            TotalAttacks = this.attacks.Count;
            GetNextAttacksCommand = new GetNextAttacksCommand(this);

            GetAttacks();
        }

        public void GetAttacks()
        {
            var partOfRound = attacks.Peek().Placement;
            RoundTitle = String.Format("Round {0}: {1}", round, partOfRound);

            var currentAttacks = new List<QueueableAction>();
            while (RoundIsActive && attacks.Peek().Placement == partOfRound)
                currentAttacks.Add(attacks.Dequeue());

            CurrentAttacks = String.Empty;
            foreach (var attack in currentAttacks)
                CurrentAttacks += "\n" + attack.Description;
        }
    }
}