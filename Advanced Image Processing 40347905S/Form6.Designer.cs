namespace Advanced_Image_Processing_40347905S
{
    partial class Form6
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
            this.h_min = new System.Windows.Forms.Button();
            this.without_hmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // h_min
            // 
            this.h_min.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.h_min.Location = new System.Drawing.Point(43, 93);
            this.h_min.Name = "h_min";
            this.h_min.Size = new System.Drawing.Size(195, 23);
            this.h_min.TabIndex = 0;
            this.h_min.Text = "Histogram Equalization with H_min";
            this.h_min.UseVisualStyleBackColor = true;
            this.h_min.Click += new System.EventHandler(this.h_min_Click);
            // 
            // without_hmin
            // 
            this.without_hmin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.without_hmin.Location = new System.Drawing.Point(43, 136);
            this.without_hmin.Name = "without_hmin";
            this.without_hmin.Size = new System.Drawing.Size(195, 23);
            this.without_hmin.TabIndex = 1;
            this.without_hmin.Text = "Histogram Equalization without H_min";
            this.without_hmin.UseVisualStyleBackColor = true;
            this.without_hmin.Click += new System.EventHandler(this.without_hmin_Click);
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.without_hmin);
            this.Controls.Add(this.h_min);
            this.Name = "Form6";
            this.Text = "Histogram Equalization";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button h_min;
        private System.Windows.Forms.Button without_hmin;
    }
}