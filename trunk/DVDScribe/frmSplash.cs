using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Shown(object sender, EventArgs e)
        {
            tmWaitForFade.Enabled = true;
        }

        private void tmWaitForFade_Tick(object sender, EventArgs e)
        {
            tmWaitForFade.Enabled = false;
            tmFade.Enabled = true;
        }

        private void tmFade_Tick(object sender, EventArgs e)
        {
            Opacity = Opacity - 0.05;
            if (Opacity == 0)
            {
                tmFade.Enabled = false;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
