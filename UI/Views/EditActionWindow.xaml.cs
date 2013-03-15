using System;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class EditActionWindow : Window
    {
        private BattleActionViewModel battleActionViewModel;

        public EditActionWindow(BattleAction action)
        {
            InitializeComponent();
            battleActionViewModel = new BattleActionViewModel(action);
            DataContext = battleActionViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IncrementPerRound(Object sender, RoutedEventArgs e)
        {
            battleActionViewModel.IncrementPerRound();
        }

        private void IncrementSpeed(Object sender, RoutedEventArgs e)
        {
            battleActionViewModel.IncrementSpeed();
        }

        private void DecrementSpeed(Object sender, RoutedEventArgs e)
        {
            battleActionViewModel.DecrementSpeed();
        }
    }
}