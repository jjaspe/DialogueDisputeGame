namespace DialogueDisputeFormsGame.Forms
{
    partial class SoMSelectorForm
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
            this.SoMJoyRadio = new System.Windows.Forms.RadioButton();
            this.SoMAngerRadio = new System.Windows.Forms.RadioButton();
            this.SoMSorrowRadio = new System.Windows.Forms.RadioButton();
            this.SoMFearRadio = new System.Windows.Forms.RadioButton();
            this.stateOfMindGroup = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.stateOfMindGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // SoMJoyRadio
            // 
            this.SoMJoyRadio.AutoSize = true;
            this.SoMJoyRadio.Checked = true;
            this.SoMJoyRadio.Enabled = false;
            this.SoMJoyRadio.Location = new System.Drawing.Point(15, 28);
            this.SoMJoyRadio.Margin = new System.Windows.Forms.Padding(2);
            this.SoMJoyRadio.Name = "SoMJoyRadio";
            this.SoMJoyRadio.Size = new System.Drawing.Size(41, 17);
            this.SoMJoyRadio.TabIndex = 0;
            this.SoMJoyRadio.TabStop = true;
            this.SoMJoyRadio.Text = "Joy";
            this.SoMJoyRadio.UseVisualStyleBackColor = true;
            this.SoMJoyRadio.CheckedChanged += new System.EventHandler(this.SoMJoyRadio_CheckedChanged);
            // 
            // SoMAngerRadio
            // 
            this.SoMAngerRadio.AutoSize = true;
            this.SoMAngerRadio.Enabled = false;
            this.SoMAngerRadio.Location = new System.Drawing.Point(15, 98);
            this.SoMAngerRadio.Margin = new System.Windows.Forms.Padding(2);
            this.SoMAngerRadio.Name = "SoMAngerRadio";
            this.SoMAngerRadio.Size = new System.Drawing.Size(53, 17);
            this.SoMAngerRadio.TabIndex = 1;
            this.SoMAngerRadio.TabStop = true;
            this.SoMAngerRadio.Text = "Anger";
            this.SoMAngerRadio.UseVisualStyleBackColor = true;
            this.SoMAngerRadio.CheckedChanged += new System.EventHandler(this.SoMAngerRadio_CheckedChanged);
            // 
            // SoMSorrowRadio
            // 
            this.SoMSorrowRadio.AutoSize = true;
            this.SoMSorrowRadio.Enabled = false;
            this.SoMSorrowRadio.Location = new System.Drawing.Point(15, 63);
            this.SoMSorrowRadio.Margin = new System.Windows.Forms.Padding(2);
            this.SoMSorrowRadio.Name = "SoMSorrowRadio";
            this.SoMSorrowRadio.Size = new System.Drawing.Size(58, 17);
            this.SoMSorrowRadio.TabIndex = 2;
            this.SoMSorrowRadio.TabStop = true;
            this.SoMSorrowRadio.Text = "Sorrow";
            this.SoMSorrowRadio.UseVisualStyleBackColor = true;
            this.SoMSorrowRadio.CheckedChanged += new System.EventHandler(this.SoMSorrowRadio_CheckedChanged);
            // 
            // SoMFearRadio
            // 
            this.SoMFearRadio.AutoSize = true;
            this.SoMFearRadio.Enabled = false;
            this.SoMFearRadio.Location = new System.Drawing.Point(15, 136);
            this.SoMFearRadio.Margin = new System.Windows.Forms.Padding(2);
            this.SoMFearRadio.Name = "SoMFearRadio";
            this.SoMFearRadio.Size = new System.Drawing.Size(46, 17);
            this.SoMFearRadio.TabIndex = 3;
            this.SoMFearRadio.TabStop = true;
            this.SoMFearRadio.Text = "Fear";
            this.SoMFearRadio.UseVisualStyleBackColor = true;
            this.SoMFearRadio.CheckedChanged += new System.EventHandler(this.SoMFearRadio_CheckedChanged);
            // 
            // stateOfMindGroup
            // 
            this.stateOfMindGroup.Controls.Add(this.SoMJoyRadio);
            this.stateOfMindGroup.Controls.Add(this.SoMFearRadio);
            this.stateOfMindGroup.Controls.Add(this.SoMAngerRadio);
            this.stateOfMindGroup.Controls.Add(this.SoMSorrowRadio);
            this.stateOfMindGroup.Location = new System.Drawing.Point(29, 69);
            this.stateOfMindGroup.Margin = new System.Windows.Forms.Padding(2);
            this.stateOfMindGroup.Name = "stateOfMindGroup";
            this.stateOfMindGroup.Padding = new System.Windows.Forms.Padding(2);
            this.stateOfMindGroup.Size = new System.Drawing.Size(167, 190);
            this.stateOfMindGroup.TabIndex = 4;
            this.stateOfMindGroup.TabStop = false;
            this.stateOfMindGroup.Tag = "0";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(248, 260);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(25, 7);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(309, 24);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = ": Select State of Mind to Change";
            // 
            // SoMSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 297);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stateOfMindGroup);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SoMSelectorForm";
            this.Text = "Change to State of Mind";
            this.stateOfMindGroup.ResumeLayout(false);
            this.stateOfMindGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.RadioButton SoMJoyRadio;
        private System.Windows.Forms.RadioButton SoMAngerRadio;
        private System.Windows.Forms.RadioButton SoMSorrowRadio;
        private System.Windows.Forms.RadioButton SoMFearRadio;
        private System.Windows.Forms.GroupBox stateOfMindGroup;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label resultLabel;
    }
}