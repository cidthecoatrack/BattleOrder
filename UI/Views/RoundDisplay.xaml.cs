using System;
using System.Collections.Generic;
using System.Windows;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class RoundDisplay : Window
    {
        private RoundViewModel roundViewModel;
        
        public RoundDisplay(Int32 round, Queue<QueueableAction> attacks)
        {
            InitializeComponent();
            roundViewModel = new RoundViewModel(attacks, round);
            DataContext = roundViewModel;
        }

        private void EndRound(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}