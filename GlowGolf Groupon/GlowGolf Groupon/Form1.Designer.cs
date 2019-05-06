namespace GlowGolf_Groupon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblVoucherNumber = new System.Windows.Forms.Label();
            this.txtVoucherNumber = new System.Windows.Forms.TextBox();
            this.lblGrouponNumber = new System.Windows.Forms.Label();
            this.txtGrouponNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(48, 52);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(328, 22);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(45, 32);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // lblVoucherNumber
            // 
            this.lblVoucherNumber.AutoSize = true;
            this.lblVoucherNumber.Location = new System.Drawing.Point(45, 109);
            this.lblVoucherNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVoucherNumber.Name = "lblVoucherNumber";
            this.lblVoucherNumber.Size = new System.Drawing.Size(112, 16);
            this.lblVoucherNumber.TabIndex = 3;
            this.lblVoucherNumber.Text = "Voucher Number:";
            // 
            // txtVoucherNumber
            // 
            this.txtVoucherNumber.Location = new System.Drawing.Point(48, 129);
            this.txtVoucherNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtVoucherNumber.Name = "txtVoucherNumber";
            this.txtVoucherNumber.Size = new System.Drawing.Size(328, 22);
            this.txtVoucherNumber.TabIndex = 1;
            // 
            // lblGrouponNumber
            // 
            this.lblGrouponNumber.AutoSize = true;
            this.lblGrouponNumber.Location = new System.Drawing.Point(45, 179);
            this.lblGrouponNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrouponNumber.Name = "lblGrouponNumber";
            this.lblGrouponNumber.Size = new System.Drawing.Size(114, 16);
            this.lblGrouponNumber.TabIndex = 5;
            this.lblGrouponNumber.Text = "Groupon Number:";
            // 
            // txtGrouponNumber
            // 
            this.txtGrouponNumber.Location = new System.Drawing.Point(48, 199);
            this.txtGrouponNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtGrouponNumber.Name = "txtGrouponNumber";
            this.txtGrouponNumber.Size = new System.Drawing.Size(328, 22);
            this.txtGrouponNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 250);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date:";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(48, 270);
            this.txtDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(328, 22);
            this.txtDate.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(48, 330);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(153, 67);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(223, 330);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(153, 67);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 422);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblGrouponNumber);
            this.Controls.Add(this.txtGrouponNumber);
            this.Controls.Add(this.lblVoucherNumber);
            this.Controls.Add(this.txtVoucherNumber);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "GlowGolf Groupon";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblVoucherNumber;
        private System.Windows.Forms.TextBox txtVoucherNumber;
        private System.Windows.Forms.Label lblGrouponNumber;
        private System.Windows.Forms.TextBox txtGrouponNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnClear;
    }
}

