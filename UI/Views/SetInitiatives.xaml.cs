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