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
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class RoundDisplay : Window
    {
        private RoundViewModel roundViewModel;
        
        public RoundDisplay(Int32 round, Queue<QueueableAttack> attacks)
        {
            InitializeComponent();
            roundViewModel = new RoundViewModel(attacks, round);
            DataContext = roundViewModel;
        }

        private void EndRound(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}