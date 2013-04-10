using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;

namespace BattleOrder.Core.ViewModels
{
    public class RoundViewModel
    {
        private Queue<QueueableAction> actions;
        private readonly Int32 round;

        public String RoundTitle { get; private set; }
        public String CurrentActions { get; private set; }
        public Int32 TotalActions { get; private set; }
        public Int32 CompletedActions { get { return TotalActions - actions.Count; } }
        public Boolean RoundIsActive { get { return actions.Any(); } }

        public ICommand GetNextActionsCommand { get; set; }

        public RoundViewModel(Int32 round, IEnumerable<ActionParticipant> participants)
        {
            this.round = round;
            var orderedActions = GetOrderedActions(participants);
            actions = new Queue<QueueableAction>(orderedActions);

            TotalActions = this.actions.Count;
            GetNextActionsCommand = new GetNextActionsCommand(this);

            GetActions();
        }

        private IEnumerable<QueueableAction> GetOrderedActions(IEnumerable<ActionParticipant> participants)
        {
            var list = new List<QueueableAction>();

            foreach (var p in participants)
            {
                var preppedActions = p.Actions.Where(a => a.Prepped);

                foreach (var a in preppedActions)
                {
                    var queueableAction = new QueueableAction(p.Name, a, p.Initiative);
                    list.Add(queueableAction);
                }
            }

            return list.OrderBy(q => q.Placement);
        }

        public void GetActions()
        {
            var partOfRound = actions.Peek().Placement;
            RoundTitle = String.Format("Round {0}: {1}", round, partOfRound);

            var currentActions = new List<QueueableAction>();
            while (RoundIsActive && actions.Peek().Placement == partOfRound)
                currentActions.Add(actions.Dequeue());

            CurrentActions = String.Empty;
            foreach (var action in currentActions)
                CurrentActions += "\n" + action.Description;
        }
    }
}