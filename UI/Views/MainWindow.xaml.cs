using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
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
using BattleOrder.Core;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using BattleOrder.UI.OldViews;

namespace BattleOrder.UI.Views
{
    public partial class MainWindow : Window
    {
        private AllParticipantsViewModel allParticipantsViewModel;
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
            allParticipantsViewModel = new AllParticipantsViewModel(party);

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
                MessageBox.Show(message, "No save directory", MessageBoxButton.OK);

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

        private void AddAttackToPartyMember(Object sender, RoutedEventArgs e)
        {
            var attack = new Attack(String.Empty);
            var addAttackWindow = new EditAttack(attack);
            addAttackWindow.Owner = this;
            addAttackWindow.ShowDialog();

            if (attack.IsValid())
                allParticipantsViewModel.AddAttackToPartyMember(attack);

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void AddPartyMember(Object sender, RoutedEventArgs e)
        {
            var partyMember = new Participant(String.Empty, false);
            var addPartyMemberWindow = new NewParticipant(partyMember);
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

        private void AddAttackToEnemy(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditEnemyAttack(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditEnemy(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditPartyMember(Object sender, RoutedEventArgs e)
        {
            var editParticipantWindow = new EditParticipant(allParticipantsViewModel.CurrentPartyMember);
            editParticipantWindow.Owner = this;
            editParticipantWindow.ShowDialog();

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void EditPartyMemberAttack(Object sender, RoutedEventArgs e)
        {
            var editAttackWindow = new EditAttack(allParticipantsViewModel.CurrentPartyMemberAttack);
            editAttackWindow.Owner = this;
            editAttackWindow.ShowDialog();

            fileAccessor.SaveParty(allParticipantsViewModel.PartyMembers);
        }

        private void RemoveEnemyAttack(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemoveEnemyAttack();
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

        private void RemovePartyMemberAttack(Object sender, RoutedEventArgs e)
        {
            allParticipantsViewModel.RemovePartyMemberAttack();
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
    }
}