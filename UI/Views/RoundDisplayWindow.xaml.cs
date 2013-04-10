using System;
using System.Collections.Generic;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class RoundDisplayWindow : Window
    {
        private RoundViewModel roundViewModel;
        
        public RoundDisplayWindow(Int32 round, IEnumerable<ActionParticipant> participants)
        {
            InitializeComponent();

            roundViewModel = new RoundViewModel(round, participants);
            DataContext = roundViewModel;
        }

        private void EndRound(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}