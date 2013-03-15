using System;
using System.Collections.Generic;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class RoundWindow : Window
    {
        private RoundViewModel roundViewModel;
        
        public RoundWindow(Int32 round, Queue<QueueableBattleAction> actions)
        {
            InitializeComponent();
            roundViewModel = new RoundViewModel(actions, round);
            DataContext = roundViewModel;
        }

        private void EndRound(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}