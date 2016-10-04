using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;

namespace Advanced_Image_Processing_40347905S
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap beforeBitmap = null;
        Bitmap afterBitmap = null;
        bool flag = false;

        private void select_img_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = @"Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpg)|*.jpg|PPM文件(*.ppm)|*.ppm|所有合適文件(*.bmp,*.jpg*.ppm)|*.bmp;*.jpg;*.ppm";
            openFileDialog1.FilterIndex = 4;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Size = new System.Drawing.Size(400, 317);
                Image<Bgr, Byte> inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                beforeBitmap = inputImage.ToBitmap();
                pictureBox1.Image = beforeBitmap;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                label1.Text = ("height: "+beforeBitmap.Height.ToString()+"  width: "+beforeBitmap.Width.ToString());
                label2.Text = Path.GetExtension(openFileDialog1.FileName);
                flag = true;
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            label1.Text = "...";
            label2.Text = "...";
            pictureBox2.Image = null;
            flag = false;
        }

        private void save_img_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                if (pictureBox2 == null || pictureBox2.Image == null)
                {
                    MessageBox.Show("Your result image is empty! Please select an effect option!");
                }
                else
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Bitmap Image|*.bmp|Jpeg Image|*.jpg|Gif Image|*.gif";
                    saveFileDialog1.Title = "Save an Image File";
                    saveFileDialog1.ShowDialog();
                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog1.FileName != "")
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.
                        System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                        // Saves the Image in the appropriate ImageFormat based upon the
                        // File type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            case 2:
                                this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case 3:
                                this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        fs.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
            
        }

        private void copy_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                afterBitmap = beforeBitmap;
                pictureBox2.Image = afterBitmap;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             this.linkLabel1.LinkVisited = true;
             System.Diagnostics.Process.Start("https://github.com/bigchou/AdvanceIP_HW"); // linking
        }

        private void toHistogram_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Bitmap histogram;
                int upperbound;
                GetHistogram(beforeBitmap,out upperbound, out histogram);
                pictureBox2.Image = histogram;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                MessageBox.Show("Set the vertical y-axis limits to [0," + ((int)upperbound).ToString() + "],\nand the horizontal x-axis limits to [0,255]", "Description");
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
            
        }

        public static void GetHistogram(Bitmap beforeBitmap, out int height, out Bitmap histo)
        {
            // Build frequency table
            int[] count = new int[256]; // 0-255
            Image<Bgr, byte> img = new Image<Bgr, byte>(beforeBitmap);
            Byte[, ,] data = img.Data; // pass by reference to 3d matrix
            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        sum += data[i, j, k];
                    }
                    count[sum / 3] += 1;
                }
            }
            //for (int i = 0; i < 256; i++)
            //    Console.WriteLine(i.ToString()+' '+count[i].ToString());

            // Drawing histogram
            Image<Bgr, Byte> histogram = new Image<Bgr, Byte>(256, 203, new Bgr(255, 255, 255));
            Byte[, ,] hdata = histogram.Data; // pass by reference to 3d matrix
            Console.WriteLine("Height:" + histogram.Height.ToString() + " Width:" + histogram.Width.ToString());
            double upperbound = Math.Ceiling(count.Max() * 1.1);
            for (int i = 0; i < 256; i++)
            {
                double val = (double)count[i] / upperbound;
                for (int j = histogram.Height - 1; j > Math.Floor(histogram.Height * (1.0 - val)); j--)
                {
                    if (j < 0)
                        break;
                    for (int k = 0; k < 3; k++)
                        hdata[j, i, k] = 0;
                }
            }
            height = (int)upperbound;
            histo = histogram.ToBitmap();
        }
        public static void toGray(Bitmap img, out Image<Bgr, byte> grayimg)
        {
            grayimg = new Image<Bgr, byte>(img);
            Byte[, ,] data = grayimg.Data; // pass by reference to 3d matrix
            // toGray
            for (int i = 0; i < grayimg.Rows; i++)
            {
                for (int j = 0; j < grayimg.Cols; j++)
                {
                    int val = 0;
                    for (int k = 0; k < 3; k++)
                        val += data[i, j, k];
                    byte result = Convert.ToByte(val / 3);
                    for (int k = 0; k < 3; k++)
                        data[i, j, k] = result;
                }
            }
        }

        private void ApplyGaussionNoise_Click(object sender, EventArgs e)
        {
            // Additive white Gaussian noise, AWGN
            if (flag == true)
            {
                Image<Bgr, byte> img;
                toGray(beforeBitmap, out img);
                Byte[, ,] data = img.Data;
                double sigma = 1.0;
                double gamma = 0.0;
                double phi = 0.0;
                double z1, z2 = 0.0;
                // Noise Model
                double[,] GNoise = new double[img.Height, img.Width];
                double min = 0.0;
                double max = 0.0;
                Random rnd = new Random();
                for (int i = 0; i < img.Height; i+=2)
                {
                    if (i+1 == img.Height)
                        break;
                    for (int j = 0; j < img.Width; j++)
                    {
                        gamma = rnd.NextDouble();
                        phi = rnd.NextDouble();
                        z1 = sigma * Math.Sqrt(-2.0 * Math.Log(gamma)) * Math.Cos(2.0 * Math.PI * phi);
                        z2 = sigma * Math.Sqrt(-2.0 * Math.Log(gamma)) * Math.Sin(2.0 * Math.PI * phi);
                        GNoise[i, j] = z1;
                        GNoise[i+1, j] = z2;
                    }
                }
                // handle the last round if the height of image is odd number
                if (img.Height % 2 != 0)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        gamma = rnd.NextDouble();
                        phi = rnd.NextDouble();
                        z1 = sigma * Math.Sqrt(-2.0 * Math.Log(gamma)) * Math.Cos(2.0 * Math.PI * phi);
                        GNoise[img.Height-1, j] = z1;
                    }
                }




                // Central Limit Theorem
                /*int samples = 12;
                for (int i = 0; i < 1000; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < samples; j++)
                    {
                        sum += rnd.NextDouble();
                    }
                    sum -= (double)samples / 2.0;
                    sum /= Math.Sqrt((double)samples / 12.0);
                    Console.WriteLine(sum);
                }*/

                double[,] XNoise = new double[img.Height, img.Width];
                // Central Limit 
                int samples = 12;
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        double sum = 0.0;
                        for (int k = 0; k < samples; k++)
                        {
                            sum += rnd.NextDouble();
                        }
                        sum -= (double)samples / 2.0;
                        sum /= Math.Sqrt((double)samples / 12.0);
                        XNoise[i, j] = sum;
                    }
                }
                // find max and min
                max = XNoise[0, 0];
                min = XNoise[0, 0];
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (XNoise[i, j] > max)
                            max = XNoise[i, j];
                        if (min > XNoise[i, j])
                            min = XNoise[i, j];
                    }
                }
                // GraySacale Stretch
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        XNoise[i, j] = 1.0 / (max - min) * (XNoise[i, j] - min);
                        //Console.WriteLine(GNoise[i, j]);
                    }
                }


                Image<Bgr, Byte> G = new Image<Bgr, Byte>(img.Width, img.Height, new Bgr(255, 255, 255));
                Byte[, ,] ndata = G.Data; // Pass by reference to 3d matrix
                // Generate Noise Model (only for display use)
                sigma = 255.0;
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int val = (int)Math.Floor(XNoise[i, j] * sigma);
                            if (val > 255)
                                val = 255;
                            else if (val < 0)
                                val = 0;
                            ndata[i, j, k] = Convert.ToByte(val);
                        }
                    }
                }








                    /*
                    double sum = 0;
                    for (int i = 0; i < img.Rows; i++)
                        for (int j = 0; j < img.Cols; j++)
                            sum += data[i, j, 0];
                    int count = img.Rows * img.Cols;
                    double mean = sum / count;
                    double[,] GauNoise = new double[100, 10];
                    double temp = 0;
                    for (int i = 0; i < img.Rows; i++)
                        for (int j = 0; j < img.Cols; j++)
                        {
                            double val = data[i, j, 0];
                            temp += ((val - mean) * (val - mean));
                        }
                    double std = Math.Sqrt(temp / (count - 1));
                    double var = std * std;
                
                
                
                    for (int i = 0; i < 100; i++)
                        for (int j = 0; j < 10; j++)
                        {
                            GauNoise[i, j] = Math.Exp(-1.0 * ((double)i - mean) * ((double)i - mean) / (2 * var)) / (Math.Sqrt(Math.PI * 2) * std);
                            //Console.WriteLine(GauNoise[i, j]);
                        }
                    // find max and min
                    max = GauNoise[0, 0];
                    min = GauNoise[0, 0];
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (GauNoise[i, j] > max)
                                max = GauNoise[i, j];
                            if (min > GauNoise[i, j])
                                min = GauNoise[i, j];
                        }
                    }
                    // GraySacale Stretch
                    int[,] hhh = new int[100,10];
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            GauNoise[i, j] = 1.0 / (max - min) * (GauNoise[i, j] - min);
                           hhh[i,j] = (int)Math.Floor(GauNoise[i, j]*255);
                            Console.WriteLine(hhh[i,j]);
                        }
                    }*/




                    // version 1
                    /*
                    double sum = 0;
                    for (int i = 0; i < img.Rows; i++)
                        for (int j = 0; j < img.Cols; j++)
                            sum += data[i, j, 0];
                    int count = img.Rows * img.Cols;
                    double mean = sum / count;
                    double temp = 0;
                    for(int i=0;i<img.Rows;i++)
                        for(int j=0;j<img.Cols;j++)
                        {
                            double val = data[i,j,0];
                            temp+= ((val-mean)*(val-mean));
                        }
                    double std = Math.Sqrt(temp / (count - 1));
                    double var = std * std;
                 

                    mean = 0;
                    //Console.WriteLine("mean: " + mean.ToString());
                    Console.WriteLine("std: "+std.ToString());
                    Console.WriteLine("variance: " + var.ToString());
                    double[] arr = new double[256];
                    for (int i = 0; i < 256; i++)
                    {
                        arr[i] = Math.Exp(-1.0 * ((double)i - mean) * ((double)i - mean) / (2 * var)) / (Math.Sqrt(Math.PI * 2) * std);
                        // Console.WriteLine(i.ToString()+": "+arr[i].ToString());
                        // Console.WriteLine(arr[i]);
                    }
                    Image<Bgr, Byte> GNoise = new Image<Bgr, Byte>(img.Width, img.Height, new Bgr(255, 255, 255));

                
                    int[] arr2 = new int[256];
                    for (int i = 0; i < 256; i++)
                    {
                        arr2[i] = 0;
                        // b = a + sqrt(p4) * randn(sizeA) + p3;
                        // arr2[i] = arr2[i] + (int)(var * arr[i]) + (int)mean;
                        Console.WriteLine(arr2[i]);
                    }
                    
                
                    for (int i = 0; i < img.Rows; i++)
                        for (int j = 0; j < img.Cols; j++)
                            for (int k = 0; k < 3; k++)
                            {
                                int val = data[i, j, k];
                                data[i, j, k] = Convert.ToByte(arr2[val]);
                            }
                    */

                    // find max and min
                    max = GNoise[0, 0];
                min = GNoise[0, 0];
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (GNoise[i, j] > max)
                            max = GNoise[i, j];
                        if (min > GNoise[i, j])
                            min = GNoise[i, j];
                    }
                }
                // GraySacale Stretch
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        GNoise[i, j] = 1.0 / (max - min) * (GNoise[i, j] - min);
                        //Console.WriteLine(GNoise[i, j]);
                    }
                }
                // Generate Noise Model
                sigma = 3.0;
                Image<Bgr, Byte> GGNoise = new Image<Bgr, Byte>(img.Width, img.Height, new Bgr(255, 255, 255));
                Byte[, ,] noisedata = GGNoise.Data; // Pass by reference to 3d matrix
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int val = (int)Math.Floor(GNoise[i, j] * sigma);
                            if (val > 255)
                                val = 255;
                            else if (val < 0)
                                val = 0;
                            noisedata[i, j, k] = Convert.ToByte(val);
                        }
                    }
                }

                // add noise model to the image
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            data[i, j, k] += noisedata[i, j, k];
                        }
                    }
                }
                // Generate Noise Model (only for display use)
                sigma = 255.0;
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int val = (int)Math.Floor(GNoise[i, j] * sigma);
                            if (val > 255)
                                val = 255;
                            else if (val < 0)
                                val = 0;
                            noisedata[i, j, k] = Convert.ToByte(val);
                        }
                    }
                }
                // genereate histogram
                int upperbound;
                Bitmap histogram;
                GetHistogram(G.ToBitmap(), out upperbound, out histogram);
                //GetHistogram(GGNoise.ToBitmap(), out upperbound, out histogram);
                pictureBox2.Image = G.ToBitmap();
                //pictureBox2.Image = img.ToBitmap();
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                Form2 myhistogram = new Form2(histogram);
                myhistogram.Show();
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }
    }
}
