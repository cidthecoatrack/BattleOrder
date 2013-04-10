using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class SaveActionEditsCommand : BaseCommand
    {
        private readonly ActionViewModel actionViewModel;
        
        public SaveActionEditsCommand(ActionViewModel actionViewModel)
        {
            this.actionViewModel = actionViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return !String.IsNullOrEmpty(actionViewModel.Name) && actionViewModel.PerRound > 0;
        }

        public override void Execute(Object parameter)
        {
            actionViewModel.SaveChanges();
        }
    }
}