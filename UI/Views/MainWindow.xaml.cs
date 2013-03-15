using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Windows;
using System.Windows.Input;
using BattleOrder.Core;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;

namespace BattleOrder.UI.Views
{
    public partial class MainWindow : Window
    {
        private PartyViewModel allParticipantsViewModel;
        private FileAccessor fileAccessor;
        private Int32 round;
        
        public MainWindow()
        {
            InitializeComponent(); 
        }

        public void Load(Object sender, RoutedEventArgs e)
        {
            Title = GetVersion();

            fileAccessor = SetupFileAccessor();
            var party = SetupParty(fileAccessor);
            allParticipantsViewModel = new PartyViewModel(party);

            DataContext = allParticipantsViewModel;
            round = 0;
        }

        private String GetVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var currentDeployment = ApplicationDeployment.CurrentDeployment;
                var version = currentDeployment.CurrentVersion;
                return String.Format("Battle Order {0}.{1}.{2}", version.Major, version.Minor, version.Revision);
            }

            return "Battle Order IN DEVELOPMENT";
        }

        private FileAccessor SetupFileAccessor()
        {
            if (!File.Exists("SaveDirectory"))
            {
                var message = "No save directory was found for the monster database and parties.  Please select a save directory.";
                System.Windows.MessageBox.Show(message, "No save directory", MessageBoxButton.OK);

                var folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowser.ShowDialog();
                var directory = folderBrowser.SelectedPath;

                FileAccessor.MakeSaveDirectoryFile(directory);
            }

            var saveDirectory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            return new FileAccessor(saveDirectory);
        }

        private IEnumerable<Participant> SetupParty(FileAccessor fileAccessor)
        {
            var result = MessageBox.Show("Load a party?", "Load?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var partyFileName = GetPartyFileNameFromUser(fileAccessor);
                return fileAccessor.LoadParty(partyFileName);

            }
            else
            {
                var partyFileName = SaveNewParty(fileAccessor);

                if (String.IsNullOrEmpty(partyFileName))
                {
                    MessageBox.Show("No party file selected. Cannot continue operations. Closing program.", "Error: No Party File to Auto Save To", MessageBoxButton.OK);
                    Close();
                }

                var party = new List<Participant>();
                fileAccessor.SaveParty(party, partyFileName);

                return party;
            }
        }

        private String GetPartyFileNameFromUser(FileAccessor fileAccessor)
        {
            var open = new System.Windows.Forms.OpenFileDialog();
            open.InitialDirectory = fileAccessor.SaveDirectory;
            var result = open.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
                return String.Empty;

            if (String.IsNullOrEmpty(open.FileName))
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return String.Empty;
            }

            return open.FileName;
        }

        private String SaveNewParty(FileAccessor fileAccessor)
        {
            var save = new System.Windows.Forms.SaveFileDialog();
            save.InitialDirectory = fileAccessor.SaveDirectory;
            var result = save.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
                return String.Empty;

            if (String.IsNullOrEmpty(save.FileName))
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return String.Empty;
            }

            return save.FileName;
        }

        private void AddActionToPartyMember(Object sender, RoutedEventArgs e)
        {
            var action = new BattleAction(String.Empty);
            var addActionWindow = new EditActionWindow(action);
            addActionWindow.Owner = this;
            addActionWindow.ShowDialog();

            if (action.IsValid())
                allParticipantsViewModel.AddActionToPartyMember(action);

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void AddPartyMember(Object sender, RoutedEventArgs e)
        {
            var partyMember = new Participant(String.Empty, false);
            var addPartyMemberWindow = new NewParticipantWindow(partyMember);
            addPartyMemberWindow.Owner = this;
            addPartyMemberWindow.ShowDialog();

            if (partyMember.IsValid())
                allParticipantsViewModel.AddParticipant(partyMember);

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void AddEnemy(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddActionToEnemy(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditEnemyAction(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditEnemy(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditPartyMember(Object sender, RoutedEventArgs e)
        {
            var editParticipantWindow = new EditParticipantWindow(allParticipantsViewModel.CurrentPartyMember);
            editParticipantWindow.Owner = this;
            editParticipantWindow.ShowDialog();

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void EditPartyMemberAction(Object sender, RoutedEventArgs e)
        {
            var editActionWindow = new EditActionWindow(allParticipantsViewModel.CurrentPartyMemberAction);
            editActionWindow.Owner = this;
            editActionWindow.ShowDialog();

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void RemoveEnemyAction(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemoveEnemyAction();
        }

        private void RemoveEnemy(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemoveParticipant(allParticipantsViewModel.CurrentEnemy);
        }

        private void RemovePartyMember(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemoveParticipant(allParticipantsViewModel.CurrentPartyMember);
            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void RemovePartyMemberAction(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemovePartyMemberAction();
            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void MakeNewBattle(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemoveAllEnemies();
            round = 0;
        }

        private void ExecuteRound(Object sender, RoutedEventArgs e)
        {
            round++;
            throw new NotImplementedException();
        }

        private void ShowPartyMemberDetails(Object sender, MouseButtonEventArgs e)
        {
            var showWindow = new ShowParticipantWindow(allParticipantsViewModel.CurrentPartyMember);
            showWindow.Owner = this;
            showWindow.ShowDialog();
        }

        private void ShowEnemyDetails(Object sender, MouseButtonEventArgs e)
        {
            var showWindow = new ShowParticipantWindow(allParticipantsViewModel.CurrentEnemy);
            showWindow.Owner = this;
            showWindow.ShowDialog();
        }

        private void ShowPartyMemberActionDetails(Object sender, MouseButtonEventArgs e)
        {
            var showWindow = new ShowActionWindow(allParticipantsViewModel.CurrentPartyMemberAction);
            showWindow.Owner = this;
            showWindow.ShowDialog();
        }

        private void ShowEnemyActionDetails(Object sender, MouseButtonEventArgs e)
        {
            var showWindow = new ShowActionWindow(allParticipantsViewModel.CurrentEnemyAction);
            showWindow.Owner = this;
            showWindow.ShowDialog();
        }
    }
}