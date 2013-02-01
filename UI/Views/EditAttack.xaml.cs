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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class EditAttack : Window
    {
        private AttackViewModel attackViewModel;
        
        public EditAttack(Attack attack)
        {
            InitializeComponent();
            attackViewModel = new AttackViewModel(attack);
            DataContext = attackViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IncrementPerRound(Object sender, RoutedEventArgs e)
        {
            attackViewModel.IncrementPerRound();
        }

        private void IncrementSpeed(Object sender, RoutedEventArgs e)
        {
            attackViewModel.IncrementSpeed();
        }

        private void DecrementSpeed(Object sender, RoutedEventArgs e)
        {
            attackViewModel.DecrementSpeed();
        }
    }
}