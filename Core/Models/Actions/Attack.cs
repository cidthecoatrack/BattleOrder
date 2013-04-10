using System;
using System.Reflection;

namespace Battle_Order
{
    [Serializable]
    public class Attack
    {
        public String Name { get; set; }
        public Double PerRound { get; set; }
        public Int32 Speed { get; set; }
        public Boolean Prepped { get; set; }
    }
}