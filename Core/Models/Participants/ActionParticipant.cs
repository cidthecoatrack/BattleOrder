using System;
using System.Collections.Generic;
using System.Linq;
using BattleOrder.Core.Models.Actions;

namespace BattleOrder.Core.Models.Participants
{
    public class ActionParticipant
    {
        public String Name { get; private set; }
        public Int32 Initiative { get; set; }
        public Boolean IsNpc { get; private set; }
        public Boolean IsEnemy { get; private set; }
        public Boolean Enabled { get; set; }
        public List<BattleAction> Actions { get; private set; }

        public ActionParticipant(String name, Boolean isEnemy = true, Boolean isNpc = true)
        {
            Name = name;
            IsNpc = isNpc;
            IsEnemy = isEnemy;
            Actions = new List<BattleAction>();
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
            Actions.Add(newAction);
        }

        public void AddActions(IEnumerable<BattleAction> newAction)
        {
            Actions.AddRange(newAction);
        }

        public void RemoveAction(BattleAction action)
        {
            Actions.Remove(action);
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