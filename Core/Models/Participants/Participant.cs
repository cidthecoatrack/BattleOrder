using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core.Models.Participants
{
    [Serializable]
    public class Participant
    {
        private List<BattleAction> actions;
        
        public String Name { get; private set; }
        public Int32 Initiative { get; set; }
        public Boolean IsNpc { get; private set; }
        public Boolean IsEnemy { get; private set; }
        public Boolean Enabled { get; set; }
        public IEnumerable<BattleAction> Actions { get { return actions; } }
        public IEnumerable<BattleAction> CurrentActions { get { return actions.Where(x => x.Prepped); } }

        public Participant(String name, Boolean isEnemy = true, Boolean isNpc = true)
        {
            Name = name;
            IsNpc = isNpc;
            IsEnemy = isEnemy;
            actions = new List<BattleAction>();
            Enabled = true;
        }

        public void PrepareForNextRound()
        {
            foreach (var action in Actions)
                action.PrepareForNextRound();

            Initiative = 0;
        }

        public void PrepareForNextBattle()
        {
            foreach (var action in Actions)
                action.PrepareForNextBattle();

            Initiative = 0;
        }

        public void AddAction(BattleAction newAction)
        {
            actions.Add(newAction);
        }

        public void AddActions(IEnumerable<BattleAction> newActions)
        {
            actions.AddRange(newActions);
        }

        public void RemoveAction(BattleAction action)
        {
            actions.Remove(action);
        }

        public void AlterInfo(String newName, Boolean npc, Boolean isEnemy)
        {
            Name = newName;
            IsNpc = npc;
            IsEnemy = isEnemy;
        }

        public Boolean IsValid()
        {
            return !String.IsNullOrEmpty(Name);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}