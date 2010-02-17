using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmGrayscalePreview : DVDScribe.frmPreviewBase
    {
        public frmGrayscalePreview()
        {
            InitializeComponent();
        }

        public frmGrayscalePreview(Bitmap image)
        {
            InitializeComponent();
            pbxOrg.Image = (Bitmap)image.Clone();
            pbxPreview.Image = (Bitmap)image.Clone();
            libImage.GrayScale((Bitmap)pbxPreview.Image);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

    }
}
