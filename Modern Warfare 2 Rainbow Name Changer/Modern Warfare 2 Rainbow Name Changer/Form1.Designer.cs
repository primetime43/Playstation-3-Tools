namespace Modern_Warfare_2_Rainbow_Name_Changer
{
    partial class Form1
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
            this.rainButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.rainLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rainTextBox = new System.Windows.Forms.TextBox();
            this.rainTimer = new System.Windows.Forms.Timer(this.components);
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rainButton
            // 
            this.rainButton.Location = new System.Drawing.Point(12, 215);
            this.rainButton.Name = "rainButton";
            this.rainButton.Size = new System.Drawing.Size(128, 47);
            this.rainButton.TabIndex = 0;
            this.rainButton.Text = "Make it rain";
            this.rainButton.UseVisualStyleBackColor = true;
            this.rainButton.Click += new System.EventHandler(this.rainButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(12, 273);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(128, 47);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop the rain";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(315, 215);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(128, 47);
            this.moveButton.TabIndex = 2;
            this.moveButton.Text = "Bounce dat";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // rainLabel
            // 
            this.rainLabel.AutoSize = true;
            this.rainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rainLabel.Location = new System.Drawing.Point(12, 130);
            this.rainLabel.Name = "rainLabel";
            this.rainLabel.Size = new System.Drawing.Size(51, 20);
            this.rainLabel.TabIndex = 3;
            this.rainLabel.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(166, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // rainTextBox
            // 
            this.rainTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rainTextBox.Location = new System.Drawing.Point(75, 42);
            this.rainTextBox.MaxLength = 20;
            this.rainTextBox.Name = "rainTextBox";
            this.rainTextBox.Size = new System.Drawing.Size(296, 26);
            this.rainTextBox.TabIndex = 5;
            // 
            // rainTimer
            // 
            this.rainTimer.Tick += new System.EventHandler(this.rainTimer_Tick);
            // 
            // moveTimer
            // 
            this.moveTimer.Tick += new System.EventHandler(this.moveTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 273);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 47);
            this.button1.TabIndex = 6;
            this.button1.Text = "Restore to default";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 332);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rainTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rainLabel);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.rainButton);
            this.Name = "Form1";
            this.Text = "Rainbow Name Changer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rainButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Label rainLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rainTextBox;
        private System.Windows.Forms.Timer rainTimer;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

