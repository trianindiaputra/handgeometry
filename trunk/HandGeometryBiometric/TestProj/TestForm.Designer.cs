namespace TestProj
{
    partial class TestForm
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
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxGray = new System.Windows.Forms.PictureBox();
            this.pictureBoxBinary = new System.Windows.Forms.PictureBox();
            this.pictureBoxContour = new System.Windows.Forms.PictureBox();
            this.buttonGo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContour)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxOriginal.TabIndex = 0;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxGray
            // 
            this.pictureBoxGray.Location = new System.Drawing.Point(319, 13);
            this.pictureBoxGray.Name = "pictureBoxGray";
            this.pictureBoxGray.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxGray.TabIndex = 0;
            this.pictureBoxGray.TabStop = false;
            // 
            // pictureBoxBinary
            // 
            this.pictureBoxBinary.Location = new System.Drawing.Point(13, 319);
            this.pictureBoxBinary.Name = "pictureBoxBinary";
            this.pictureBoxBinary.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxBinary.TabIndex = 0;
            this.pictureBoxBinary.TabStop = false;
            // 
            // pictureBoxContour
            // 
            this.pictureBoxContour.Location = new System.Drawing.Point(319, 319);
            this.pictureBoxContour.Name = "pictureBoxContour";
            this.pictureBoxContour.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxContour.TabIndex = 0;
            this.pictureBoxContour.TabStop = false;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(280, 623);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 1;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 658);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.pictureBoxContour);
            this.Controls.Add(this.pictureBoxGray);
            this.Controls.Add(this.pictureBoxBinary);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Name = "TestForm";
            this.Text = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxGray;
        private System.Windows.Forms.PictureBox pictureBoxBinary;
        private System.Windows.Forms.PictureBox pictureBoxContour;
        private System.Windows.Forms.Button buttonGo;

    }
}