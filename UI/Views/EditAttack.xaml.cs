using System;
using System.Windows;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class EditAttack : Window
    {
        private ActionViewModel attackViewModel;
        
        public EditAttack(Attack attack)
        {
            InitializeComponent();
            attackViewModel = new ActionViewModel(attack);
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