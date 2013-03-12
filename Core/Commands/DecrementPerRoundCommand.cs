﻿using System;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.Core.Commands
{
    public class DecrementPerRoundCommand : BaseCommand
    {
        private readonly AttackViewModel attackViewModel;

        public DecrementPerRoundCommand(AttackViewModel attackViewModel)
        {
            this.attackViewModel = attackViewModel;
        }

        public override Boolean CanExecute(Object parameter)
        {
            return attackViewModel.PerRound > .5;
        }

        public override void Execute(Object parameter)
        {
            attackViewModel.DecrementPerRound();
        }
    }
}