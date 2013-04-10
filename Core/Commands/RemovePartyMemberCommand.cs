using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class RemovePartyMemberCommand : BaseCommand
    {
        private readonly PartyViewModel allParticipantsViewModel;

        public RemovePartyMemberCommand(PartyViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentPartyMember != null;
        }

        public override void Execute(Object parameter) { }
    }
}