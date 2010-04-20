using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DVDScribe
{
    class libControls
    {

        public class dsControl
        {
            public delegate void OnChangedEvent();
            public OnChangedEvent OnChanged;
            public Point Location;
            public Size Dimention;
            public bool isPickedUp = false;
            public bool editorVisible = false;
            private bool pSelected = false;
            protected bool pHasBorder = false;
            protected bool pHide = false;
            private int dragStartX;
            private int dragStartY;

            public bool Selected
            {
                get
                {
                    return pSelected;
                }
                set
                {
                    pSelected = value;
                    if (OnChanged != null)
                    {
                        OnChanged();
                    }
                }
            }


            public int Height
            {
                get
                {
                    return Dimention.Height;
                }
                set
                {
             
                }
            }

            public int Width
            {
                get
                {
                    return Dimention.Width;
                }
                set
                {

                }
            }


            public dsControl()
            {
                Location = new Point();
                Dimention = new Size();
            }

            public dsControl(int X,int Y, int Height, int Width)
            {
                Location = new Point(X, Y);
                Dimention = new Size(Width, Height);
            }

            public void PickUp(int X, int Y)
            {
                isPickedUp = true;
                Selected = true;
                dragStartX = this.Location.X - X;
                dragStartY = this.Location.Y - Y;
            }

            public void Drag(int X, int Y)
            {
                Point MPosition = new Point(X,Y);
                MPosition.Offset(dragStartX, dragStartY);
                this.Location = MPosition;                
            }

            public void PutDown()
            {
                isPickedUp = false;
            }

            public virtual void Paint(Graphics g){}
            public virtual void LauchEditor(Control c) { }
            public virtual void AddToImage(Graphics g) { }
            public virtual void CloseEditor(Control c) { }

            public bool isOver(int x, int y)
            {
                if ((x >= this.Location.X) && (x <= (this.Location.X + this.Dimention.Width)) &&
                    (y >= this.Location.Y) && (y <= (this.Location.Y + this.Dimention.Height)))
                {
                    return true;
                } else 
                {
                    return false;
                }
            }

        }

        public class ImageField : dsControl
        {
            private Bitmap pImage = null;
            private string FilePath = "";
            private double pZoomH = 0;
            private double pZoomV = 0;
            private ImageEdit Editor;

            public ImageField(string FilePath,int X, int Y, int Height, int Width) : base(X, Y, Height, Width) 
            {
                
                LoadFromFile(FilePath);
                Location = new Point(X,Y);
                if (Height > 0 && Width > 0)
                {
                    Dimention.Height = Height;
                    Dimention.Width = Width;
                }
            }

            public void LoadFromFile(string FilePath)
            {
                if (!System.IO.File.Exists(FilePath)) return;
                this.FilePath = FilePath;
                pImage = (Bitmap)Bitmap.FromFile(FilePath, false);
                pZoomH = 1;
                pZoomV = 1;
                if (pImage.Width > 300)
                {
                    pZoomH = 200.0 / pImage.Width;
                    pZoomV = pZoomH;
                }
                if (pImage.Height > 300)
                {
                    pZoomH = 200.0 / pImage.Height;
                    pZoomV = pZoomH;
                }
                Dimention.Height = (int)(pImage.Height * pZoomV);
                Dimention.Width = (int)(pImage.Width * pZoomH);
            }

            
            public void SaveToFile(string FilePath)
            {
                
                pImage.Save(FilePath, System.Drawing.Imaging.ImageFormat.Png);
              
            }

            public override void Paint(Graphics g)
            {
                if (pHide) return;
                if (pImage != null)
                {
                    Rectangle rect = new Rectangle(Location.X, Location.Y, Dimention.Width , Dimention.Height );
                    g.DrawImage(pImage, rect);
                }
                Color BorderColor = Color.FromArgb(255, 0, 0, 0);
                if (Selected)
                {
                    BorderColor = Color.Lime;
                }
                Pen borderPen = new Pen(BorderColor);               
                g.DrawRectangle(borderPen, this.Location.X, this.Location.Y, this.Dimention.Width, this.Dimention.Height);
            }

            public override void AddToImage(Graphics g)
            {
                if (pHide) return;
                if (pImage != null)
                {
                    Rectangle rect = new Rectangle(Location.X, Location.Y, (int)(pImage.Width * pZoomH), (int)(pImage.Height * pZoomV));
                    g.DrawImage(pImage, rect);
                }
                Color BorderColor = Color.FromArgb(255, 0, 0, 0);
                if (pHasBorder)
                {
                    Pen borderPen = new Pen(BorderColor);
                    g.DrawRectangle(borderPen, this.Location.X, this.Location.Y, this.Dimention.Width, this.Dimention.Height);
                }                  
            }

            public override void LauchEditor(Control c)
            {
                pHide = true;
                if (this.FilePath == "")
                {
                    this.Editor = new ImageEdit();
                }
                else
                {
                    this.Editor = new ImageEdit(this.FilePath,new Size(this.Dimention.Width + 2,this.Dimention.Height + 2));
                }
                c.Controls.Add(Editor);
                Point NewLocation = new Point(this.Location.X - 1, this.Location.Y - 1);
                Editor.Location = NewLocation;
                editorVisible = true;
            }

            public override void CloseEditor(Control c)
            {
                this.FilePath = Editor.dlgOpenFile.FileName;
                this.LoadFromFile(this.FilePath);
                this.Dimention = Editor.pbxImage.Size;
                this.Location = new Point(Editor.Location.X + 1, Editor.Location.Y + 1);
                if (pImage != null)
                {
                    pZoomH = (double)this.Dimention.Width / (double)pImage.Width;
                    pZoomV = (double)this.Dimention.Height / (double)pImage.Height;
                }
                c.Controls.Remove(Editor);
                editorVisible = false;
                pHide = false;
                if (OnChanged != null)
                {
                    OnChanged();
                }
            }
        }

        public class TextField : dsControl
        {
            private String pText = "<Label>";
            private Font pFont = new Font("Verdana", 10);
            private Color pColor = Color.Black;            
            private TextEdit Editor;

            public String Text {
                get
                {
                    return pText;
                }
                set
                {
                    pText = value;
                    if (OnChanged != null)
                    {
                        OnChanged();
                    }
                }
            }            

            public TextField(int X, int Y, int Height, int Width):base(X, Y, Height, Width) { }

            public override void Paint(Graphics g)
            {
                this.Dimention.Height = (pFont.Height * getLineCount(Text)) + 2;
                this.Dimention.Width = g.MeasureString(pText, pFont).ToSize().Width + 4;
                Color BorderColor = Color.FromArgb(255, 0, 0, 0);
                if (Selected)
                {
                    BorderColor = Color.Lime;
                }
                Pen borderPen = new Pen(BorderColor);
                //Pen borderPen = new Pen(Color.FromArgb(255, 0, 0, 0));
                g.DrawRectangle(borderPen, this.Location.X, this.Location.Y, this.Dimention.Width, this.Dimention.Height);
                g.DrawString(pText, pFont, new SolidBrush(pColor), this.Location.X + 2, this.Location.Y + 2);
            }

            public override void AddToImage(Graphics g)
            {
                this.Dimention.Height = (pFont.Height * getLineCount(Text)) + 2;
                this.Dimention.Width = g.MeasureString(pText, pFont).ToSize().Width + 4;
                Color BorderColor = Color.FromArgb(255, 0, 0, 0);
                
                Pen borderPen = new Pen(BorderColor);
                if (pHasBorder)
                {
                    g.DrawRectangle(borderPen, this.Location.X, this.Location.Y, this.Dimention.Width, this.Dimention.Height);
                }                
                g.DrawString(pText, pFont, new SolidBrush(pColor), this.Location.X + 2, this.Location.Y + 2);
            }

            private int getLineCount(string Text)
            {
                int lineCount = 0;
                foreach (string line in Text.Split(new char[]{'\n'}))
                {
                    lineCount++;
                }
                if (lineCount == 0)
                {
                    lineCount = 1;
                }
                return lineCount;
            }

            private int getCharCount(string Text)
            {
                return Text.Length;
            }

            public override void CloseEditor(Control c)
            {                
                pFont = Editor.txtText.Font;
                pColor = Editor.txtText.ForeColor;
                Text = Editor.txtText.Text;
                c.Controls.Remove(Editor);
                editorVisible = false;
                pHide = false;
            }

            private void EditorClosed(object sender, ControlEventArgs e)
            {

            }

            public override void LauchEditor(Control c) 
            {
                pHide = true;
                this.Editor = new TextEdit(Text, pFont, pColor);
                c.Controls.Add(Editor);
                Editor.Location = this.Location;
                editorVisible = true;
                
               /* if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    pFont = frmEdit.txtText.Font;
                    pColor = frmEdit.dlgFont.Color;
                    Text = frmEdit.txtText.Text;                                        
                }*/
            }
        }
    }
}
