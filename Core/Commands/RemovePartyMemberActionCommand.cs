using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemovePartyMemberActionCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public RemovePartyMemberActionCommand(AllParticipantsViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentPartyMember != null
                && allParticipantsViewModel.CurrentPartyMemberAttack != null;
        }

        public override void Execute(Object parameter) { }
    }
}