namespace Client
{
    partial class frmKarsiPc
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
            this.pbResim = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbResim)).BeginInit();
            this.SuspendLayout();
            // 
            // pbResim
            // 
            this.pbResim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbResim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbResim.Location = new System.Drawing.Point(0, 0);
            this.pbResim.Name = "pbResim";
            this.pbResim.Size = new System.Drawing.Size(244, 118);
            this.pbResim.TabIndex = 0;
            this.pbResim.TabStop = false;
            this.pbResim.Click += new System.EventHandler(this.pbResim_Click);
            this.pbResim.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbResim_MouseDown);
            this.pbResim.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResim_MouseMove);
            this.pbResim.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResim_MouseUp);
            // 
            // frmKarsiPc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 118);
            this.Controls.Add(this.pbResim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmKarsiPc";
            this.Text = "Karşı Bilgisayar";
            this.Load += new System.EventHandler(this.frmKarsiPc_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmKarsiPc_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbResim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbResim;

    }
}