namespace DialogueDisputeOpenGLView
{
    partial class DisputeOpenGLView
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
            this.myView = new Canvas_Window_Template.simpleOpenGlView();
            this.grpArguments = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.persuasionLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.myNavigator = new Canvas_Window_Template.Navigator();
            this.grpArguments.SuspendLayout();
            this.SuspendLayout();
            // 
            // myView
            // 
            this.myView.AccumBits = ((byte)(0));
            this.myView.AutoCheckErrors = false;
            this.myView.AutoFinish = false;
            this.myView.AutoMakeCurrent = true;
            this.myView.AutoSize = true;
            this.myView.AutoSwapBuffers = true;
            this.myView.BackColor = System.Drawing.Color.Black;
            this.myView.ColorBits = ((byte)(32));
            this.myView.DepthBits = ((byte)(16));
            this.myView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myView.EyeCustom = new double[] {
        -300D,
        0D,
        -0.00010000000000000002D};
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.PerspectiveEye = new double[] {
        300D,
        0D,
        0.0001D};
            this.myView.Size = new System.Drawing.Size(896, 711);
            this.myView.StencilBits = ((byte)(0));
            this.myView.TabIndex = 0;
            this.myView.ViewDistance = 300D;
            this.myView.ViewPhi = 0D;
            this.myView.ViewTheta = 0D;
            // 
            // grpArguments
            // 
            this.grpArguments.BackColor = System.Drawing.Color.Black;
            this.grpArguments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
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
            this.grpArguments.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grpArguments.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpArguments.ForeColor = System.Drawing.SystemColors.Control;
            this.grpArguments.Location = new System.Drawing.Point(683, 152);
            this.grpArguments.Name = "grpArguments";
            this.grpArguments.Size = new System.Drawing.Size(166, 463);
            this.grpArguments.TabIndex = 63;
            this.grpArguments.TabStop = false;
            this.grpArguments.Text = "Arguments";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(269, 340);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 20);
            this.label6.TabIndex = 70;
            this.label6.Text = "Subterfuge";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(50, 469);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 71;
            this.label1.Text = "Perception";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(226, 660);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 67;
            this.label2.Text = "Self Control";
            // 
            // persuasionLabel
            // 
            this.persuasionLabel.AutoSize = true;
            this.persuasionLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.persuasionLabel.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.persuasionLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.persuasionLabel.Location = new System.Drawing.Point(173, 394);
            this.persuasionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.persuasionLabel.Name = "persuasionLabel";
            this.persuasionLabel.Size = new System.Drawing.Size(81, 20);
            this.persuasionLabel.TabIndex = 68;
            this.persuasionLabel.Text = "Persuasion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(89, 660);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 66;
            this.label7.Text = "Fortitude";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.Location = new System.Drawing.Point(388, 660);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 20);
            this.label8.TabIndex = 65;
            this.label8.Text = "Resistance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(335, 295);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 20);
            this.label9.TabIndex = 69;
            this.label9.Text = "Intimidation";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.AutoEllipsis = true;
            this.btnAnalyze.BackColor = System.Drawing.Color.Maroon;
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyze.Location = new System.Drawing.Point(24, 418);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 30);
            this.btnAnalyze.TabIndex = 64;
            this.btnAnalyze.Tag = "Subterfuge Vs. Perception";
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            // 
            // btnTrick
            // 
            this.btnTrick.AutoEllipsis = true;
            this.btnTrick.BackColor = System.Drawing.Color.Maroon;
            this.btnTrick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrick.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrick.Location = new System.Drawing.Point(24, 378);
            this.btnTrick.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrick.Name = "btnTrick";
            this.btnTrick.Size = new System.Drawing.Size(120, 30);
            this.btnTrick.TabIndex = 63;
            this.btnTrick.Tag = "Subterfuge Vs. Perception";
            this.btnTrick.Text = "Trick";
            this.btnTrick.UseVisualStyleBackColor = false;
            // 
            // btnFocus
            // 
            this.btnFocus.AutoEllipsis = true;
            this.btnFocus.BackColor = System.Drawing.Color.Maroon;
            this.btnFocus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFocus.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFocus.Location = new System.Drawing.Point(24, 338);
            this.btnFocus.Margin = new System.Windows.Forms.Padding(2);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(120, 30);
            this.btnFocus.TabIndex = 62;
            this.btnFocus.Tag = "Subterfuge Vs. Perception";
            this.btnFocus.Text = "Focus";
            this.btnFocus.UseVisualStyleBackColor = false;
            // 
            // btnManipulate
            // 
            this.btnManipulate.AutoEllipsis = true;
            this.btnManipulate.BackColor = System.Drawing.Color.Maroon;
            this.btnManipulate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManipulate.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManipulate.Location = new System.Drawing.Point(24, 18);
            this.btnManipulate.Margin = new System.Windows.Forms.Padding(2);
            this.btnManipulate.Name = "btnManipulate";
            this.btnManipulate.Size = new System.Drawing.Size(120, 30);
            this.btnManipulate.TabIndex = 54;
            this.btnManipulate.Tag = "Perception Vs. Self Control";
            this.btnManipulate.Text = "Manipulate";
            this.btnManipulate.UseVisualStyleBackColor = false;
            this.btnManipulate.Click += new System.EventHandler(this.btnManipulate_Click);
            // 
            // btnEmpathy
            // 
            this.btnEmpathy.AutoEllipsis = true;
            this.btnEmpathy.BackColor = System.Drawing.Color.Maroon;
            this.btnEmpathy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpathy.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpathy.Location = new System.Drawing.Point(24, 138);
            this.btnEmpathy.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmpathy.Name = "btnEmpathy";
            this.btnEmpathy.Size = new System.Drawing.Size(120, 30);
            this.btnEmpathy.TabIndex = 61;
            this.btnEmpathy.Tag = "Intimidation Vs. Self Control";
            this.btnEmpathy.Text = "Empathy";
            this.btnEmpathy.UseVisualStyleBackColor = false;
            // 
            // btnCharm
            // 
            this.btnCharm.AutoEllipsis = true;
            this.btnCharm.BackColor = System.Drawing.Color.Maroon;
            this.btnCharm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCharm.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCharm.Location = new System.Drawing.Point(24, 58);
            this.btnCharm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCharm.Name = "btnCharm";
            this.btnCharm.Size = new System.Drawing.Size(120, 30);
            this.btnCharm.TabIndex = 6;
            this.btnCharm.Tag = "Persuasion Vs. Fortitude";
            this.btnCharm.Text = "Charm";
            this.btnCharm.UseVisualStyleBackColor = false;
            // 
            // btnScare
            // 
            this.btnScare.AutoEllipsis = true;
            this.btnScare.BackColor = System.Drawing.Color.Maroon;
            this.btnScare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScare.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScare.Location = new System.Drawing.Point(24, 178);
            this.btnScare.Margin = new System.Windows.Forms.Padding(2);
            this.btnScare.Name = "btnScare";
            this.btnScare.Size = new System.Drawing.Size(120, 30);
            this.btnScare.TabIndex = 7;
            this.btnScare.Tag = "Intimidation Vs. Self Control";
            this.btnScare.Text = "Scare";
            this.btnScare.UseVisualStyleBackColor = false;
            // 
            // btnBluff
            // 
            this.btnBluff.AutoEllipsis = true;
            this.btnBluff.BackColor = System.Drawing.Color.Maroon;
            this.btnBluff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBluff.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBluff.Location = new System.Drawing.Point(24, 298);
            this.btnBluff.Margin = new System.Windows.Forms.Padding(2);
            this.btnBluff.Name = "btnBluff";
            this.btnBluff.Size = new System.Drawing.Size(120, 30);
            this.btnBluff.TabIndex = 59;
            this.btnBluff.Tag = "Subterfuge Vs. Perception";
            this.btnBluff.Text = "Bluff";
            this.btnBluff.UseVisualStyleBackColor = false;
            // 
            // btnConvince
            // 
            this.btnConvince.AutoEllipsis = true;
            this.btnConvince.BackColor = System.Drawing.Color.Maroon;
            this.btnConvince.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvince.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvince.Location = new System.Drawing.Point(24, 98);
            this.btnConvince.Margin = new System.Windows.Forms.Padding(2);
            this.btnConvince.Name = "btnConvince";
            this.btnConvince.Size = new System.Drawing.Size(120, 30);
            this.btnConvince.TabIndex = 8;
            this.btnConvince.Tag = "Persuasion Vs. Fortitude";
            this.btnConvince.Text = "Convince";
            this.btnConvince.UseVisualStyleBackColor = false;
            this.btnConvince.Click += new System.EventHandler(this.btnConvince_Click);
            // 
            // btnCoerce
            // 
            this.btnCoerce.AutoEllipsis = true;
            this.btnCoerce.BackColor = System.Drawing.Color.Maroon;
            this.btnCoerce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCoerce.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoerce.Location = new System.Drawing.Point(24, 258);
            this.btnCoerce.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoerce.Name = "btnCoerce";
            this.btnCoerce.Size = new System.Drawing.Size(120, 30);
            this.btnCoerce.TabIndex = 58;
            this.btnCoerce.Tag = "Intimidation Vs. Fortitude";
            this.btnCoerce.Text = "Coerce";
            this.btnCoerce.UseVisualStyleBackColor = false;
            // 
            // btnTaunt
            // 
            this.btnTaunt.AutoEllipsis = true;
            this.btnTaunt.BackColor = System.Drawing.Color.Maroon;
            this.btnTaunt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaunt.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaunt.Location = new System.Drawing.Point(24, 218);
            this.btnTaunt.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaunt.Name = "btnTaunt";
            this.btnTaunt.Size = new System.Drawing.Size(120, 30);
            this.btnTaunt.TabIndex = 57;
            this.btnTaunt.Tag = "Intimidation Vs. Self Control";
            this.btnTaunt.Text = "Taunt";
            this.btnTaunt.UseVisualStyleBackColor = false;
            // 
            // myNavigator
            // 
            this.myNavigator.BackColor = System.Drawing.Color.Black;
            this.myNavigator.Location = new System.Drawing.Point(36, 0);
            this.myNavigator.MyView = null;
            this.myNavigator.MyWindowOwner = null;
            this.myNavigator.Name = "myNavigator";
            this.myNavigator.Orientation = Canvas_Window_Template.Basic_Drawing_Functions.Common.planeOrientation.None;
            this.myNavigator.Size = new System.Drawing.Size(186, 126);
            this.myNavigator.TabIndex = 2;
            // 
            // DisputeOpenGLView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 711);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.persuasionLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grpArguments);
            this.Controls.Add(this.myNavigator);
            this.Controls.Add(this.myView);
            this.Name = "DisputeOpenGLView";
            this.Text = "DisputeOpenGLView";
            this.grpArguments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Canvas_Window_Template.simpleOpenGlView myView;
        private System.Windows.Forms.GroupBox grpArguments;
        private DisputeCommon.customButton btnAnalyze;
        private DisputeCommon.customButton btnTrick;
        private DisputeCommon.customButton btnFocus;
        private DisputeCommon.customButton btnManipulate;
        private DisputeCommon.customButton btnEmpathy;
        private DisputeCommon.customButton btnCharm;
        private DisputeCommon.customButton btnScare;
        private DisputeCommon.customButton btnBluff;
        private DisputeCommon.customButton btnConvince;
        private DisputeCommon.customButton btnCoerce;
        private DisputeCommon.customButton btnTaunt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label persuasionLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Canvas_Window_Template.Navigator myNavigator;
    }
}