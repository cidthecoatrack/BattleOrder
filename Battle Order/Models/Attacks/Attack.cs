using System;
using System.Linq;

namespace BattleOrder.Models.Attacks
{
    [Serializable]
    public class Attack
    {
        private Int32 currentPartOfAttack;
        private Boolean allUsable;
        private Boolean differingPerRound { get { return (PerRound - Math.Floor(PerRound)) > 0; } }

        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Boolean Prepped { get; set; }
        public Int32 AttacksUsed { get { return currentPartOfAttack; } }
        public Int32 AttacksLeft { get { return ThisRound - currentPartOfAttack; } }

        public Int32 ThisRound
        {
            get
            {
                if (allUsable)
                    return Convert.ToInt32(Math.Ceiling(PerRound));
                return Convert.ToInt32(Math.Floor(PerRound));
            }
        }

        public Attack() 
        {
            Name = String.Empty;
            allUsable = true;
        }

        public Attack(String name, Double perRound, Int32 speed, Boolean prepped = false)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            Prepped = prepped;
            allUsable = true;
        }

        public void FinishCurrentPartOfAttack()
        {
            if (AttackIsDone())
                return;

            currentPartOfAttack++;

            if (differingPerRound && AttackIsDone())
                allUsable = !allUsable;
        }

        private Boolean AttackIsDone()
        {
            return currentPartOfAttack >= ThisRound;
        }

        public void PrepareForNextBattle()
        {
            currentPartOfAttack = 0;
            allUsable = true;
        }

        public void PrepareForNextRound()
        {
            currentPartOfAttack = 0;
        }

        public void AlterInfo(String newName, Double newPerRound, Int32 newSpeed)
        {
            Name = newName;
            PerRound = newPerRound;
            Speed = newSpeed;
        }
    }
}