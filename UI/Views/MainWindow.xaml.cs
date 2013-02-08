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
using BattleOrder.Core.Models.Participants;
using BattleOrder.UI.OldViews;

namespace BattleOrder.UI.Views
{
    public partial class MainWindow : Window
    {
        private List<Participant> party;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Load(Object sender, RoutedEventArgs e)
        {
            Title = GetVersion();

            var fileAccessor = SetupFileAccessor();
            SetupParty(fileAccessor);
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

        private void SetupParty(FileAccessor fileAccessor)
        {
            var result = MessageBox.Show("Load a party?", "Load?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var partyFileName = GetPartyFileNameFromUser(fileAccessor);
                party = fileAccessor.LoadParty(partyFileName) as List<Participant>;
            }
            else
            {
                var partyFileName = SaveNewParty(fileAccessor);

                if (String.IsNullOrEmpty(partyFileName))
                {
                    MessageBox.Show("No party file selected. Cannot continue operations. Closing program.", "Error: No Party File to Auto Save To", MessageBoxButton.OK);
                    Close();
                }

                party = new List<Participant>();
                fileAccessor.SaveParty(party, partyFileName);
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
        }

        private void AddPartyMember(Object sender, RoutedEventArgs e)
        {
            var partyMember = new Participant();
            var addPartyMemberWindow = new NewParticipant(partyMember);
            addPartyMemberWindow.Owner = this;
            addPartyMemberWindow.ShowDialog();

            if (partyMember.IsValid())
                party.Add(partyMember);
        }

        private void AddEnemy(Object sender, RoutedEventArgs e)
        {
        }

        private void AddAttackToEnemy(Object sender, RoutedEventArgs e)
        {
        }

        private void MakeNewBattle(Object sender, RoutedEventArgs e)
        {
        }

        private void ExecuteRound(Object sender, RoutedEventArgs e)
        {
        }
    }
}