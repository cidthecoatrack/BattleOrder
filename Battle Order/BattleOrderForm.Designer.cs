namespace BattleOrder
{
    partial class BattleOrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleOrderForm));
            this.PartyNameText = new System.Windows.Forms.TextBox();
            this.MonsterQuantText = new System.Windows.Forms.TextBox();
            this.MonsterChecklist = new System.Windows.Forms.CheckedListBox();
            this.badguycontextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAnNPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PartyChecklist = new System.Windows.Forms.CheckedListBox();
            this.goodguycontextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.displayAllStatsForSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAnNPCToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonsterSpeedText = new System.Windows.Forms.TextBox();
            this.PartyPerRoundText = new System.Windows.Forms.TextBox();
            this.PartySpeedText = new System.Windows.Forms.TextBox();
            this.PartyAdd = new System.Windows.Forms.Button();
            this.MonsterAdd = new System.Windows.Forms.Button();
            this.MonsterPerRoundText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EnemAttackLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CharAttackLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Battle = new System.Windows.Forms.Button();
            this.ResetBattle = new System.Windows.Forms.Button();
            this.InitiativeNameLabel = new System.Windows.Forms.Label();
            this.InitiativeText = new System.Windows.Forms.TextBox();
            this.InitiativeButton = new System.Windows.Forms.Button();
            this.InitiativeTitle = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.MonsterAttackCombo = new System.Windows.Forms.ComboBox();
            this.MonsterAttackMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.PartyAttackMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PartyAttackCombo = new System.Windows.Forms.ComboBox();
            this.MonsterInitiativeLabel = new System.Windows.Forms.Label();
            this.MonsterInitiativeButton = new System.Windows.Forms.Button();
            this.MonsterInitiativeTextBox = new System.Windows.Forms.TextBox();
            this.MonsterInitiativeNameLabel = new System.Windows.Forms.Label();
            this.InitiativeAttackLabel = new System.Windows.Forms.Label();
            this.MonsterInitiativeAttackLabel = new System.Windows.Forms.Label();
            this.MonsterNameCombo = new System.Windows.Forms.ComboBox();
            this.MonsterNameMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.InAdditionCheckBox = new System.Windows.Forms.CheckBox();
            this.EditMonsterDatabaseCheckbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeANewPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPartyInAdditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutBattleOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.badguycontextMenuStrip1.SuspendLayout();
            this.goodguycontextMenuStrip2.SuspendLayout();
            this.MonsterAttackMenu.SuspendLayout();
            this.PartyAttackMenu.SuspendLayout();
            this.MonsterNameMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PartyNameText
            // 
            this.PartyNameText.Location = new System.Drawing.Point(563, 112);
            this.PartyNameText.Name = "PartyNameText";
            this.PartyNameText.Size = new System.Drawing.Size(175, 20);
            this.PartyNameText.TabIndex = 1;
            this.PartyNameText.TextChanged += new System.EventHandler(this.PartyNameText_TextChanged);
            // 
            // MonsterQuantText
            // 
            this.MonsterQuantText.Location = new System.Drawing.Point(144, 137);
            this.MonsterQuantText.Name = "MonsterQuantText";
            this.MonsterQuantText.Size = new System.Drawing.Size(74, 20);
            this.MonsterQuantText.TabIndex = 8;
            // 
            // MonsterChecklist
            // 
            this.MonsterChecklist.ContextMenuStrip = this.badguycontextMenuStrip1;
            this.MonsterChecklist.FormattingEnabled = true;
            this.MonsterChecklist.Location = new System.Drawing.Point(242, 93);
            this.MonsterChecklist.Name = "MonsterChecklist";
            this.MonsterChecklist.Size = new System.Drawing.Size(146, 244);
            this.MonsterChecklist.Sorted = true;
            this.MonsterChecklist.TabIndex = 99;
            this.MonsterChecklist.SelectedIndexChanged += new System.EventHandler(this.MonsterChecklist_SelectedIndexChanged);
            // 
            // badguycontextMenuStrip1
            // 
            this.badguycontextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.displayToolStripMenuItem,
            this.makeAnNPCToolStripMenuItem,
            this.selectAllToolStripMenuItem1,
            this.selectNoneToolStripMenuItem1,
            this.dToolStripMenuItem});
            this.badguycontextMenuStrip1.Name = "contextMenuStrip1";
            this.badguycontextMenuStrip1.Size = new System.Drawing.Size(232, 136);
            this.badguycontextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.badguycontextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuItem1.Text = "Make selected a good guy";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.displayToolStripMenuItem.Text = "Display all attacks for selected";
            this.displayToolStripMenuItem.Click += new System.EventHandler(this.displayToolStripMenuItem_Click);
            // 
            // makeAnNPCToolStripMenuItem
            // 
            this.makeAnNPCToolStripMenuItem.Name = "makeAnNPCToolStripMenuItem";
            this.makeAnNPCToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.makeAnNPCToolStripMenuItem.Text = "Make an NPC";
            this.makeAnNPCToolStripMenuItem.Click += new System.EventHandler(this.makeAnNPCToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.selectAllToolStripMenuItem1.Text = "Check All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem1_Click);
            // 
            // selectNoneToolStripMenuItem1
            // 
            this.selectNoneToolStripMenuItem1.Name = "selectNoneToolStripMenuItem1";
            this.selectNoneToolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.selectNoneToolStripMenuItem1.Text = "Check None";
            this.selectNoneToolStripMenuItem1.Click += new System.EventHandler(this.selectNoneToolStripMenuItem1_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.dToolStripMenuItem.Text = "Delete Checked";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // PartyChecklist
            // 
            this.PartyChecklist.ContextMenuStrip = this.goodguycontextMenuStrip2;
            this.PartyChecklist.FormattingEnabled = true;
            this.PartyChecklist.HorizontalScrollbar = true;
            this.PartyChecklist.Location = new System.Drawing.Point(406, 93);
            this.PartyChecklist.Name = "PartyChecklist";
            this.PartyChecklist.Size = new System.Drawing.Size(151, 244);
            this.PartyChecklist.Sorted = true;
            this.PartyChecklist.TabIndex = 999;
            this.PartyChecklist.SelectedIndexChanged += new System.EventHandler(this.PartyChecklist_SelectedIndexChanged);
            // 
            // goodguycontextMenuStrip2
            // 
            this.goodguycontextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.displayAllStatsForSelectedToolStripMenuItem,
            this.makeAnNPCToolStripMenuItem1,
            this.selectAllToolStripMenuItem,
            this.selectNoneToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem});
            this.goodguycontextMenuStrip2.Name = "contextMenuStrip1";
            this.goodguycontextMenuStrip2.Size = new System.Drawing.Size(232, 136);
            this.goodguycontextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.goodguycontextMenuStrip2_Opening);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuItem2.Text = "Make selected a bad guy";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // displayAllStatsForSelectedToolStripMenuItem
            // 
            this.displayAllStatsForSelectedToolStripMenuItem.Name = "displayAllStatsForSelectedToolStripMenuItem";
            this.displayAllStatsForSelectedToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.displayAllStatsForSelectedToolStripMenuItem.Text = "Display all attacks for selected";
            this.displayAllStatsForSelectedToolStripMenuItem.Click += new System.EventHandler(this.displayAllStatsForSelectedToolStripMenuItem_Click);
            // 
            // makeAnNPCToolStripMenuItem1
            // 
            this.makeAnNPCToolStripMenuItem1.Name = "makeAnNPCToolStripMenuItem1";
            this.makeAnNPCToolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.makeAnNPCToolStripMenuItem1.Text = "Make an NPC";
            this.makeAnNPCToolStripMenuItem1.Click += new System.EventHandler(this.makeAnNPCToolStripMenuItem1_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // selectNoneToolStripMenuItem
            // 
            this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
            this.selectNoneToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.selectNoneToolStripMenuItem.Text = "Select None";
            this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.selectNoneToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Checked";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
            // 
            // MonsterSpeedText
            // 
            this.MonsterSpeedText.Location = new System.Drawing.Point(167, 216);
            this.MonsterSpeedText.Name = "MonsterSpeedText";
            this.MonsterSpeedText.Size = new System.Drawing.Size(51, 20);
            this.MonsterSpeedText.TabIndex = 11;
            // 
            // PartyPerRoundText
            // 
            this.PartyPerRoundText.Location = new System.Drawing.Point(693, 212);
            this.PartyPerRoundText.Name = "PartyPerRoundText";
            this.PartyPerRoundText.Size = new System.Drawing.Size(45, 20);
            this.PartyPerRoundText.TabIndex = 4;
            // 
            // PartySpeedText
            // 
            this.PartySpeedText.Location = new System.Drawing.Point(693, 186);
            this.PartySpeedText.Name = "PartySpeedText";
            this.PartySpeedText.Size = new System.Drawing.Size(45, 20);
            this.PartySpeedText.TabIndex = 3;
            // 
            // PartyAdd
            // 
            this.PartyAdd.BackColor = System.Drawing.SystemColors.Control;
            this.PartyAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartyAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PartyAdd.Location = new System.Drawing.Point(563, 247);
            this.PartyAdd.Name = "PartyAdd";
            this.PartyAdd.Size = new System.Drawing.Size(175, 40);
            this.PartyAdd.TabIndex = 5;
            this.PartyAdd.Text = "Add to the current party";
            this.PartyAdd.UseVisualStyleBackColor = false;
            this.PartyAdd.Click += new System.EventHandler(this.PartyAdd_Click);
            // 
            // MonsterAdd
            // 
            this.MonsterAdd.BackColor = System.Drawing.SystemColors.Control;
            this.MonsterAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonsterAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MonsterAdd.Location = new System.Drawing.Point(21, 293);
            this.MonsterAdd.Name = "MonsterAdd";
            this.MonsterAdd.Size = new System.Drawing.Size(197, 39);
            this.MonsterAdd.TabIndex = 13;
            this.MonsterAdd.Text = "Add to the current enemies";
            this.MonsterAdd.UseVisualStyleBackColor = false;
            this.MonsterAdd.Click += new System.EventHandler(this.MonsterAdd_Click);
            // 
            // MonsterPerRoundText
            // 
            this.MonsterPerRoundText.Location = new System.Drawing.Point(167, 242);
            this.MonsterPerRoundText.Name = "MonsterPerRoundText";
            this.MonsterPerRoundText.Size = new System.Drawing.Size(51, 20);
            this.MonsterPerRoundText.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Enemy Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Quantity of Enemy:";
            // 
            // EnemAttackLabel
            // 
            this.EnemAttackLabel.AutoSize = true;
            this.EnemAttackLabel.BackColor = System.Drawing.Color.Transparent;
            this.EnemAttackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnemAttackLabel.Location = new System.Drawing.Point(18, 170);
            this.EnemAttackLabel.Name = "EnemAttackLabel";
            this.EnemAttackLabel.Size = new System.Drawing.Size(93, 16);
            this.EnemAttackLabel.TabIndex = 17;
            this.EnemAttackLabel.Text = "Enemy Attack:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Attacks per Round:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(560, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Character Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(560, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Attacks per Round:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(560, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Attack Speed:";
            // 
            // CharAttackLabel
            // 
            this.CharAttackLabel.AutoSize = true;
            this.CharAttackLabel.BackColor = System.Drawing.Color.Transparent;
            this.CharAttackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CharAttackLabel.Location = new System.Drawing.Point(560, 141);
            this.CharAttackLabel.Name = "CharAttackLabel";
            this.CharAttackLabel.Size = new System.Drawing.Size(109, 16);
            this.CharAttackLabel.TabIndex = 22;
            this.CharAttackLabel.Text = "Character Attack:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Attack Speed:";
            // 
            // Battle
            // 
            this.Battle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Battle.Location = new System.Drawing.Point(406, 392);
            this.Battle.Name = "Battle";
            this.Battle.Size = new System.Drawing.Size(151, 39);
            this.Battle.TabIndex = 24;
            this.Battle.Text = "Begin Round";
            this.Battle.UseVisualStyleBackColor = true;
            this.Battle.Click += new System.EventHandler(this.Battle_Click);
            // 
            // ResetBattle
            // 
            this.ResetBattle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetBattle.Location = new System.Drawing.Point(242, 392);
            this.ResetBattle.Name = "ResetBattle";
            this.ResetBattle.Size = new System.Drawing.Size(146, 40);
            this.ResetBattle.TabIndex = 14;
            this.ResetBattle.Text = "New Battle";
            this.ResetBattle.UseVisualStyleBackColor = true;
            this.ResetBattle.Click += new System.EventHandler(this.ResetBattle_Click);
            // 
            // InitiativeNameLabel
            // 
            this.InitiativeNameLabel.AutoSize = true;
            this.InitiativeNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.InitiativeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitiativeNameLabel.Location = new System.Drawing.Point(565, 388);
            this.InitiativeNameLabel.Name = "InitiativeNameLabel";
            this.InitiativeNameLabel.Size = new System.Drawing.Size(53, 16);
            this.InitiativeNameLabel.TabIndex = 28;
            this.InitiativeNameLabel.Text = "(Name)";
            this.InitiativeNameLabel.Visible = false;
            // 
            // InitiativeText
            // 
            this.InitiativeText.Enabled = false;
            this.InitiativeText.Location = new System.Drawing.Point(566, 365);
            this.InitiativeText.Name = "InitiativeText";
            this.InitiativeText.Size = new System.Drawing.Size(40, 20);
            this.InitiativeText.TabIndex = 21;
            this.InitiativeText.Visible = false;
            // 
            // InitiativeButton
            // 
            this.InitiativeButton.Enabled = false;
            this.InitiativeButton.Location = new System.Drawing.Point(612, 363);
            this.InitiativeButton.Name = "InitiativeButton";
            this.InitiativeButton.Size = new System.Drawing.Size(126, 23);
            this.InitiativeButton.TabIndex = 22;
            this.InitiativeButton.Text = "Set";
            this.InitiativeButton.UseVisualStyleBackColor = true;
            this.InitiativeButton.Visible = false;
            this.InitiativeButton.Click += new System.EventHandler(this.InitiativeButton_Click);
            // 
            // InitiativeTitle
            // 
            this.InitiativeTitle.AutoSize = true;
            this.InitiativeTitle.BackColor = System.Drawing.Color.Transparent;
            this.InitiativeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitiativeTitle.Location = new System.Drawing.Point(563, 349);
            this.InitiativeTitle.Name = "InitiativeTitle";
            this.InitiativeTitle.Size = new System.Drawing.Size(56, 16);
            this.InitiativeTitle.TabIndex = 1001;
            this.InitiativeTitle.Text = "Initiative";
            this.InitiativeTitle.Visible = false;
            // 
            // ShowButton
            // 
            this.ShowButton.BackColor = System.Drawing.SystemColors.Control;
            this.ShowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowButton.Location = new System.Drawing.Point(242, 348);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(315, 38);
            this.ShowButton.TabIndex = 6;
            this.ShowButton.Text = "Show All Current Attacks";
            this.ShowButton.UseVisualStyleBackColor = false;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // MonsterAttackCombo
            // 
            this.MonsterAttackCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.MonsterAttackCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.MonsterAttackCombo.ContextMenuStrip = this.MonsterAttackMenu;
            this.MonsterAttackCombo.FormattingEnabled = true;
            this.MonsterAttackCombo.Location = new System.Drawing.Point(21, 189);
            this.MonsterAttackCombo.Name = "MonsterAttackCombo";
            this.MonsterAttackCombo.Size = new System.Drawing.Size(197, 21);
            this.MonsterAttackCombo.Sorted = true;
            this.MonsterAttackCombo.TabIndex = 10;
            this.MonsterAttackCombo.SelectedIndexChanged += new System.EventHandler(this.MonsterAttackCombo_SelectedIndexChanged);
            // 
            // MonsterAttackMenu
            // 
            this.MonsterAttackMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.MonsterAttackMenu.Name = "ComboBoxMenu";
            this.MonsterAttackMenu.Size = new System.Drawing.Size(192, 26);
            this.MonsterAttackMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MonsterAttackMenu_Opening);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem3.Text = "Delete Selected Attack";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // PartyAttackMenu
            // 
            this.PartyAttackMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedToolStripMenuItem1});
            this.PartyAttackMenu.Name = "ComboBoxMenu";
            this.PartyAttackMenu.Size = new System.Drawing.Size(192, 26);
            this.PartyAttackMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PartyAttackMenu_Opening);
            // 
            // deleteSelectedToolStripMenuItem1
            // 
            this.deleteSelectedToolStripMenuItem1.Name = "deleteSelectedToolStripMenuItem1";
            this.deleteSelectedToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.deleteSelectedToolStripMenuItem1.Text = "Delete Selected Attack";
            this.deleteSelectedToolStripMenuItem1.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem1_Click);
            // 
            // PartyAttackCombo
            // 
            this.PartyAttackCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PartyAttackCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PartyAttackCombo.ContextMenuStrip = this.PartyAttackMenu;
            this.PartyAttackCombo.FormattingEnabled = true;
            this.PartyAttackCombo.Location = new System.Drawing.Point(563, 160);
            this.PartyAttackCombo.Name = "PartyAttackCombo";
            this.PartyAttackCombo.Size = new System.Drawing.Size(175, 21);
            this.PartyAttackCombo.Sorted = true;
            this.PartyAttackCombo.TabIndex = 2;
            this.PartyAttackCombo.SelectedIndexChanged += new System.EventHandler(this.PartyAttackCombo_SelectedIndexChanged);
            // 
            // MonsterInitiativeLabel
            // 
            this.MonsterInitiativeLabel.AutoSize = true;
            this.MonsterInitiativeLabel.BackColor = System.Drawing.Color.Transparent;
            this.MonsterInitiativeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonsterInitiativeLabel.Location = new System.Drawing.Point(18, 347);
            this.MonsterInitiativeLabel.Name = "MonsterInitiativeLabel";
            this.MonsterInitiativeLabel.Size = new System.Drawing.Size(184, 16);
            this.MonsterInitiativeLabel.TabIndex = 1010;
            this.MonsterInitiativeLabel.Text = "How many attacks this round?";
            this.MonsterInitiativeLabel.Visible = false;
            // 
            // MonsterInitiativeButton
            // 
            this.MonsterInitiativeButton.Enabled = false;
            this.MonsterInitiativeButton.Location = new System.Drawing.Point(67, 363);
            this.MonsterInitiativeButton.Name = "MonsterInitiativeButton";
            this.MonsterInitiativeButton.Size = new System.Drawing.Size(151, 23);
            this.MonsterInitiativeButton.TabIndex = 20;
            this.MonsterInitiativeButton.Text = "Set";
            this.MonsterInitiativeButton.UseVisualStyleBackColor = true;
            this.MonsterInitiativeButton.Visible = false;
            this.MonsterInitiativeButton.Click += new System.EventHandler(this.MonsterInitiativeButton_Click);
            // 
            // MonsterInitiativeTextBox
            // 
            this.MonsterInitiativeTextBox.Enabled = false;
            this.MonsterInitiativeTextBox.Location = new System.Drawing.Point(21, 365);
            this.MonsterInitiativeTextBox.Name = "MonsterInitiativeTextBox";
            this.MonsterInitiativeTextBox.Size = new System.Drawing.Size(40, 20);
            this.MonsterInitiativeTextBox.TabIndex = 19;
            this.MonsterInitiativeTextBox.Visible = false;
            // 
            // MonsterInitiativeNameLabel
            // 
            this.MonsterInitiativeNameLabel.AutoSize = true;
            this.MonsterInitiativeNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.MonsterInitiativeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonsterInitiativeNameLabel.Location = new System.Drawing.Point(18, 388);
            this.MonsterInitiativeNameLabel.Name = "MonsterInitiativeNameLabel";
            this.MonsterInitiativeNameLabel.Size = new System.Drawing.Size(53, 16);
            this.MonsterInitiativeNameLabel.TabIndex = 1007;
            this.MonsterInitiativeNameLabel.Text = "(Name)";
            this.MonsterInitiativeNameLabel.Visible = false;
            // 
            // InitiativeAttackLabel
            // 
            this.InitiativeAttackLabel.AutoSize = true;
            this.InitiativeAttackLabel.BackColor = System.Drawing.Color.Transparent;
            this.InitiativeAttackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitiativeAttackLabel.Location = new System.Drawing.Point(565, 407);
            this.InitiativeAttackLabel.Name = "InitiativeAttackLabel";
            this.InitiativeAttackLabel.Size = new System.Drawing.Size(53, 16);
            this.InitiativeAttackLabel.TabIndex = 1014;
            this.InitiativeAttackLabel.Text = "(Attack)";
            this.InitiativeAttackLabel.Visible = false;
            // 
            // MonsterInitiativeAttackLabel
            // 
            this.MonsterInitiativeAttackLabel.AutoSize = true;
            this.MonsterInitiativeAttackLabel.BackColor = System.Drawing.Color.Transparent;
            this.MonsterInitiativeAttackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonsterInitiativeAttackLabel.Location = new System.Drawing.Point(18, 407);
            this.MonsterInitiativeAttackLabel.Name = "MonsterInitiativeAttackLabel";
            this.MonsterInitiativeAttackLabel.Size = new System.Drawing.Size(53, 16);
            this.MonsterInitiativeAttackLabel.TabIndex = 1015;
            this.MonsterInitiativeAttackLabel.Text = "(Attack)";
            this.MonsterInitiativeAttackLabel.Visible = false;
            // 
            // MonsterNameCombo
            // 
            this.MonsterNameCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.MonsterNameCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.MonsterNameCombo.ContextMenuStrip = this.MonsterNameMenu;
            this.MonsterNameCombo.FormattingEnabled = true;
            this.MonsterNameCombo.Location = new System.Drawing.Point(21, 109);
            this.MonsterNameCombo.Name = "MonsterNameCombo";
            this.MonsterNameCombo.Size = new System.Drawing.Size(197, 21);
            this.MonsterNameCombo.Sorted = true;
            this.MonsterNameCombo.TabIndex = 7;
            this.MonsterNameCombo.SelectedIndexChanged += new System.EventHandler(this.MonsterNameCombo_SelectedIndexChanged);
            // 
            // MonsterNameMenu
            // 
            this.MonsterNameMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4});
            this.MonsterNameMenu.Name = "ComboBoxMenu";
            this.MonsterNameMenu.Size = new System.Drawing.Size(349, 26);
            this.MonsterNameMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MonsterNameMenu_Opening);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(348, 22);
            this.toolStripMenuItem4.Text = "Delete Selected Monster from the Monster Database";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // InAdditionCheckBox
            // 
            this.InAdditionCheckBox.AutoSize = true;
            this.InAdditionCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.InAdditionCheckBox.Location = new System.Drawing.Point(144, 160);
            this.InAdditionCheckBox.Name = "InAdditionCheckBox";
            this.InAdditionCheckBox.Size = new System.Drawing.Size(74, 17);
            this.InAdditionCheckBox.TabIndex = 9;
            this.InAdditionCheckBox.Text = "in addition";
            this.toolTip1.SetToolTip(this.InAdditionCheckBox, "Only use this when adding additional monsters to a number of monsters already in " +
                    "battle.  i.e., if you\'ve already got 3 werewolves and want 5 more, use the \"in a" +
                    "ddition\" checkbox");
            this.InAdditionCheckBox.UseVisualStyleBackColor = false;
            // 
            // EditMonsterDatabaseCheckbox
            // 
            this.EditMonsterDatabaseCheckbox.AutoSize = true;
            this.EditMonsterDatabaseCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.EditMonsterDatabaseCheckbox.Location = new System.Drawing.Point(40, 270);
            this.EditMonsterDatabaseCheckbox.Name = "EditMonsterDatabaseCheckbox";
            this.EditMonsterDatabaseCheckbox.Size = new System.Drawing.Size(152, 17);
            this.EditMonsterDatabaseCheckbox.TabIndex = 1016;
            this.EditMonsterDatabaseCheckbox.Text = "Edit the Monster Database";
            this.toolTip1.SetToolTip(this.EditMonsterDatabaseCheckbox, "Only use this when you want to change the standard attack for a monster as saved " +
                    "in the Monster Database");
            this.EditMonsterDatabaseCheckbox.UseVisualStyleBackColor = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(767, 29);
            this.menuStrip1.TabIndex = 1018;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAPartyToolStripMenuItem,
            this.makeANewPartyToolStripMenuItem,
            this.importPartyInAdditionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadAPartyToolStripMenuItem
            // 
            this.loadAPartyToolStripMenuItem.Name = "loadAPartyToolStripMenuItem";
            this.loadAPartyToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.loadAPartyToolStripMenuItem.Text = "Load a Party";
            this.loadAPartyToolStripMenuItem.Click += new System.EventHandler(this.loadAPartyToolStripMenuItem_Click);
            // 
            // makeANewPartyToolStripMenuItem
            // 
            this.makeANewPartyToolStripMenuItem.Name = "makeANewPartyToolStripMenuItem";
            this.makeANewPartyToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.makeANewPartyToolStripMenuItem.Text = "Save Party As";
            this.makeANewPartyToolStripMenuItem.Click += new System.EventHandler(this.makeANewPartyToolStripMenuItem_Click);
            // 
            // importPartyInAdditionToolStripMenuItem
            // 
            this.importPartyInAdditionToolStripMenuItem.Name = "importPartyInAdditionToolStripMenuItem";
            this.importPartyInAdditionToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.importPartyInAdditionToolStripMenuItem.Text = "Import Party in Addition";
            this.importPartyInAdditionToolStripMenuItem.Click += new System.EventHandler(this.importPartyInAdditionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutBattleOrderToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutBattleOrderToolStripMenuItem
            // 
            this.aboutBattleOrderToolStripMenuItem.Name = "aboutBattleOrderToolStripMenuItem";
            this.aboutBattleOrderToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.aboutBattleOrderToolStripMenuItem.Text = "About Battle Order";
            this.aboutBattleOrderToolStripMenuItem.Click += new System.EventHandler(this.aboutBattleOrderToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(459, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 1019;
            this.label3.Text = "Party";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(280, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 20);
            this.label11.TabIndex = 1020;
            this.label11.Text = "Enemy";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "When to Use";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BattleOrder.Properties.Resources.Suggestion07_copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(767, 443);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MonsterChecklist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MonsterSpeedText);
            this.Controls.Add(this.EnemAttackLabel);
            this.Controls.Add(this.PartyNameText);
            this.Controls.Add(this.MonsterQuantText);
            this.Controls.Add(this.MonsterNameCombo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.PartyAttackCombo);
            this.Controls.Add(this.InAdditionCheckBox);
            this.Controls.Add(this.InitiativeText);
            this.Controls.Add(this.InitiativeAttackLabel);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.MonsterInitiativeButton);
            this.Controls.Add(this.PartyAdd);
            this.Controls.Add(this.MonsterInitiativeTextBox);
            this.Controls.Add(this.CharAttackLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MonsterInitiativeAttackLabel);
            this.Controls.Add(this.MonsterInitiativeLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.EditMonsterDatabaseCheckbox);
            this.Controls.Add(this.PartyChecklist);
            this.Controls.Add(this.InitiativeTitle);
            this.Controls.Add(this.Battle);
            this.Controls.Add(this.MonsterInitiativeNameLabel);
            this.Controls.Add(this.MonsterAttackCombo);
            this.Controls.Add(this.MonsterPerRoundText);
            this.Controls.Add(this.InitiativeNameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResetBattle);
            this.Controls.Add(this.PartySpeedText);
            this.Controls.Add(this.PartyPerRoundText);
            this.Controls.Add(this.InitiativeButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MonsterAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Battle Order 2.0.8";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.badguycontextMenuStrip1.ResumeLayout(false);
            this.goodguycontextMenuStrip2.ResumeLayout(false);
            this.MonsterAttackMenu.ResumeLayout(false);
            this.PartyAttackMenu.ResumeLayout(false);
            this.MonsterNameMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PartyNameText;
        private System.Windows.Forms.TextBox MonsterQuantText;
        private System.Windows.Forms.CheckedListBox MonsterChecklist;
        private System.Windows.Forms.CheckedListBox PartyChecklist;
        private System.Windows.Forms.TextBox MonsterSpeedText;
        private System.Windows.Forms.TextBox PartyPerRoundText;
        private System.Windows.Forms.TextBox PartySpeedText;
        private System.Windows.Forms.Button PartyAdd;
        private System.Windows.Forms.Button MonsterAdd;
        private System.Windows.Forms.TextBox MonsterPerRoundText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label EnemAttackLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label CharAttackLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Battle;
        private System.Windows.Forms.Button ResetBattle;
        private System.Windows.Forms.Label InitiativeNameLabel;
        private System.Windows.Forms.TextBox InitiativeText;
        private System.Windows.Forms.Button InitiativeButton;
        private System.Windows.Forms.Label InitiativeTitle;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.ComboBox MonsterAttackCombo;
        private System.Windows.Forms.ComboBox PartyAttackCombo;
        private System.Windows.Forms.Label MonsterInitiativeLabel;
        private System.Windows.Forms.Button MonsterInitiativeButton;
        private System.Windows.Forms.TextBox MonsterInitiativeTextBox;
        private System.Windows.Forms.Label MonsterInitiativeNameLabel;
        private System.Windows.Forms.Label InitiativeAttackLabel;
        private System.Windows.Forms.Label MonsterInitiativeAttackLabel;
        private System.Windows.Forms.ComboBox MonsterNameCombo;
        private System.Windows.Forms.CheckBox InAdditionCheckBox;
        private System.Windows.Forms.ContextMenuStrip badguycontextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip goodguycontextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayAllStatsForSelectedToolStripMenuItem;
        private System.Windows.Forms.CheckBox EditMonsterDatabaseCheckbox;
        private System.Windows.Forms.ToolStripMenuItem makeAnNPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeAnNPCToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAPartyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeANewPartyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutBattleOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip PartyAttackMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip MonsterAttackMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip MonsterNameMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem importPartyInAdditionToolStripMenuItem;
    }
}

