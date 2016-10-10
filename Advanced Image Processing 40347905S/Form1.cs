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
        public static void Gaussian_approximation(Image<Bgr, byte> img, out Bitmap noisemodel, out Bitmap result, double sigma)
        {
            int size = 256;
            double mean = 0.0, std = size / 8;
            double var = std * std;
            
            // Generate PDF of Gaussian
            double[] valtable = new double[size];
            for (int i = 0; i < size; i++)
            {
                valtable[i] = Math.Exp(-1.0 * ((double)(i - size/2) - mean) * ((double)(i - size/2) - mean) / (2 * var)) / (Math.Sqrt(Math.PI * 2) * std);
                //Console.WriteLine(valtable[i]);
            }
            // find max and min
            double max = valtable[0];
            double min = valtable[0];
            for (int i = 0; i < size; i++)
            {
                if (max < valtable[i])
                    max = valtable[i];
                if (min > valtable[i])
                    min = valtable[i];
            }
            //Console.WriteLine(max);
            //Console.WriteLine(min);
            // GraySacale Stretch
            for (int i = 0; i < size; i++)
            {
                valtable[i] = 255.0 / (max - min) * (valtable[i] - min);
                //Console.WriteLine(valtable[i]);
            }
            // Generate Gaussian Sequence
            List<double> Gaussian = new List<double>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < valtable[i]; j++)
                {
                    Gaussian.Add(i);
                    //Console.WriteLine(i);
                }
                
            }


            // Generate Noise Model
            double[,] noise = new double[img.Height,img.Width];
            Random rnd = new Random();
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    noise[i, j] = Gaussian[rnd.Next() % Gaussian.Count];
                    //Console.WriteLine(noise[i, j]);
                }
            }





            // find max and min
            max = noise[0,0];
            min = noise[0,0];
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    if (max < noise[i,j])
                        max = noise[i,j];
                    if (min > noise[i,j])
                        min = noise[i,j];
                }
            }
            //Console.WriteLine(min);
            //Console.WriteLine(max);
            // GraySacale Stretch
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    noise[i,j] = (10.0 / (max - min) * (noise[i,j] - min) - 5.0) * sigma;
                    //Console.WriteLine(noise[i,j]);
                }  
            }
            //test
            /*
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    int val = (int)(255.0 / (max - min) * (noise[i, j] - min));
                    if(val > 255)
                        Console.WriteLine(255);
                    else if(val < 0)
                        Console.WriteLine(0);
                    else
                        Console.WriteLine(noise[i, j]);
                }
            }*/



            //================================


            Byte[, ,] idata = img.Data;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    int val = idata[i, j, 0];
                    val += (int)Math.Floor(noise[i, j]);
                    if (val > 255)
                        val = 255;
                    else if (val < 0)
                        val = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        idata[i, j, k] = Convert.ToByte(val);
                    }
                }
            }
            result = img.ToBitmap();







            // find max and min
            max = noise[0, 0];
            min = noise[0, 0];
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    if (max < noise[i, j])
                        max = noise[i, j];
                    if (min > noise[i, j])
                        min = noise[i, j];
                }
            }
            Image<Bgr, Byte> G = new Image<Bgr, Byte>(img.Width, img.Height, new Bgr(255, 255, 255));
            Byte[, ,] ndata = G.Data; // Pass by reference to 3d matrix
            for (int i = 0; i <img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    noise[i, j] = 255.0 / (max - min) * (noise[i, j] - min);
                    int val = (int)Math.Floor(noise[i, j]);
                    if (val > 255)
                        val = 255;
                    else if (val < 0)
                        val = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        ndata[i, j, k] = Convert.ToByte(val);
                    }
                }
            }
            noisemodel = G.ToBitmap();
        }

        public static void Central_Limit_Theorem(Image<Bgr, byte> img, out Bitmap noisemodel, out Bitmap result, double sigma)
        {
            int height = img.Height, width = img.Width;
            // Central Limit 
            double min = 0.0, max = 0.0;
            double[,] XNoise = new double[height, width];
            Random rnd = new Random();
            int samples = 12;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < samples; k++)
                    {
                        sum += rnd.NextDouble();
                    }
                    sum -= (double)samples / 2.0;
                    sum /= Math.Sqrt((double)samples / 12.0);
                    XNoise[i, j] = sum * sigma;
                }
            }
            // Print
            /*
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    Console.WriteLine(XNoise[i, j]);
             */
            Byte[, ,] idata = img.Data;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int val = idata[i, j, 0];
                    val += (int)Math.Floor(XNoise[i, j]);
                    if (val > 255)
                        val = 255;
                    else if (val < 0)
                        val = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        idata[i, j, k] = Convert.ToByte(val);
                    }
                }
            }
            result = img.ToBitmap();

            // ====== Display Noise Model =====
            // find max and min
            max = XNoise[0, 0];
            min = XNoise[0, 0];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (XNoise[i, j] > max)
                        max = XNoise[i, j];
                    if (min > XNoise[i, j])
                        min = XNoise[i, j];
                }
            }
            //Console.WriteLine(min);
            //Console.WriteLine(max);
            // GraySacale Stretch
            Image<Bgr, Byte> G = new Image<Bgr, Byte>(width, height, new Bgr(255, 255, 255));
            Byte[, ,] ndata = G.Data; // Pass by reference to 3d matrix
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    XNoise[i, j] = 255.0 / (max - min) * (XNoise[i, j] - min);
                    //Console.WriteLine(GNoise[i, j]);
                    int val = (int)Math.Floor(XNoise[i, j]);
                    if (val > 255)
                        val = 255;
                    else if (val < 0)
                        val = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        ndata[i, j, k] = Convert.ToByte(val);
                    }
                }
            }
            noisemodel = G.ToBitmap();
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
        
        public static void BoxMuller(Image<Bgr,byte> img, out Bitmap noisemodel, out Bitmap result,double sigma){
                // Noise Model
                Byte[, ,] data = img.Data;
                double gamma = 0.0, phi = 0.0, z = 0.0;
                



                double[,] GauNoise = new double[img.Height, img.Width];
                double min = 0.0, max = 0.0;
                Random rnd = new Random();
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (j % 2 == 0)
                        {
                            gamma = rnd.NextDouble();
                            phi = rnd.NextDouble();
                        }
                        if(j % 2 != 0 && j == img.Width-1){
                            gamma = rnd.NextDouble();
                            phi = rnd.NextDouble();
                        }
                        if (j % 2 == 0)
                            z = sigma * Math.Sqrt(-2.0 * Math.Log(gamma)) * Math.Cos(2.0 * Math.PI * phi);
                        else
                            z = sigma * Math.Sqrt(-2.0 * Math.Log(gamma)) * Math.Sin(2.0 * Math.PI * phi);
                        GauNoise[i, j] = z;
                        z += data[i, j, 0];
                        if (z > 255.0)
                            z = 255.0;
                        else if (z < 0.0)
                            z = 0.0;
                        for (int k = 0; k < 3; k++)
                            data[i, j, k] = (Convert.ToByte(z));
                    }
                }
                // find max and min
                max = GauNoise[0, 0];
                min = GauNoise[0, 0];
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (GauNoise[i, j] > max)
                            max = GauNoise[i, j];
                        if (min > GauNoise[i, j])
                            min = GauNoise[i, j];
                    }
                }
                // Apply effect of graysacale stretch to the noise model
                Image<Bgr, Byte> noiseimg = new Image<Bgr, Byte>(img.Width, img.Height, new Bgr(255, 255, 255));
                Byte[, ,] noise = noiseimg.Data;
                for (int i = 0; i < img.Height; i++)
                    for (int j = 0; j < img.Width; j++)
                    {
                        double val;
                        if (max == 0.0 && min == 0.0)
                            val = 0.0;
                        else
                            val = Math.Floor((1.0 / (max - min) * (GauNoise[i, j] - min)) * 255.0);



                        if (val > 255.0)
                            GauNoise[i, j] = 255.0;
                        else if (val < 0.0)
                            GauNoise[i, j] = 0.0;
                        else
                            GauNoise[i, j] = val;

                        for (int k = 0; k < 3; k++)
                            noise[i, j, k] = Convert.ToByte((int)GauNoise[i, j]);
                    }
                noisemodel = noiseimg.ToBitmap();
                result = img.ToBitmap();
                //Console.WriteLine(min);
                //Console.WriteLine(max);
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
                // toGray
                Image<Bgr, byte> img;
                toGray(beforeBitmap, out img);
                // select effect
                Form3 input = new Form3();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    double sigma = input.GetSigma();
                    Bitmap noisemodel, result;
                    if (input.GetMode() == 1)
                        BoxMuller(img, out noisemodel, out result, sigma);
                    else if(input.GetMode() == 2)
                        Central_Limit_Theorem(img, out noisemodel, out result, sigma);
                    else
                        Gaussian_approximation(img, out noisemodel, out result, sigma);

                    // test
                    /*
                    for (int i = 0; i < 1000; i++) {
                        Console.WriteLine("BM");
                        BoxMuller(img, out noisemodel, out result, i);
                        Console.WriteLine("CLT");
                        Central_Limit_Theorem(img, out noisemodel, out result, i);
                        Console.WriteLine("GA");
                        Gaussian_approximation(img, out noisemodel, out result, i);
                    }*/
                        
                    
                    // genereate histogram
                    int upperbound;
                    Bitmap histogram;
                    GetHistogram(noisemodel, out upperbound, out histogram);
                    pictureBox2.Image = result;
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    Form2 myhistogram = new Form2(histogram);
                    myhistogram.Show();
                }      
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }
    }
}
