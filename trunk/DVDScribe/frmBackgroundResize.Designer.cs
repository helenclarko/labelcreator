namespace DVDScribe
{
    partial class frmBackgroundResize
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxRatio = new System.Windows.Forms.CheckBox();
            this.tbrHZoom = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbrVZoom = new System.Windows.Forms.TrackBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrHZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrVZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxRatio);
            this.groupBox1.Controls.Add(this.tbrHZoom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbrVZoom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resize options";
            // 
            // cbxRatio
            // 
            this.cbxRatio.AutoSize = true;
            this.cbxRatio.Location = new System.Drawing.Point(15, 143);
            this.cbxRatio.Name = "cbxRatio";
            this.cbxRatio.Size = new System.Drawing.Size(78, 17);
            this.cbxRatio.TabIndex = 4;
            this.cbxRatio.Text = "Keep ratio";
            this.cbxRatio.UseVisualStyleBackColor = true;
            // 
            // tbrHZoom
            // 
            this.tbrHZoom.Location = new System.Drawing.Point(6, 104);
            this.tbrHZoom.Maximum = 3000;
            this.tbrHZoom.Name = "tbrHZoom";
            this.tbrHZoom.Size = new System.Drawing.Size(335, 45);
            this.tbrHZoom.TabIndex = 3;
            this.tbrHZoom.TickFrequency = 100;
            this.tbrHZoom.ValueChanged += new System.EventHandler(this.OnTrackbarValuesChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Height";
            // 
            // tbrVZoom
            // 
            this.tbrVZoom.Location = new System.Drawing.Point(6, 36);
            this.tbrVZoom.Maximum = 3000;
            this.tbrVZoom.Name = "tbrVZoom";
            this.tbrVZoom.Size = new System.Drawing.Size(335, 45);
            this.tbrVZoom.TabIndex = 0;
            this.tbrVZoom.TickFrequency = 100;
            this.tbrVZoom.ValueChanged += new System.EventHandler(this.OnTrackbarValuesChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(275, 174);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmBackgroundResize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 203);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBackgroundResize";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resize background";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrHZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrVZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar tbrHZoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbrVZoom;
        private System.Windows.Forms.CheckBox cbxRatio;
        private System.Windows.Forms.Button btnReset;
    }
}