using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BattleOrder.Core;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.UI.Views;

namespace BattleOrder.UI.OldViews
{
    public partial class BattleOrderForm : Form
    {
        private List<Participant> party;
        private List<Participant> enemies;
        private List<Participant> monsterDatabase;
        private Random random;
        private Int32 roundCount;
        private String currentPartyMember;
        private String currentEnemy;
        private FileAccessor fileAccessor;

        public BattleOrderForm()
        {
            party = new List<Participant>();
            enemies = new List<Participant>();
            random = new Random();
            roundCount = 0;
            InitializeComponent();

            this.Text = GetVersion();

            SetupFileAccessor();
            SetupMonsterDatabase();
            SetupParty();
        }

        private void SetupParty()
        {
            var result = MessageBox.Show("Load a party?", "Load?", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (result == DialogResult.Yes)
            {
                var partyFileName = GetPartyFileNameFromUser();
                party = fileAccessor.LoadParty(partyFileName) as List<Participant>;
                LoadPartyIntoUi();
            }
            else
            {
                var partyFileName = SaveNewParty();
                party = new List<Participant>();
                fileAccessor.SaveParty(party, partyFileName);
            }
        }

        private void SetupMonsterDatabase()
        {
            monsterDatabase = fileAccessor.LoadMonsterDatabase() as List<Participant>;
            LoadMonsterDatabaseIntoUi();
        }

        private void SetupFileAccessor()
        {
            if (!File.Exists("SaveDirectory"))
            {
                var message = "No save directory was found for the monster database and parties.  Please select a save directory.";
                MessageBox.Show(message, "No save directory", MessageBoxButtons.OK);

                var folderBrowser = new FolderBrowserDialog();
                folderBrowser.ShowDialog();
                var directory = folderBrowser.SelectedPath;

                FileAccessor.MakeSaveDirectoryFile(directory);
            }

            var saveDirectory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            fileAccessor = new FileAccessor(saveDirectory);
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

        private String SaveNewParty()
        {
            var save = new SaveFileDialog();
            save.InitialDirectory = fileAccessor.SaveDirectory;
            var result = save.ShowDialog();

            if (result == DialogResult.Cancel)
                return String.Empty;

            if (String.IsNullOrEmpty(save.FileName))
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }

            return save.FileName;
        }

        private String GetPartyFileNameFromUser()
        {
            var open = new OpenFileDialog();
            open.InitialDirectory = fileAccessor.SaveDirectory;
            var result = open.ShowDialog();

            if (result == DialogResult.Cancel)
                return String.Empty;

            if (String.IsNullOrEmpty(open.FileName))
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }

            return open.FileName;
        }

        private void LoadMonsterDatabaseIntoUi()
        {
            foreach (var monster in monsterDatabase)
                MonsterNameCombo.Items.Add(monster.Name);

            ClearMonsterInputFields();
        }

        private void ClearMonsterInputFields()
        {
            MonsterAttackCombo.Items.Clear();
            MonsterAttackCombo.Text = String.Empty;
            MonsterNameCombo.Text = String.Empty;
            MonsterPerRoundText.Text = String.Empty;
            MonsterQuantText.Text = String.Empty;
            MonsterSpeedText.Text = String.Empty;
        }

        private void LoadPartyIntoUi()
        {
            foreach (var goodguy in party)
            {
                PartyChecklist.Items.Add(goodguy.Name);
                goodguy.PrepareForNextBattle();
            }

            ClearPartyInputFields();
        }

        private void ClearPartyInputFields()
        {            
            PartyAttackCombo.Items.Clear();
            PartyAttackCombo.Text = String.Empty;
            PartyNameText.Text = String.Empty;
            PartyPerRoundText.Text = String.Empty;
            PartySpeedText.Text = String.Empty;
        }

        private void PartyAdd_Click(object sender, EventArgs e)
        {
            var newParticipant = new Participant(String.Empty);
            var newParticipantWindow = new NewParticipantWindow(newParticipant);

            newParticipantWindow.ShowDialog();

            if (newParticipant.IsValid())
            {
                party.Add(newParticipant);
                PartyChecklist.Items.Add(newParticipant.Name, true);
            }

            fileAccessor.SaveParty(party);
        }

        private void TrimPartyInputFields()
        {
            PartyAttackCombo.Text = PartyAttackCombo.Text.Trim();
            PartyPerRoundText.Text = PartyPerRoundText.Text.Trim();
            PartySpeedText.Text = PartySpeedText.Text.Trim();
            PartyNameText.Text = PartyNameText.Text.Trim();
        }

        private String AddZerosInFrontOfNumber(Int32 number, Int32 digitAdjustment)
        {
            var stringNumber = Convert.ToString(number);

            while ((digitAdjustment - stringNumber.Length) > 0)
                stringNumber = "0" + stringNumber;

            return stringNumber;
        }

        private Int32 GetNumberFromEndOfString(String source)
        {
            var result = String.Empty;

            if (source.Last() >= '0' && source.Last() <= '9')
            {
                var split = source.Split(' ');
                result = split.Last();
            }

            var number = Convert.ToInt32(result);

            return number;
        }

