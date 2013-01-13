using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battle_Order
{
    [Serializable]
    public class Attack
    {
        public String Name { get; private set; }
        public Double PerRound { get; private set; }
        public Int32 Speed { get; private set; }
        public Double Placement { get; private set; }
        public Boolean AllUsable { get; set; }
        public Int32 ThisRound { get; set; }
        public Boolean Prepped { get; set; }
        public Boolean DifferingPerRound { get { return (PerRound - Math.Floor(PerRound)) != 0; } }
        public Int32 AttacksUsed { get { return used.Count(x => x); } }

        private Boolean[] used;

        public Int32 AttacksLeft
        {
            get
            {
                if (AllUsable)
                    return used.Count(x => !x);
                return Math.Max(0, used.Count(x => !x) - 1);
            }
        }

        public Attack(String name, Double perRound, Int32 speed, Boolean[] used, Double placement = 11, Boolean allUsable = true, Int32 thisRound = -1)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            AllUsable = allUsable;
            ThisRound = thisRound;

            used = new Boolean[Convert.ToInt16(perRound)];
            if (DifferingPerRound && !RoundUp(perRound))
                used = new Boolean[Convert.ToInt16(perRound) + 1];
        }

        public Attack(String name, Double perRound, Int32 speed, Boolean prepped = true)
        {
            Name = name;
            PerRound = perRound;
            Speed = speed;
            Prepped = prepped;

            used = new Boolean[Convert.ToInt16(perRound)];
            if (DifferingPerRound && !RoundUp(perRound))
                used = new Boolean[Convert.ToInt16(perRound) + 1];
        }

        public void SetPlacement(Int32 initiative)
        {
            if (ThisRound == -1 || used[ThisRound - 1])
            {
                Placement = 11;
                return;
            }

            var currentPartOfAttack = used.Count(x => x);

            if (DifferingPerRound)
            {
                try
                {
                    Placement = (Double)(currentPartOfAttack + 1) * (Double)Speed / (Double)ThisRound - (Double)initiative;
                }
                catch (DivideByZeroException)
                {
                    Placement = 11;
                }
            }
            else
            {
                Placement = (Double)(currentPartOfAttack + 1) * (Double)Speed / PerRound - (Double)initiative;
            }
        }

        public Boolean RoundUp(Double test)
        {
            var decimalPortion = test - Math.Floor(test);

            if (decimalPortion < .5 && test % 2 != 0)
                return false;
            else if (decimalPortion <= .5 && test % 2 == 0)
                return false;
            return true;
        }

        public void AttackFinished()
        {
            if (used[used.Length - 1])
                return;

            var firstUnused = used.Where(x => !x).First();
            firstUnused = true;

            AllUsable = true;
            if (DifferingPerRound && AllUsable && used[used.Length - 1])
                AllUsable = false;
        }

        public void Reset()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            AllUsable = true;
            Placement = 11;
            ThisRound = -1;
        }

        public void ResetPartial()
        {
            for (var i = 0; i < used.Length; i++)
                used[i] = false;

            Placement = 11;
            ThisRound = -1;
        }

        public Boolean Equals(Attack toCompare)
        {
            return Name == toCompare.Name
                   && PerRound == toCompare.PerRound
                   && Speed == toCompare.Speed;
        }
    }
}