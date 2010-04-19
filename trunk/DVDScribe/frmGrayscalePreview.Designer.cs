namespace DVDScribe
{
    partial class frmGrayscalePreview
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
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrg)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxPreview
            // 
            this.pbxPreview.Location = new System.Drawing.Point(3, 18);
            this.pbxPreview.Size = new System.Drawing.Size(215, 220);
            // 
            // groupBox2
            // 
            this.groupBox2.Text = "Grayscale";
            // 
            // pbxOrg
            // 
            this.pbxOrg.Location = new System.Drawing.Point(3, 18);
            this.pbxOrg.Size = new System.Drawing.Size(225, 220);
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmGrayscalePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(460, 294);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmGrayscalePreview";
            this.Text = "Grayscale preview";
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
