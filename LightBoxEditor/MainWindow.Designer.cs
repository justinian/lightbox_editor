namespace LightBoxEditor
{
    partial class MainWindow
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
			this.framePicker = new System.Windows.Forms.NumericUpDown();
			this.frameLabel = new System.Windows.Forms.Label();
			this.addFrameButton = new System.Windows.Forms.Button();
			this.playButton = new System.Windows.Forms.Button();
			this.colorGroupBox = new System.Windows.Forms.GroupBox();
			this.currentColorImageBox = new System.Windows.Forms.PictureBox();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.frameDisplay = new System.Windows.Forms.Panel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer( this.components );
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.lerpColorsCheckbox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.framePicker)).BeginInit();
			this.colorGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.currentColorImageBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// framePicker
			// 
			this.framePicker.Location = new System.Drawing.Point( 460, 33 );
			this.framePicker.Name = "framePicker";
			this.framePicker.Size = new System.Drawing.Size( 64, 20 );
			this.framePicker.TabIndex = 0;
			this.framePicker.ValueChanged += new System.EventHandler( this.framePicker_ValueChanged );
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point( 418, 35 );
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size( 36, 13 );
			this.frameLabel.TabIndex = 1;
			this.frameLabel.Text = "Frame";
			// 
			// addFrameButton
			// 
			this.addFrameButton.Location = new System.Drawing.Point( 447, 59 );
			this.addFrameButton.Name = "addFrameButton";
			this.addFrameButton.Size = new System.Drawing.Size( 75, 23 );
			this.addFrameButton.TabIndex = 3;
			this.addFrameButton.Text = "Add Frame";
			this.addFrameButton.UseVisualStyleBackColor = true;
			this.addFrameButton.Click += new System.EventHandler( this.addFrameButton_Click );
			// 
			// playButton
			// 
			this.playButton.Location = new System.Drawing.Point( 447, 88 );
			this.playButton.Name = "playButton";
			this.playButton.Size = new System.Drawing.Size( 75, 23 );
			this.playButton.TabIndex = 4;
			this.playButton.Text = "Play";
			this.playButton.UseVisualStyleBackColor = true;
			this.playButton.Click += new System.EventHandler( this.playButton_Click );
			// 
			// colorGroupBox
			// 
			this.colorGroupBox.Controls.Add( this.currentColorImageBox );
			this.colorGroupBox.Location = new System.Drawing.Point( 418, 139 );
			this.colorGroupBox.Name = "colorGroupBox";
			this.colorGroupBox.Size = new System.Drawing.Size( 104, 59 );
			this.colorGroupBox.TabIndex = 5;
			this.colorGroupBox.TabStop = false;
			this.colorGroupBox.Text = "Current Color";
			// 
			// currentColorImageBox
			// 
			this.currentColorImageBox.Location = new System.Drawing.Point( 6, 19 );
			this.currentColorImageBox.Name = "currentColorImageBox";
			this.currentColorImageBox.Size = new System.Drawing.Size( 92, 34 );
			this.currentColorImageBox.TabIndex = 0;
			this.currentColorImageBox.TabStop = false;
			this.currentColorImageBox.Click += new System.EventHandler( this.currentColorImageBox_Click );
			// 
			// colorDialog
			// 
			this.colorDialog.AnyColor = true;
			this.colorDialog.SolidColorOnly = true;
			// 
			// frameDisplay
			// 
			this.frameDisplay.Location = new System.Drawing.Point( 12, 33 );
			this.frameDisplay.Name = "frameDisplay";
			this.frameDisplay.Size = new System.Drawing.Size( 400, 400 );
			this.frameDisplay.TabIndex = 6;
			this.frameDisplay.Click += new System.EventHandler( this.frameDisplay_Click );
			this.frameDisplay.Paint += new System.Windows.Forms.PaintEventHandler( this.frameDisplay_Paint );
			this.frameDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler( this.frameDisplay_MouseDown );
			this.frameDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler( this.frameDisplay_MouseMove );
			this.frameDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler( this.frameDisplay_MouseUp );
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem} );
			this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size( 534, 24 );
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem} );
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler( this.newToolStripMenuItem_Click );
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.openToolStripMenuItem.Text = "Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler( this.openToolStripMenuItem_Click );
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.saveToolStripMenuItem.Text = "Save...";
			this.saveToolStripMenuItem.Click += new System.EventHandler( this.saveToolStripMenuItem_Click );
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler( this.saveAsToolStripMenuItem_Click );
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.helpToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem} );
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size( 44, 20 );
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size( 107, 22 );
			this.helpToolStripMenuItem1.Text = "Help";
			this.helpToolStripMenuItem1.Click += new System.EventHandler( this.helpToolStripMenuItem1_Click );
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
			// 
			// timer1
			// 
			this.timer1.Interval = 30;
			this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
			// 
			// lerpColorsCheckbox
			// 
			this.lerpColorsCheckbox.AutoSize = true;
			this.lerpColorsCheckbox.Location = new System.Drawing.Point( 424, 117 );
			this.lerpColorsCheckbox.Name = "lerpColorsCheckbox";
			this.lerpColorsCheckbox.Size = new System.Drawing.Size( 79, 17 );
			this.lerpColorsCheckbox.TabIndex = 8;
			this.lerpColorsCheckbox.Text = "Lerp Colors";
			this.lerpColorsCheckbox.UseVisualStyleBackColor = true;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 534, 447 );
			this.Controls.Add( this.lerpColorsCheckbox );
			this.Controls.Add( this.frameDisplay );
			this.Controls.Add( this.colorGroupBox );
			this.Controls.Add( this.playButton );
			this.Controls.Add( this.addFrameButton );
			this.Controls.Add( this.frameLabel );
			this.Controls.Add( this.framePicker );
			this.Controls.Add( this.menuStrip1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "LightBox Editor";
			((System.ComponentModel.ISupportInitialize)(this.framePicker)).EndInit();
			this.colorGroupBox.ResumeLayout( false );
			((System.ComponentModel.ISupportInitialize)(this.currentColorImageBox)).EndInit();
			this.menuStrip1.ResumeLayout( false );
			this.menuStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown framePicker;
        private System.Windows.Forms.Label frameLabel;
        private System.Windows.Forms.Button addFrameButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.GroupBox colorGroupBox;
        private System.Windows.Forms.PictureBox currentColorImageBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel frameDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.CheckBox lerpColorsCheckbox;
    }
}

