using System;
using System.Linq;

namespace BattleOrder.Models.Attacks
{
    [Serializable]
    public class Attack
    {
        private Boolean[] used;
        private Boolean differingPerRound { get { return (PerRound - Math.Floor(PerRound)) > 0; } }
        private Boolean allUsable;

        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Boolean Prepped { get; set; }
        public Int32 AttacksUsed { get { return used.Count(x => x); } }
        public Int32 AttacksLeft { get { return ThisRound - AttacksUsed; } }

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
            used = new Boolean[0];
        }

        public Attack(String name, Double perRound, Int32 speed, Boolean prepped = false)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            Prepped = prepped;
            allUsable = true;

            used = new Boolean[Convert.ToInt32(Math.Ceiling(PerRound))];
        }

        public void FinishCurrentPartOfAttack()
        {
            if (AttackIsDone())
                return;

            var firstUnusedIndex = used.Count(x => x);
            used[firstUnusedIndex] = true;

            if (differingPerRound && AttackIsDone())
                allUsable = !allUsable;
        }

        private Boolean AttackIsDone()
        {
            return used[ThisRound - 1];
        }

        public void Reset()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            allUsable = true;
        }

        public void ResetPartial()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;
        }

        public void AlterInfo(String newName, Double newPerRound, Int32 newSpeed)
        {
            Name = newName;
            PerRound = newPerRound;
            Speed = newSpeed;
            used = new Boolean[Convert.ToInt32(Math.Ceiling(PerRound))];
        }
    }
}