using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmBackgroundResize : Form
    {
        public delegate void OnSizeChange(int HZoom, int VZoom);

        private OnSizeChange OnSizeChangeEvent;
        private int StartHValue;
        private int StartVValue;

        public frmBackgroundResize()
        {
            InitializeComponent();
        }

        public frmBackgroundResize(int AHValue, int AVValue, OnSizeChange AEvent)           
        {
            InitializeComponent();            
            StartHValue = AHValue;
            StartVValue = AVValue;
            OnSizeChangeEvent = AEvent;
            if (StartHValue >= tbrHZoom.Maximum)
            {
                tbrHZoom.Maximum = StartHValue * 2;
            }

            if (StartVValue >= tbrVZoom.Maximum)
            {
                tbrVZoom.Maximum = StartVValue * 2;
            }
            tbrHZoom.Value = StartHValue;
            tbrVZoom.Value = StartVValue;
        }

        private void OnTrackbarValuesChanged(object sender, EventArgs e)
        {
            if (cbxRatio.Checked)
            {
                if ((sender as TrackBar).Name == "tbrHZoom")
                {
                    tbrVZoom.Value = tbrHZoom.Value;
                }
                else
                {
                    tbrHZoom.Value = tbrVZoom.Value;
                }
            }
            if (OnSizeChangeEvent != null)
            {
                OnSizeChangeEvent(tbrHZoom.Value,tbrVZoom.Value);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbrHZoom.Value = StartHValue;
            tbrVZoom.Value = StartVValue;
        }

        private void tbrHZoom_Scroll(object sender, EventArgs e)
        {

        }
    }
}
