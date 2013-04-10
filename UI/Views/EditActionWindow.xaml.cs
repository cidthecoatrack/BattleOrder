using System;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class EditAction : Window
    {
        private ActionViewModel actionViewModel;
        
        public EditAction(BattleAction action)
        {
            InitializeComponent();
            actionViewModel = new ActionViewModel(action);
            DataContext = actionViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IncrementPerRound(Object sender, RoutedEventArgs e)
        {
            actionViewModel.IncrementPerRound();
        }

        private void IncrementSpeed(Object sender, RoutedEventArgs e)
        {
            actionViewModel.IncrementSpeed();
        }

        private void DecrementSpeed(Object sender, RoutedEventArgs e)
        {
            actionViewModel.DecrementSpeed();
        }
    }
}