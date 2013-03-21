using System;
using System.Windows;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class NewParticipant : Window
    {
        private ParticipantViewModel participantViewModel;

        public NewParticipant(Participant participant)
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
            var attack = new Attack(String.Empty);
            var newAttackWindow = new EditAttack(attack);

            newAttackWindow.Owner = this;
            newAttackWindow.ShowDialog();

            if (attack.IsValid())
                participantViewModel.AddAttack(attack);

            Close();
        }
    }
}