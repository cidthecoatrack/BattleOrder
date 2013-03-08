using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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