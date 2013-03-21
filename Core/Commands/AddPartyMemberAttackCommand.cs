using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class AddPartyMemberAttackCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public AddPartyMemberAttackCommand(AllParticipantsViewModel allParticipantsViewModel)
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