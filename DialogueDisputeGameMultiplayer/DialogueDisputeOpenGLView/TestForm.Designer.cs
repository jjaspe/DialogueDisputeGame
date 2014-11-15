namespace DialogueDisputeOpenGLView
{
    partial class TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.myView = new Canvas_Window_Template.simpleOpenGlView();
            this.myNavigator = new Canvas_Window_Template.Navigator();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // myView
            // 
            this.myView.AccumBits = ((byte)(0));
            this.myView.AutoCheckErrors = false;
            this.myView.AutoFinish = false;
            this.myView.AutoMakeCurrent = true;
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
            this.myView.Size = new System.Drawing.Size(882, 486);
            this.myView.StencilBits = ((byte)(0));
            this.myView.TabIndex = 0;
            this.myView.ViewDistance = 300D;
            this.myView.ViewPhi = 0D;
            this.myView.ViewTheta = 0D;
            // 
            // myNavigator
            // 
            this.myNavigator.Location = new System.Drawing.Point(667, 26);
            this.myNavigator.MyView = null;
            this.myNavigator.MyWindowOwner = null;
            this.myNavigator.Name = "myNavigator";
            this.myNavigator.Orientation = Canvas_Window_Template.Basic_Drawing_Functions.Common.planeOrientation.None;
            this.myNavigator.Size = new System.Drawing.Size(193, 129);
            this.myNavigator.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 486);
            this.Controls.Add(this.myNavigator);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.myView);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Resize += new System.EventHandler(this.TestForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Canvas_Window_Template.simpleOpenGlView myView;
        private System.Windows.Forms.Button button1;
        private Canvas_Window_Template.Navigator myNavigator;
    }
}