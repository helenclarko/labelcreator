using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDScribe
{
    public partial class frmBackgroundAngle : Form
    {
        public delegate void OnAngleChange(float AAngle);

        private OnAngleChange OnAngleChangeEvent;
        private float StartAngle;

        public frmBackgroundAngle()
        {
            InitializeComponent();
        }

        public frmBackgroundAngle(float CurrentAngle, OnAngleChange AEvent)
        {
            InitializeComponent();
            StartAngle = CurrentAngle;
            OnAngleChangeEvent = AEvent;
            edtAngle.Value = (Decimal)StartAngle;
        }

        private void edtAngle_ValueChanged(object sender, EventArgs e)
        {
            if (OnAngleChangeEvent != null)
            {
                OnAngleChangeEvent((float)edtAngle.Value);
            }
        }
    }
}
