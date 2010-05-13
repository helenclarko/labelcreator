using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ExpTreeLib;
using System.Collections;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

namespace DVDScribe
{
    public partial class frmMain : Form
    {
        private enum Mode { [Description("Drag mode selected")] mDrag,[Description("Text adding mode selected")] mText, [Description("Image adding mode selected")] mImage }
        private Bitmap Cover = new Bitmap(640, 640);
        private Bitmap BufferImage = null;
        private Double ZoomV = 1.0;
        private Double ZoomH = 1.0;
        private int StartX = 0;
        private int StartY = 0;
        private int DeltaX = 0;
        private int DeltaY = 0;
        private float pAngle;
        private string CurrentCoverPath = String.Empty;

        private float Angle
        {
            get
            {
                return pAngle;
            }
            set
            {
                pAngle = value;
                rotateImage(Cover, pAngle);
                pbxCanvas.Invalidate();
            }
        }

        private Mode pMode;
        private Mode CurrentMode
        {
            get
            {
                return pMode;
            }
            set
            {
                pMode = value;
                tsbtnDragMode.Checked = false;
                tsbtnImageMode.Checked = false;
                tsbtnTextMode.Checked = false;
                switch (value)
                {
                    case Mode.mDrag:
                        tsbtnDragMode.Checked = true;
                        lbMode.Text = Utils.getEnumText(Mode.mDrag);
                        break;
                    case Mode.mImage:
                        tsbtnImageMode.Checked = true;
                        lbMode.Text = Utils.getEnumText(Mode.mImage);
                        break;
                    case Mode.mText:
                        tsbtnTextMode.Checked = true;
                        lbMode.Text = Utils.getEnumText(Mode.mText);
                        break;
                }
            }
        }
        private List<libControls.dsControl> dsControls;

        private byte brushTransparency = 127;
        private bool DragStart = false;      
 
        public frmMain()
        {
            InitializeComponent();
            dsControls = new List<libControls.dsControl>();
            CurrentMode = Mode.mDrag;
        }

        private void OnControlChanged()
        {
            pbxCanvas.Invalidate();
        }
        
        private void rotateImage(Bitmap b, float angle)
        {
            if (b != null)
            {
                if (BufferImage == null)
                {
                    BufferImage = new Bitmap((int)(b.Width), (int)(b.Height));
                }
                Graphics g = Graphics.FromImage(BufferImage);
                g.TranslateTransform(((float)b.Width / 2) + StartX, ((float)b.Height / 2) + StartY);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                Rectangle rect = new Rectangle(0, 0, (int)b.Width, (int)b.Height);
                g.DrawImage(b, rect);
            }            
        }


        private void pbxCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap bmp = (BufferImage == null) ? Cover : BufferImage;
            Rectangle rect = new Rectangle(StartX, StartY, (int)(Cover.Width * ZoomH), (int)(Cover.Height * ZoomV));
            g.DrawImage(bmp, rect);

            foreach (libControls.dsControl aControl in dsControls)
            {
                aControl.Paint(g);
            }   

            SolidBrush transBrush = new SolidBrush(Color.FromArgb(brushTransparency, 150, 150, 150));            
            Pen pn = new Pen(Color.FromArgb(128, 100, 100, 100));
            Pen thickPen = new Pen(transBrush,200);
            g.DrawEllipse(thickPen, -100, -100, 840, 840);
            g.DrawEllipse(pn, 0, 0, 640, 640);           
            g.DrawEllipse(pn, 192, 192, 256, 256);
            g.FillEllipse(transBrush, 192, 192, 256, 256);                               
        }

