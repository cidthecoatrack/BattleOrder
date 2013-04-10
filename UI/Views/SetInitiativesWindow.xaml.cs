using System;
using System.Collections.Generic;
using System.Windows;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;

namespace BattleOrder.UI.Views
{
    public partial class SetInitiativesWindow : Window
    {
        private SetInitiativesViewModel setParticipantInitiativesViewModel;

        public SetInitiativesWindow(IEnumerable<ActionParticipant> participants, IDice dice)
        {
            InitializeComponent();
            setParticipantInitiativesViewModel = new SetInitiativesViewModel(participants, dice);
            DataContext = setParticipantInitiativesViewModel;
        }

        private void SetInitiative(Object sender, RoutedEventArgs e)
        {
            if (setParticipantInitiativesViewModel.AllInitiativesSet)
                Close();
        }
    }
}