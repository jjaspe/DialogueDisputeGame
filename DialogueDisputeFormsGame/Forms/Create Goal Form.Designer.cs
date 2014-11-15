namespace DialogueDisputeGameClient.Forms
{
    partial class Create_Goal_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpGoalType = new System.Windows.Forms.GroupBox();
            this.radBelowValue = new System.Windows.Forms.RadioButton();
            this.radAboveValue = new System.Windows.Forms.RadioButton();
            this.radReachValue = new System.Windows.Forms.RadioButton();
            this.nmrValue = new System.Windows.Forms.NumericUpDown();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpGoalType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Property Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value";
            // 
            // grpGoalType
            // 
            this.grpGoalType.Controls.Add(this.radBelowValue);
            this.grpGoalType.Controls.Add(this.radAboveValue);
            this.grpGoalType.Controls.Add(this.radReachValue);
            this.grpGoalType.Location = new System.Drawing.Point(259, 32);
            this.grpGoalType.Name = "grpGoalType";
            this.grpGoalType.Size = new System.Drawing.Size(146, 142);
            this.grpGoalType.TabIndex = 2;
            this.grpGoalType.TabStop = false;
            this.grpGoalType.Text = "Goal Type";
            // 
            // radBelowValue
            // 
            this.radBelowValue.AutoSize = true;
            this.radBelowValue.Location = new System.Drawing.Point(19, 104);
            this.radBelowValue.Name = "radBelowValue";
            this.radBelowValue.Size = new System.Drawing.Size(108, 17);
            this.radBelowValue.TabIndex = 2;
            this.radBelowValue.Text = "Stay Below Value";
            this.radBelowValue.UseVisualStyleBackColor = true;
            this.radBelowValue.CheckedChanged += new System.EventHandler(this.radBelowValue_CheckedChanged);
            // 
            // radAboveValue
            // 
            this.radAboveValue.AutoSize = true;
            this.radAboveValue.Location = new System.Drawing.Point(19, 65);
            this.radAboveValue.Name = "radAboveValue";
            this.radAboveValue.Size = new System.Drawing.Size(110, 17);
            this.radAboveValue.TabIndex = 1;
            this.radAboveValue.Text = "Stay Above Value";
            this.radAboveValue.UseVisualStyleBackColor = true;
            this.radAboveValue.CheckedChanged += new System.EventHandler(this.radAboveValue_CheckedChanged);
            // 
            // radReachValue
            // 
            this.radReachValue.AutoSize = true;
            this.radReachValue.Checked = true;
            this.radReachValue.Location = new System.Drawing.Point(20, 28);
            this.radReachValue.Name = "radReachValue";
            this.radReachValue.Size = new System.Drawing.Size(87, 17);
            this.radReachValue.TabIndex = 0;
            this.radReachValue.TabStop = true;
            this.radReachValue.Text = "Reach Value";
            this.radReachValue.UseVisualStyleBackColor = true;
            this.radReachValue.CheckedChanged += new System.EventHandler(this.radReachValue_CheckedChanged);
            // 
            // nmrValue
            // 
            this.nmrValue.Location = new System.Drawing.Point(179, 60);
            this.nmrValue.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nmrValue.Name = "nmrValue";
            this.nmrValue.Size = new System.Drawing.Size(29, 20);
            this.nmrValue.TabIndex = 3;
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(32, 60);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(100, 20);
            this.txtPropertyName.TabIndex = 4;
            this.txtPropertyName.Text = "Resistance";
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.Location = new System.Drawing.Point(330, 223);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 0;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(438, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GoalCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 258);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.nmrValue);
            this.Controls.Add(this.grpGoalType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GoalCreationForm";
            this.Text = "GoalCreationForm";
            this.grpGoalType.ResumeLayout(false);
            this.grpGoalType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpGoalType;
        private System.Windows.Forms.RadioButton radBelowValue;
        private System.Windows.Forms.RadioButton radAboveValue;
        private System.Windows.Forms.RadioButton radReachValue;
        private System.Windows.Forms.NumericUpDown nmrValue;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
    }
}