        private void resetToBlank()
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar la etiqueta actual?","Nueva etiqueta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Cover = new Bitmap(640, 640);
                BufferImage = null;
                StartX = 0;
                StartY = 0;
                cleardsControls();
                pbxCanvas.Invalidate();
                CurrentCoverPath = String.Empty;
                pAngle = 0;
            }
        }

        private void SelectBackground()
        {
            frmSelectBackground f = new frmSelectBackground();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Cover = (Bitmap)Bitmap.FromFile(f.SelectedFile, false);                
                ZoomH = 640.00 / Cover.Width;
                ZoomV = 640.00 / Cover.Height;
            }
            else
            {
                Cover = new Bitmap(640, 640);
            }
            pbxCanvas.Invalidate();
        }

        private void acnNewCover(object sender, EventArgs e)
        {
            resetToBlank();
        }

        private void pbxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.isPickedUp)
                {
                    aControl.Drag(e.X, e.Y);
                    pbxCanvas.Invalidate();
                    return;
                }
            }

            if (DragStart)
            {
                if (DeltaX < e.X)
                {
                    StartX = StartX + (e.X - DeltaX);
                }
                else if (DeltaX > e.X)
                {
                    StartX = StartX + (e.X - DeltaX);
                }
                if (DeltaY < e.Y)
                {
                    StartY = StartY + (e.Y - DeltaY);
                }
                else if (DeltaY > e.Y)
                {
                    StartY = StartY + (e.Y - DeltaY);
                }                
                pbxCanvas.Invalidate();                
            }
            DeltaX = e.X;
            DeltaY = e.Y;
        }

        private void pbxCanvas_MouseDown(object sender, MouseEventArgs e)
        {         
            Cursor = Cursors.NoMove2D;
            DragStart = true;

            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.editorVisible)
                {
                    aControl.CloseEditor(pbxCanvas);
                }
                if (aControl.isOver(e.X,e.Y))
                {
                    aControl.PickUp(e.X, e.Y);                    
                    return;
                }
            }

            if (DeltaX < e.X)
            {
                DeltaX = DeltaX + (e.X - DeltaX);
            }
            else if (DeltaX > e.X)
            {
                DeltaX = DeltaX + (e.X - DeltaX);
            }

            if (DeltaY < e.Y)
            {
                DeltaY = DeltaY + (e.Y - DeltaY);
            }
            else if (DeltaY > e.Y)
            {
                DeltaY = DeltaY + (e.Y - DeltaY);
            }
        }

        private void pbxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.isPickedUp)
                {
                    aControl.PutDown();                    
                }
            }

            DragStart = false;
            Cursor = Cursors.Default;
            DeltaX = e.X;
            DeltaY = e.Y;
        }

        private libFile.CoverFileArgs getStandardArgs()
        {
            libFile.CoverFileArgs Args = new libFile.CoverFileArgs();
            Args.Image = (BufferImage == null) ? Cover : BufferImage;
            Args.dsControls = dsControls;
            Args.Location = new Point(StartX, StartY);
            Args.HZoom = ZoomH;
            Args.VZoom = ZoomV;
            return Args; 
        }

        private string genLightScribeFile()
        {
            libFile.CoverFileArgs Args = getStandardArgs();
            return libFile.GenTempFile(Args);              
        }

        private void genSavedFile(string AFileName)
        {
            libFile.CoverFileArgs Args = getStandardArgs();
            Args.FileName = AFileName;

            // ToDo: Get file type as arg from dialog
            Args.FileFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            libFile.GenFile(Args);
        }

        private void acnResetCover(object sender, EventArgs e)
        {
            resetToBlank();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                libLS.LightScribeSoftwareInstalled();
                if (!libLS.HasLightScribeDrive())
                {
                    tsDriveState.Text = "No LightScribe drive found";
                    tsDriveState.Visible = true;
                }
            }
            catch (libLS.DllNotFound ex)
            {
                MessageBox.Show(ex.Message);
                tsSoftwareState.Text = "LightScribe software not found";
                tsSoftwareState.Visible = true;
            }
        }

        private void acnPreview(object sender, EventArgs e)
        {
            try
            {
                libLS.DoPrintPreview(genLightScribeFile());
            }
            catch (System.ArgumentNullException ArgEx)
            {                
                MessageBox.Show("Preview failed, LightScribe library not found.", "LightScribe Library Error.", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unexpected error, %s",ex.Message),"DVDScribe Error",MessageBoxButtons.OK);
            }            
        }

        private void acnDoGrayScale(object sender, EventArgs e)
        {
            Bitmap bmp = (BufferImage == null) ? Cover : BufferImage;
            using (frmGrayscalePreview f = new frmGrayscalePreview(bmp))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    libImage.GrayScale(bmp);
                    pbxCanvas.Invalidate();
                }
            }
        }

        private void acnDoCiontrastChange(object sender, EventArgs e)
        {
            Bitmap bmp = (BufferImage == null) ? Cover : BufferImage;
            using (frmContrastPreview f = new frmContrastPreview(bmp))
            {
                if (f.ShowDialog() == DialogResult.OK){
                    if (libImage.SetContrast(bmp, (sbyte)f.tbLevel.Value))
                    {
                        pbxCanvas.Invalidate();
                    }
                }
            }            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetToBlank();
        }

        private void pbxCanvas_DoubleClick(object sender, EventArgs e)
        {
            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.isOver(DeltaX, DeltaY))
                {
                    aControl.LauchEditor(pbxCanvas); 
                }
            }            
        }

        private void tsbtnTextMode_Click(object sender, EventArgs e)
        {
            CurrentMode = Mode.mText;
        }

        private void tsbtnDragMode_Click(object sender, EventArgs e)
        {
            CurrentMode = Mode.mDrag;
        }

        private void tsbtnImageMode_Click(object sender, EventArgs e)
        {
            CurrentMode = Mode.mImage;            
        }

        private void pbxCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            switch (CurrentMode)
            {
                case Mode.mText: libControls.TextField tf = new libControls.TextField(e.X,e.Y,0,0);
                    dsControls.Add(tf);
                    tf.OnChanged = OnControlChanged;
                    tf.LauchEditor(pbxCanvas);                    
                    CurrentMode = Mode.mDrag;
                    break;
                case Mode.mImage: libControls.ImageField imf = new libControls.ImageField(String.Empty,e.X,e.Y,0,0);
                    dsControls.Add(imf);
                    imf.OnChanged = OnControlChanged;
                    imf.LauchEditor(pbxCanvas);
                    CurrentMode = Mode.mDrag;
                    break;
                case Mode.mDrag:
                    foreach(libControls.dsControl aControl in dsControls)
                    {
                        if (aControl.Selected)
                        {
                            aControl.Selected = false;
                        }
                        if (aControl.isOver(e.X,e.Y))
                        {
                            aControl.Selected = true;
                        }
                    }
                    break;
            }
        }

        private void deleteControls()
        {
            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.Selected)
                {
                    libControls.dsControl fControl = aControl;
                    dsControls.Remove(fControl);
                    fControl = null;
                    pbxCanvas.Invalidate();
                    break;
                }
            }
        }

        private void cleardsControls()
        {
            int aCount = 0;
            while (dsControls.Count > 0)
            {
                libControls.dsControl aControl = dsControls[aCount];
                dsControls.Remove(aControl);
                aControl = null;
            }
        }

        private void acnDeleteControl(object sender, EventArgs e)
        {
            deleteControls();
        }

        private void acnExitApplication(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close DVDScribe?", "Exit application", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // This is where the refactoring starts for the Zooming of the background image.
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void OnBackgroundZoomChanged(int HZoomValue, int VZoomValue)
        {
            ZoomH = (HZoomValue * 0.001);
            ZoomV = (VZoomValue * 0.001);
            pbxCanvas.Invalidate();
        }

        private void zoomBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackgroundResize frmResizeBackground = new frmBackgroundResize((int)(ZoomH * 1000), (int)(ZoomV * 1000), OnBackgroundZoomChanged);
            frmResizeBackground.Show();
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // This is where the refactoring starts for the Rotation of the background image.
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void OnBackgroundAngleChanged(float AAngle)
        {
            Angle = AAngle;
        }

        private void rotateBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackgroundAngle frmRotateBackGround = new frmBackgroundAngle(Angle, OnBackgroundAngleChanged);
            frmRotateBackGround.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void tsbtnSaveCover_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentCoverPath))
            {
                if (dlgSaveFile.ShowDialog() == DialogResult.OK)
                {
                    CurrentCoverPath = dlgSaveFile.FileName;
                }
                else
                {
                    return;
                }
            }
            genSavedFile(CurrentCoverPath);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frmAboutBox = new frmAbout();
            frmAboutBox.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imgList.ImageSize = new System.Drawing.Size(130, 130);
            
            lvIncludedBG.BeginUpdate();
            lvIncludedBG.Items.Clear();

            ArrayList dirList = new ArrayList();
            ArrayList fileList = new ArrayList();

            CShItem CSI = new CShItem(Application.StartupPath + "\\Backgrounds");
            dirList = CSI.GetDirectories(true);
            fileList = CSI.GetFiles();


            try
            {
                foreach (CShItem file in fileList)
                {
                    if (file.Path.ToLower().EndsWith(".jpg") || file.Path.ToLower().EndsWith(".png") || file.Path.ToLower().EndsWith(".bmp"))
                    {
                        try
                        {
                            Image thumb = Image.FromFile(file.Path);
                            imgList.Images.Add(thumb);

                            ListViewItem lvi = lvIncludedBG.Items.Add(String.Empty);
                            lvi.ImageIndex = lvi.Index;
                            lvi.Tag = file.Path;
                        }
                        catch (Exception Ex)
                        {
                        }
                    }
                }
                lvIncludedBG.LargeImageList = imgList;
                lvIncludedBG.EndUpdate();
            }
            catch (Exception Exc) { MessageBox.Show(Exc.ToString()); }
        }


        private void expTree1_ExpTreeNodeSelected(string SelPath, CShItem CSI)
        {
            ImageList imgList = new ImageList();
            imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imgList.ImageSize = new System.Drawing.Size(130, 130);
            lv.BeginUpdate();
            lv.Items.Clear();

            ArrayList dirList = new ArrayList();
            ArrayList fileList = new ArrayList();

            if (CSI.DisplayName.Equals(CShItem.strMyComputer))
            {
                dirList = CSI.GetDirectories(true);
            }
            else
            {
                dirList = CSI.GetDirectories(true);
                fileList = CSI.GetFiles();
            }

            
           try {
            foreach (CShItem file in fileList)
            {
                if (file.Path.ToLower().EndsWith(".jpg") || file.Path.ToLower().EndsWith(".png") || file.Path.ToLower().EndsWith(".bmp"))
                {
                    try { 
                        Image thumb = Image.FromFile(file.Path);
                        imgList.Images.Add(thumb);
                        ListViewItem lvi = lv.Items.Add(file.GetFileName());
                        lvi.ImageIndex = lvi.Index;
                        lvi.Tag = file.Path;
                    }
                    catch (Exception Ex) {
                    }
                }
            }
            lv.LargeImageList = imgList;
            lv.EndUpdate();
            }
           catch (Exception Exc) { MessageBox.Show(Exc.ToString()); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lv.SelectedItems[0].Tag.ToString();
                Cover = (Bitmap)Bitmap.FromFile(lv.SelectedItems[0].Tag.ToString(), false);
                ZoomH = 640.00 / Cover.Width;
                ZoomV = 640.00 / Cover.Height;
            } 
            catch(Exception Exc)
            {
                Cover = new Bitmap(640, 640);
                 
            }
            pbxCanvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap CoverX = (Bitmap)Bitmap.FromFile(lv.SelectedItems[0].Tag.ToString(), false);
                libControls.ImageField imf = new libControls.ImageField(lv.SelectedItems[0].Tag.ToString(), 200, 200, 0, 0);
                dsControls.Add(imf);
                imf.OnChanged = OnControlChanged;
                imf.LauchEditor(pbxCanvas);
                CurrentMode = Mode.mDrag;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void lvIncludedBG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvIncludedBG.SelectedItems[0].Tag.ToString();
                Cover = (Bitmap)Bitmap.FromFile(lvIncludedBG.SelectedItems[0].Tag.ToString(), false);
                StartX = 0;
                StartY = 0;
                ZoomH = 640.00 / Cover.Width;
                ZoomV = 640.00 / Cover.Height;
            }
            catch (Exception Exc)
            {
                Cover = new Bitmap(640, 640);


            }
            pbxCanvas.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            foreach (libControls.dsControl aControl in dsControls)
            {
                if (aControl.Selected)
                {
                    aControl.Selected = false;
                }
                if (aControl.editorVisible)
                {
                    aControl.CloseEditor(pbxCanvas);
                }
            }
        }

        private void tsQMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpenDialog.Filter = "Label Creator Files (*.llf)|*.llf";
            if (dlgOpenDialog.ShowDialog() == DialogResult.OK)
            {
                resetToBlank();
                String labelPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(labelPath);

                FastZipEvents events = null;
                FastZip fastZip = new FastZip(events);
                fastZip.CreateEmptyDirectories = true;
                fastZip.RestoreAttributesOnExtract = true;
                fastZip.RestoreDateTimeOnExtract = true;

                fastZip.ExtractZip(dlgOpenDialog.FileName, labelPath, "");
                openXML(labelPath);
            }
        }

        private void openXML(String labelPath)
        {
            XmlDocument xDoc = new XmlDocument();
            
            xDoc.Load(Path.Combine(labelPath , "label.xml"));

            XmlNodeList xmlLabel = xDoc.GetElementsByTagName("label");

            XmlNodeList listTexts = ((XmlElement)xmlLabel[0]).GetElementsByTagName("text");
            XmlNodeList listImages = ((XmlElement)xmlLabel[0]).GetElementsByTagName("image");

            foreach (XmlElement nodo in listImages)
            {

                int nTop = Int32.Parse(nodo.GetAttribute("top"));
                int nLeft = Int32.Parse(nodo.GetAttribute("left"));
                int nHeight = Int32.Parse(nodo.GetAttribute("height"));
                int nWidth = Int32.Parse(nodo.GetAttribute("width"));
                string nSrc = nodo.GetAttribute("src");

                libControls.ImageField imf = new libControls.ImageField(Path.Combine(Path.Combine(labelPath , "images"), nSrc), nLeft, nTop, nHeight, nWidth);
                dsControls.Add(imf);
                imf.OnChanged = OnControlChanged;
            }

            foreach (XmlElement nodo in listTexts)
            {
                int nTop = Int32.Parse(nodo.GetAttribute("top"));
                int nLeft = Int32.Parse(nodo.GetAttribute("left"));
                string nValue = nodo.GetAttribute("value");
                libControls.TextField tf = new libControls.TextField(nLeft, nTop, 0, 0);
                tf.Text = nValue;

                if (nodo.HasAttribute("format"))
                {
                    String nFormat = nodo.GetAttribute("format");
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                    tf.pFont = (Font)tc.ConvertFromString(nFormat);
                    if (tf.pFont == null) tf.pFont = new Font("Verdana", 10);
                }
                else
                {
                    tf.pFont = new Font("Verdana", 10);
                }
                dsControls.Add(tf);
                tf.OnChanged = OnControlChanged;
            }

            XmlNodeList xmlBg = ((XmlElement)xmlLabel[0]).GetElementsByTagName("background");
            foreach (XmlElement nodo in xmlBg)
            {
                    if (nodo.HasAttribute("src"))
                    {
                        try
                        {
                            string nSrc = nodo.GetAttribute("src");
                            Cover = (Bitmap)Bitmap.FromFile(Path.Combine(Path.Combine(labelPath, "images"), nSrc), false);
                        }
                        catch {
                            Cover = new Bitmap(640, 640);
                        }
                    }
                    else 
                    {
                        Cover = new Bitmap(640, 640);
                    }
                    if (nodo.HasAttribute("zoomh"))
                        ZoomH = double.Parse(nodo.GetAttribute("zoomh"));
                    else
	                    ZoomH = 1;

                    if (nodo.HasAttribute("zoomv"))
                        ZoomV = double.Parse(nodo.GetAttribute("zoomv"));
                    else 
                        ZoomV = 1;

                    if (nodo.HasAttribute("left"))
                        StartX = int.Parse(nodo.GetAttribute("left"));
                    else
                        StartX = 0;

                    if (nodo.HasAttribute("top"))
                        StartY = int.Parse(nodo.GetAttribute("top"));
                    else
                        StartY = 0;

            }
            pbxCanvas.Invalidate();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlelem;
            XmlElement xmlelem2;
            String labelPath;
            String zipPath;
            dlgSaveFile.Filter = "Label Creator Files (*.llf)|*.llf";
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                zipPath = Path.GetDirectoryName(dlgSaveFile.FileName);
            }
            else
            {
                return;
            }

            labelPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(labelPath);
            Directory.CreateDirectory(Path.Combine(labelPath, "images"));

            xmldoc=new XmlDocument();
            xmlnode=xmldoc.CreateNode(XmlNodeType.XmlDeclaration,String.Empty,String.Empty);
            xmldoc.AppendChild(xmlnode);
            xmlelem=xmldoc.CreateElement(String.Empty,"label", String.Empty );
            xmldoc.AppendChild(xmlelem);

            int n = 0;
            try
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                foreach (libControls.dsControl mControl in dsControls)
                {
                    if (mControl.GetType().ToString() == "DVDScribe.libControls+ImageField")
                    {
                        n++;
                        xmlelem2 = xmldoc.CreateElement(String.Empty, "image", String.Empty);
                        libControls.ImageField mImage = (libControls.ImageField)mControl;
                        mImage.SaveToFile(Path.Combine(Path.Combine(labelPath, "images"), "image" + n.ToString() + ".png"));
                        xmlelem2.SetAttribute("src", "image" + n.ToString() + ".png");
                        xmlelem2.SetAttribute("top", mImage.Location.Y.ToString());
                        xmlelem2.SetAttribute("left", mImage.Location.X.ToString());
                        xmlelem2.SetAttribute("height", mImage.Height.ToString());
                        xmlelem2.SetAttribute("width", mImage.Width.ToString());
                        xmldoc.ChildNodes.Item(1).AppendChild(xmlelem2);
                    } else {
                        libControls.TextField mText = (libControls.TextField)mControl;
                        xmlelem2 = xmldoc.CreateElement(String.Empty, "text", String.Empty);
                        xmlelem2.SetAttribute("value", mText.Text);
                        string fontString = tc.ConvertToString(mText.pFont);
                        xmlelem2.SetAttribute("format", fontString);
                        xmlelem2.SetAttribute("top", mText.Location.Y.ToString());
                        xmlelem2.SetAttribute("left", mText.Location.X.ToString());
                        xmldoc.ChildNodes.Item(1).AppendChild(xmlelem2);
                    }
                }
            }
            catch (Exception xx)
            {

            }

            xmlelem2 = xmldoc.CreateElement(String.Empty, "background", String.Empty);
            Cover.Save(Path.Combine(Path.Combine(labelPath, "images"), "background.png"), ImageFormat.Png);
            xmlelem2.SetAttribute("src", "background.png");
            xmlelem2.SetAttribute("top", StartY.ToString());
            xmlelem2.SetAttribute("left", StartX.ToString());
            xmlelem2.SetAttribute("zoomh", ZoomH.ToString());
            xmlelem2.SetAttribute("zoomv", ZoomV.ToString());
            xmlelem2.SetAttribute("angle", pAngle.ToString());
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem2);

            try
            {
                xmldoc.Save(Path.Combine(labelPath, "label.xml"));
            }
            catch (Exception exx)
            {
                MessageBox.Show("Error guardando XML");
            }

            FastZipEvents events = null;

            FastZip fastZip = new FastZip(events);
            fastZip.CreateEmptyDirectories = true;
            fastZip.RestoreAttributesOnExtract = true;
            fastZip.RestoreDateTimeOnExtract = true;

            fastZip.CreateZip(dlgSaveFile.FileName, labelPath, true, "");
        }


        private bool ZipDirectory(string strDirectory, string strFileZip)
        {
             
            if (!Directory.Exists(strDirectory))
            {
                Console.WriteLine("Cannot find directory '{0}'", strDirectory);
                return false;
            }

            try
            {
                string[] filenames = Directory.GetFiles(strDirectory);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(strFileZip)))
                {

                    s.SetLevel(9); 
                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }


                    s.Finish();
                    s.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
                return false;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                deleteControls();
        }

        private void tsbtnImage_ButtonClick(object sender, EventArgs e)
        {

        }


    }
}
