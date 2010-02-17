using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmPreviewBase : Form
    {
        public frmPreviewBase()
        {
            InitializeComponent();
        }

        public frmPreviewBase(Bitmap image)
        {
            InitializeComponent();
            pbxOrg.Image = (Bitmap)image.Clone();
            pbxPreview.Image = (Bitmap)image.Clone();
        }
    }
}
