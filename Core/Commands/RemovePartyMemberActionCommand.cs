using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemovePartyMemberActionCommand : BaseCommand
    {
        private readonly PartyViewModel allParticipantsViewModel;

        public RemovePartyMemberActionCommand(PartyViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentPartyMember != null
                && allParticipantsViewModel.CurrentPartyMemberAction != null;
        }

        public override void Execute(Object parameter) { }
    }
}