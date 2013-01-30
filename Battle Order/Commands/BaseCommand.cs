using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BattleOrder.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract Boolean CanExecute(Object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract void Execute(Object parameter);
    }
}