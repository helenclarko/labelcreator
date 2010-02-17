using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DVDScribe
{
    public partial class frmSelectBackground : Form
    {
        private const string CFOLDER_KEY = "LAST_FOLDER";
        public String SelectedFile
        {
            get
            {
                return lv.SelectedItems[0].Tag.ToString();
            }
        }

        public frmSelectBackground()
        {
            InitializeComponent();
            string lastFolderBrowsed = libRegistry.ReadValue(CFOLDER_KEY);
            if (lastFolderBrowsed != "")
            {               
              //  expBrowser.setCurrentPath(lastFolderBrowsed);
            }
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void explorerTree1_PathChanged(object sender, EventArgs e)
        {
          /*  DirectoryInfo dir = new DirectoryInfo(expBrowser.SelectedPath);
            ImageList imgList = new ImageList();
            imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imgList.ImageSize = new System.Drawing.Size(128, 128);
            lv.Items.Clear();
                      
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".jpg" || file.Extension == ".png" || file.Extension == ".bmp")
                {
                    Image thumb = Image.FromFile(file.FullName);
                    imgList.Images.Add(thumb);

                    ListViewItem lvi = lv.Items.Add(file.Name);
                    lvi.ImageIndex = lvi.Index;
                    lvi.Tag = file.FullName;
                }                                       
            }
            lv.LargeImageList = imgList;*/
        }

        private void frmSelectBackground_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    libRegistry.WriteValue(CFOLDER_KEY, expBrowser.SelectedPath);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
 
    }

}
