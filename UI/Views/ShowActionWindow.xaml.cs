using System;
using System.Windows;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class ShowActionWindow : Window
    {
        private BattleActionViewModel battleActionViewModel;

        public ShowActionWindow(BattleAction action)
        {
            InitializeComponent();
            battleActionViewModel = new BattleActionViewModel(action);
            DataContext = battleActionViewModel;
        }

        private void Close(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}