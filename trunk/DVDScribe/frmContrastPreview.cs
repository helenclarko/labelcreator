using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmContrastPreview : DVDScribe.frmPreviewBase
    {
        public frmContrastPreview()
        {
            InitializeComponent();
        }

        public frmContrastPreview(Bitmap image)
        {
            InitializeComponent();
            pbxOrg.Image = (Bitmap)image.Clone();
            pbxPreview.Image = (Bitmap)image.Clone();
        }

        private void tbLevel_MouseUp(object sender, MouseEventArgs e)
        {
            pbxPreview.Image = (Bitmap)pbxOrg.Image.Clone();
            libImage.SetContrast((Bitmap)pbxPreview.Image, (sbyte)tbLevel.Value);               
        }
    }
}
