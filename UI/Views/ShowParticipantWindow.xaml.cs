using System;
using System.Windows;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class ShowParticipantWindow : Window
    {
        private ParticipantViewModel participantViewModel;

        public ShowParticipantWindow(Participant participant)
        {
            InitializeComponent();
            participantViewModel = new ParticipantViewModel(participant);
            DataContext = participantViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}