        //*********************************************************************************
        //Adds new monster participant or attack on monster side
        private void MonsterAdd_Click(object sender, EventArgs e)
        {
            Participant[] newMonsters;
            BattleAction newAttack;

            MonsterAttackCombo.Text = MonsterAttackCombo.Text.Trim();
            MonsterNameCombo.Text = MonsterNameCombo.Text.Trim();
            MonsterPerRoundText.Text = MonsterPerRoundText.Text.Trim();
            MonsterQuantText.Text = MonsterQuantText.Text.Trim();
            MonsterSpeedText.Text = MonsterSpeedText.Text.Trim();
            
            try
            {
                var numforlength = Convert.ToInt32(MonsterQuantText.Text);
                newMonsters = new Participant[numforlength];

                var count = enemies.Count(x => RemoveNumberFromName(x.Name) == MonsterNameCombo.Text);
                var start = 0;

                if (InAdditionCheckBox.Checked)
                {
                    numforlength += count;
                    var monstersToRename = enemies.Where(x => RemoveNumberFromName(x.Name) == RemoveNumberFromName(MonsterNameCombo.Text));

                    foreach (var badguy in monstersToRename)
                    {
                        var ischecked = MonsterChecklist.CheckedItems.Contains(badguy.Name);
                        var stringNumber = AddZerosInFrontOfNumber(GetNumberFromEndOfString(badguy.Name), Convert.ToString(numforlength).Length);
                        var newName = String.Format("{0} {1}", RemoveNumberFromName(badguy.Name), stringNumber);
                        
                        MonsterChecklist.Items.Remove(badguy.Name);
                        badguy.AlterInfo(newName, badguy.IsNpc, badguy.IsEnemy);
                        MonsterChecklist.Items.Add(badguy.Name, ischecked);
                    }
                }
                else
                {
                    if (RemoveNumberFromName(MonsterNameCombo.Text) == MonsterNameCombo.Text && count != 0 && MonsterAttackCombo.Text == String.Empty)
                    {
                        MessageBox.Show("What are you trying to do, anyway?  Add more monsters?  Click addition.  Edit some?  Tell me where to start.  Edit an attack?  Give me a new attack to edit.  But come on, make some sense here.", "Error, you dufus.");
                        return;
                    }
                    else if (count < numforlength && count != 0)
                    {
                        var message = String.Format("There aren't that many {0}s in the battle.  You fail at counting.  And life.", MonsterNameCombo.Text);
                        MessageBox.Show(message, "Error, you dufus.");
                        return;
                    }
                    else if (RemoveNumberFromName(MonsterNameCombo.Text) != MonsterNameCombo.Text)
                    {
                        start = GetNumberFromEndOfString(MonsterNameCombo.Text);
                        if (start + numforlength > count + 1)
                        {
                            var message = String.Format("There aren't that many {0}s in the battle.  You fail at counting.  And life.", MonsterNameCombo.Text);
                            MessageBox.Show(message, "Error, you dufus.");
                            return;
                        }
                    }
                    else if (count > numforlength && MonsterAttackCombo.Text == String.Empty)
                    {
                        MessageBox.Show("Trying to edit there?  Might wanna enter in an attack.  Trying to add more?  Try checking the \"addition\" box, dufus.", "Error, you dufus.");
                        return;
                    }
                }

                if (newMonsters.Length != 1 || InAdditionCheckBox.Checked)
                {
                    foreach (var databaseEntry in monsterDatabase)
                    {
                        if (databaseEntry.Name == RemoveNumberFromName(MonsterNameCombo.Text) && MonsterAttackCombo.Text == String.Empty)
                        {
                            for (var i = 1; i <= newMonsters.Length; i++)
                            {
                                var stringNumber = AddZerosInFrontOfNumber(i + count, Convert.ToString(numforlength).Length);
                                var monsterName = String.Format("{0} {1}", MonsterNameCombo.Text, stringNumber);
                                var newParticipant = new Participant(monsterName);
                                newParticipant.AddActions(databaseEntry.Actions);
                                enemies.Add(newParticipant);
                                MonsterChecklist.Items.Add(newParticipant.Name, true);
                            }

                            MonsterNameCombo.SelectedItem = null;
                            MonsterAttackCombo.Text = String.Empty;
                            MonsterQuantText.Text = String.Empty;
                            MonsterSpeedText.Enabled = true;
                            MonsterSpeedText.Text = String.Empty;
                            MonsterPerRoundText.Text = String.Empty;
                            MonsterPerRoundText.Enabled = true;
                            InAdditionCheckBox.Checked = false;
                            return;
                        }
                    }

                    for (var i = 0; i < newMonsters.Length; i++)
                    {
                        if (start == 0)
                        {
                            if (InAdditionCheckBox.Checked)
                            {
                                var name = String.Format("{0} {1}", MonsterNameCombo.Text, AddZerosInFrontOfNumber(i + 1 + count, numforlength.ToString().Length));
                                var attack = new BattleAction(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                                newMonsters[i] = new Participant(name);
                                newMonsters[i].AddAction(attack);
                            }
                            else
                            {
                                var name = String.Format("{0} {1}", MonsterNameCombo.Text, AddZerosInFrontOfNumber(i + 1, numforlength.ToString().Length));
                                var attack = new BattleAction(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                                newMonsters[i] = new Participant(name);
                                newMonsters[i].AddAction(attack);
                            }
                        }
                        else
                        {
                            var name = String.Format("{0} {1}", RemoveNumberFromName(MonsterNameCombo.Text), AddZerosInFrontOfNumber(i + start, count.ToString().Length));
                            var attack = new BattleAction(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                            newMonsters[i] = new Participant(name);
                            newMonsters[i].AddAction(attack);
                        }
                    }
                }
                else
                {
                    foreach (Participant databaseEntry in monsterDatabase)
                    {
                        if (databaseEntry.Name == RemoveNumberFromName(MonsterNameCombo.Text) && MonsterAttackCombo.Text == String.Empty)
                        {
                            var newParticipant = new Participant(MonsterNameCombo.Text);
                            newParticipant.AddActions(databaseEntry.Actions);
                            enemies.Add(newParticipant);
                            MonsterChecklist.Items.Add(newParticipant.Name, true);
                            MonsterNameCombo.SelectedItem = null;
                            MonsterAttackCombo.Text = String.Empty;
                            MonsterQuantText.Text = String.Empty;
                            MonsterSpeedText.Enabled = true;
                            MonsterSpeedText.Text = String.Empty;
                            MonsterPerRoundText.Text = String.Empty;
                            MonsterPerRoundText.Enabled = true;
                            InAdditionCheckBox.Checked = false;
                            return;
                        }
                    }

                    var name = MonsterNameCombo.Text;
                    var attack = new BattleAction(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                    newMonsters[0] = new Participant(name);
                    newMonsters[0].AddAction(attack);
                }

                newAttack = new BattleAction(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                bool There = false;
                foreach (Participant databaseEntry in monsterDatabase)
                {
                    if (databaseEntry.Name == RemoveNumberFromName(MonsterNameCombo.Text))
                    {
                        There = true;
                        bool AttackThere = false;
                        foreach (var attack in databaseEntry.Actions)
                        {
                            if (newAttack.Equals(attack))
                            {
                                if (EditMonsterDatabaseCheckbox.Checked)
                                {
                                    if (databaseEntry.CurrentActions.Any())
                                    {
                                        var message = String.Format("Make {1} a standard current attack for {0}s?\n\nPress \"Yes\" to switch {0}'s standard current attack to {2}.\nPress \"No\" to add {1} to {0}'s standard current attacks.\nPress \"Cancel\" to not make {1} a standard current attack for {0}s.", databaseEntry.Name, newAttack.Name);
                                        var result = MessageBox.Show(message, "Edit the Monster Database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (var attack2 in databaseEntry.Actions)
                                                attack2.Prepped = false;
                                            attack.Prepped = true;
                                        }
                                        else if (result == DialogResult.No)
                                            attack.Prepped = true;
                                    }
                                    else
                                        attack.Prepped = true;
                                }
                                AttackThere = true;
                                break;
                            }
                            else if (!newAttack.Equals(attack) && newAttack.Name == attack.Name && EditMonsterDatabaseCheckbox.Checked)
                            {
                                AttackThere = true;
                                string message = String.Format("The attack data for {1} in the Monster Database is different than what you entered.  Did you mean to edit the attack data?  (This will also change the data for any monsters currently in battle)", attack.Name, databaseEntry.Name);
                                DialogResult result = MessageBox.Show(message, "Differing Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    databaseEntry.RemoveAction(attack);
                                    databaseEntry.AddAction(newAttack);
                                    
                                    foreach (var badguy in enemies)
                                    {
                                        if (RemoveNumberFromName(badguy.Name) == databaseEntry.Name)
                                        {
                                            foreach (var attack2 in badguy.Actions)
                                            {
                                                if (newAttack.Name == attack2.Name)
                                                {
                                                    bool prepit = attack2.Prepped;
                                                    badguy.RemoveAction(attack2);
                                                    newAttack.Prepped = prepit;
                                                    badguy.AddAction(newAttack);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    MonsterNameCombo.SelectedItem = null;
                                    MonsterAttackCombo.Text = "";
                                    MonsterQuantText.Text = "";
                                    MonsterSpeedText.Text = "";
                                    MonsterSpeedText.Enabled = true;
                                    MonsterPerRoundText.Text = "";
                                    MonsterPerRoundText.Enabled = true;
                                    InAdditionCheckBox.Checked = false;
                                    return;
                                }
                                else if (result == DialogResult.No)
                                {
                                    message = String.Format("Do you want to enter the attack in the Monster Database anyway, as a seperate attack for {0}s, sharing the same name as {1}?", databaseEntry.Name, attack.Name);
                                    result = MessageBox.Show(message, "Enter Anyway?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                        AttackThere = false;
                                }
                                break;
                            }
                        }
                        if (!AttackThere)
                        {
                            if (databaseEntry.CurrentActions.Any() && EditMonsterDatabaseCheckbox.Checked)
                            {
                                var message = String.Format("Make {2} a standard current attack for {0}s?\n\nPress \"Yes\" to switch {0}'s standard current attack to {1}.\nPress \"No\" to add {1} to {0}'s standard current attacks.\nPress \"Cancel\" to not make {1} a standard current attack for {0}s.", databaseEntry.Name, newAttack.Name);
                                var result = MessageBox.Show(message, "Edit the Monster Database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                    foreach (var attack in databaseEntry.Actions)
                                        attack.Prepped = false;

                                if (result == DialogResult.No || result == DialogResult.Yes)
                                    newAttack.Prepped = true;
                            }

                            databaseEntry.AddAction(newAttack);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (!There)
                {
                    var result = MessageBox.Show("Would you like to add this monster to the Monster Database?", "Add to Monster Database?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var monster = new Participant(RemoveNumberFromName(MonsterNameCombo.Text));
                        monster.AddAction(newAttack);
                        monsterDatabase.Add(monster);
                        MonsterNameCombo.Items.Add(RemoveNumberFromName(MonsterNameCombo.Text));
                    }
                }

                foreach (var newBadGuy in newMonsters)
                {
                    var exists = enemies.Any(x => x.Name == newBadGuy.Name);

                    if (!exists)
                    {
                        enemies.Add(newBadGuy);
                        MonsterChecklist.Items.Add(newBadGuy.Name, true);
                    }
                    else
                    {
                        var badguy = enemies.First(x => x.Name == newBadGuy.Name);

                        var attackExists = badguy.Actions.Any(x => x.Name == newAttack.Name);

                        if (!attackExists)
                        {
                            var attack = newBadGuy.Actions.Last();
                            attack.Prepped = false;
                            badguy.AddAction(attack);
                            if (badguy.CurrentActions.Any())
                            {
                                var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack to {1}.\nPress \"No\" to add {1} to {0}'s attacks.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, newAttack.Name);
                                var result = MessageBox.Show(message, "Add to current attacks?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    foreach (var currentAttack in badguy.CurrentActions)
                                        attack.Prepped = false;
                                    attack.Prepped = true;
                                }
                                else if (result == DialogResult.No)
                                {
                                    attack.Prepped = true;
                                }
                            }
                            else
                            {
                                attack.Prepped = true;
                            }
                        }
                        else
                        {
                            var attack = badguy.Actions.First(x => x.Name == newAttack.Name);

                            if (newAttack.Equals(attack))
                            {
                                if (attack.Prepped)
                                {
                                    if (badguy.CurrentActions.Count() == 1)
                                    {
                                        var message = String.Format("This attack has been already entered, and is prepped as {0}'s current attack.  You're being of the redundant, dufus.", badguy.Name);
                                        MessageBox.Show(message, "Error, dufus.");
                                    }
                                    else
                                    {
                                        var message = String.Format("This attack has already been entered and is prepped as one of several current attacks for {0}.  Make it the only current attack?", badguy.Name);
                                        var result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (var attack2 in badguy.Actions)
                                                attack2.Prepped = false;
                                            attack.Prepped = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (badguy.CurrentActions.Any())
                                    {
                                        var message = String.Format("This attack has already been entered.  Make it a current attack?\n\nPress \"Yes\" to switch {0}'s current attack to {1}.\nPress \"No\" to add {1} to {0}'s attacks.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, newAttack.Name);
                                        var result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (var attack2 in badguy.Actions)
                                                attack2.Prepped = false;
                                            attack.Prepped = true;
                                        }
                                        else if (result == DialogResult.No)
                                        {
                                            attack.Prepped = true;
                                        }
                                    }
                                    else
                                    {
                                        var message = String.Format("This attack has already been entered.  Make it {0}'s current attack?", badguy.Name);
                                        var result = MessageBox.Show(message, "Prep as current attack?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                            attack.Prepped = true;
                                    }
                                }
                            }
                            else
                            {
                                var message = String.Format("The attack data does not match.  Did you mean to edit the attack data for {1}'s {0}?", attack.Name, badguy.Name);
                                var result = MessageBox.Show(message, "Edit?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    badguy.RemoveAction(attack);
                                    var editedAttack = newBadGuy.Actions.Last();
                                    badguy.AddAction(editedAttack);
                                    editedAttack.Prepped = false;
                                    if (badguy.CurrentActions.Any())
                                    {
                                        message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack to {1}.\nPress \"No\" to add {1} to {0}'s attacks.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, newAttack.Name);
                                        result = MessageBox.Show(message, "Add to current attacks?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                        if (result == DialogResult.Yes)
                                            foreach (var currentAttack in badguy.CurrentActions)
                                                currentAttack.Prepped = false;

                                        if (result == DialogResult.No || result == DialogResult.Yes)
                                            editedAttack.Prepped = true;
                                    }
                                    else
                                    {
                                        editedAttack.Prepped = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter information correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MonsterNameCombo.SelectedItem = null;
            MonsterAttackCombo.Text = "";
            MonsterAttackCombo.Items.Clear();
            MonsterQuantText.Text = "";
            MonsterSpeedText.Text = "";
            MonsterSpeedText.Enabled = true;
            MonsterPerRoundText.Text = "";
            MonsterPerRoundText.Enabled = true;
            InAdditionCheckBox.Checked = false;
            fileAccessor.SaveMonsterDatabase(monsterDatabase);
        }

        private void ComputeRound()
        {
            var activeGoodGuys = party.Where(x => PartyChecklist.CheckedItems.Contains(x.Name));
            var activeBadGuys = enemies.Where(x => MonsterChecklist.CheckedItems.Contains(x.Name));
            var activeParticipants = activeGoodGuys.Union(activeBadGuys);

            var queueableAttacks = new List<QueueableBattleAction>();
            foreach (var participant in activeParticipants)
            {
                foreach (var attack in participant.CurrentActions)
                {
                    for (var count = attack.ThisRound; count > 0; count--)
                    {
                        queueableAttacks.Add(new QueueableBattleAction(participant.Name, attack, participant.Initiative));
                        attack.FinishCurrentPartOfAction();
                    }
                }
            }

            var orderedAttacks = (Queue<QueueableBattleAction>)queueableAttacks.OrderBy(x => x.Placement);

            while (orderedAttacks.Any())
            {
                var output = String.Format("The following attacks may go:\n");
                var currentPlacement = orderedAttacks.Peek().Placement;
                var title = String.Format("Round {0}: {1}", roundCount, currentPlacement);
                
                while (orderedAttacks.Peek().Placement == currentPlacement)
                    output += "\n\t" + orderedAttacks.Dequeue().Description;   

                output += String.Format("\n\n(Click \"Cancel\" to end the round early.)");

                var result = MessageBox.Show(output, title, MessageBoxButtons.OKCancel, MessageBoxIcon.None);
                if (result == DialogResult.Cancel)
                {
                    output = "Are you sure the following attacks do not need to go?\n";

                    foreach (var attack in orderedAttacks)
                        output += "\n\t" + orderedAttacks.Dequeue().Description;

                    result = MessageBox.Show(output, "End " + title + " early?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var everyone = party.Union(enemies);

                        foreach (var person in everyone)
                            foreach (var attack in person.CurrentActions)
                                attack.PrepareForNextBattle();

                        break;
                    }
                }
            }

            MessageBox.Show("Round complete.", "Round Complete");
        }

        //*********************************************************************************
        //Checks to make sure initiatives and per round values are filled
        private void CheckInitiative()
        {
            var activeGoodGuys = party.Where(x => PartyChecklist.CheckedItems.Contains(x.Name));
            var activeBadGuys = enemies.Where(x => MonsterChecklist.CheckedItems.Contains(x.Name));

            var activeParticipants = new List<Participant>();
            activeParticipants.AddRange(activeGoodGuys);
            activeParticipants.AddRange(activeBadGuys);

            foreach (var participant in activeParticipants)
            {
                foreach (var attack in participant.CurrentActions)
                {
                    if (attack.ThisRound == -1)
                    {
                        PartyNameText.Enabled = false;
                        PartyAttackCombo.Enabled = false;
                        PartyPerRoundText.Enabled = false;
                        PartySpeedText.Enabled = false;
                        InitiativeText.Enabled = true;
                        InitiativeText.Visible = true;
                        InitiativeNameLabel.Visible = true;
                        InitiativeButton.Enabled = true;
                        InitiativeButton.Visible = true;
                        InitiativeTitle.Visible = true;
                        PartyChecklist.Enabled = false;
                        MonsterChecklist.Enabled = false;
                        PartyAdd.Enabled = false;
                        MonsterAdd.Enabled = false;
                        MonsterAttackCombo.Enabled = false;
                        MonsterNameCombo.Enabled = false;
                        MonsterPerRoundText.Enabled = false;
                        MonsterQuantText.Enabled = false;
                        MonsterSpeedText.Enabled = false;
                        Battle.Enabled = false;
                        ResetBattle.Enabled = false;

                        InitiativeTitle.Text = "How many attacks this round?";
                        InitiativeButton.Text = "Set Attacks This Round";
                        InitiativeNameLabel.Text = participant.Name;
                        InitiativeAttackLabel.Visible = true;
                        InitiativeAttackLabel.Text = attack.Name;
                        return;
                    }
                    if (participant.Initiative == 0)
                    {
                        PartyNameText.Enabled = false;
                        PartyAttackCombo.Enabled = false;
                        PartyPerRoundText.Enabled = false;
                        PartySpeedText.Enabled = false;
                        InitiativeText.Enabled = true;
                        InitiativeText.Visible = true;
                        InitiativeNameLabel.Visible = true;
                        InitiativeButton.Enabled = true;
                        InitiativeButton.Visible = true;
                        InitiativeTitle.Visible = true;
                        PartyChecklist.Enabled = false;
                        MonsterChecklist.Enabled = false;
                        PartyAdd.Enabled = false;
                        MonsterAdd.Enabled = false;
                        MonsterAttackCombo.Enabled = false;
                        MonsterNameCombo.Enabled = false;
                        MonsterPerRoundText.Enabled = false;
                        MonsterQuantText.Enabled = false;
                        MonsterSpeedText.Enabled = false;
                        Battle.Enabled = false;
                        ResetBattle.Enabled = false;

                        InitiativeTitle.Text = "Initiative";
                        InitiativeButton.Text = "Set Initiative";
                        InitiativeNameLabel.Text = participant.Name;
                        return;
                    }
                }
            }

            InitiativeButton.Enabled = false; 
            InitiativeButton.Visible = false;
            InitiativeNameLabel.Visible = false;
            InitiativeAttackLabel.Visible = false;
            InitiativeText.Enabled = false; 
            InitiativeText.Visible = false;
            InitiativeTitle.Visible = false;

            foreach (var badguy in activeBadGuys)
            {
                if (badguy.Initiative == 0)
                {
                    MonsterInitiativeButton.Enabled = true; MonsterInitiativeButton.Visible = true;
                    MonsterInitiativeLabel.Visible = true;
                    MonsterInitiativeNameLabel.Visible = true;
                    MonsterInitiativeTextBox.Enabled = true; MonsterInitiativeTextBox.Visible = true;

                    MonsterInitiativeLabel.Text = "Initiative";
                    MonsterInitiativeButton.Text = "Set Initiative";
                    MonsterInitiativeNameLabel.Text = badguy.Name;
                    return;
                }
            }

            MonsterInitiativeButton.Enabled = false; MonsterInitiativeButton.Visible = false;
            MonsterInitiativeLabel.Visible = false;
            MonsterInitiativeAttackLabel.Visible = false;
            MonsterInitiativeNameLabel.Visible = false;
            MonsterInitiativeTextBox.Enabled = false; MonsterInitiativeTextBox.Visible = false;
            PartyNameText.Enabled = true;
            PartyAttackCombo.Enabled = true;
            PartyPerRoundText.Enabled = true;
            PartySpeedText.Enabled = true;
            PartyChecklist.Enabled = true;
            MonsterChecklist.Enabled = true;
            PartyAdd.Enabled = true;
            MonsterAdd.Enabled = true;
            MonsterAttackCombo.Enabled = true;
            MonsterNameCombo.Enabled = true;
            MonsterPerRoundText.Enabled = true;
            MonsterQuantText.Enabled = true;
            MonsterSpeedText.Enabled = true;
            Battle.Enabled = true;
            ResetBattle.Enabled = true;

            ComputeRound();
        }

        //*************************************************************************************
        //Method to begin round, from button click.  Assigns monster initiatives
        private void Battle_Click(object sender, EventArgs e)
        {
            roundCount++;

            PartyNameText.Text = String.Empty;
            PartyAttackCombo.Text = String.Empty;
            PartyAttackCombo.Items.Clear();
            PartySpeedText.Text = String.Empty; ;
            PartyPerRoundText.Text = String.Empty;
            MonsterNameCombo.Text = String.Empty;
            MonsterQuantText.Text = String.Empty;
            MonsterAttackCombo.Text = String.Empty;
            MonsterAttackCombo.Items.Clear();
            MonsterPerRoundText.Text = String.Empty;
            MonsterSpeedText.Text = String.Empty;

            var activeBadGuys = enemies.Where(x => MonsterChecklist.CheckedItems.Contains(x.Name));
            var activeGoodGuys = party.Where(x => PartyChecklist.CheckedItems.Contains(x.Name));

            var activeParticipants = activeBadGuys.Union(activeGoodGuys);

            foreach (var participant in activeParticipants)
            {
                participant.PrepareForNextRound();
                if (participant.IsNpc)
                {
                    var initiative = random.Next(1, 11);
                    participant.Initiative = initiative;
                }
            }

            var inactiveBadGuys = enemies.Where(x => !activeBadGuys.Contains(x));
            var inactiveGoodGuys = party.Where(x => !activeGoodGuys.Contains(x));
            var inactiveParticipants = inactiveBadGuys.Union(inactiveGoodGuys);

            foreach (var participant in inactiveParticipants)
                participant.PrepareForNextBattle();

            CheckInitiative();
        }

        //************************************************************************************
        //Clears all info from battle, preps for new battle with new enemies
        private void ResetBattle_Click(object sender, EventArgs e)
        {
            roundCount = 0;
            MonsterChecklist.Items.Clear();
            enemies.Clear();

            foreach (Participant goodguy in party)
                goodguy.PrepareForNextBattle();

            MonsterAttackCombo.Text = "";
            MonsterAttackCombo.Items.Clear();
            MonsterNameCombo.Text = "";
            MonsterPerRoundText.Text = "";
            MonsterQuantText.Text = "";
            MonsterSpeedText.Text = "";
            PartyNameText.Text = "";
            PartyAttackCombo.Text = "";
            PartyAttackCombo.Items.Clear();
            PartyPerRoundText.Text = "";
            PartySpeedText.Text = "";
        }

        //***********************************************************************************
        //Inputs initiative values for party
        private void InitiativeButton_Click(object sender, EventArgs e)
        {
            Int32 input;
            InitiativeText.Text = InitiativeText.Text.Trim();

            try 
            { 
                input = Convert.ToInt16(InitiativeText.Text); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer, you dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((input < 1) || (input > 10)) && (InitiativeTitle.Text == "Initiative"))
            {
                MessageBox.Show("Please enter an initiative roll in the range of 1 - 10, you dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var goodguy = party.First(x => x.Name == InitiativeNameLabel.Text);
            goodguy.Initiative = input;

            InitiativeText.Text = String.Empty;

            CheckInitiative();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            var activeParty = party.Where(x => PartyChecklist.Items.Contains(x.Name));
            var activeEnemies = enemies.Where(x => MonsterChecklist.Items.Contains(x.Name));
            var activeParticipants = activeParty.Union(activeEnemies);

            var linecount = 1 + activeParticipants.Sum(x => x.CurrentActions.Count());

            if (linecount == 1)
            {
                MessageBox.Show("No one has any current attacks ready.  Everyone's just twiddling their thumbs, dufus.", "No one's ready");
                return;
            }

            var tab = linecount;
            var output = String.Empty;

            foreach (var participant in activeParticipants)
            {
                foreach (var attack in participant.CurrentActions)
                {
                    output += String.Format("{0}: {1} (SP: {2}) ({3} / rd)\n", participant.Name, attack.Name, attack.Speed, attack.PerRound);
                    tab -= 40;
                    if (tab < 0)
                    {
                        output += String.Format("\n");
                        tab = linecount;
                    }
                }
            }

            MessageBox.Show(output, "All Attacks");
        }

        //********************************************************************************************
        //Fills in info for attack chosen when selected in combo box
        private void PartyAttackCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Participant goodguy in party)
                if (goodguy.Name == PartyNameText.Text)
                    foreach (var attack in goodguy.Actions)
                        if (attack.Name == PartyAttackCombo.Text)
                        {

                            PartyPerRoundText.Enabled = true;
                            PartySpeedText.Enabled = true;
                            PartyPerRoundText.Text = Convert.ToString(attack.PerRound);
                            PartySpeedText.Text = Convert.ToString(attack.Speed);
                            return;
                        }
        }

        //********************************************************************************************
        //Fills in info for attack chosen when selected in combo box
        private void MonsterAttackCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonsterPerRoundText.Enabled = true;
            MonsterSpeedText.Enabled = true;

            foreach (Participant databaseEntry in monsterDatabase)
            {
                if (databaseEntry.Name == RemoveNumberFromName(MonsterNameCombo.Text))
                    foreach (var attack in databaseEntry.Actions)
                        if (attack.Name == MonsterAttackCombo.Text)
                        {
                            MonsterPerRoundText.Enabled = true;
                            MonsterPerRoundText.Text = Convert.ToString(attack.PerRound);
                            MonsterSpeedText.Enabled = true;
                            MonsterSpeedText.Text = Convert.ToString(attack.Speed);
                            return;
                        }
            }
        }

        //********************************************************************************************
        //Inputs per round values for monsters, if needed
        private void MonsterInitiativeButton_Click(object sender, EventArgs e)
        {
            Int32 input;
            MonsterInitiativeTextBox.Text = MonsterInitiativeTextBox.Text.Trim();
            
            try 
            { 
                input = Convert.ToInt16(MonsterInitiativeTextBox.Text); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer, you dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((input < 1) || (input > 10)) && (InitiativeTitle.Text == "Initiative"))
            {
                MessageBox.Show("Please enter an initiative roll in the range of 1 - 10, you dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var badGuy = enemies.First(x => x.Name == MonsterInitiativeNameLabel.Text);
            badGuy.Initiative = input;

            MonsterInitiativeTextBox.Text = String.Empty;

            CheckInitiative();
        }

        //********************************************************************************************
        //Changes values in combo box with attacks from name in test box
        private void PartyNameText_TextChanged(object sender, EventArgs e)
        {
            foreach (var goodguy in party)
            {
                PartyAdd.Text = "Add to the current party";
                if (goodguy.Name == PartyNameText.Text)
                {
                    PartyAdd.Text = String.Format("Change {0}'s Attack", goodguy.Name);
                    currentPartyMember = goodguy.Name;
                    PartyAttackCombo.Items.Clear();

                    foreach (var attack in goodguy.Actions)
                        PartyAttackCombo.Items.Add(attack.Name);

                    if (goodguy.CurrentActions.Any())
                    {
                        var attack = goodguy.CurrentActions.First(x => PartyAttackCombo.Items.Contains(x.Name));
                        PartyAttackCombo.SelectedItem = (Object)attack.Name;
                    }

                    CharAttackLabel.Text = String.Format("Character Attack (1 of {0}):", goodguy.CurrentActions.Count());
                    return;
                }
            }
        }

        private String RemoveNumberAndParanthesesFromName(String source)
        {
            var result = source.Trim();

            while (EndsInNumberOrParantheses(result))
            {
                if (result.EndsWith(")"))
                {
                    var index = result.LastIndexOf('(');
                    result = result.Remove(index);
                }

                result = RemoveNumberFromName(result);
                result = result.Trim();
            }

            return result;
        }

        private Boolean EndsInNumberOrParantheses(String source)
        {
            return (EndsInNumber(source)) || (source.EndsWith(")"));
        }

        private Boolean EndsInNumber(String source)
        {
            return source[source.Length - 1] >= '0' && source[source.Length - 1] <= '9';
        }

        private String RemoveNumberFromName(String source)
        {
            var result = source.Trim();

            while (EndsInNumber(result))
            {
                result = result.Remove(result.Length - 1);
                result = result.Trim();
            }

            return result;
        }

        //********************************************************************************************
        //Puts selected party member in checklist into name box
        private void PartyChecklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PartyChecklist.SelectedItem != null)
                PartyNameText.Text = PartyChecklist.SelectedItem.ToString();
        }

        //********************************************************************************************
        //puts type of monster selected in monster checklist into monster name box
        private void MonsterChecklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MonsterChecklist.SelectedItem != null)
            {
                foreach (var databaseEntry in monsterDatabase)
                    if (databaseEntry.Name == RemoveNumberFromName(MonsterChecklist.SelectedItem.ToString()))
                    {
                        if (MonsterNameCombo.SelectedIndex != 0)
                            MonsterNameCombo.SelectedIndex = 0;
                        else
                            MonsterNameCombo.SelectedIndex = 1;

                        MonsterNameCombo.SelectedIndex = MonsterNameCombo.Items.IndexOf(databaseEntry.Name);
                        MonsterNameCombo.Text = MonsterChecklist.SelectedItem.ToString();
                        
                        foreach (var badguy in enemies)
                        {
                            if (badguy.Name == MonsterNameCombo.Text)
                            {
                                if (badguy.CurrentActions.Any())
                                {
                                    var attack = badguy.CurrentActions.First(x => MonsterAttackCombo.Items.Contains(x.Name));
                                    MonsterAttackCombo.SelectedItem = (Object)attack.Name;
                                }

                                EnemAttackLabel.Text = String.Format("Enemy Attack (1 of {0}):", badguy.CurrentActions.Count());
                            }
                        }

                        return;
                    }
                MonsterNameCombo.Text = MonsterChecklist.SelectedItem.ToString();
                MonsterAttackCombo.Items.Clear();

                foreach (var badguy in enemies)
                    if (badguy.Name == MonsterNameCombo.Text)
                    {
                        currentEnemy = badguy.Name;
                        MonsterQuantText.Text = "1";

                        foreach (var attack in badguy.Actions)
                            if (!MonsterAttackCombo.Items.Contains(attack.Name))
                                MonsterAttackCombo.Items.Add(attack.Name);

                        return;
                    }
            }
        }

        //********************************************************************************************
        //puts attacks from the type of monster dictated in the monster name box in the attack box's list
        private void MonsterNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonsterQuantText.Enabled = true;
            MonsterPerRoundText.Enabled = true;
            MonsterSpeedText.Enabled = true;
            MonsterAttackCombo.Items.Clear();
            MonsterAttackCombo.Text = "";
            MonsterPerRoundText.Text = "";
            MonsterSpeedText.Text = "";

            if (MonsterNameCombo.Text != "")
            {
                foreach (var databaseEntry in monsterDatabase)
                {
                    if (databaseEntry.Name == RemoveNumberFromName(MonsterNameCombo.Text))
                    {
                        currentEnemy = MonsterNameCombo.Text;

                        foreach (var attack in databaseEntry.Actions)
                            if (!MonsterAttackCombo.Items.Contains(attack.Name))
                                MonsterAttackCombo.Items.Add(attack.Name);

                        MonsterQuantText.Text = "1";

                        return;
                    }
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var selectedPartyMemberName = Convert.ToString(PartyChecklist.SelectedItem);
            var goodguy = party.First(x => x.Name == selectedPartyMemberName);

            enemies.Add(goodguy);
            MonsterChecklist.Items.Add(goodguy.Name, true);
            party.Remove(goodguy);
            PartyChecklist.Items.Remove(goodguy.Name);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedMonsterName = Convert.ToString(MonsterChecklist.SelectedItem);
            var badguy = enemies.First(x => x.Name == selectedMonsterName);

            party.Add(badguy);
            PartyChecklist.Items.Add(badguy.Name, true);
            enemies.Remove(badguy);
            MonsterChecklist.Items.Remove(badguy.Name);
        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var badGuy = enemies.First(x => MonsterChecklist.SelectedItems.Contains(x.Name));
            DisplayAllStats(badGuy);
        }

        private void DisplayAllStats(Participant person)
        {
            var output = person.Name;

            if (person.IsNpc)
                output += " (NPC)";
            else
                output += " (Player Character)";

            output += ":\n\nAttacks:\n";

            foreach (var attack in person.Actions)
            {
                output += String.Format("\t{0}:\n", attack.Name);
                output += String.Format("\t\tSpeed: {0}\tAttacks per Round: {1}\n", attack.Speed, attack.PerRound);
                output += String.Format("\t\tPrepped as Current: {0}\n", Convert.ToString(attack.Prepped));
            }

            MessageBox.Show(output, person.Name);
            return;
        }

        private void displayAllStatsForSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var goodGuy = party.First(x => PartyChecklist.SelectedItems.Contains(x.Name));
            DisplayAllStats(goodGuy);
        }

        private void makeAnNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var badGuy = enemies.First(x => MonsterChecklist.SelectedItems.Contains(x.Name));
            badGuy.AlterInfo(badGuy.Name, !badGuy.IsNpc, badGuy.IsEnemy);
        }

        private void badguycontextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (MonsterChecklist.SelectedItem != null)
            {
                ConvertToGoodGuy.Enabled = true;
                displayToolStripMenuItem.Enabled = true;
                makeAnNPCToolStripMenuItem.Enabled = true;

                var badGuy = enemies.First(x => MonsterChecklist.SelectedItems.Contains(x.Name));

                if (badGuy.IsNpc)
                    makeAnNPCToolStripMenuItem.Text = "Make selected a player character";
                else
                    makeAnNPCToolStripMenuItem.Text = "Make selected an NPC";
            }
            else
            {
                ConvertToGoodGuy.Enabled = false;
                displayToolStripMenuItem.Enabled = false;
                makeAnNPCToolStripMenuItem.Enabled = false;
            }
        }

        private void goodguycontextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (PartyChecklist.SelectedItem != null)
            {
                toolStripMenuItem2.Enabled = true;
                displayAllStatsForSelectedToolStripMenuItem.Enabled = true;
                makeAnNPCToolStripMenuItem1.Enabled = true;

                var goodGuy = party.First(x => PartyChecklist.SelectedItems.Contains(x.Name));

                if (goodGuy.IsNpc)
                    makeAnNPCToolStripMenuItem1.Text = "Make selected a player character";
                else
                    makeAnNPCToolStripMenuItem1.Text = "Make selected an NPC";
            }
            else
            {
                toolStripMenuItem2.Enabled = false;
                displayAllStatsForSelectedToolStripMenuItem.Enabled = false;
                makeAnNPCToolStripMenuItem1.Enabled = false;
            }
        }

        private void makeAnNPCToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var goodGuy = party.First(x => PartyChecklist.SelectedItems.Contains(x.Name));
            goodGuy.AlterInfo(goodGuy.Name, !goodGuy.IsNpc, goodGuy.IsEnemy);
            fileAccessor.SaveParty(party);
        }

        private void aboutBattleOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var output = this.Text;
            output += "\n\nLead Programmers:";
            output += "\n\tKarl Speer";
            output += "\n\nCreated by Moosentertainment.  Copyright Moosentertainment 2008 (c).  All rights reserved.";
            output += "\nMoosentertainment, a division of Moose Inc.";
            MessageBox.Show(output, this.Text);
        }

        private void loadAPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPartyIntoUi();
        }

        private void makeANewPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            var result = save.ShowDialog();

            if (result == DialogResult.Cancel)
                return;
            var partyFileName = save.FileName;

            if (String.IsNullOrEmpty(partyFileName))
            {
                MessageBox.Show("Empty file name.  Cannot save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fileAccessor.SaveParty(party, partyFileName);
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < MonsterChecklist.Items.Count; i++)
                MonsterChecklist.SetItemChecked(i, true);
        }

        private void selectNoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < MonsterChecklist.Items.Count; i++)
                MonsterChecklist.SetItemChecked(i, false);
        }

        //********************************************************************************************
        //Selects all party members in checkbox
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PartyChecklist.Items.Count; i++)
                PartyChecklist.SetItemChecked(i, true);
        }

        //********************************************************************************************
        //Unselects all enemies in checkbox
        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PartyChecklist.Items.Count; i++)
                PartyChecklist.SetItemChecked(i, false);
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var output = "Are you sure you wish to permanently delete all of the following currently-checked party members?\n\n";
            foreach (var source in PartyChecklist.CheckedItems)
                output += String.Format("\t{0}\n", Convert.ToString(source));
            
            var result = MessageBox.Show(output, "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var toDelete = party.Where(x => PartyChecklist.CheckedItems.Contains(x.Name)) as Queue<Participant>;

                while (toDelete.Any())
                {
                    var toRemove = toDelete.Dequeue();
                    party.Remove(toRemove);
                    PartyChecklist.Items.Remove(toRemove.Name);
                }
            }

            fileAccessor.SaveParty(party);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = "Are you sure you wish to permanently delete all of the following currently-checked enemies?\n\n";
            foreach (object source in MonsterChecklist.CheckedItems)
                output += String.Format("\t{0}\n", source.ToString());

            DialogResult result = MessageBox.Show(output, "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Queue<Participant> ToDelete = new Queue<Participant>();

                foreach (Participant badguy in enemies)
                    if (MonsterChecklist.CheckedItems.Contains(badguy.Name))
                        ToDelete.Enqueue(badguy);

                while (ToDelete.Count != 0)
                {
                    enemies.Remove(ToDelete.Peek());
                    MonsterChecklist.Items.Remove(ToDelete.Dequeue().Name);
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            var comboName = Convert.ToString(MonsterNameCombo.SelectedItem);
            var output = String.Format("Are you sure you wish to delete {0} from the Monster Database?", comboName);
            var result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var monster = monsterDatabase.First(x => x.Name == comboName);
                monsterDatabase.Remove(monster);
                MonsterNameCombo.Items.Remove(monster.Name);
            }

            fileAccessor.SaveMonsterDatabase(monsterDatabase);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var comboName = Convert.ToString(MonsterAttackCombo.SelectedItem);
            var output = String.Format("Are you sure you wish to delete {0} from {1}'s attacks in the Monster Database?", comboName, currentEnemy);
            var result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var monster = monsterDatabase.First(x => x.Name == currentEnemy);
                var attack = monster.Actions.First(x => x.Name == comboName);

                monster.RemoveAction(attack);
                MonsterAttackCombo.Items.Remove(attack.Name);
            }

            fileAccessor.SaveMonsterDatabase(monsterDatabase);
        }

        private void deleteSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var comboName = Convert.ToString(PartyAttackCombo.SelectedItem);
            var output = String.Format("Are you sure you wish to delete {0} from {1}'s attacks?", comboName, currentPartyMember);
            var result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var partyMember = party.First(x => x.Name == currentPartyMember);
                var attack = partyMember.Actions.First(x => x.Name == comboName);

                partyMember.RemoveAction(attack);
                PartyAttackCombo.Items.Remove(attack.Name);
            }

            fileAccessor.SaveParty(party);
        }

        private void PartyAttackMenu_Opening(object sender, CancelEventArgs e)
        {
            deleteSelectedToolStripMenuItem1.Enabled = (PartyAttackCombo.SelectedItem != null);
        }

        private void MonsterAttackMenu_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem3.Enabled = (MonsterAttackCombo.SelectedItem != null);
        }

        private void MonsterNameMenu_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem4.Enabled = (MonsterNameCombo.SelectedItem != null);
        }

        private void importPartyInAdditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var partyFileName = GetPartyFileNameFromUser();
            var importedParty = fileAccessor.LoadParty(partyFileName);

            party = party.Union(importedParty) as List<Participant>;

            foreach (var goodguy in party)
                goodguy.PrepareForNextBattle();

            var result = MessageBox.Show("Save as a new party?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                partyFileName = GetPartyFileNameFromUser();
                fileAccessor.SaveParty(party, partyFileName);
            }
            else
            {
                fileAccessor.SaveParty(party);
            }

            PartyAttackCombo.Items.Clear();
            PartyAttackCombo.Text = String.Empty;
            PartyNameText.Text = String.Empty;
            PartyPerRoundText.Text = String.Empty;
            PartySpeedText.Text = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}