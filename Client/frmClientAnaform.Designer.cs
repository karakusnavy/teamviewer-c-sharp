namespace Client
{
    partial class frmClientAnaform
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
            this.txtKarsiIP = new System.Windows.Forms.TextBox();
            this.btnBaglan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Karşı IP Giriniz";
            // 
            // txtKarsiIP
            // 
            this.txtKarsiIP.Location = new System.Drawing.Point(104, 25);
            this.txtKarsiIP.Name = "txtKarsiIP";
            this.txtKarsiIP.Size = new System.Drawing.Size(116, 20);
            this.txtKarsiIP.TabIndex = 1;
            // 
            // btnBaglan
            // 
            this.btnBaglan.Location = new System.Drawing.Point(236, 23);
            this.btnBaglan.Name = "btnBaglan";
            this.btnBaglan.Size = new System.Drawing.Size(80, 23);
            this.btnBaglan.TabIndex = 2;
            this.btnBaglan.Text = "Bağlan";
            this.btnBaglan.UseVisualStyleBackColor = true;
            this.btnBaglan.Click += new System.EventHandler(this.btnBaglan_Click);
            // 
            // frmClientAnaform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 67);
            this.Controls.Add(this.btnBaglan);
            this.Controls.Add(this.txtKarsiIP);
            this.Controls.Add(this.label1);
            this.Name = "frmClientAnaform";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKarsiIP;
        private System.Windows.Forms.Button btnBaglan;
    }
}

