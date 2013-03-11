﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class EditEnemyCommand : BaseCommand
    {
        private readonly AllParticipantsViewModel allParticipantsViewModel;

        public EditEnemyCommand(AllParticipantsViewModel allParticipantsViewModel)
        {
            this.allParticipantsViewModel = allParticipantsViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return allParticipantsViewModel.CurrentEnemy != null;
        }

        public override void Execute(Object parameter)
        {

        }
    }
}