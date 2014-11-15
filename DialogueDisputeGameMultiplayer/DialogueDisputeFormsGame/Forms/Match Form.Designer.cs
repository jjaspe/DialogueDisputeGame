using DisputeCommon;
namespace DialogueDisputeGameClient
{
    partial class MatchForm
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
            this.grpPlayerInfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.resistancePointsLabel1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.persuasionLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.subterfugePointsLabel1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.selfControlPointsLabel1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.persuasionPointsLabel1 = new System.Windows.Forms.Label();
            this.fortitudePointsLabel1 = new System.Windows.Forms.Label();
            this.intimidationPointsLabel1 = new System.Windows.Forms.Label();
            this.perceptionPointsLabel1 = new System.Windows.Forms.Label();
            this.disputeBox = new System.Windows.Forms.GroupBox();
            this.toneComboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMatchTranscript = new System.Windows.Forms.TextBox();
            this.messagesTimer = new System.Windows.Forms.Timer(this.components);
            this.grpArguments = new System.Windows.Forms.GroupBox();
            this.btnAnalyze = new DisputeCommon.customButton();
            this.btnTrick = new DisputeCommon.customButton();
            this.btnFocus = new DisputeCommon.customButton();
            this.btnManipulate = new DisputeCommon.customButton();
            this.btnEmpathy = new DisputeCommon.customButton();
            this.btnCharm = new DisputeCommon.customButton();
            this.btnScare = new DisputeCommon.customButton();
            this.btnBluff = new DisputeCommon.customButton();
            this.btnConvince = new DisputeCommon.customButton();
            this.btnCoerce = new DisputeCommon.customButton();
            this.btnTaunt = new DisputeCommon.customButton();
            this.txtArgumentFeedback = new System.Windows.Forms.TextBox();
            this.SoMGridP1 = new DisputeCommon.SoMGrid();
            this.btnEndGame = new System.Windows.Forms.Button();
            this.grpPlayerInfo.SuspendLayout();
            this.disputeBox.SuspendLayout();
            this.grpArguments.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPlayerInfo
            // 
            this.grpPlayerInfo.Controls.Add(this.label6);
            this.grpPlayerInfo.Controls.Add(this.resistancePointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.label1);
            this.grpPlayerInfo.Controls.Add(this.label2);
            this.grpPlayerInfo.Controls.Add(this.persuasionLabel);
            this.grpPlayerInfo.Controls.Add(this.label7);
            this.grpPlayerInfo.Controls.Add(this.subterfugePointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.label8);
            this.grpPlayerInfo.Controls.Add(this.selfControlPointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.label9);
            this.grpPlayerInfo.Controls.Add(this.persuasionPointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.fortitudePointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.intimidationPointsLabel1);
            this.grpPlayerInfo.Controls.Add(this.perceptionPointsLabel1);
            this.grpPlayerInfo.Location = new System.Drawing.Point(10, 10);
            this.grpPlayerInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpPlayerInfo.Name = "grpPlayerInfo";
            this.grpPlayerInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpPlayerInfo.Size = new System.Drawing.Size(130, 364);
            this.grpPlayerInfo.TabIndex = 4;
            this.grpPlayerInfo.TabStop = false;
            this.grpPlayerInfo.Text = "Player 1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 316);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Subterfuge";
            // 
            // resistancePointsLabel1
            // 
            this.resistancePointsLabel1.AutoSize = true;
            this.resistancePointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.resistancePointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resistancePointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.resistancePointsLabel1.Location = new System.Drawing.Point(91, 141);
            this.resistancePointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resistancePointsLabel1.Name = "resistancePointsLabel1";
            this.resistancePointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.resistancePointsLabel1.TabIndex = 33;
            this.resistancePointsLabel1.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 232);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Perception";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Self Control";
            // 
            // persuasionLabel
            // 
            this.persuasionLabel.AutoSize = true;
            this.persuasionLabel.Location = new System.Drawing.Point(12, 275);
            this.persuasionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.persuasionLabel.Name = "persuasionLabel";
            this.persuasionLabel.Size = new System.Drawing.Size(59, 13);
            this.persuasionLabel.TabIndex = 34;
            this.persuasionLabel.Text = "Persuasion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Fortitude";
            // 
            // subterfugePointsLabel1
            // 
            this.subterfugePointsLabel1.AutoSize = true;
            this.subterfugePointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.subterfugePointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subterfugePointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.subterfugePointsLabel1.Location = new System.Drawing.Point(92, 316);
            this.subterfugePointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subterfugePointsLabel1.Name = "subterfugePointsLabel1";
            this.subterfugePointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.subterfugePointsLabel1.TabIndex = 41;
            this.subterfugePointsLabel1.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 139);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Resistance";
            // 
            // selfControlPointsLabel1
            // 
            this.selfControlPointsLabel1.AutoSize = true;
            this.selfControlPointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.selfControlPointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selfControlPointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.selfControlPointsLabel1.Location = new System.Drawing.Point(91, 83);
            this.selfControlPointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.selfControlPointsLabel1.Name = "selfControlPointsLabel1";
            this.selfControlPointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.selfControlPointsLabel1.TabIndex = 27;
            this.selfControlPointsLabel1.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 188);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Intimidation";
            // 
            // persuasionPointsLabel1
            // 
            this.persuasionPointsLabel1.AutoSize = true;
            this.persuasionPointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.persuasionPointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.persuasionPointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.persuasionPointsLabel1.Location = new System.Drawing.Point(92, 275);
            this.persuasionPointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.persuasionPointsLabel1.Name = "persuasionPointsLabel1";
            this.persuasionPointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.persuasionPointsLabel1.TabIndex = 40;
            this.persuasionPointsLabel1.Text = "0";
            // 
            // fortitudePointsLabel1
            // 
            this.fortitudePointsLabel1.AutoSize = true;
            this.fortitudePointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fortitudePointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fortitudePointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.fortitudePointsLabel1.Location = new System.Drawing.Point(91, 28);
            this.fortitudePointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fortitudePointsLabel1.Name = "fortitudePointsLabel1";
            this.fortitudePointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.fortitudePointsLabel1.TabIndex = 24;
            this.fortitudePointsLabel1.Text = "0\r\n";
            // 
            // intimidationPointsLabel1
            // 
            this.intimidationPointsLabel1.AutoSize = true;
            this.intimidationPointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.intimidationPointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intimidationPointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.intimidationPointsLabel1.Location = new System.Drawing.Point(92, 188);
            this.intimidationPointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.intimidationPointsLabel1.Name = "intimidationPointsLabel1";
            this.intimidationPointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.intimidationPointsLabel1.TabIndex = 38;
            this.intimidationPointsLabel1.Text = "0";
            // 
            // perceptionPointsLabel1
            // 
            this.perceptionPointsLabel1.AutoSize = true;
            this.perceptionPointsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.perceptionPointsLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.perceptionPointsLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.perceptionPointsLabel1.Location = new System.Drawing.Point(92, 232);
            this.perceptionPointsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.perceptionPointsLabel1.Name = "perceptionPointsLabel1";
            this.perceptionPointsLabel1.Size = new System.Drawing.Size(19, 20);
            this.perceptionPointsLabel1.TabIndex = 39;
            this.perceptionPointsLabel1.Text = "0";
            // 
            // disputeBox
            // 
            this.disputeBox.Controls.Add(this.toneComboBox);
            this.disputeBox.Controls.Add(this.label18);
            this.disputeBox.Location = new System.Drawing.Point(144, 167);
            this.disputeBox.Margin = new System.Windows.Forms.Padding(2);
            this.disputeBox.Name = "disputeBox";
            this.disputeBox.Padding = new System.Windows.Forms.Padding(2);
            this.disputeBox.Size = new System.Drawing.Size(149, 74);
            this.disputeBox.TabIndex = 52;
            this.disputeBox.TabStop = false;
            this.disputeBox.Text = "Dispute";
            // 
            // toneComboBox
            // 
            this.toneComboBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.toneComboBox.Enabled = false;
            this.toneComboBox.ForeColor = System.Drawing.SystemColors.Window;
            this.toneComboBox.FormattingEnabled = true;
            this.toneComboBox.Items.AddRange(new object[] {
            "Close",
            "Friendly",
            "Neutral",
            "Unfriendly",
            "Hostile"});
            this.toneComboBox.Location = new System.Drawing.Point(48, 28);
            this.toneComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.toneComboBox.Name = "toneComboBox";
            this.toneComboBox.Size = new System.Drawing.Size(92, 21);
            this.toneComboBox.TabIndex = 42;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 28);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Tone ";
            // 
            // txtMatchTranscript
            // 
            this.txtMatchTranscript.Location = new System.Drawing.Point(11, 517);
            this.txtMatchTranscript.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatchTranscript.Multiline = true;
            this.txtMatchTranscript.Name = "txtMatchTranscript";
            this.txtMatchTranscript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMatchTranscript.Size = new System.Drawing.Size(430, 157);
            this.txtMatchTranscript.TabIndex = 53;
            // 
            // messagesTimer
            // 
            this.messagesTimer.Interval = 1000;
            this.messagesTimer.Tick += new System.EventHandler(this.messagesTimer_Tick);
            // 
            // grpArguments
            // 
            this.grpArguments.Controls.Add(this.btnAnalyze);
            this.grpArguments.Controls.Add(this.btnTrick);
            this.grpArguments.Controls.Add(this.btnFocus);
            this.grpArguments.Controls.Add(this.btnManipulate);
            this.grpArguments.Controls.Add(this.btnEmpathy);
            this.grpArguments.Controls.Add(this.btnCharm);
            this.grpArguments.Controls.Add(this.btnScare);
            this.grpArguments.Controls.Add(this.btnBluff);
            this.grpArguments.Controls.Add(this.btnConvince);
            this.grpArguments.Controls.Add(this.btnCoerce);
            this.grpArguments.Controls.Add(this.btnTaunt);
            this.grpArguments.Location = new System.Drawing.Point(378, 12);
            this.grpArguments.Name = "grpArguments";
            this.grpArguments.Size = new System.Drawing.Size(166, 458);
            this.grpArguments.TabIndex = 62;
            this.grpArguments.TabStop = false;
            this.grpArguments.Text = "Arguments";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(50, 418);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(71, 24);
            this.btnAnalyze.TabIndex = 64;
            this.btnAnalyze.Tag = "Subterfuge Vs. Perception";
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnTrick
            // 
            this.btnTrick.Location = new System.Drawing.Point(50, 378);
            this.btnTrick.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrick.Name = "btnTrick";
            this.btnTrick.Size = new System.Drawing.Size(71, 24);
            this.btnTrick.TabIndex = 63;
            this.btnTrick.Tag = "Subterfuge Vs. Perception";
            this.btnTrick.Text = "Trick";
            this.btnTrick.UseVisualStyleBackColor = true;
            this.btnTrick.Click += new System.EventHandler(this.btnTrick_Click);
            // 
            // btnFocus
            // 
            this.btnFocus.Location = new System.Drawing.Point(50, 338);
            this.btnFocus.Margin = new System.Windows.Forms.Padding(2);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(71, 24);
            this.btnFocus.TabIndex = 62;
            this.btnFocus.Tag = "Subterfuge Vs. Perception";
            this.btnFocus.Text = "Focus";
            this.btnFocus.UseVisualStyleBackColor = true;
            this.btnFocus.Click += new System.EventHandler(this.btnFocus_Click);
            // 
            // btnManipulate
            // 
            this.btnManipulate.Location = new System.Drawing.Point(50, 18);
            this.btnManipulate.Margin = new System.Windows.Forms.Padding(2);
            this.btnManipulate.Name = "btnManipulate";
            this.btnManipulate.Size = new System.Drawing.Size(71, 24);
            this.btnManipulate.TabIndex = 54;
            this.btnManipulate.Tag = "Perception Vs. Self Control";
            this.btnManipulate.Text = "Manipulate";
            this.btnManipulate.UseVisualStyleBackColor = true;
            this.btnManipulate.Click += new System.EventHandler(this.manipulateButton_Click);
            // 
            // btnEmpathy
            // 
            this.btnEmpathy.Location = new System.Drawing.Point(50, 138);
            this.btnEmpathy.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmpathy.Name = "btnEmpathy";
            this.btnEmpathy.Size = new System.Drawing.Size(71, 24);
            this.btnEmpathy.TabIndex = 61;
            this.btnEmpathy.Tag = "Intimidation Vs. Self Control";
            this.btnEmpathy.Text = "Empathy";
            this.btnEmpathy.UseVisualStyleBackColor = true;
            this.btnEmpathy.Click += new System.EventHandler(this.empathyButton_Click);
            // 
            // btnCharm
            // 
            this.btnCharm.Location = new System.Drawing.Point(50, 58);
            this.btnCharm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCharm.Name = "btnCharm";
            this.btnCharm.Size = new System.Drawing.Size(71, 24);
            this.btnCharm.TabIndex = 6;
            this.btnCharm.Tag = "Persuasion Vs. Fortitude";
            this.btnCharm.Text = "Charm";
            this.btnCharm.UseVisualStyleBackColor = true;
            this.btnCharm.Click += new System.EventHandler(this.charmAction_Click);
            // 
            // btnScare
            // 
            this.btnScare.Location = new System.Drawing.Point(50, 178);
            this.btnScare.Margin = new System.Windows.Forms.Padding(2);
            this.btnScare.Name = "btnScare";
            this.btnScare.Size = new System.Drawing.Size(71, 24);
            this.btnScare.TabIndex = 7;
            this.btnScare.Tag = "Intimidation Vs. Self Control";
            this.btnScare.Text = "Scare";
            this.btnScare.UseVisualStyleBackColor = true;
            this.btnScare.Click += new System.EventHandler(this.scareButton_Click);
            // 
            // btnBluff
            // 
            this.btnBluff.Location = new System.Drawing.Point(50, 298);
            this.btnBluff.Margin = new System.Windows.Forms.Padding(2);
            this.btnBluff.Name = "btnBluff";
            this.btnBluff.Size = new System.Drawing.Size(71, 24);
            this.btnBluff.TabIndex = 59;
            this.btnBluff.Tag = "Subterfuge Vs. Perception";
            this.btnBluff.Text = "Bluff";
            this.btnBluff.UseVisualStyleBackColor = true;
            this.btnBluff.Click += new System.EventHandler(this.bluffButton_Click);
            // 
            // btnConvince
            // 
            this.btnConvince.Location = new System.Drawing.Point(50, 98);
            this.btnConvince.Margin = new System.Windows.Forms.Padding(2);
            this.btnConvince.Name = "btnConvince";
            this.btnConvince.Size = new System.Drawing.Size(71, 24);
            this.btnConvince.TabIndex = 8;
            this.btnConvince.Tag = "Persuasion Vs. Fortitude";
            this.btnConvince.Text = "Convince";
            this.btnConvince.UseVisualStyleBackColor = true;
            this.btnConvince.Click += new System.EventHandler(this.convinceButton_Click);
            // 
            // btnCoerce
            // 
            this.btnCoerce.Location = new System.Drawing.Point(50, 258);
            this.btnCoerce.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoerce.Name = "btnCoerce";
            this.btnCoerce.Size = new System.Drawing.Size(71, 24);
            this.btnCoerce.TabIndex = 58;
            this.btnCoerce.Tag = "Intimidation Vs. Fortitude";
            this.btnCoerce.Text = "Coerce";
            this.btnCoerce.UseVisualStyleBackColor = true;
            this.btnCoerce.Click += new System.EventHandler(this.coerceButton_Click);
            // 
            // btnTaunt
            // 
            this.btnTaunt.Location = new System.Drawing.Point(50, 218);
            this.btnTaunt.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaunt.Name = "btnTaunt";
            this.btnTaunt.Size = new System.Drawing.Size(71, 24);
            this.btnTaunt.TabIndex = 57;
            this.btnTaunt.Tag = "Intimidation Vs. Self Control";
            this.btnTaunt.Text = "Taunt";
            this.btnTaunt.UseVisualStyleBackColor = true;
            this.btnTaunt.Click += new System.EventHandler(this.tauntButton_Click);
            // 
            // txtArgumentFeedback
            // 
            this.txtArgumentFeedback.Location = new System.Drawing.Point(144, 262);
            this.txtArgumentFeedback.Margin = new System.Windows.Forms.Padding(2);
            this.txtArgumentFeedback.Multiline = true;
            this.txtArgumentFeedback.Name = "txtArgumentFeedback";
            this.txtArgumentFeedback.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArgumentFeedback.Size = new System.Drawing.Size(229, 157);
            this.txtArgumentFeedback.TabIndex = 63;
            // 
            // SoMGridP1
            // 
            this.SoMGridP1.Location = new System.Drawing.Point(144, 0);
            this.SoMGridP1.Margin = new System.Windows.Forms.Padding(2);
            this.SoMGridP1.Name = "SoMGridP1";
            this.SoMGridP1.Size = new System.Drawing.Size(200, 163);
            this.SoMGridP1.SoMAnger = 0;
            this.SoMGridP1.SoMJoy = 0;
            this.SoMGridP1.TabIndex = 55;
            // 
            // btnEndGame
            // 
            this.btnEndGame.Location = new System.Drawing.Point(461, 650);
            this.btnEndGame.Name = "btnEndGame";
            this.btnEndGame.Size = new System.Drawing.Size(75, 23);
            this.btnEndGame.TabIndex = 64;
            this.btnEndGame.Text = "End Game";
            this.btnEndGame.UseVisualStyleBackColor = true;
            this.btnEndGame.Click += new System.EventHandler(this.btnEndGame_Click);
            // 
            // MatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 742);
            this.Controls.Add(this.btnEndGame);
            this.Controls.Add(this.txtArgumentFeedback);
            this.Controls.Add(this.grpArguments);
            this.Controls.Add(this.SoMGridP1);
            this.Controls.Add(this.txtMatchTranscript);
            this.Controls.Add(this.disputeBox);
            this.Controls.Add(this.grpPlayerInfo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MatchForm";
            this.Text = "Match";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_Closed);
            this.grpPlayerInfo.ResumeLayout(false);
            this.grpPlayerInfo.PerformLayout();
            this.disputeBox.ResumeLayout(false);
            this.disputeBox.PerformLayout();
            this.grpArguments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPlayerInfo;
        private System.Windows.Forms.Label resistancePointsLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label selfControlPointsLabel1;
        private System.Windows.Forms.Label fortitudePointsLabel1;
        private customButton btnCharm;
        private customButton btnScare;
        private customButton btnConvince;
        private System.Windows.Forms.GroupBox disputeBox;
        private System.Windows.Forms.ComboBox toneComboBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMatchTranscript;
        private customButton btnManipulate;
        private SoMGrid SoMGridP1;
        private customButton btnTaunt;
        private customButton btnCoerce;
        private customButton btnBluff;
        private System.Windows.Forms.Label subterfugePointsLabel1;
        private System.Windows.Forms.Label persuasionPointsLabel1;
        private System.Windows.Forms.Label perceptionPointsLabel1;
        private System.Windows.Forms.Label intimidationPointsLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label persuasionLabel;
        private customButton btnEmpathy;
        private System.Windows.Forms.Timer messagesTimer;
        private System.Windows.Forms.GroupBox grpArguments;
        private customButton btnFocus;
        private customButton btnAnalyze;
        private customButton btnTrick;
        private System.Windows.Forms.TextBox txtArgumentFeedback;
        private System.Windows.Forms.Button btnEndGame;
    }
}