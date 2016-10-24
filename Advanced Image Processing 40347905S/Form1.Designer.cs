namespace Advanced_Image_Processing_40347905S
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.select_img = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reset = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.save_img = new System.Windows.Forms.Button();
            this.copy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toHistogram = new System.Windows.Forms.Button();
            this.ApplyGaussionNoise = new System.Windows.Forms.Button();
            this.ConvertColorSpace = new System.Windows.Forms.Button();
            this.FFT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // select_img
            // 
            this.select_img.Location = new System.Drawing.Point(176, 395);
            this.select_img.Name = "select_img";
            this.select_img.Size = new System.Drawing.Size(105, 23);
            this.select_img.TabIndex = 0;
            this.select_img.Text = "Select Image...";
            this.select_img.UseVisualStyleBackColor = true;
            this.select_img.Click += new System.EventHandler(this.select_img_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(35, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 317);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "... ";
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(458, 331);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 3;
            this.reset.Text = "Reset All";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox2.Location = new System.Drawing.Point(553, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 317);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // save_img
            // 
            this.save_img.Location = new System.Drawing.Point(724, 394);
            this.save_img.Name = "save_img";
            this.save_img.Size = new System.Drawing.Size(75, 23);
            this.save_img.TabIndex = 5;
            this.save_img.Text = "Save";
            this.save_img.UseVisualStyleBackColor = true;
            this.save_img.Click += new System.EventHandler(this.save_img_Click);
            // 
            // copy
            // 
            this.copy.Location = new System.Drawing.Point(458, 61);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(75, 23);
            this.copy.TabIndex = 6;
            this.copy.Text = "HW1";
            this.toolTip1.SetToolTip(this.copy, "Copy Directly");
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // toHistogram
            // 
            this.toHistogram.Location = new System.Drawing.Point(458, 100);
            this.toHistogram.Name = "toHistogram";
            this.toHistogram.Size = new System.Drawing.Size(75, 23);
            this.toHistogram.TabIndex = 12;
            this.toHistogram.Text = "HW2";
            this.toolTip1.SetToolTip(this.toHistogram, "Generate Histogram");
            this.toHistogram.UseVisualStyleBackColor = true;
            this.toHistogram.Click += new System.EventHandler(this.toHistogram_Click);
            // 
            // ApplyGaussionNoise
            // 
            this.ApplyGaussionNoise.Location = new System.Drawing.Point(458, 139);
            this.ApplyGaussionNoise.Name = "ApplyGaussionNoise";
            this.ApplyGaussionNoise.Size = new System.Drawing.Size(75, 23);
            this.ApplyGaussionNoise.TabIndex = 13;
            this.ApplyGaussionNoise.Text = "HW3";
            this.toolTip1.SetToolTip(this.ApplyGaussionNoise, "Apply white Gaussion noise to image");
            this.ApplyGaussionNoise.UseVisualStyleBackColor = true;
            this.ApplyGaussionNoise.Click += new System.EventHandler(this.ApplyGaussionNoise_Click);
            // 
            // ConvertColorSpace
            // 
            this.ConvertColorSpace.Location = new System.Drawing.Point(458, 177);
            this.ConvertColorSpace.Name = "ConvertColorSpace";
            this.ConvertColorSpace.Size = new System.Drawing.Size(75, 23);
            this.ConvertColorSpace.TabIndex = 14;
            this.ConvertColorSpace.Text = "HW4";
            this.toolTip1.SetToolTip(this.ConvertColorSpace, "Color Space Conversion");
            this.ConvertColorSpace.UseVisualStyleBackColor = true;
            this.ConvertColorSpace.Click += new System.EventHandler(this.ConvertColorSpace_Click);
            // 
            // FFT
            // 
            this.FFT.Location = new System.Drawing.Point(458, 217);
            this.FFT.Name = "FFT";
            this.FFT.Size = new System.Drawing.Size(75, 23);
            this.FFT.TabIndex = 15;
            this.FFT.Text = "HW5";
            this.toolTip1.SetToolTip(this.FFT, "Fourier Transformation");
            this.FFT.UseVisualStyleBackColor = true;
            this.FFT.Click += new System.EventHandler(this.FFT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "...";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(95, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(59, 12);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "40347905S";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Author:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "original";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(744, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 486);
            this.Controls.Add(this.FFT);
            this.Controls.Add(this.ConvertColorSpace);
            this.Controls.Add(this.ApplyGaussionNoise);
            this.Controls.Add(this.toHistogram);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.save_img);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.select_img);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "AIP40347905S";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button select_img;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button save_img;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button toHistogram;
        private System.Windows.Forms.Button ApplyGaussionNoise;
        private System.Windows.Forms.Button ConvertColorSpace;
        private System.Windows.Forms.Button FFT;
    }
}

