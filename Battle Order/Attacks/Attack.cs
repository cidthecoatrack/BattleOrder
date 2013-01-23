using System;
using System.Linq;

namespace BattleOrder.Attacks
{
    [Serializable]
    public class Attack
    {
        private Boolean[] used;
        private Boolean differingPerRound { get { return (PerRound - Math.Floor(PerRound)) != 0; } }

        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Boolean AllUsable { get; set; }
        public Boolean Prepped { get; set; }
        public Int32 AttacksUsed { get { return used.Count(x => x); } }

        public Int32 ThisRound
        {
            get
            {
                if (AllUsable)
                    return Convert.ToInt32(Math.Ceiling(PerRound));
                return Convert.ToInt32(Math.Floor(PerRound));
            }
        }

        public Int32 AttacksLeft
        {
            get
            {
                if (AllUsable)
                    return used.Count(x => !x);
                return Math.Max(0, used.Count(x => !x) - 1);
            }
        }

        public Attack(String name, Double perRound, Int32 speed, Boolean prepped = true)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            Prepped = prepped;
            AllUsable = true;

            used = new Boolean[Convert.ToInt32(Math.Ceiling(PerRound))];
        }

        public void FinishCurrentPartOfAttack()
        {
            if (AttackIsDone())
                return;

            var firstUnusedIndex = used.Count(x => x);
            used[firstUnusedIndex] = true;

            if (differingPerRound && AttackIsDone())
                AllUsable = !AllUsable;
        }

        private Boolean AttackIsDone()
        {
            return used[ThisRound - 1];
        }

        public void Reset()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            AllUsable = true;
        }

        public void ResetPartial()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;
        }
    }
}