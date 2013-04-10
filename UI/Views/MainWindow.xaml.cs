using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Windows;
using BattleOrder.Core;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using D20Dice.Dice;

namespace BattleOrder.UI.Views
{
    public partial class MainWindow : Window
    {
        private PartyViewModel partyViewModel;
        private FileAccessor fileAccessor;
        private Int32 round;
        
        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void Load(Object sender, RoutedEventArgs e)
        {
            fileAccessor = SetupFileAccessor();
            var party = SetupParty(fileAccessor);
            partyViewModel = new PartyViewModel(party);

            Title = GetVersion();
            DataContext = partyViewModel;
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

        private IEnumerable<ActionParticipant> SetupParty(FileAccessor fileAccessor)
        {
            var result = System.Windows.MessageBox.Show("Load a party?", "Load?", MessageBoxButton.YesNo);
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
                    System.Windows.MessageBox.Show("No party file selected. Cannot continue operations. Closing program.", "Error: No Party File to Auto Save To", MessageBoxButton.OK);
                    Close();
                }

                var party = new List<ActionParticipant>();
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
                System.Windows.MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                System.Windows.MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return String.Empty;
            }

            return save.FileName;
        }

        private void AddActionToPartyMember(Object sender, RoutedEventArgs e)
        {
            var action = new BattleAction(String.Empty);
            var addActionWindow = new EditAction(action);
            addActionWindow.Owner = this;
            addActionWindow.ShowDialog();

            if (action.IsValid())
                partyViewModel.AddActionToPartyMember(action);

            fileAccessor.SaveParty(partyViewModel.Party);
        }

        private void AddPartyMember(Object sender, RoutedEventArgs e)
        {
            var partyMember = new ActionParticipant(String.Empty, false);
            var addPartyMemberWindow = new NewParticipant(partyMember);
            addPartyMemberWindow.Owner = this;
            addPartyMemberWindow.ShowDialog();

            if (partyMember.IsValid())
                partyViewModel.AddParticipant(partyMember);

            fileAccessor.SaveParty(partyViewModel.Party);
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
            var editParticipantWindow = new EditParticipant(partyViewModel.CurrentPartyMember);
            editParticipantWindow.Owner = this;
            editParticipantWindow.ShowDialog();

            fileAccessor.SaveParty(partyViewModel.Party);
        }

        private void EditPartyMemberAction(Object sender, RoutedEventArgs e)
        {
            var editActionWindow = new EditAction(partyViewModel.CurrentPartyMemberAction);
            editActionWindow.Owner = this;
            editActionWindow.ShowDialog();

            fileAccessor.SaveParty(partyViewModel.Party);
        }

        private void RemoveEnemyAction(Object sender, RoutedEventArgs e)
        {
            var message = String.Format("Are you sure that you wish to remove {0} from {1}?", partyViewModel.CurrentEnemyAction, partyViewModel.CurrentEnemy);
            var answer = MessageBox.Show(message, "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
                partyViewModel.RemoveEnemyAction();
        }

        private void RemoveEnemy(Object sender, RoutedEventArgs e)
        {
            var message = String.Format("Are you sure that you wish to remove {0}?", partyViewModel.CurrentEnemy);
            var answer = MessageBox.Show(message, "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
                partyViewModel.RemoveParticipant(partyViewModel.CurrentEnemy);
        }

        private void RemovePartyMember(Object sender, RoutedEventArgs e)
        {
            var message = String.Format("Are you sure that you wish to remove {0}?", partyViewModel.CurrentPartyMember);
            var answer = MessageBox.Show(message, "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
            {
                partyViewModel.RemoveParticipant(partyViewModel.CurrentPartyMember);
                fileAccessor.SaveParty(partyViewModel.Party);
            }
        }

        private void RemovePartyMemberAction(Object sender, RoutedEventArgs e)
        {
            var message = String.Format("Are you sure that you wish to remove {0} from {1}?", partyViewModel.CurrentPartyMemberAction, partyViewModel.CurrentPartyMember);
            var answer = MessageBox.Show(message, "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
            {
                partyViewModel.RemovePartyMemberAction();
                fileAccessor.SaveParty(partyViewModel.Party);
            }
        }

        private void MakeNewBattle(Object sender, RoutedEventArgs e)
        {
            partyViewModel.RemoveAllEnemies();
            round = 0;
        }

        private void ExecuteRound(Object sender, RoutedEventArgs e)
        {
            round++;

            var enabledParticipants = partyViewModel.GetEnabledParticipants();

            var dice = DiceFactory.Create(new Random());
            var setInitiativesWindow = new SetInitiativesWindow(enabledParticipants, dice);
            setInitiativesWindow.Owner = this;
            setInitiativesWindow.ShowDialog();

            var roundWindow = new RoundDisplayWindow(round, enabledParticipants);
            roundWindow.Owner = this;
            roundWindow.ShowDialog();
        }
    }
}