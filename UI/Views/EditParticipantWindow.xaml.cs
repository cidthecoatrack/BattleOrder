using System;
using System.Windows;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class EditParticipantWindow : Window
    {
        private ParticipantViewModel participantViewModel;
        
        public EditParticipantWindow(Participant participant)
        {
            InitializeComponent();
            participantViewModel = new ParticipantViewModel(participant);
            DataContext = participantViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save(Object sender, RoutedEventArgs e)
        {
            participantViewModel.SaveChanges();
            Close();
        }
    }
}