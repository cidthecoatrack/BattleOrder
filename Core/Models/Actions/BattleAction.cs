using System;

namespace BattleOrder.Core.Models.Actions
{
    [Serializable]
    public class BattleAction
    {
        private Int32 currentPartOfAction;
        private Boolean allUsable;
        private Boolean differingPerRound { get { return (PerRound - Math.Floor(PerRound)) > 0; } }

        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Boolean Prepped { get; set; }
        public Int32 Used { get { return currentPartOfAction; } }
        public Int32 Left { get { return ThisRound - currentPartOfAction; } }

        public Int32 ThisRound
        {
            get
            {
                if (allUsable)
                    return Convert.ToInt32(Math.Ceiling(PerRound));
                return Convert.ToInt32(Math.Floor(PerRound));
            }
        }

        public BattleAction(String name, Double perRound = 0, Int32 speed = 0, Boolean prepped = false)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            Prepped = prepped;
            allUsable = true;
        }

        public void FinishCurrentPartOfAction()
        {
            if (ActionIsDone())
                return;

            currentPartOfAction++;

            if (differingPerRound && ActionIsDone())
                allUsable = !allUsable;
        }

        private Boolean ActionIsDone()
        {
            return currentPartOfAction >= ThisRound;
        }

        public void PrepareForNextBattle()
        {
            currentPartOfAction = 0;
            allUsable = true;
        }

        public void PrepareForNextRound()
        {
            currentPartOfAction = 0;
        }

        public void AlterInfo(String newName, Double newPerRound, Int32 newSpeed)
        {
            Name = newName;
            PerRound = newPerRound;
            Speed = newSpeed;
        }

        public Boolean IsValid()
        {
            return !String.IsNullOrEmpty(Name) && PerRound > 0;
        }

        public override String ToString()
        {
            return Name;
        }
    }
}