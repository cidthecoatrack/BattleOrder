using System;
using System.Collections.Generic;
using System.Windows;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class SetInitiatives : Window
    {
        private SetParticipantInitiativesViewModel setParticipantInitiativesViewModel;
        
        public SetInitiatives(IEnumerable<Participant> participants)
        {
            InitializeComponent();
            setParticipantInitiativesViewModel = new SetParticipantInitiativesViewModel(participants);
            DataContext = setParticipantInitiativesViewModel;
        }

        private void SetInitiative(Object sender, RoutedEventArgs e)
        {
            if (setParticipantInitiativesViewModel.AllInitiativesSet)
                Close();
        }
    }
}