using System;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class NewParticipant : Window
    {
        private ParticipantViewModel participantViewModel;

        public NewParticipant(ActionParticipant participant)
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
            var action = new BattleAction(String.Empty);
            var newActionWindow = new EditAction(action);

            newActionWindow.Owner = this;
            newActionWindow.ShowDialog();

            if (action.IsValid())
                participantViewModel.AddAction(action);

            Close();
        }
    }
}