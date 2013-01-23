using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleOrder
{
    [Serializable]
    public class Attack
    {
        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Double Placement { get; private set; }
        public Boolean AllUsable { get; set; }
        public Boolean Prepped { get; set; }
        public Boolean DifferingPerRound { get { return (PerRound - Math.Floor(PerRound)) != 0; } }
        public Int32 AttacksUsed { get { return used.Count(x => x); } }

        private Boolean[] used;

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

            used = new Boolean[Convert.ToInt32(Math.Ceiling(PerRound))];
        }

        public void SetPlacementForNextPartOfAttack()
        {
            if (used[ThisRound - 1])
            {
                Placement = 11;
                return;
            }

            var currentPartOfAttack = used.Count(x => x);
            Placement += Placement / currentPartOfAttack;

        }

        public void SetPlacement(Int32 initiative)
        {
            if (used[ThisRound - 1])
            {
                Placement = 11;
                return;
            }

            var denominator = Convert.ToDouble(ThisRound - initiative);
            if (denominator == 0)
            {
                Placement = 11;
                return;
            }

            var currentPartOfAttack = used.Count(x => x);
            var numerator = Convert.ToDouble((currentPartOfAttack + 1) * Speed);
            Placement = numerator / denominator;
        }

        public void FinishCurrentPartOfAttack()
        {
            if (used[used.Length - 1])
                return;

            var firstUnusedIndex = used.Count(x => x);
            used[firstUnusedIndex] = true;

            if (DifferingPerRound && AllUsable && used[used.Length - 1])
                AllUsable = false;
            else
                AllUsable = true;
        }

        public void Reset()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            AllUsable = true;
            Placement = 11;
        }

        public void ResetPartial()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            Placement = 11;
        }

        public Boolean Equals(Attack toCompare)
        {
            return Name == toCompare.Name
                   && PerRound == toCompare.PerRound
                   && Speed == toCompare.Speed;
        }
    }
}