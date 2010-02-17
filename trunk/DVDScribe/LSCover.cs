using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DVDScribe
{
    class LSCover
    {
        private Bitmap pCover;

        public Bitmap Cover
        {
            get
            {
                return pCover;
            }
        }

        public LSCover()
        {
            pCover = new Bitmap(640, 640);
        }
    }
}
