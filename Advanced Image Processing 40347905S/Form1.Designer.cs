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
            this.label1 = new System.Windows.Forms.Label();
            this.reset = new System.Windows.Forms.Button();
            this.save_img = new System.Windows.Forms.Button();
            this.copy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toHistogram = new System.Windows.Forms.Button();
            this.ApplyGaussionNoise = new System.Windows.Forms.Button();
            this.ConvertColorSpace = new System.Windows.Forms.Button();
            this.FFT = new System.Windows.Forms.Button();
            this.ColorDetect = new System.Windows.Forms.Button();
            this.Histogram_Equalization = new System.Windows.Forms.Button();
            this.smooth_and_edge_detect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UnSharpenedMask_Button = new System.Windows.Forms.Button();
            this.InnerBoundaryTracing_Btn = new System.Windows.Forms.Button();
            this.bilater_filter_btn = new System.Windows.Forms.Button();
            this.DofColor = new System.Windows.Forms.Button();
            this.Gray_1_project = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.projectdemo2 = new System.Windows.Forms.Button();
            this.proje = new System.Windows.Forms.Button();
            this.videomode = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.resizeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.copy.Location = new System.Drawing.Point(458, 37);
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
            this.toHistogram.Location = new System.Drawing.Point(458, 66);
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
            this.ApplyGaussionNoise.Location = new System.Drawing.Point(458, 95);
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
            this.ConvertColorSpace.Location = new System.Drawing.Point(458, 124);
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
            this.FFT.Location = new System.Drawing.Point(458, 153);
            this.FFT.Name = "FFT";
            this.FFT.Size = new System.Drawing.Size(75, 23);
            this.FFT.TabIndex = 15;
            this.FFT.Text = "HW5";
            this.toolTip1.SetToolTip(this.FFT, "Fourier Transformation");
            this.FFT.UseVisualStyleBackColor = true;
            this.FFT.Click += new System.EventHandler(this.FFT_Click);
            // 
            // ColorDetect
            // 
            this.ColorDetect.Location = new System.Drawing.Point(458, 182);
            this.ColorDetect.Name = "ColorDetect";
            this.ColorDetect.Size = new System.Drawing.Size(75, 23);
            this.ColorDetect.TabIndex = 16;
            this.ColorDetect.Text = "HW6";
            this.toolTip1.SetToolTip(this.ColorDetect, "Colour Detection");
            this.ColorDetect.UseMnemonic = false;
            this.ColorDetect.UseVisualStyleBackColor = true;
            this.ColorDetect.Click += new System.EventHandler(this.ColorDetect_Click);
            // 
            // Histogram_Equalization
            // 
            this.Histogram_Equalization.Location = new System.Drawing.Point(458, 211);
            this.Histogram_Equalization.Name = "Histogram_Equalization";
            this.Histogram_Equalization.Size = new System.Drawing.Size(75, 23);
            this.Histogram_Equalization.TabIndex = 17;
            this.Histogram_Equalization.Text = "HW7";
            this.toolTip1.SetToolTip(this.Histogram_Equalization, "Apply Histogram Equalization");
            this.Histogram_Equalization.UseVisualStyleBackColor = true;
            this.Histogram_Equalization.Click += new System.EventHandler(this.Histogram_Equalization_Click);
            // 
            // smooth_and_edge_detect
            // 
            this.smooth_and_edge_detect.Location = new System.Drawing.Point(458, 240);
            this.smooth_and_edge_detect.Name = "smooth_and_edge_detect";
            this.smooth_and_edge_detect.Size = new System.Drawing.Size(75, 23);
            this.smooth_and_edge_detect.TabIndex = 18;
            this.smooth_and_edge_detect.Text = "HW8";
            this.toolTip1.SetToolTip(this.smooth_and_edge_detect, "Apply Image Smoothing / Edge Detectors");
            this.smooth_and_edge_detect.UseVisualStyleBackColor = true;
            this.smooth_and_edge_detect.Click += new System.EventHandler(this.smooth_and_edge_detect_Click);
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
            // UnSharpenedMask_Button
            // 
            this.UnSharpenedMask_Button.Location = new System.Drawing.Point(35, 447);
            this.UnSharpenedMask_Button.Name = "UnSharpenedMask_Button";
            this.UnSharpenedMask_Button.Size = new System.Drawing.Size(103, 23);
            this.UnSharpenedMask_Button.TabIndex = 19;
            this.UnSharpenedMask_Button.Text = "Image Sharpening";
            this.UnSharpenedMask_Button.UseVisualStyleBackColor = true;
            this.UnSharpenedMask_Button.Click += new System.EventHandler(this.UnSharpenedMask_Button_Click);
            // 
            // InnerBoundaryTracing_Btn
            // 
            this.InnerBoundaryTracing_Btn.Location = new System.Drawing.Point(279, 447);
            this.InnerBoundaryTracing_Btn.Name = "InnerBoundaryTracing_Btn";
            this.InnerBoundaryTracing_Btn.Size = new System.Drawing.Size(184, 23);
            this.InnerBoundaryTracing_Btn.TabIndex = 20;
            this.InnerBoundaryTracing_Btn.Text = "Connected-Component Labeling";
            this.InnerBoundaryTracing_Btn.UseVisualStyleBackColor = true;
            this.InnerBoundaryTracing_Btn.Click += new System.EventHandler(this.InnerBoundaryTracing_Btn_Click);
            // 
            // bilater_filter_btn
            // 
            this.bilater_filter_btn.Location = new System.Drawing.Point(144, 447);
            this.bilater_filter_btn.Name = "bilater_filter_btn";
            this.bilater_filter_btn.Size = new System.Drawing.Size(110, 23);
            this.bilater_filter_btn.TabIndex = 21;
            this.bilater_filter_btn.Text = "Bilateral Filter";
            this.bilater_filter_btn.UseVisualStyleBackColor = true;
            this.bilater_filter_btn.Click += new System.EventHandler(this.bilater_filter_btn_Click);
            // 
            // DofColor
            // 
            this.DofColor.Location = new System.Drawing.Point(724, 470);
            this.DofColor.Name = "DofColor";
            this.DofColor.Size = new System.Drawing.Size(75, 23);
            this.DofColor.TabIndex = 22;
            this.DofColor.Text = "Demo 4";
            this.DofColor.UseVisualStyleBackColor = true;
            this.DofColor.Click += new System.EventHandler(this.DofColor_Click);
            // 
            // Gray_1_project
            // 
            this.Gray_1_project.Location = new System.Drawing.Point(472, 470);
            this.Gray_1_project.Name = "Gray_1_project";
            this.Gray_1_project.Size = new System.Drawing.Size(75, 23);
            this.Gray_1_project.TabIndex = 23;
            this.Gray_1_project.Text = "Demo 1";
            this.Gray_1_project.UseVisualStyleBackColor = true;
            this.Gray_1_project.Click += new System.EventHandler(this.Gray_1_project_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 427);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "Extra Function";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 427);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "Under Development";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(483, 427);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "Project Demo and Experiment";
            // 
            // projectdemo2
            // 
            this.projectdemo2.Location = new System.Drawing.Point(553, 470);
            this.projectdemo2.Name = "projectdemo2";
            this.projectdemo2.Size = new System.Drawing.Size(75, 23);
            this.projectdemo2.TabIndex = 27;
            this.projectdemo2.Text = "Demo 2";
            this.projectdemo2.UseVisualStyleBackColor = true;
            this.projectdemo2.Click += new System.EventHandler(this.projectdemo2_Click);
            // 
            // proje
            // 
            this.proje.Location = new System.Drawing.Point(634, 470);
            this.proje.Name = "proje";
            this.proje.Size = new System.Drawing.Size(75, 23);
            this.proje.TabIndex = 28;
            this.proje.Text = "Demo 3";
            this.proje.UseVisualStyleBackColor = true;
            this.proje.Click += new System.EventHandler(this.proje_Click);
            // 
            // videomode
            // 
            this.videomode.Location = new System.Drawing.Point(805, 395);
            this.videomode.Name = "videomode";
            this.videomode.Size = new System.Drawing.Size(75, 23);
            this.videomode.TabIndex = 29;
            this.videomode.Text = "Video Mode";
            this.videomode.UseVisualStyleBackColor = true;
            this.videomode.Click += new System.EventHandler(this.videomode_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(551, 368);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(510, 442);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "Demo 1 - 3 using grey image for test";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(510, 456);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "Demo 4 using color image for test";
            // 
            // resizeBtn
            // 
            this.resizeBtn.Location = new System.Drawing.Point(458, 270);
            this.resizeBtn.Name = "resizeBtn";
            this.resizeBtn.Size = new System.Drawing.Size(75, 23);
            this.resizeBtn.TabIndex = 33;
            this.resizeBtn.Text = "Resize";
            this.resizeBtn.UseVisualStyleBackColor = true;
            this.resizeBtn.Click += new System.EventHandler(this.resizeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 505);
            this.Controls.Add(this.resizeBtn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.videomode);
            this.Controls.Add(this.proje);
            this.Controls.Add(this.projectdemo2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Gray_1_project);
            this.Controls.Add(this.DofColor);
            this.Controls.Add(this.bilater_filter_btn);
            this.Controls.Add(this.InnerBoundaryTracing_Btn);
            this.Controls.Add(this.UnSharpenedMask_Button);
            this.Controls.Add(this.smooth_and_edge_detect);
            this.Controls.Add(this.Histogram_Equalization);
            this.Controls.Add(this.ColorDetect);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Button ColorDetect;
        private System.Windows.Forms.Button Histogram_Equalization;
        private System.Windows.Forms.Button smooth_and_edge_detect;
        private System.Windows.Forms.Button UnSharpenedMask_Button;
        private System.Windows.Forms.Button InnerBoundaryTracing_Btn;
        private System.Windows.Forms.Button bilater_filter_btn;
        private System.Windows.Forms.Button DofColor;
        private System.Windows.Forms.Button Gray_1_project;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button projectdemo2;
        private System.Windows.Forms.Button proje;
        private System.Windows.Forms.Button videomode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button resizeBtn;
    }
}

