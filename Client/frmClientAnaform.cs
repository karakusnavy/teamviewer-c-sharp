using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frmClientAnaform : Form
    {
        public frmClientAnaform()
        {
            InitializeComponent();
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            frmKarsiPc frm = new frmKarsiPc(txtKarsiIP.Text);
            frm.Show();
        }
    }
}
