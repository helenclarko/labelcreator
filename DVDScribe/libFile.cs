using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace DVDScribe
{
    class libFile
    {
        public struct CoverFileArgs
        {
            public string FileName;
            public Bitmap Image;
            public List<libControls.dsControl> dsControls;
            public System.Drawing.Imaging.ImageFormat FileFormat;
            public Point Location;
            public double HZoom;
            public double VZoom;
        }

        public static void GenFile(CoverFileArgs Args)
        {
            Bitmap b = new Bitmap(640, 640);
            b.MakeTransparent(Color.White);

            Graphics g = Graphics.FromImage(b);

            g.DrawImage(Args.Image, new Rectangle(Args.Location.X, Args.Location.Y, (int)(Args.Image.Width * Args.HZoom), (int)(Args.Image.Height * Args.VZoom)));
            foreach (libControls.dsControl aControl in Args.dsControls)
            {
                aControl.AddToImage(g);
            }
            b.Save(Args.FileName, Args.FileFormat);
        }

        public static string GenTempFile(CoverFileArgs Args)
        {
            Args.FileName = Path.GetTempFileName() + ".bmp";
            Args.FileFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            GenFile(Args);
            return Args.FileName;
        }
    }
}
