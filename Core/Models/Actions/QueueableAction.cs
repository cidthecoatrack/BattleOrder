using System;

namespace BattleOrder.Core.Models.Actions
{
    public class QueueableAction
    {
        public Double Placement { get; private set; }
        public String Description { get; private set; }

        public QueueableAction(String participantName, BattleAction action, Int32 initiative)
        {
            var calculator = new PlacementCalculator(action);
            Placement = calculator.ComputePlacement(initiative);

            if (action.ThisRound == 1)
                Description = String.Format("{0}'s {1}", participantName, action.Name);
            else
                Description = String.Format("{0}'s {1} {2}", participantName, NumberToNthWord(action.Used + 1), action.Name);
        }

        private String NumberToNthWord(Int32 number)
        {
            switch (number)
            {
                case 1: return "first";
                case 2: return "second";
                case 3: return "third";
                case 4: return "fourth";
                case 5: return "fifth";
                case 6: return "sixth";
                case 7: return "seventh";
                case 8: return "eighth";
                case 9: return "ninth";
                case 10: return "tenth";
                case 11: return "eleventh";
                case 12: return "twelfth";
                case 13: return "thirteenth";
                case 14: return "fourteenth";
                case 15: return "fifteenth";
                case 16: return "sixteenth";
                case 17: return "seventeenth";
                case 18: return "eighteenth";
                case 19: return "ninteenth";
                default: return ErrorHandler.ShowDebugInfo(ErrorHandler.ERROR_TYPES.OUT_OF_RANGE, number);
            }
        }
    }
}