﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{

    public partial class ImageEdit : UserControl
    {

        private enum SizeDirection { sdSizeUp, sdSizeDown, sdSizeLeft, sdSizeRight, sdUpLeft, sdUpRight, sdDownLeft, sdDownRight, sdNotSizing }
        private bool isSizing = false;

        public Size Dimentions
        {
            get
            {
                return pbxImage.Size;
            }
            set
            {
                pbxImage.Size = value;
            }
        }

        public ImageEdit()
        {
            InitializeComponent();
        }

        public ImageEdit(string FilePath, Size Dimention) : base()
        {
            InitializeComponent();
            pbxImage.Image = (Bitmap)Bitmap.FromFile(FilePath);
            this.Size = Dimention;
            this.dlgOpenFile.FileName = FilePath;
            MoveResizeBox();
        }

        private void MoveResizeBox()
        {
            bottomLeft.Top = this.Height - 8;
            centerLeft.Top = (this.Height / 2) - 4;
            centerTop.Left = (this.Width / 2) - 4;
            centerBottom.Left = (this.Width / 2) - 4;
            centerRight.Top = (this.Height / 2) - 4;
            centerRight.Left = this.Width - 8;
            centerBottom.Top = this.Height - 8;
            bottomRight.Top = this.Height - 8;
            bottomRight.Left = this.Width - 8;
            topRight.Left = this.Width - 8;
        }


        private SizeDirection GetSizeDirection(MouseEventArgs e)
        {
            if (e.X == 0 && e.Y == 0)
            {
                this.Cursor = Cursors.SizeNWSE;
                return SizeDirection.sdUpLeft;
            }
            else if (e.X == this.Width - 3 && e.Y == 0)
            {
                this.Cursor = Cursors.SizeNESW;
                return SizeDirection.sdUpRight;
            }
            else if (e.X == 0 && e.Y == this.Height - 1)
            {
                this.Cursor = Cursors.SizeNESW;
                return SizeDirection.sdDownLeft;
            }
            else if (e.X == this.Width - 3 && e.Y == this.Height - 1)
            {
                this.Cursor = Cursors.SizeNWSE;
                return SizeDirection.sdDownRight;
            }
            else if (e.X < 1)
            {
                this.Cursor = Cursors.SizeWE;
                return SizeDirection.sdSizeLeft;
            }
            else if (e.X == this.Width - 1)
            {
                this.Cursor = Cursors.SizeWE;
                return SizeDirection.sdSizeRight;
            }
            else if (e.Y == 0)
            {
                this.Cursor = Cursors.SizeNS;
                return SizeDirection.sdSizeUp;
            }
            else if (e.Y == this.Height - 1)
            {
                this.Cursor = Cursors.SizeNS;
                return SizeDirection.sdSizeDown;
            }
            else
            {
                this.Cursor = Cursors.Default;
                return SizeDirection.sdNotSizing;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            if ((isSizing) && (this.Parent != null) && (this.Parent.IsHandleCreated))
            {
                hwnd = Parent.Handle;
            }
            int nul = 0;
            SizeDirection sdir = GetSizeDirection(e);
            switch (sdir)
            {
                case SizeDirection.sdSizeLeft:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_W, ref nul);
                    } break;

                case SizeDirection.sdSizeRight:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_E, ref nul);
                    } break;

                case SizeDirection.sdSizeDown:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_S, ref nul);
                    } break;
                case SizeDirection.sdSizeUp:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_N, ref nul);
                    } break;
                case SizeDirection.sdUpLeft:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_NW, ref nul);
                    } break;
                case SizeDirection.sdUpRight:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_NE, ref nul);
                    } break;
                case SizeDirection.sdDownLeft:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_SW, ref nul);
                    } break;
                case SizeDirection.sdDownRight:
                    {
                        NativeCalls.ReleaseCapture(hwnd);
                        NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_SE, ref nul);
                    } break;
            }
        }

        private void ImageEdit_MouseMove(object sender, MouseEventArgs e)
        {
            GetSizeDirection(e);
        }

        private void ImageEdit_MouseUp(object sender, MouseEventArgs e)
        {
            isSizing = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbxImage.Image = (Bitmap)Bitmap.FromFile(dlgOpenFile.FileName);
                } catch (OutOfMemoryException err) {
                    MessageBox.Show ("Ocurrio un error al cargar la imagen. Archivo de imagen invalido.");
                    dlgOpenFile.FileName = "";
                }
            }
        }

        private void ImageEdit_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Dotted);
        }

        private void ImageEdit_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            MoveResizeBox();
        }

        private void topLeft_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_NW, ref nul);
        }

        private void topRight_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_NE, ref nul);
        }

        private void bottomRight_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_SE, ref nul);
        }

        private void bottomLeft_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_SW, ref nul);
        }

        private void centerTop_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_N, ref nul);
        }

        private void centerRight_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_E, ref nul);

        }

        private void centerLeft_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_W, ref nul);

        }

        private void centerBottom_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IntPtr hwnd = this.Handle;
            int nul = 0;
            NativeCalls.ReleaseCapture(hwnd);
            NativeCalls.SendMessage(hwnd, NativeCalls.WM_SYSCOMMAND, NativeCalls.SC_DRAGSIZE_S, ref nul);
            
        }

        private void topLeft_Click(object sender, EventArgs e)
        {

        }
    }
}
