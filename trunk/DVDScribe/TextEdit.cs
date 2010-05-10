using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class TextEdit : UserControl
    {
        public TextEdit()
        {
            InitializeComponent();
        }
        public TextEdit(string Text, Font AFont, Color AColor)
            : base()
        {
            InitializeComponent();
            txtText.ForeColor = AColor;
            txtText.Text = Text;
            txtText.Font = AFont;
            resizeForText();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            dlgFont.Font = txtText.Font;
            if (dlgFont.ShowDialog() == DialogResult.OK)
            {
                txtText.Font = dlgFont.Font;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                txtText.ForeColor = dlgColor.Color;
            }
        }

        private void resizeForText()
        {
            int lineCount = 0;
            foreach (string line in txtText.Text.Split(new char[] { '\n' }))
            {
                lineCount++;
            }
            if (lineCount == 0)
            {
                lineCount = 1;
            }
            int aHeight = (txtText.Font.Height * txtText.Lines.Length) + 12;
            //if (aHeight < this.Size.Height)
            //{
            //    aHeight = this.Size.Height * txtText.Lines.Length;
            //}
            Size aSize = new Size(this.Width, aHeight);
            
            this.Size = aSize;
           
        }

        private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
           // /resizeForText();
        }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            //e.
            resizeForText();

        }

        private void TextEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X == this.Location.X)
            {                
                this.Cursor = Cursors.PanWest;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtText_MultilineChanged(object sender, EventArgs e)
        {
            txtText.Text = txtText.Text + "\n";
        }
    }
}
