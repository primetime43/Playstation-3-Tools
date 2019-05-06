namespace Red_EyeX32___Test_Drive_Unlimited_2_Save_Editor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.GameSaveInformationTabPage = new System.Windows.Forms.TabPage();
            this.AccountInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.BackUpSFOButton = new System.Windows.Forms.Button();
            this.AccountIDTextBox = new System.Windows.Forms.TextBox();
            this.BackUpDATAButton = new System.Windows.Forms.Button();
            this.AccountIDLabel = new System.Windows.Forms.Label();
            this.PatchPARAMButton = new System.Windows.Forms.Button();
            this.UpdateAccountIDButton = new System.Windows.Forms.Button();
            this.VerifyVersionButton = new System.Windows.Forms.Button();
            this.GameVersionTextBox = new System.Windows.Forms.TextBox();
            this.GameVersionLabel = new System.Windows.Forms.Label();
            this.GameSaveKeyTextBox = new System.Windows.Forms.TextBox();
            this.GameSaveKeyLabel = new System.Windows.Forms.Label();
            this.ImageGroupBox = new System.Windows.Forms.GroupBox();
            this.ViewImageButton = new System.Windows.Forms.Button();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.SaveGameImagePictureBox = new System.Windows.Forms.PictureBox();
            this.GameSaveEditingTabPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CasinoChipsLabel = new System.Windows.Forms.Label();
            this.CasinoChipsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MoneyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MoneyLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TabControl.SuspendLayout();
            this.GameSaveInformationTabPage.SuspendLayout();
            this.AccountInfoGroupBox.SuspendLayout();
            this.ImageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveGameImagePictureBox)).BeginInit();
            this.GameSaveEditingTabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CasinoChipsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoneyNumericUpDown)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.GameSaveInformationTabPage);
            this.TabControl.Controls.Add(this.GameSaveEditingTabPage);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Enabled = false;
            this.TabControl.Location = new System.Drawing.Point(0, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(499, 202);
            this.TabControl.TabIndex = 5;
            // 
            // GameSaveInformationTabPage
            // 
            this.GameSaveInformationTabPage.Controls.Add(this.AccountInfoGroupBox);
            this.GameSaveInformationTabPage.Controls.Add(this.ImageGroupBox);
            this.GameSaveInformationTabPage.Location = new System.Drawing.Point(4, 22);
            this.GameSaveInformationTabPage.Name = "GameSaveInformationTabPage";
            this.GameSaveInformationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GameSaveInformationTabPage.Size = new System.Drawing.Size(491, 176);
            this.GameSaveInformationTabPage.TabIndex = 0;
            this.GameSaveInformationTabPage.Text = "Game Save Information";
            this.GameSaveInformationTabPage.UseVisualStyleBackColor = true;
            // 
            // AccountInfoGroupBox
            // 
            this.AccountInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.AccountInfoGroupBox.Controls.Add(this.BackUpSFOButton);
            this.AccountInfoGroupBox.Controls.Add(this.AccountIDTextBox);
            this.AccountInfoGroupBox.Controls.Add(this.BackUpDATAButton);
            this.AccountInfoGroupBox.Controls.Add(this.AccountIDLabel);
            this.AccountInfoGroupBox.Controls.Add(this.PatchPARAMButton);
            this.AccountInfoGroupBox.Controls.Add(this.UpdateAccountIDButton);
            this.AccountInfoGroupBox.Controls.Add(this.VerifyVersionButton);
            this.AccountInfoGroupBox.Controls.Add(this.GameVersionTextBox);
            this.AccountInfoGroupBox.Controls.Add(this.GameVersionLabel);
            this.AccountInfoGroupBox.Controls.Add(this.GameSaveKeyTextBox);
            this.AccountInfoGroupBox.Controls.Add(this.GameSaveKeyLabel);
            this.AccountInfoGroupBox.Location = new System.Drawing.Point(134, 5);
            this.AccountInfoGroupBox.Name = "AccountInfoGroupBox";
            this.AccountInfoGroupBox.Size = new System.Drawing.Size(354, 163);
            this.AccountInfoGroupBox.TabIndex = 14;
            this.AccountInfoGroupBox.TabStop = false;
            // 
            // BackUpSFOButton
            // 
            this.BackUpSFOButton.Location = new System.Drawing.Point(179, 134);
            this.BackUpSFOButton.Name = "BackUpSFOButton";
            this.BackUpSFOButton.Size = new System.Drawing.Size(169, 23);
            this.BackUpSFOButton.TabIndex = 1;
            this.BackUpSFOButton.Text = "Back Up .SFO";
            this.BackUpSFOButton.UseVisualStyleBackColor = true;
            this.BackUpSFOButton.Click += new System.EventHandler(this.BackUpSFOButton_Click);
            // 
            // AccountIDTextBox
            // 
            this.AccountIDTextBox.Location = new System.Drawing.Point(103, 19);
            this.AccountIDTextBox.Name = "AccountIDTextBox";
            this.AccountIDTextBox.ReadOnly = true;
            this.AccountIDTextBox.Size = new System.Drawing.Size(245, 20);
            this.AccountIDTextBox.TabIndex = 9;
            // 
            // BackUpDATAButton
            // 
            this.BackUpDATAButton.Location = new System.Drawing.Point(6, 134);
            this.BackUpDATAButton.Name = "BackUpDATAButton";
            this.BackUpDATAButton.Size = new System.Drawing.Size(169, 23);
            this.BackUpDATAButton.TabIndex = 0;
            this.BackUpDATAButton.Text = "Back Up DATA";
            this.BackUpDATAButton.UseVisualStyleBackColor = true;
            this.BackUpDATAButton.Click += new System.EventHandler(this.BackUpDATAButton_Click);
            // 
            // AccountIDLabel
            // 
            this.AccountIDLabel.AutoSize = true;
            this.AccountIDLabel.Location = new System.Drawing.Point(24, 22);
            this.AccountIDLabel.Name = "AccountIDLabel";
            this.AccountIDLabel.Size = new System.Drawing.Size(73, 13);
            this.AccountIDLabel.TabIndex = 8;
            this.AccountIDLabel.Text = "Account ID:";
            // 
            // PatchPARAMButton
            // 
            this.PatchPARAMButton.Location = new System.Drawing.Point(179, 105);
            this.PatchPARAMButton.Name = "PatchPARAMButton";
            this.PatchPARAMButton.Size = new System.Drawing.Size(169, 23);
            this.PatchPARAMButton.TabIndex = 6;
            this.PatchPARAMButton.Text = "Patch PARAM.SFO";
            this.PatchPARAMButton.UseVisualStyleBackColor = true;
            this.PatchPARAMButton.Click += new System.EventHandler(this.PatchPARAMButton_Click);
            // 
            // UpdateAccountIDButton
            // 
            this.UpdateAccountIDButton.Location = new System.Drawing.Point(6, 105);
            this.UpdateAccountIDButton.Name = "UpdateAccountIDButton";
            this.UpdateAccountIDButton.Size = new System.Drawing.Size(169, 23);
            this.UpdateAccountIDButton.TabIndex = 5;
            this.UpdateAccountIDButton.Text = "Update Account ID";
            this.UpdateAccountIDButton.UseVisualStyleBackColor = true;
            this.UpdateAccountIDButton.Click += new System.EventHandler(this.UpdateAccountIDButton_Click);
            // 
            // VerifyVersionButton
            // 
            this.VerifyVersionButton.Location = new System.Drawing.Point(218, 71);
            this.VerifyVersionButton.Name = "VerifyVersionButton";
            this.VerifyVersionButton.Size = new System.Drawing.Size(130, 22);
            this.VerifyVersionButton.TabIndex = 4;
            this.VerifyVersionButton.Text = "Verify Version";
            this.VerifyVersionButton.UseVisualStyleBackColor = true;
            this.VerifyVersionButton.Click += new System.EventHandler(this.VerifyVersionButton_Click);
            // 
            // GameVersionTextBox
            // 
            this.GameVersionTextBox.Location = new System.Drawing.Point(103, 73);
            this.GameVersionTextBox.Name = "GameVersionTextBox";
            this.GameVersionTextBox.ReadOnly = true;
            this.GameVersionTextBox.Size = new System.Drawing.Size(109, 20);
            this.GameVersionTextBox.TabIndex = 3;
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.AutoSize = true;
            this.GameVersionLabel.Location = new System.Drawing.Point(12, 76);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(85, 13);
            this.GameVersionLabel.TabIndex = 2;
            this.GameVersionLabel.Text = "Game Version:";
            // 
            // GameSaveKeyTextBox
            // 
            this.GameSaveKeyTextBox.Location = new System.Drawing.Point(103, 45);
            this.GameSaveKeyTextBox.Name = "GameSaveKeyTextBox";
            this.GameSaveKeyTextBox.ReadOnly = true;
            this.GameSaveKeyTextBox.Size = new System.Drawing.Size(245, 20);
            this.GameSaveKeyTextBox.TabIndex = 1;
            // 
            // GameSaveKeyLabel
            // 
            this.GameSaveKeyLabel.AutoSize = true;
            this.GameSaveKeyLabel.Location = new System.Drawing.Point(6, 48);
            this.GameSaveKeyLabel.Name = "GameSaveKeyLabel";
            this.GameSaveKeyLabel.Size = new System.Drawing.Size(91, 13);
            this.GameSaveKeyLabel.TabIndex = 0;
            this.GameSaveKeyLabel.Text = "Game Save Key:";
            // 
            // ImageGroupBox
            // 
            this.ImageGroupBox.Controls.Add(this.ViewImageButton);
            this.ImageGroupBox.Controls.Add(this.SaveImageButton);
            this.ImageGroupBox.Controls.Add(this.SaveGameImagePictureBox);
            this.ImageGroupBox.Location = new System.Drawing.Point(8, 5);
            this.ImageGroupBox.Name = "ImageGroupBox";
            this.ImageGroupBox.Size = new System.Drawing.Size(120, 163);
            this.ImageGroupBox.TabIndex = 11;
            this.ImageGroupBox.TabStop = false;
            // 
            // ViewImageButton
            // 
            this.ViewImageButton.Location = new System.Drawing.Point(6, 134);
            this.ViewImageButton.Name = "ViewImageButton";
            this.ViewImageButton.Size = new System.Drawing.Size(108, 23);
            this.ViewImageButton.TabIndex = 7;
            this.ViewImageButton.Text = "View Image";
            this.ViewImageButton.UseVisualStyleBackColor = true;
            this.ViewImageButton.Click += new System.EventHandler(this.ViewImageButton_Click);
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.Location = new System.Drawing.Point(6, 105);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(108, 23);
            this.SaveImageButton.TabIndex = 6;
            this.SaveImageButton.Text = "Save Image";
            this.SaveImageButton.UseVisualStyleBackColor = true;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            // 
            // SaveGameImagePictureBox
            // 
            this.SaveGameImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaveGameImagePictureBox.Location = new System.Drawing.Point(6, 14);
            this.SaveGameImagePictureBox.Name = "SaveGameImagePictureBox";
            this.SaveGameImagePictureBox.Size = new System.Drawing.Size(108, 85);
            this.SaveGameImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SaveGameImagePictureBox.TabIndex = 5;
            this.SaveGameImagePictureBox.TabStop = false;
            // 
            // GameSaveEditingTabPage
            // 
            this.GameSaveEditingTabPage.Controls.Add(this.groupBox3);
            this.GameSaveEditingTabPage.Location = new System.Drawing.Point(4, 22);
            this.GameSaveEditingTabPage.Name = "GameSaveEditingTabPage";
            this.GameSaveEditingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GameSaveEditingTabPage.Size = new System.Drawing.Size(491, 176);
            this.GameSaveEditingTabPage.TabIndex = 1;
            this.GameSaveEditingTabPage.Text = "Game Save Editing";
            this.GameSaveEditingTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.CasinoChipsLabel);
            this.groupBox3.Controls.Add(this.CasinoChipsNumericUpDown);
            this.groupBox3.Controls.Add(this.MoneyNumericUpDown);
            this.groupBox3.Controls.Add(this.MoneyLabel);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 131);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Player Data";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(109, 20);
            this.textBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Current Planet:";
            // 
            // CasinoChipsLabel
            // 
            this.CasinoChipsLabel.AutoSize = true;
            this.CasinoChipsLabel.Location = new System.Drawing.Point(6, 75);
            this.CasinoChipsLabel.Name = "CasinoChipsLabel";
            this.CasinoChipsLabel.Size = new System.Drawing.Size(73, 13);
            this.CasinoChipsLabel.TabIndex = 13;
            this.CasinoChipsLabel.Text = "Raritanium:";
            // 
            // CasinoChipsNumericUpDown
            // 
            this.CasinoChipsNumericUpDown.Location = new System.Drawing.Point(131, 73);
            this.CasinoChipsNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.CasinoChipsNumericUpDown.Name = "CasinoChipsNumericUpDown";
            this.CasinoChipsNumericUpDown.Size = new System.Drawing.Size(110, 20);
            this.CasinoChipsNumericUpDown.TabIndex = 12;
            // 
            // MoneyNumericUpDown
            // 
            this.MoneyNumericUpDown.Location = new System.Drawing.Point(131, 45);
            this.MoneyNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.MoneyNumericUpDown.Name = "MoneyNumericUpDown";
            this.MoneyNumericUpDown.Size = new System.Drawing.Size(110, 20);
            this.MoneyNumericUpDown.TabIndex = 11;
            // 
            // MoneyLabel
            // 
            this.MoneyLabel.AutoSize = true;
            this.MoneyLabel.Location = new System.Drawing.Point(6, 47);
            this.MoneyLabel.Name = "MoneyLabel";
            this.MoneyLabel.Size = new System.Drawing.Size(43, 13);
            this.MoneyLabel.TabIndex = 10;
            this.MoneyLabel.Text = "Bolts:";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(499, 24);
            this.MenuStrip.TabIndex = 4;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.creditsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 226);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(515, 265);
            this.MinimumSize = new System.Drawing.Size(515, 265);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ratchet & Clank Save Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.TabControl.ResumeLayout(false);
            this.GameSaveInformationTabPage.ResumeLayout(false);
            this.AccountInfoGroupBox.ResumeLayout(false);
            this.AccountInfoGroupBox.PerformLayout();
            this.ImageGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaveGameImagePictureBox)).EndInit();
            this.GameSaveEditingTabPage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CasinoChipsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoneyNumericUpDown)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage GameSaveInformationTabPage;
        private System.Windows.Forms.GroupBox AccountInfoGroupBox;
        private System.Windows.Forms.Button BackUpSFOButton;
        private System.Windows.Forms.TextBox AccountIDTextBox;
        private System.Windows.Forms.Button BackUpDATAButton;
        private System.Windows.Forms.Label AccountIDLabel;
        private System.Windows.Forms.Button PatchPARAMButton;
        private System.Windows.Forms.Button UpdateAccountIDButton;
        private System.Windows.Forms.Button VerifyVersionButton;
        private System.Windows.Forms.TextBox GameVersionTextBox;
        private System.Windows.Forms.Label GameVersionLabel;
        private System.Windows.Forms.TextBox GameSaveKeyTextBox;
        private System.Windows.Forms.Label GameSaveKeyLabel;
        private System.Windows.Forms.GroupBox ImageGroupBox;
        private System.Windows.Forms.Button ViewImageButton;
        private System.Windows.Forms.Button SaveImageButton;
        private System.Windows.Forms.PictureBox SaveGameImagePictureBox;
        private System.Windows.Forms.TabPage GameSaveEditingTabPage;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label CasinoChipsLabel;
        private System.Windows.Forms.NumericUpDown CasinoChipsNumericUpDown;
        private System.Windows.Forms.NumericUpDown MoneyNumericUpDown;
        private System.Windows.Forms.Label MoneyLabel;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

