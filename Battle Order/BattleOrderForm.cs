using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BattleOrder
{
    public partial class BattleOrderForm : Form
    {
        private List<Participant> party;
        private List<Participant> enemies;
        private List<Participant> monsterDatabase;
        private Random random;
        private Int32 roundCount;
        private Double min;
        private Boolean stop;
        private String partyFileName;
        private String currentPartyMember;
        private String currentEnemy;
        private FileAccessor fileAccessor;

        private Double minimum
        {
            get { return min; }
            set
            {
                if (value < min)
                    min = value;
                else if (value == 11)
                    min = 11;
                else if (value > 10)
                    MessageBox.Show("Error: Placement over 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public BattleOrderForm()
        {
            party = new List<Participant>();
            enemies = new List<Participant>();
            monsterDatabase = new List<Participant>();
            random = new Random();
            roundCount = 0;
            stop = false;
            InitializeComponent();

            var saveDirectory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            fileAccessor = new FileAccessor(saveDirectory);
            FileStream input;

            try
            {
                input = new FileStream(saveDirectory, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException)
            {
                FileStream output = new FileStream(saveDirectory, FileMode.OpenOrCreate, FileAccess.Read);
                output.Close();
                input = new FileStream(saveDirectory, FileMode.Open, FileAccess.Read);
            }

            while (true)
            {
                try
                {
                    var binary = new BinaryFormatter();
                    var New = (Participant)binary.Deserialize(input);
                    AddParticipant(monsterDatabase, New);
                    MonsterNameCombo.Items.Add(New.Name);
                }
                catch (SerializationException)
                {
                    break;
                }
            }

            input.Close();

            var result = MessageBox.Show("Load a party?", "Load?", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (result == DialogResult.Yes)
                LoadParty();
        }

        private void LoadParty()
        {
            var binary = new BinaryFormatter();
            var open = new OpenFileDialog();
            var result = open.ShowDialog();

            if (result == DialogResult.Cancel)
                return;
            partyFileName = open.FileName;

            if (partyFileName == "" || partyFileName == null)
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var input = new FileStream(partyFileName, FileMode.Open, FileAccess.Read);

            party = new List<Participant>();
            PartyChecklist.Items.Clear();

            while (true)
            {
                try
                {
                    var New = (Participant)binary.Deserialize(input);
                    AddParticipant(party, New);
                    PartyChecklist.Items.Add(New.Name, true);
                }
                catch (SerializationException)
                {
                    break;
                }
            }

            input.Close();

            foreach (Participant goodguy in party)
            {
                foreach (Attack attack in goodguy.Attacks)
                {
                    if (attack.Name == "(None)")
                        goodguy.Attacks.Remove(attack);
                    break;
                }
                goodguy.TotalReset();
            }

            PartyAttackCombo.Items.Clear();
            PartyAttackCombo.Text = "";
            PartyNameText.Text = "";
            PartyPerRoundText.Text = "";
            PartySpeedText.Text = "";
        }

        //***********************************************************************
        //Saves the monster database and the current party.
        private void Save()
        {
            var binary = new BinaryFormatter();
            FileStream output;
            
            if (!String.IsNullOrEmpty(partyFileName))
            {
                output = new FileStream(partyFileName, FileMode.OpenOrCreate, FileAccess.Write);

                foreach (var goodguy in party)
                    binary.Serialize(output, goodguy);

                output.Close();
            }

            output = new FileStream("C:\\Users\\Andrew Wiggin\\Documents\\Dungeons and Dragons\\Battle Order Parties\\MonsterDatabase", FileMode.OpenOrCreate, FileAccess.Write);

            foreach (var databaseEntry in monsterDatabase)
                binary.Serialize(output, databaseEntry);

            output.Close();
        }

        //***********************************************************************************
        //Method for adding new participant to battle on party's side
        private void PartyAdd_Click(object sender, EventArgs e)
        {
            Attack NewAttack;
            PartyAttackCombo.Text = PartyAttackCombo.Text.Trim();
            PartyPerRoundText.Text = PartyPerRoundText.Text.Trim();
            PartySpeedText.Text = PartySpeedText.Text.Trim();
            PartyNameText.Text = PartyNameText.Text.Trim();
            
            var NameThere = false;
            try
            {
                NewAttack = new Attack(PartyAttackCombo.Text, Convert.ToDouble(PartyPerRoundText.Text), Convert.ToInt16(PartySpeedText.Text));

                foreach (var goodguy in party)
                {
                    if (goodguy.Name == PartyNameText.Text)
                    {
                        NameThere = true;
                        stop = false;

                        foreach (var attack in goodguy.Attacks)
                        {
                            if (attack.Equals(NewAttack))
                            {
                                if (attack.Prepped)
                                {
                                    if (goodguy.CurrentAttacks.Count() == 1)
                                    {
                                        MessageBox.Show("This attack has been already entered, and is prepped as the current attack.  You're being of the redundant, dufus.", "Error, dufus.");
                                        stop = true;
                                        break;
                                    }
                                    else
                                    {
                                        string message = "This attack has already been entered and is prepped as a current attack.  Make it the only current attack?";
                                        DialogResult result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (Attack attack2 in goodguy.Attacks)
                                                attack2.Prepped = false;
                                            attack.Prepped = true;
                                        }
                                        stop = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (goodguy.CurrentAttacks.Any())
                                    {
                                        string message = String.Format("This attack has already been entered.  Make it a current attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", goodguy.Name, goodguy.CurrentAttacksToString(), NewAttack.Name);
                                        DialogResult result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (Attack attack2 in goodguy.Attacks)
                                                attack2.Prepped = false;
                                            attack.Prepped = true;
                                        }
                                        else if (result == DialogResult.No)
                                            attack.Prepped = true;
                                    }
                                    else
                                    {
                                        string message = String.Format("This attack has already been entered.  Make it {0}'s current attack?", goodguy.Name);
                                        DialogResult result = MessageBox.Show(message, "Prep as current attack?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                            attack.Prepped = true;
                                    }
                                    stop = true;
                                    break;
                                }
                            }
                            else if (!attack.Equals(NewAttack) && attack.Name == NewAttack.Name)
                            {
                                stop = true;
                                var result = MessageBox.Show("The attack data does not match.  Did you mean to edit the attack data?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    goodguy.Attacks.Remove(attack);

                                    if (goodguy.CurrentAttacks.Any())
                                    {
                                        var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", goodguy.Name, goodguy.CurrentAttacksToString(), NewAttack.Name);
                                        result = MessageBox.Show(message, "Attack already there", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        
                                        if (result == DialogResult.Yes)
                                            foreach (var attack2 in goodguy.Attacks)
                                                attack2.Prepped = false;

                                        if (result == DialogResult.No || result == DialogResult.Yes)
                                            NewAttack.Prepped = true;
                                    }
                                    else
                                    {
                                        NewAttack.Prepped = true;
                                    }

                                    goodguy.AddAttack(NewAttack);
                                    break;
                                }
                            }
                        }
                        if (!stop)
                        {
                            if (goodguy.CurrentAttacks.Any())
                            {
                                var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", goodguy.Name, goodguy.CurrentAttacksToString(), NewAttack.Name);
                                var result = MessageBox.Show(message, "Attack already there", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                    foreach (var attack in goodguy.Attacks)
                                        attack.Prepped = false;

                                if (result == DialogResult.No || result == DialogResult.Yes)
                                    NewAttack.Prepped = true;
                            }
                            else
                            {
                                NewAttack.Prepped = true;
                            }

                            goodguy.AddAttack(NewAttack);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (!NameThere)
                {
                    var result = MessageBox.Show("Is this character an NPC?", "NPC?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    var npc = (result != DialogResult.No);
                    var newPerson = new Participant(PartyNameText.Text, npc, NewAttack);

                    AddParticipant(party, newPerson);
                    PartyChecklist.Items.Add(PartyNameText.Text, true);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter information correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PartyNameText.Text = "";
            PartyAttackCombo.Text = "";
            PartyAttackCombo.Items.Clear();
            PartySpeedText.Text = "";
            PartySpeedText.Enabled = true;
            PartyPerRoundText.Text = "";
            PartyPerRoundText.Enabled = true;
            Save();
        }

        //*********************************************************************************
        //Adds zeros to front of number accordingly
        private String AdjustNumber(Int32 number, Int32 adjust)
        {
            var Number = number.ToString();

            if (Number.Length < adjust)
                while (adjust - Number.Length != 0)
                    Number = "0" + Number;

            return Number;
        }

        //*********************************************************************************
        //Adds zeros to front of number accordingly
        private Int32 GetNumber(String source)
        {
            Int32 number;
            Int32 WhereCut;
            var result = String.Empty;

            if (source[source.Length - 1] >= '0' && source[source.Length - 1] <= '9')
            {
                WhereCut = source.Length - 1;
                while (WhereCut >= 0 && source[WhereCut] != ' ')
                    WhereCut--;

                WhereCut++;

                for (var j = WhereCut; j < source.Length; j++)
                    result += source[j];
            }

            number = Convert.ToInt32(result);

            return number;
        }

        //*********************************************************************************
        //Adds new monster participant or attack on monster side
        private void MonsterAdd_Click(object sender, EventArgs e)
        {
            Participant[] NewMonsters;
            Attack NewAttack;

            MonsterAttackCombo.Text = MonsterAttackCombo.Text.Trim();
            MonsterNameCombo.Text = MonsterNameCombo.Text.Trim();
            MonsterPerRoundText.Text = MonsterPerRoundText.Text.Trim();
            MonsterQuantText.Text = MonsterQuantText.Text.Trim();
            MonsterSpeedText.Text = MonsterSpeedText.Text.Trim();
            
            var NameThere = false;
            try
            {
                var count = 0;

                NewMonsters = new Participant[Convert.ToInt32(MonsterQuantText.Text)];

                count = 0;
                var numforlength = NewMonsters.Length;
                var start = 0;

                foreach (var badguy in enemies)
                    if (EditName(badguy.Name, false) == EditName(MonsterNameCombo.Text, false))
                        count++;

                if (InAdditionCheckBox.Checked)
                {
                    numforlength += count;
                    foreach (var badguy in enemies)
                    {
                        if (EditName(badguy.Name, false) == MonsterNameCombo.Text)
                        {
                            var ischecked = MonsterChecklist.CheckedItems.Contains(badguy.Name);
                            var newName = EditName(badguy.Name, false) + " " + AdjustNumber(GetNumber(badguy.Name), numforlength.ToString().Length); 
                            MonsterChecklist.Items.Remove(badguy.Name);
                            badguy.Name = newName;
                            MonsterChecklist.Items.Add(badguy.Name, ischecked);
                        }
                    }
                }
                else
                {
                    if (EditName(MonsterNameCombo.Text, false) == MonsterNameCombo.Text && count != 0 && MonsterAttackCombo.Text == "")
                    {
                        MessageBox.Show("What are you trying to do, anyway?  Add more monsters?  Click addition.  Edit some?  Tell me where to start.  Edit an attack?  Give me a new attack to edit.  But come on, make some sense here.", "Error, you dufus.");
                        return;
                    }
                    else if (count < numforlength && count != 0)
                    {
                        string message = String.Format("There aren't that many {0}s in the battle.  You fail at counting.  And life.", MonsterNameCombo.Text);
                        MessageBox.Show(message, "Error, you dufus.");
                        return;
                    }
                    else if (EditName(MonsterNameCombo.Text, false) != MonsterNameCombo.Text)
                    {
                        start = GetNumber(MonsterNameCombo.Text);
                        if (start + numforlength > count + 1)
                        {
                            string message = String.Format("There aren't that many {0}s in the battle.  You fail at counting.  And life.", MonsterNameCombo.Text);
                            MessageBox.Show(message, "Error, you dufus.");
                            return;
                        }
                    }
                    else if (count > numforlength && MonsterAttackCombo.Text == "")
                    {
                        MessageBox.Show("Trying to edit there?  Might wanna enter in an attack.  Trying to add more?  Try checking the \"addition\" box, dufus.", "Error, you dufus.");
                        return;
                    }
                }

                if (NewMonsters.Length != 1 || InAdditionCheckBox.Checked)
                {
                    foreach (Participant databaseEntry in monsterDatabase)
                        if (databaseEntry.Name == EditName(MonsterNameCombo.Text, true) && MonsterAttackCombo.Text == "")
                        {
                            for (int i = 0; i < NewMonsters.Length; i++)
                            {
                                Participant New = new Participant(String.Format("{0} {1}", MonsterNameCombo.Text, AdjustNumber(i + 1 + count, numforlength.ToString().Length)), databaseEntry.Attacks);
                                AddParticipant(enemies, New);
                                MonsterChecklist.Items.Add(New.Name, true);
                            }
                            MonsterNameCombo.SelectedItem = null;
                            MonsterAttackCombo.Text = "";
                            MonsterQuantText.Text = "";
                            MonsterSpeedText.Enabled = true;
                            MonsterSpeedText.Text = "";
                            MonsterPerRoundText.Text = "";
                            MonsterPerRoundText.Enabled = true;
                            InAdditionCheckBox.Checked = false;
                            return;
                        }
                    for (int i = 0; i < NewMonsters.Length; i++)
                    {
                        if (start == 0)
                        {
                            if (InAdditionCheckBox.Checked)
                                NewMonsters[i] = new Participant(String.Format("{0} {1}", MonsterNameCombo.Text, AdjustNumber(i + 1 + count, numforlength.ToString().Length)), MonsterAttackCombo.Text, Convert.ToInt16(MonsterSpeedText.Text), Convert.ToDouble(MonsterPerRoundText.Text));
                            else
                                NewMonsters[i] = new Participant(String.Format("{0} {1}", MonsterNameCombo.Text, AdjustNumber(i + 1, numforlength.ToString().Length)), MonsterAttackCombo.Text, Convert.ToInt16(MonsterSpeedText.Text), Convert.ToDouble(MonsterPerRoundText.Text));
                        }
                        else
                            NewMonsters[i] = new Participant(String.Format("{0} {1}", EditName(MonsterNameCombo.Text, false), AdjustNumber(i + start, count.ToString().Length)), MonsterAttackCombo.Text, Convert.ToInt16(MonsterSpeedText.Text), Convert.ToDouble(MonsterPerRoundText.Text));
                    }
                }
                else
                {
                    foreach (Participant databaseEntry in monsterDatabase)
                        if (databaseEntry.Name == EditName(MonsterNameCombo.Text, true) && MonsterAttackCombo.Text == "")
                        {
                            Participant New = new Participant(MonsterNameCombo.Text, databaseEntry.Attacks);
                            AddParticipant(enemies, New);
                            MonsterChecklist.Items.Add(New.Name, true);
                            MonsterNameCombo.SelectedItem = null;
                            MonsterAttackCombo.Text = "";
                            MonsterQuantText.Text = "";
                            MonsterSpeedText.Enabled = true;
                            MonsterSpeedText.Text = "";
                            MonsterPerRoundText.Text = "";
                            MonsterPerRoundText.Enabled = true;
                            InAdditionCheckBox.Checked = false;
                            return;
                        }
                    NewMonsters[0] = new Participant(MonsterNameCombo.Text, MonsterAttackCombo.Text, Convert.ToInt16(MonsterSpeedText.Text), Convert.ToDouble(MonsterPerRoundText.Text));
                }

                NewAttack = new Attack(MonsterAttackCombo.Text, Convert.ToDouble(MonsterPerRoundText.Text), Convert.ToInt16(MonsterSpeedText.Text));
                bool There = false;
                foreach (Participant databaseEntry in monsterDatabase)
                {
                    if (databaseEntry.Name == EditName(MonsterNameCombo.Text, true))
                    {
                        There = true;
                        bool AttackThere = false;
                        foreach (Attack attack in databaseEntry.Attacks)
                        {
                            if (NewAttack.Equals(attack))
                            {
                                if (EditMonsterDatabaseCheckbox.Checked)
                                {
                                    if (databaseEntry.CurrentAttacks.Any())
                                    {
                                        var message = String.Format("Make {2} a standard current attack for {0}s?\n\nPress \"Yes\" to switch {0}'s standard current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s standard current attacks, in addition to {1}.\nPress \"Cancel\" to not make {2} a standard current attack for {0}s.", databaseEntry.Name, databaseEntry.CurrentAttacksToString(), NewAttack.Name);
                                        var result = MessageBox.Show(message, "Edit the Monster Database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            foreach (Attack attack2 in databaseEntry.Attacks)
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
                            else if (!NewAttack.Equals(attack) && NewAttack.Name == attack.Name && EditMonsterDatabaseCheckbox.Checked)
                            {
                                AttackThere = true;
                                string message = String.Format("The attack data for {1} in the Monster Database is different than what you entered.  Did you mean to edit the attack data?  (This will also change the data for any monsters currently in battle)", attack.Name, databaseEntry.Name);
                                DialogResult result = MessageBox.Show(message, "Differing Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    databaseEntry.Attacks.Remove(attack);
                                    databaseEntry.AddAttack(NewAttack);
                                    
                                    foreach (var badguy in enemies)
                                    {
                                        if (EditName(badguy.Name, true) == databaseEntry.Name)
                                        {
                                            foreach (var attack2 in badguy.Attacks)
                                            {
                                                if (NewAttack.Name == attack2.Name)
                                                {
                                                    bool prepit = attack2.Prepped;
                                                    badguy.Attacks.Remove(attack2);
                                                    NewAttack.Prepped = prepit;
                                                    badguy.AddAttack(NewAttack);
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
                            if (databaseEntry.CurrentAttacks.Any() && EditMonsterDatabaseCheckbox.Checked)
                            {
                                var message = String.Format("Make {2} a standard current attack for {0}s?\n\nPress \"Yes\" to switch {0}'s standard current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s standard current attacks, in addition to {1}.\nPress \"Cancel\" to not make {2} a standard current attack for {0}s.", databaseEntry.Name, databaseEntry.CurrentAttacksToString(), NewAttack.Name);
                                var result = MessageBox.Show(message, "Edit the Monster Database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                    foreach (var attack in databaseEntry.Attacks)
                                        attack.Prepped = false;

                                if (result == DialogResult.No || result == DialogResult.Yes)
                                    NewAttack.Prepped = true;
                            }

                            databaseEntry.AddAttack(NewAttack);
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
                        AddParticipant(monsterDatabase, new Participant(EditName(MonsterNameCombo.Text, true), NewAttack));
                        MonsterNameCombo.Items.Add(EditName(MonsterNameCombo.Text, true));
                    }
                }

                foreach (var newbadguy in NewMonsters)
                {
                    NameThere = false;
                    foreach (var badguy in enemies)
                    {
                        if (badguy.Name == newbadguy.Name)
                        {
                            NameThere = true;
                            stop = false;

                            foreach (var attack in badguy.Attacks)
                            {
                                if (NewAttack.Equals(attack))
                                {
                                    if (attack.Prepped)
                                    {
                                        if (badguy.CurrentAttacks.Count() == 1)
                                        {
                                            var message = String.Format("This attack has been already entered, and is prepped as {0}'s current attack.  You're being of the redundant, dufus.", badguy.Name);
                                            MessageBox.Show(message, "Error, dufus.");
                                            stop = true;
                                            break;
                                        }
                                        else
                                        {
                                            var message = String.Format("This attack has already been entered and is prepped as one of several current attacks for {0} ({1}).  Make it the only current attack?", badguy.Name, badguy.CurrentAttacksToString());
                                            var result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                            if (result == DialogResult.Yes)
                                            {
                                                foreach (var attack2 in badguy.Attacks)
                                                    attack2.Prepped = false;
                                                attack.Prepped = true;
                                            }
                                            stop = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (badguy.CurrentAttacks.Any())
                                        {
                                            var message = String.Format("This attack has already been entered.  Make it a current attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, badguy.CurrentAttacksToString(), NewAttack.Name);
                                            var result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                            if (result == DialogResult.Yes)
                                            {
                                                foreach (Attack attack2 in badguy.Attacks)
                                                    attack2.Prepped = false;
                                                attack.Prepped = true;
                                            }
                                            else if (result == DialogResult.No)
                                                attack.Prepped = true;
                                        }
                                        else
                                        {
                                            var message = String.Format("This attack has already been entered.  Make it {0}'s current attack?", badguy.Name);
                                            var result = MessageBox.Show(message, "Prep as current attack?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                            if (result == DialogResult.Yes)
                                                attack.Prepped = true;
                                        }
                                        stop = true;
                                        break;
                                    }
                                }
                                else if (!NewAttack.Equals(attack) && attack.Name == NewAttack.Name)
                                {
                                    stop = true;
                                    var message = String.Format("The attack data does not match.  Did you mean to edit the attack data for {1}'s {0}?", attack.Name, badguy.Name);
                                    var result = MessageBox.Show(message, "Edit?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (result == DialogResult.Yes)
                                    {
                                        badguy.Attacks.Remove(attack);
                                        badguy.AddAttack(newbadguy.Attacks.Last());
                                        badguy.Attacks[badguy.Attacks.Count - 1].Prepped = false;
                                        if (badguy.CurrentAttacks.Any())
                                        {
                                            message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, badguy.CurrentAttacksToString(), NewAttack.Name);
                                            result = MessageBox.Show(message, "Add to current attacks?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                            
                                            if (result == DialogResult.Yes)
                                                foreach (var attack2 in badguy.Attacks)
                                                    attack2.Prepped = false;

                                            if (result == DialogResult.No || result == DialogResult.Yes)
                                                badguy.Attacks[badguy.Attacks.Count - 1].Prepped = true;
                                        }
                                        else
                                        {
                                            badguy.Attacks[badguy.Attacks.Count - 1].Prepped = true;
                                        }
                                        break;
                                    }
                                }
                            }

                            if (!stop)
                            {
                                badguy.AddAttack(newbadguy.Attacks.Last());
                                badguy.Attacks[badguy.Attacks.Count - 1].Prepped = false;
                                if (badguy.CurrentAttacks.Any())
                                {
                                    var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", badguy.Name, badguy.CurrentAttacksToString(), NewAttack.Name);
                                    var result = MessageBox.Show(message, "Add to current attacks?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                    if (result == DialogResult.Yes)
                                    {
                                        foreach (Attack attack in badguy.Attacks)
                                            attack.Prepped = false;
                                        badguy.Attacks[badguy.Attacks.Count - 1].Prepped = true;
                                    }
                                    else if (result == DialogResult.No)
                                        badguy.Attacks[badguy.Attacks.Count - 1].Prepped = true;
                                }
                                else
                                    badguy.Attacks[badguy.Attacks.Count - 1].Prepped = true;
                            }
                            else
                                break;
                        }
                    }
                    if (!NameThere)
                    {
                        AddParticipant(enemies, newbadguy);
                        MonsterChecklist.Items.Add(newbadguy.Name, true);
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
            Save();
        }

        private void ComputeRound()
        {
            var output = String.Empty;
            var orderedAttacks = new Queue<Participant>();

            var activeParticipants = new List<Participant>();

            var activeGoodGuys = party.Where(x => PartyChecklist.CheckedItems.Contains(x.Name));
            activeParticipants.AddRange(activeGoodGuys);

            var activeBadGuys = enemies.Where(x => MonsterChecklist.CheckedItems.Contains(x.Name));
            activeParticipants.AddRange(activeBadGuys);

            foreach (var participant in activeParticipants)
                foreach (var attack in participant.CurrentAttacks)
                    attack.SetPlacement(participant.Initiative);


            while(true)
            {
                minimum = 11;

                foreach (var participant in activeParticipants)
                    foreach (var attack in participant.CurrentAttacks)
                        if (attack.Placement != 11)
                            minimum = attack.Placement;

                if (minimum == 11)
                    break;

                foreach (Participant goodguy in party)
                    if (PartyChecklist.CheckedItems.Contains(goodguy.Name))
                        foreach (Attack attack in goodguy.CurrentAttacks)
                            if (attack.Placement == minimum)
                                orderedAttacks.Enqueue(new Participant(goodguy.Name, goodguy.Initiative, attack));

                foreach (Participant badguy in enemies)
                    if (MonsterChecklist.CheckedItems.Contains(badguy.Name))
                        foreach (Attack attack in badguy.CurrentAttacks)
                            if (attack.Placement == minimum)
                                orderedAttacks.Enqueue(new Participant(badguy.Name, badguy.Initiative, attack));

                output = String.Format("The following attacks may go:\n");
                string title = String.Format("Round {0}: {1}", roundCount, orderedAttacks.Peek().SingleAttack.Placement);
                
                while (orderedAttacks.Count != 0)
                {
                    if (orderedAttacks.Peek().SingleAttack.ThisRound == 1)
                        output += String.Format("\n\t{0}'s {1}", orderedAttacks.Peek().Name, orderedAttacks.Peek().SingleAttack.Name);
                    else
                        output += String.Format("\n\t{0}'s {1} {2}", orderedAttacks.Peek().Name, numtoword(orderedAttacks.Peek().SingleAttack.AttacksUsed + 1), orderedAttacks.Peek().SingleAttack.Name);
                    orderedAttacks.Peek().SingleAttack.FinishAttack();
                    orderedAttacks.Peek().SingleAttack.SetPlacement(orderedAttacks.Peek().Initiative);
                    orderedAttacks.Dequeue();
                }

                output += String.Format("\n\n(Click \"Cancel\" to end the round early.)");

                DialogResult result = MessageBox.Show(output, title, MessageBoxButtons.OKCancel, MessageBoxIcon.None);

                if (result == DialogResult.Cancel)
                {
                    output = "Are you sure the following attacks do not need to go?\n";

                    foreach (Participant goodguy in party)
                        if (PartyChecklist.CheckedItems.Contains(goodguy.Name))
                            foreach (Attack attack in goodguy.CurrentAttacks)
                                if (attack.Placement != 11)
                                {
                                    if (attack.AttacksLeft > 1)
                                        output += String.Format("\n{0} {1}s from {2}", attack.AttacksLeft, attack.Name, goodguy.Name);
                                    else
                                        output += String.Format("\n{0} {1} from {2}", attack.AttacksLeft, attack.Name, goodguy.Name);
                                }

                    foreach (Participant badguy in enemies)
                        if (MonsterChecklist.CheckedItems.Contains(badguy.Name))
                            foreach (Attack attack in badguy.CurrentAttacks)
                                if (attack.Placement != 11)
                                {
                                    if (attack.AttacksLeft > 1)
                                        output += String.Format("\n{0} {1}s from {2}", attack.AttacksLeft, attack.Name, badguy.Name);
                                    else
                                        output += String.Format("\n{0} {1} from {2}", attack.AttacksLeft, attack.Name, badguy.Name);
                                }

                    result = MessageBox.Show(output, "End " + title + " early?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (Participant goodguy in party)
                            foreach (Attack attack in goodguy.CurrentAttacks)
                                if (attack.AttacksLeft > 0)
                                    attack.AllUsable = true;

                        foreach (Participant badguy in enemies)
                            foreach (Attack attack in badguy.CurrentAttacks)
                                if (attack.AttacksLeft > 0)
                                    attack.AllUsable = true;

                        return;
                    }
                }
            }

            MessageBox.Show("Round complete.", "Round Complete");
        }

        //*********************************************************************************
        //Checks to make sure initiatives and per round values are filled
        private void CheckInitiative()
        {
            foreach (var goodguy in party)
                if (PartyChecklist.CheckedItems.Contains(goodguy.Name))
                {
                    foreach (var attack in goodguy.CurrentAttacks)
                        if (attack.ThisRound == -1)
                        {
                            if (attack.DifferingPerRound)
                            {
                                if (attack.PerRound < 1 && !attack.AllUsable)
                                    attack.ThisRound = 0;
                                else if ((attack.PerRound < 2 && !attack.AllUsable) || (attack.PerRound < 1 && attack.AllUsable))
                                    attack.ThisRound = 1;
                                else
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
                                    InitiativeNameLabel.Text = goodguy.Name;
                                    InitiativeAttackLabel.Visible = true;
                                    InitiativeAttackLabel.Text = attack.Name;
                                    goodguy.SingleAttack = attack;
                                    return;
                                }
                            }
                            else { attack.ThisRound = (Int32)attack.PerRound; }
                        }
                    if (goodguy.Initiative == 0)
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
                        InitiativeNameLabel.Text = goodguy.Name;
                        return;
                    }
                }

            InitiativeButton.Enabled = false; 
            InitiativeButton.Visible = false;
            InitiativeNameLabel.Visible = false;
            InitiativeAttackLabel.Visible = false;
            InitiativeText.Enabled = false; 
            InitiativeText.Visible = false;
            InitiativeTitle.Visible = false;

            foreach (Participant badguy in enemies)
                if (MonsterChecklist.CheckedItems.Contains(badguy.Name))
                {
                    foreach (Attack attack in badguy.CurrentAttacks)
                        if (attack.ThisRound == -1)
                        {
                            if (attack.DifferingPerRound)
                            {
                                if (attack.PerRound < 1 && !attack.AllUsable)
                                    attack.ThisRound = 0;
                                else if ((attack.PerRound < 2 && !attack.AllUsable) || (attack.PerRound < 1 && !attack.AllUsable))
                                    attack.ThisRound = 1;
                                else
                                {
                                    MonsterInitiativeButton.Enabled = true; MonsterInitiativeButton.Visible = true;
                                    MonsterInitiativeLabel.Visible = true;
                                    MonsterInitiativeNameLabel.Visible = true;
                                    MonsterInitiativeTextBox.Enabled = true; MonsterInitiativeTextBox.Visible = true;
                                    
                                    MonsterInitiativeLabel.Text = "How many attacks this round?";
                                    MonsterInitiativeButton.Text = "Set Attacks This Round";
                                    MonsterInitiativeNameLabel.Text = badguy.Name;
                                    MonsterInitiativeAttackLabel.Text = attack.Name;
                                    MonsterInitiativeAttackLabel.Visible = true;
                                    badguy.SingleAttack = attack;
                                    return;
                                }
                            }
                            else { attack.ThisRound = (int)attack.PerRound; }
                        }
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
            int initiative;

            PartyNameText.Text = "";
            PartyAttackCombo.Text = "";
            PartyAttackCombo.Items.Clear();
            PartySpeedText.Text = "";
            PartyPerRoundText.Text = "";
            MonsterNameCombo.Text = "";
            MonsterQuantText.Text = "";
            MonsterAttackCombo.Text = "";
            MonsterAttackCombo.Items.Clear();
            MonsterPerRoundText.Text = "";
            MonsterSpeedText.Text = "";

            foreach (Participant badguy in enemies)
            {
                if (MonsterChecklist.CheckedItems.Contains(badguy.Name))
                {
                    badguy.Reset();
                    if (badguy.NPC)
                    {
                        initiative = random.Next(1, 11);
                        badguy.Initiative = initiative;
                    }
                }
                else
                    badguy.TotalReset();
            }
            foreach (Participant goodguy in party)
            {
                if (PartyChecklist.CheckedItems.Contains(goodguy.Name))
                {
                    goodguy.Reset();
                    if (goodguy.NPC)
                    {
                        initiative = random.Next(1, 11);
                        goodguy.Initiative = initiative;
                    }
                }
                else
                    goodguy.TotalReset();
            }

            CheckInitiative();
        }

        //*********************************************************************************
        //Converts numbers to words, useful for listing attacks in prompts
        private string numtoword(int number)
        {
            switch (number)
            {
                case 0:
                    return "";
                case 1:
                    return "first";
                case 2:
                    return "second";
                case 3:
                    return "third";
                case 4:
                    return "fourth";
                case 5:
                    return "fifth";
                case 6:
                    return "sixth";
                case 7:
                    return "seventh";
                case 8:
                    return "eighth";
                case 9:
                    return "ninth";
                case 10:
                    return "tenth";
                case 11:
                    return "eleventh";
                case 12:
                    return "twelfth";
                case 13:
                    return "thirteenth";
                case 14:
                    return "fourteenth";
                case 15:
                    return "fifteenth";
                case 16:
                    return "sixteenth";
                case 17:
                    return "seventeenth";
                case 18:
                    return "eighteenth";
                case 19:
                    return "ninteenth";
                default:
                    return "[Error: out of range]";
            }
        }

        //************************************************************************************
        //Clears all info from battle, preps for new battle with new enemies
        private void ResetBattle_Click(object sender, EventArgs e)
        {
            roundCount = 0;
            MonsterChecklist.Items.Clear();
            enemies.Clear();

            foreach (Participant goodguy in party)
                goodguy.TotalReset();

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
            int input;
            InitiativeText.Text = InitiativeText.Text.Trim();

            try { input = Convert.ToInt16(InitiativeText.Text); }
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

            foreach (Participant goodguy in party)
                if (PartyChecklist.CheckedItems.Contains(goodguy.Name) && goodguy.Name == InitiativeNameLabel.Text)
                {
                    if (InitiativeTitle.Text == "Initiative")
                        goodguy.Initiative = input;
                    else if (InitiativeTitle.Text == "How many attacks this round?")
                    {
                        if (input > goodguy.SingleAttack.AttacksLeft)
                        {
                            MessageBox.Show("You don't have that many attacks to use, dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        InitiativeAttackLabel.Visible = false;
                        foreach (Attack attack in goodguy.CurrentAttacks)
                            if (attack == goodguy.SingleAttack)
                                attack.ThisRound = input;
                    }
                }

            InitiativeText.Text = "";

            CheckInitiative();
        }

        //************************************************************************************
        //Displays all current attacks
        private void ShowButton_Click(object sender, EventArgs e)
        {            
            string output = "";
            int linecount = 1; int tab;

            foreach (Participant goodguy in party)
                if (PartyChecklist.Items.Contains(goodguy.Name))
                    linecount += goodguy.CurrentAttacks.Length;
            foreach (Participant badguy in enemies)
                if (MonsterChecklist.Items.Contains(badguy.Name))
                    linecount += badguy.CurrentAttacks.Length;

            if (linecount == 1)
            {
                MessageBox.Show("No one has any current attacks ready.  Everyone's just twiddling their thumbs, dufus.", "No one's ready");
                return;
            }

            foreach (string alphaname in PartyChecklist.CheckedItems)
                foreach (Participant goodguy in party)
                    if (alphaname == goodguy.Name)
                    {
                        foreach (Attack attack in goodguy.CurrentAttacks)
                            output += String.Format("{0}: {1} (SP: {2}) ({3} / rd)\n", goodguy.Name, attack.Name, attack.Speed, attack.PerRound);
                        break;
                    }
            output += String.Format("\n"); tab = linecount;
            foreach (string alphaname in MonsterChecklist.CheckedItems)
                foreach (Participant badguy in enemies)
                    if (alphaname == badguy.Name)
                        foreach (Attack attack in badguy.CurrentAttacks)
                        {

                            output += String.Format("{0}: {1} (SP: {2}) ({3} / rd)   |   ", badguy.Name, attack.Name, attack.Speed, attack.PerRound);
                            tab -= 40;
                            if (tab < 0)
                            {
                                output += String.Format("\n");
                                tab = linecount;
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
                    foreach (Attack attack in goodguy.Attacks)
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
                if (databaseEntry.Name == EditName(MonsterNameCombo.Text, true))
                    foreach (Attack attack in databaseEntry.Attacks)
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
            int input;
            MonsterInitiativeTextBox.Text = MonsterInitiativeTextBox.Text.Trim();
            
            try { input = Convert.ToInt16(MonsterInitiativeTextBox.Text); }
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

            foreach (Participant badguy in enemies)
                if (MonsterChecklist.CheckedItems.Contains(badguy.Name) && badguy.Name == MonsterInitiativeNameLabel.Text)
                {
                    if (MonsterInitiativeLabel.Text == "Initiative")
                        badguy.Initiative = input;
                    else if (MonsterInitiativeLabel.Text == "How many attacks this round?")
                    {
                        if (input > badguy.SingleAttack.AttacksLeft)
                        {
                            MessageBox.Show("You don't have that many attacks to use, dufus.", "Error, you dufus.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        MonsterInitiativeAttackLabel.Visible = false;
                        foreach (Attack attack in badguy.CurrentAttacks)
                            if (attack == badguy.SingleAttack)
                                attack.ThisRound = input;
                    }
                }

            MonsterInitiativeTextBox.Text = "";

            CheckInitiative();
        }

        //********************************************************************************************
        //Changes values in combo box with attacks from name in test box
        private void PartyNameText_TextChanged(object sender, EventArgs e)
        {
            foreach (Participant goodguy in party)
            {
                PartyAdd.Text = "Add to the current party";
                if (goodguy.Name == PartyNameText.Text)
                {
                    PartyAdd.Text = String.Format("Change {0}'s Attack", goodguy.Name);
                    currentPartyMember = goodguy.Name;
                    PartyAttackCombo.Items.Clear();
                    foreach (Attack attack in goodguy.Attacks)
                        PartyAttackCombo.Items.Add(attack.Name);
                    if (goodguy.CurrentAttacks.Length != 0)
                        foreach (string attackname in PartyAttackCombo.Items)
                            if (goodguy.CurrentAttacks[0].Name == attackname)
                            {
                                PartyAttackCombo.SelectedItem = (object)attackname;
                                break;
                            }
                    CharAttackLabel.Text = String.Format("Character Attack (1 of {0}):", goodguy.CurrentAttacks.Length);
                    return;
                }
            }
        }

        //********************************************************************************************
        //Edits monster name to remove numbers from name
        private string EditName(string source, bool Full)
        {
            string result = ""; int WhereCut;

            if ((source[source.Length - 1] >= '0' && source[source.Length - 1] <= '9') || (source[source.Length - 1] == ')' && Full))
            {
                WhereCut = source.Length - 1;
                while (WhereCut >= 0 && source[WhereCut] != ' ' && source[WhereCut] >= '0' && source[WhereCut] <= '9')
                    WhereCut--;

                if (Full)
                    if (source[WhereCut - 1] == ')' || source[WhereCut] == ')')
                    {
                        while (WhereCut >= 0 && source[WhereCut] != '(')
                            WhereCut--;
                        WhereCut--;
                    }

                for (int j = 0; j < WhereCut; j++)
                    result += source[j];
            }
            else
                result = source;

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
                foreach (Participant databaseEntry in monsterDatabase)
                    if (databaseEntry.Name == EditName(MonsterChecklist.SelectedItem.ToString(), true))
                    {
                        if (MonsterNameCombo.SelectedIndex != 0)
                            MonsterNameCombo.SelectedIndex = 0;
                        else
                            MonsterNameCombo.SelectedIndex = 1;
                        MonsterNameCombo.SelectedIndex = MonsterNameCombo.Items.IndexOf(databaseEntry.Name);
                        MonsterNameCombo.Text = MonsterChecklist.SelectedItem.ToString();
                        
                        foreach (Participant badguy in enemies)
                        {
                            if (badguy.Name == MonsterNameCombo.Text)
                            {
                                if (badguy.CurrentAttacks.Length != 0)
                                    foreach (string attackname in MonsterAttackCombo.Items)
                                        if (badguy.CurrentAttacks[0].Name == attackname)
                                        {
                                            MonsterAttackCombo.SelectedItem = (object)attackname;
                                            break;
                                        }
                                EnemAttackLabel.Text = String.Format("Enemy Attack (1 of {0}):", badguy.CurrentAttacks.Length);
                            }
                        }

                        return;
                    }
                MonsterNameCombo.Text = MonsterChecklist.SelectedItem.ToString();
                MonsterAttackCombo.Items.Clear();
                foreach (Participant badguy in enemies)
                    if (badguy.Name == MonsterNameCombo.Text)
                    {
                        currentEnemy = badguy.Name;
                        MonsterQuantText.Text = "1";

                        foreach (Attack attack in badguy.Attacks)
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
                    if (databaseEntry.Name == EditName(MonsterNameCombo.Text, true))
                    {
                        currentEnemy = MonsterNameCombo.Text;

                        foreach (var attack in databaseEntry.Attacks)
                            if (!MonsterAttackCombo.Items.Contains(attack.Name))
                                MonsterAttackCombo.Items.Add(attack.Name);

                        MonsterQuantText.Text = "1";

                        return;
                    }
                }
            }
        }

        //********************************************************************************************
        //puts new participants in their respective list alphabetically
        private void AddParticipant(List<Participant> list, Participant NewParticipant)
        {
            list.Add(NewParticipant);
        }

        //********************************************************************************************
        //switches goodguys to badguys
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (Participant goodguy in party)
                if (PartyChecklist.SelectedItem.ToString() == goodguy.Name)
                {
                    AddParticipant(enemies, goodguy);
                    MonsterChecklist.Items.Add(goodguy.Name, true);
                    party.Remove(goodguy);
                    PartyChecklist.Items.Remove(goodguy.Name);
                    return;
                }
        }

        //********************************************************************************************
        //Switches badguys to goodguys
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Participant badguy in enemies)
                if (MonsterChecklist.SelectedItem.ToString() == badguy.Name)
                {
                    AddParticipant(party, badguy);
                    PartyChecklist.Items.Add(badguy.Name, true);
                    enemies.Remove(badguy);
                    MonsterChecklist.Items.Remove(badguy.Name);
                    break;
                }

            MonsterNameCombo.Text = "";
            MonsterAttackCombo.Text = "";
            MonsterAttackCombo.Items.Clear();
            MonsterQuantText.Text = "";
            MonsterSpeedText.Text = "";
            MonsterSpeedText.Enabled = true;
            MonsterPerRoundText.Text = "";
            MonsterPerRoundText.Enabled = true;
        }

        //********************************************************************************************
        //Preps a badguy to display all of its stats
        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Participant badguy in enemies)
                if (MonsterChecklist.SelectedItem.ToString() == badguy.Name)
                    DisplayAllStats(badguy);
        }

        //********************************************************************************************
        //Displays all stats for a given participant
        private void DisplayAllStats(Participant person)
        {
            string output = person.Name;
            if (person.NPC)
                output += " (NPC)";
            else
                output += " (Player Character)";
            output += ":\n\nAttacks:\n";
            foreach (Attack attack in person.Attacks)
            {
                output += String.Format("\t{0}:\n", attack.Name);
                output += String.Format("\t\tSpeed: {0}\tAttacks per Round: {1}\n", attack.Speed, attack.PerRound);
                string maybe;
                if (attack.Prepped)
                    maybe = "Yes";
                else
                    maybe = "No";
                output += String.Format("\t\tPrepped as Current: {0}\n", maybe);
            }
            MessageBox.Show(output, person.Name);
            return;
        }

        //********************************************************************************************
        //Preps a goodguy to have all of his or her stats displayed
        private void displayAllStatsForSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Participant goodguy in party)
                if (PartyChecklist.SelectedItem.ToString() == goodguy.Name)
                    DisplayAllStats(goodguy);
        }

        //********************************************************************************************
        //Switches a badguy to or from being an NPC
        private void makeAnNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Participant badguy in enemies)
                if (MonsterChecklist.SelectedItem.ToString() == badguy.Name)
                {
                    if (badguy.NPC)
                        badguy.NPC = false;
                    else
                        badguy.NPC = true;
                }
            Save();
        }

        //********************************************************************************************
        //Makes sure the "switch npc" command is to or from, according to selected, for badguy list
        private void badguycontextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (MonsterChecklist.SelectedItem != null)
            {
                toolStripMenuItem1.Enabled = true;
                displayToolStripMenuItem.Enabled = true;
                makeAnNPCToolStripMenuItem.Enabled = true;
                
                foreach (Participant badguy in enemies)
                    if (MonsterChecklist.SelectedItem.ToString() == badguy.Name)
                    {
                        if (badguy.NPC)
                            makeAnNPCToolStripMenuItem.Text = "Make selected a player character";
                        else
                            makeAnNPCToolStripMenuItem.Text = "Make selected an NPC";
                    }
            }
            else
            {
                toolStripMenuItem1.Enabled = false;
                displayToolStripMenuItem.Enabled = false;
                makeAnNPCToolStripMenuItem.Enabled = false;
            }
        }

        //********************************************************************************************
        //Makes sure the "switch npc" command is to or from, according to selected, for goodguy list
        private void goodguycontextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (PartyChecklist.SelectedItem != null)
            {
                toolStripMenuItem2.Enabled = true;
                displayAllStatsForSelectedToolStripMenuItem.Enabled = true;
                makeAnNPCToolStripMenuItem1.Enabled = true;
                
                foreach (Participant goodguy in party)
                    if (PartyChecklist.SelectedItem.ToString() == goodguy.Name)
                    {
                        if (goodguy.NPC)
                            makeAnNPCToolStripMenuItem1.Text = "Make selected a player character";
                        else
                            makeAnNPCToolStripMenuItem1.Text = "Make selected an NPC";
                    }
            }
            else
            {
                toolStripMenuItem2.Enabled = false;
                displayAllStatsForSelectedToolStripMenuItem.Enabled = false;
                makeAnNPCToolStripMenuItem1.Enabled = false;
            }
        }

        //********************************************************************************************
        //Switches a goodguy to or from being an NPC
        private void makeAnNPCToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Participant goodguy in party)
                if (PartyChecklist.SelectedItem.ToString() == goodguy.Name)
                {
                    if (goodguy.NPC)
                        goodguy.NPC = false;
                    else
                        goodguy.NPC = true;
                }
            Save();
        }

        //********************************************************************************************
        //Displays basic information about the program
        private void aboutBattleOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = this.Text;
            output += "\n\nLead Programmers:";
            output += "\n\tKarl Speer";
            output += "\n\tAndrew Wiggin";
            output += "\n\tMooseman";
            output += "\n\tKlarx";
            output += "\n\tDJ Fraktal";
            output += "\n\nCreated by Moosentertainment.  Copyright Moosentertainment 2008 (c).  All rights reserved.";
            output += "\nMoosentertainment, a division of Moose Inc.";
            MessageBox.Show(output, this.Text);
        }

        //********************************************************************************************
        //Loads a party from a file
        private void loadAPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadParty();
        }

        //********************************************************************************************
        //Opens a new file for a new party
        private void makeANewPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter binary = new BinaryFormatter();
            DialogResult result;
            SaveFileDialog save = new SaveFileDialog();
            result = save.ShowDialog();

            if (result == DialogResult.Cancel)
                return;
            partyFileName = save.FileName;

            if (partyFileName == "" || partyFileName == null)
            {
                MessageBox.Show("Empty file name.  Cannot save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Save();
        }

        //********************************************************************************************
        //Selects all enemies in checkbox
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MonsterChecklist.Items.Count; i++)
                MonsterChecklist.SetItemChecked(i, true);
        }

        //********************************************************************************************
        //Unselects all enemies in checkbox
        private void selectNoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MonsterChecklist.Items.Count; i++)
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

        //********************************************************************************************
        //Deletes all selected party members from the checkbox
        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = "Are you sure you wish to permanently delete all of the following currently-checked party members?\n\n";
            foreach (object source in PartyChecklist.CheckedItems)
                output += String.Format("\t{0}\n", source.ToString());
            
            DialogResult result = MessageBox.Show(output, "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Queue<Participant> ToDelete = new Queue<Participant>();
                
                foreach (Participant goodguy in party)
                    if (PartyChecklist.CheckedItems.Contains(goodguy.Name))
                        ToDelete.Enqueue(goodguy);

                while (ToDelete.Count != 0)
                {
                    party.Remove(ToDelete.Peek());
                    PartyChecklist.Items.Remove(ToDelete.Dequeue().Name);
                }
            }
            Save();
        }

        //********************************************************************************************
        //Deletes all selected opponents from the checkbox
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

        //********************************************************************************************
        //Allows user to delete a monster from the monster database
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string output = String.Format("Are you sure you wish to delete {0} from the Monster Database?", MonsterNameCombo.SelectedItem.ToString());
            DialogResult result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                foreach (Participant databaseEntry in monsterDatabase)
                    if (databaseEntry.Name == MonsterNameCombo.SelectedItem.ToString())
                    {
                        monsterDatabase.Remove(databaseEntry);
                        MonsterNameCombo.Items.Remove(databaseEntry.Name);
                        return;
                    }
            Save();
        }

        //********************************************************************************************
        //Allows user to delete an attack from a monster in the monster database
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string output = String.Format("Are you sure you wish to delete {0} from {1}'s attacks in the Monster Database?", MonsterAttackCombo.SelectedItem.ToString(), currentEnemy);
            DialogResult result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                foreach (Participant databaseEntry in monsterDatabase)
                    if (databaseEntry.Name == currentEnemy)
                        foreach (Attack attack in databaseEntry.Attacks)
                            if (attack.Name == MonsterAttackCombo.SelectedItem.ToString())
                            {
                                databaseEntry.Attacks.Remove(attack);
                                MonsterAttackCombo.Items.Remove(attack.Name);
                                return;
                            }
            Save();
        }

        //********************************************************************************************
        //Allows user to delete an attack from a party member
        private void deleteSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string output = String.Format("Are you sure you wish to delete {0} from {1}'s attacks?",PartyAttackCombo.SelectedItem.ToString(), currentPartyMember);
            DialogResult result = MessageBox.Show(output, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
                foreach(Participant goodguy in party)
                    if (goodguy.Name == currentPartyMember)
                        foreach(Attack attack in goodguy.Attacks)
                            if (attack.Name == PartyAttackCombo.SelectedItem.ToString())
                            {
                                goodguy.Attacks.Remove(attack);
                                PartyAttackCombo.Items.Remove(attack.Name);
                                return;
                            }
            Save();
        }

        //********************************************************************************************
        //Makes sure option involving a selected item are not enabled
        private void PartyAttackMenu_Opening(object sender, CancelEventArgs e)
        {
            if (PartyAttackCombo.SelectedItem == null)
                deleteSelectedToolStripMenuItem1.Enabled = false;
            else
                deleteSelectedToolStripMenuItem1.Enabled = true;
        }

        //********************************************************************************************
        //Makes sure option involving a selected item are not enabled
        private void MonsterAttackMenu_Opening(object sender, CancelEventArgs e)
        {
            if (MonsterAttackCombo.SelectedItem == null)
                toolStripMenuItem3.Enabled = false;
            else
                toolStripMenuItem3.Enabled = true;
        }

        //********************************************************************************************
        //Makes sure option involving a selected item are not enabled
        private void MonsterNameMenu_Opening(object sender, CancelEventArgs e)
        {
            if (MonsterNameCombo.SelectedItem == null)
                toolStripMenuItem4.Enabled = false;
            else
                toolStripMenuItem4.Enabled = true;
        }

        //********************************************************************************************
        //Imports a party into the current party.
        private void importPartyInAdditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult result = open.ShowDialog();
            BinaryFormatter binary = new BinaryFormatter();

            if (result == DialogResult.Cancel)
                return;
            partyFileName = open.FileName;

            if (partyFileName == "" || partyFileName == null)
            {
                MessageBox.Show("Empty file name.  Cannot open the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileStream input = new FileStream(partyFileName, FileMode.Open, FileAccess.Read);

            while (true)
            {
                try
                {
                    Participant New = (Participant)binary.Deserialize(input);
                    AddParticipant(party, New);
                    PartyChecklist.Items.Add(New.Name, true);
                }
                catch (SerializationException)
                {
                    break;
                }
            }

            input.Close();

            foreach (Participant goodguy in party)
            {
                foreach (Attack attack in goodguy.Attacks)
                {
                    if (attack.Name == "(None)")
                        goodguy.Attacks.Remove(attack);
                    break;
                }
                goodguy.TotalReset();
            }

            result = MessageBox.Show("Save as a new party?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveFileDialog save = new SaveFileDialog();
                result = save.ShowDialog();

                if (result == DialogResult.Cancel)
                    return;
                partyFileName = save.FileName;
                Save();
            }
            else
                Save();

            PartyAttackCombo.Items.Clear();
            PartyAttackCombo.Text = "";
            PartyNameText.Text = "";
            PartyPerRoundText.Text = "";
            PartySpeedText.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}