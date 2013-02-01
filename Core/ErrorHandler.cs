using System;
using System.Diagnostics;
using System.Linq;

namespace BattleOrder.Core
{
    public static class ErrorHandler
    {
        public enum ERROR_TYPES { OUT_OF_RANGE };
        
        public static String ShowDebugInfo(ERROR_TYPES errorType, object causeOfError)
        {
            var stackFrame = new StackFrame(1, true);

            var file = stackFrame.GetFileName();
            var split = file.Split('\\', '.');
            file = split[split.Length - 2];

            var method = Convert.ToString(stackFrame.GetMethod());
            split = method.Split(' ');
            method = split.Last();
            var paranthesisStart = method.IndexOf('(');
            method = method.Remove(paranthesisStart);

            var line = stackFrame.GetFileLineNumber();

            return String.Format("[ERROR: {0} {1} - {2}.{3}.{4}]", causeOfError, errorType, file, method, line);
        }
    }
}