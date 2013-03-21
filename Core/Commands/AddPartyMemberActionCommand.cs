using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class AddPartyMemberActionCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public AddPartyMemberActionCommand(AllParticipantsViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentPartyMember != null;
        }

        public override void Execute(Object parameter)
        {

        }
    }
}