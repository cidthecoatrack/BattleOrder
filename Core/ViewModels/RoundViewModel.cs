using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core.ViewModels
{
    public class RoundViewModel
    {
        private Queue<QueueableBattleAction> actions;
        private readonly Int32 round;

        public String RoundTitle { get; private set; }
        public String CurrentActions { get; private set; }
        public Int32 TotalActions { get; private set; }
        public Int32 CompletedActions { get { return TotalActions - actions.Count; } }
        public Boolean RoundIsActive { get { return actions.Any(); } }

        public ICommand GetNextActionsCommand { get; set; }

        public RoundViewModel(Queue<QueueableBattleAction> actions, Int32 round)
        {
            this.actions = actions;
            this.round = round;
            TotalActions = this.actions.Count;
            GetNextActionsCommand = new GetNextActionsCommand(this);

            GetActions();
        }

        public void GetActions()
        {
            var partOfRound = actions.Peek().Placement;
            RoundTitle = String.Format("Round {0}: {1}", round, partOfRound);

            var currentActions = new List<QueueableBattleAction>();
            while (RoundIsActive && actions.Peek().Placement == partOfRound)
                currentActions.Add(actions.Dequeue());

            CurrentActions = String.Empty;
            foreach (var action in currentActions)
                CurrentActions += "\n" + action.Description;
        }
    }
}