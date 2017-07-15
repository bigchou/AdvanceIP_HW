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
            //Console.WriteLine("test");






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

        private void ConvertColorSpace_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                
                Image<Bgr, byte> img = new Image<Bgr, byte>(beforeBitmap);
                Byte[, ,] data = img.Data;
                Image<Bgr, byte> smallimg = new Image<Bgr, byte>(img.Width, img.Height, new Bgr(0, 0, 0));
                Byte[, ,] sdata = smallimg.Data;
                Form4 input = new Form4();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (input.GetMode() == 1)
                    {
                        // toRBG
                        Console.WriteLine("RGB");
                    }else if (input.GetMode() == 2)
                    {
                        // toXYZ
                        for (int i = 0; i < img.Height; i++)
                        {
                            for (int j = 0; j < img.Width; j++)
                            {
                                double r = data[i, j, 2];
                                double g = data[i, j, 1];
                                double b = data[i, j, 0];
                                double x = 0.41 * r + 0.36 * g + 0.18 * b; // r2x
                                if (x > 255)
                                    x = 255;
                                double y = 0.21 * r + 0.72 * g + 0.07 * b;// g2y
                                if (y > 255)
                                    y = 255;
                                double z = 0.02 * r + 0.12 * g + 0.95 * b;// b2z
                                if (z > 255)
                                    z = 255;
                                data[i, j, 2] = Convert.ToByte(x);
                                data[i, j, 1] = Convert.ToByte(y);
                                data[i, j, 0] = Convert.ToByte(z);
                            }
                        }
                        Console.WriteLine("XYZ");
                    }
                    else if (input.GetMode() == 3)
                    {
                        // toCMY
                        for (int i = 0; i < img.Height; i++)
                            for (int j = 0; j < img.Width;j++ )
                                for(int k=0; k < 3; k++)
                                    data[i,j,k] = Convert.ToByte(255 -data[i,j,k]);
                        Console.WriteLine("CMY");
                    }
                    else if (input.GetMode() == 4)
                    {
                        // toCMYK without profile
                        for (int i = 0; i < img.Height; i++)
                        {
                            for (int j = 0; j < img.Width; j++)
                            {
                                int[] bgr_bar = new int[3];
                                for (int k = 0; k < 3; k++)
                                    bgr_bar[k] = data[i, j, k];
                                double[] bgr = new double[3];
                                for (int k = 0; k < 3; k++)
                                    bgr[k] = (double)bgr_bar[k] / 255.0;

                                double big = bgr[0];
                                for(int k=0;k<3;k++)
                                    if(big < bgr[k])
                                        big = bgr[k];
                                double blacK = 1.0 - big;
                                double[] cmy = new double[3];
                                if (blacK == 1.0)
                                {
                                    for (int k = 0; k < 3; k++)
                                        cmy[k] = 0.0;
                                }
                                else
                                {
                                    for (int k = 0; k < 3; k++)
                                        cmy[k] = (1.0 - bgr[k] - blacK) / (1.0 - blacK);
                                }

                                for (int k = 0; k < 3;k++ )
                                    data[i, j, k] = Convert.ToByte((int)((1.0-cmy[k]) * (1.0-blacK) *255.0));
                            }
                        }
                        Console.WriteLine("CMYK without color profile");
                    }
                    else if (input.GetMode() == 5)
                    {
                        // CIE Lab
                        double[, ,] cielabimg = new double[img.Height, img.Width, 3];
                        // CIE-to-XYZ-to-LAB Conversion
                        double Xn = 0.9515;
                        double Yn = 1.0;
                        double Zn = 1.0886;
                        for (int i = 0; i < img.Height; i += 1)
                        {
                            for (int j = 0; j < img.Width; j += 1)
                            {
                                double R = (double)(data[i, j, 2]) / 255.0;
                                double G = (double)(data[i, j, 1]) / 255.0;
                                double B = (double)(data[i, j, 0]) / 255.0;
                                double X = B * 0.180423 + R * 0.412453 + G * 0.357580;
                                double Y = B * 0.072169 + R * 0.212671 + G * 0.715160;
                                double Z = B * 0.950227 + R * 0.019334 + G * 0.119193;
                                double tmpY = Y / Yn;
                                double tmpX = X / Xn;
                                double tmpZ = Z / Zn;
                                double L = 0.0;
                                if (tmpY > 0.008856)
                                    L = 116.0 * Math.Pow(tmpY, 1.0 / 3.0) - 16.0;
                                else
                                    L = 903.3 * tmpY;
                                double a1 = 0.0;
                                double a2 = 0.0;
                                double a = 0.0;
                                if (tmpX > 0.008856)
                                    a1 = Math.Pow(tmpX, 1.0 / 3.0);
                                else
                                    a1 = 7.787 * tmpX + 16.0 / 116.0;
                                if (tmpY > 0.008856)
                                    a2 = Math.Pow(tmpY, 1.0 / 3.0);
                                else
                                    a2 = 7.787 * tmpY + 16.0 / 116.0;
                                a = 500.0 * (a1 - a2);

                                double b2 = 0.0;
                                double b = 0.0;
                                if (tmpZ > 0.008856)
                                    b2 = Math.Pow(tmpZ, 1.0 / 3.0);
                                else
                                    b2 = 7.787 * tmpZ + 16.0 / 116.0;
                                b = 200.0 * (a2 - b2);
                                /*
                                Console.WriteLine(L);
                                Console.WriteLine(a);
                                Console.WriteLine(b);*/
                                cielabimg[i, j, 0] = L;
                                cielabimg[i, j, 1] = a;
                                cielabimg[i, j, 2] = b;
                            }
                        }
                        // find max and min
                        double[] max = new double[3];
                        double[] min = new double[3];
                        for (int i = 0; i < 3; i += 1)
                        {
                            max[i] = cielabimg[0, 0,i];
                            min[i] = cielabimg[0, 0,i];
                        }
                        for (int k = 0; k < 3;k+=1 )
                            for (int i = 0; i < img.Height; i++)
                            {
                                for (int j = 0; j < img.Width; j++)
                                {
                                    if (cielabimg[i, j,k] > max[k])
                                        max[k] = cielabimg[i, j, k];
                                    if (min[k] > cielabimg[i, j, k])
                                        min[k] = cielabimg[i, j, k];
                                }
                            }
                        // linear adjustment
                        for (int k = 0; k < 3;k+=1 )
                            for (int i = 0; i < img.Height; i++)
                                for (int j = 0; j < img.Width; j++)
                                {
                                    double val = Math.Floor((1.0 / (max[k] - min[k]) * (cielabimg[i, j, k] - min[k])) * 255.0);
                                    if (val > 255.0)
                                        val = 255.0;
                                    else if (val < 0.0)
                                        val = 0.0;
                                    data[i, j, k] = Convert.ToByte(Math.Round(val));
                                }
                        Console.WriteLine("CIE Lab");
                    }
                    int u = 0, v = 0;
                    // upperleft --- Gray
                    for (int i = 0; i < img.Height; i += 2)
                    {
                        for (int j = 0; j < img.Width; j += 2)
                        {
                            double r = data[i, j, 2];
                            double g = data[i, j, 1];
                            double b = data[i, j, 0];
                            int val = (int)(r * 0.299 + g * 0.587 + b * 0.114);
                            if (val > 255)
                                val = 255;
                            else if (val < 0)
                                val = 0;
                            for (int k = 0; k < 3; k++)
                                sdata[u, v, k] = Convert.ToByte(val);
                            v += 1;
                        }
                        u += 1;
                        v = 0;
                    }
                    // upperright --- R
                    u = 0;
                    v = img.Width / 2;
                    for (int i = 0; i < img.Height; i += 2)
                    {
                        for (int j = 0; j < img.Width; j += 2)
                        {
                            double r = data[i, j, 2];
                            if (r < 0)
                                r = 0;
                            else if (r > 255)
                                r = 255;
                            for (int k = 0; k < 3; k++)
                                sdata[u, v, k] = Convert.ToByte(r);
                            v += 1;
                        }
                        u += 1;
                        v = img.Width / 2;
                    }
                    // lowerleft --- G
                    u = img.Height / 2;
                    v = 0;
                    for (int i = 0; i < img.Height; i += 2)
                    {
                        for (int j = 0; j < img.Width; j += 2)
                        {
                            double g = data[i, j, 1];
                            if (g < 0)
                                g = 0;
                            else if (g > 255)
                                g = 255;
                            for (int k = 0; k < 3; k++)
                                sdata[u, v, k] = Convert.ToByte(g);
                            v += 1;
                        }
                        u += 1;
                        v = 0;
                    }
                    // lowerright --- B
                    u = img.Height / 2;
                    v = img.Width / 2;
                    for (int i = 0; i < img.Height; i += 2)
                    {
                        for (int j = 0; j < img.Width; j += 2)
                        {
                            double b = data[i, j, 0];
                            if (b < 0)
                                b = 0;
                            else if (b > 255)
                                b = 255;
                            for (int k = 0; k < 3; k++)
                                sdata[u, v, k] = Convert.ToByte(b);
                            v += 1;
                        }
                        u += 1;
                        v = img.Width / 2;
                    }
                    pictureBox2.Image = smallimg.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private void FFT_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Image<Gray, float> image = new Image<Gray, float>(beforeBitmap);
                // 1 channel to 2 channel (Real and Imaginary)
                IntPtr complexImage = CvInvoke.cvCreateImage(image.Size, Emgu.CV.CvEnum.IPL_DEPTH.IPL_DEPTH_32F, 2);
                CvInvoke.cvSetZero(complexImage);
                CvInvoke.cvSetImageCOI(complexImage, 1); // Select the channel to copy into
                CvInvoke.cvCopy(image, complexImage, IntPtr.Zero);
                CvInvoke.cvSetImageCOI(complexImage, 0); // Select all channels
                // DFT 
                Matrix<float> forwardDft = new Matrix<float>(image.Rows, image.Cols, 2);
                CvInvoke.cvDFT(complexImage, forwardDft, Emgu.CV.CvEnum.CV_DXT.CV_DXT_FORWARD, 0);
                CvInvoke.cvReleaseImage(ref complexImage);
                // Show 
                Matrix<float> forwardDftMagnitude = GetDftMagnitude(forwardDft);
                SwitchQuadrants(ref forwardDftMagnitude);
                pictureBox2.Image = Matrix2Bitmap(forwardDftMagnitude);
                if (forwardDftMagnitude.Size.Height * forwardDftMagnitude.Size.Width > pictureBox2.Size.Height * pictureBox2.Size.Width)
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                else
                    pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private Bitmap Matrix2Bitmap(Matrix<float> matrix)
        {
            // Stretch
            float min = matrix[0,0], max = min;
            for(int i=0;i<matrix.Size.Height;i++)
                for(int j=0;j<matrix.Size.Width;j++){
                    if (max < matrix[i, j])
                        max = matrix[i, j];

                    if(min > matrix[i,j])
                        min = matrix[i,j];
                }
            Image<Gray, float> image = new Image<Gray, float>(matrix.Size);
            float[, ,] data = image.Data;
            // Copy
            for (int i = 0; i < image.Height; i++)
                for (int j = 0; j < image.Width; j++)
                    data[i,j,0] = (float)Math.Floor(255.0 / (max - min) * (matrix[i,j] - min));
            return image.ToBitmap();
        }

        private Matrix<float> GetDftMagnitude(Matrix<float> fftData)
        {
            // Split into Real and Imaginary
            Matrix<float> outReal = new Matrix<float>(fftData.Size);
            Matrix<float> outIm = new Matrix<float>(fftData.Size);
            CvInvoke.cvSplit(fftData, outReal, outIm, IntPtr.Zero, IntPtr.Zero);
            // 1 + log(sqrt(a^2 + b^2))
            CvInvoke.cvPow(outReal, outReal, 2.0);
            CvInvoke.cvPow(outIm, outIm, 2.0);
            CvInvoke.cvAdd(outReal, outIm, outReal, IntPtr.Zero);
            CvInvoke.cvPow(outReal, outReal, 0.5);
            CvInvoke.cvAddS(outReal, new MCvScalar(1.0), outReal, IntPtr.Zero);
            CvInvoke.cvLog(outReal, outReal);
            return outReal;
        }

        private void SwitchQuadrants(ref Matrix<float> matrix)
        {
            int cx = matrix.Cols / 2;
            int cy = matrix.Rows / 2;
            Matrix<float> q0 = matrix.GetSubRect(new Rectangle(0, 0, cx, cy));
            Matrix<float> q1 = matrix.GetSubRect(new Rectangle(cx, 0, cx, cy));
            Matrix<float> q2 = matrix.GetSubRect(new Rectangle(0, cy, cx, cy));
            Matrix<float> q3 = matrix.GetSubRect(new Rectangle(cx, cy, cx, cy));
            Matrix<float> tmp = new Matrix<float>(q0.Size);
            q0.CopyTo(tmp);
            q3.CopyTo(q0);
            tmp.CopyTo(q3);
            q1.CopyTo(tmp);
            q2.CopyTo(q1);
            tmp.CopyTo(q2);
        }


        private void ColorDetect_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Dictionary<int, String> ColorCode = new Dictionary<int, String>(){
                    {0,"Black"},
                    {1,"White"},
                    {2,"Gray"},
                    {3,"Red"},
                    {4,"Orange"},
                    {5,"Yellow"},
                    {6,"Green"},
                    {7,"Blue"},
                    {8,"Purple"}
                };
                int[] ColorTable = new int[ColorCode.Count()];
                for (int i = 0; i < ColorCode.Count(); i++)
                    ColorTable[i] = 0;
                Image<Bgr, byte> img = new Image<Bgr, byte>(beforeBitmap);
                Byte[, ,] data = img.Data;
                double [,,] imgHSV = new double[img.Height,img.Width,3];
                double[,] mark = new double[img.Height, img.Width];
                
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        // HSV
                        double[] bgr = new double[3];
                        for (int k = 0; k < 3; k++)
                        {
                            double val = data[i, j, k];
                            bgr[k] = val / 255.0;
                        }
                        double delta = bgr.Max() - bgr.Min();
                        // Hue
                        double Hue = 0.0;
                        if (delta == 0.0)
                        {
                            Hue = 0.0;
                        }
                        else
                        {
                            double maxValue = bgr.Max();
                            int maxIndex = bgr.ToList().IndexOf(maxValue);
                            //Console.WriteLine(maxIndex);
                            if (maxIndex == 0)//b
                                Hue = (bgr[2] - bgr[1]) / delta + 4.0;
                            else if (maxIndex == 1)//g
                                Hue = (bgr[0] - bgr[2]) / delta + 2.0;
                            else if (maxIndex == 2)//r
                                Hue = ((bgr[1] - bgr[0]) / delta) % 6;
                            Hue *= 60;
                        }
                        // Saturation
                        double Saturation = 0.0;
                        if (bgr.Max() == 0.0)
                            Saturation = 0.0;
                        else
                            Saturation = delta / bgr.Max();
                        // Value
                        double Value = bgr.Max();

                        //Hue *= 360.0;
                        imgHSV[i, j, 0] = Hue;
                        imgHSV[i, j, 1] = Saturation;
                        imgHSV[i, j, 2] = Value;
                        //Console.WriteLine(Hue);
                        //Console.WriteLine(Saturation);
                        //Console.WriteLine(Value);

                        if (Value < 0.001)
                        {
                            ColorTable[0] += 1;//black
                            mark[i, j] = 0;
                        }
                        else if (Saturation == 0)
                        {
                            if (Value == 1)
                            {
                                ColorTable[1] += 1;//white
                                mark[i, j] = 1;
                            }
                            else
                            {
                                ColorTable[2] += 1;//gray
                                mark[i, j] = 2;
                            }
                        }
                        else
                        {
                            if (Hue <= 18 || Hue > 354)
                            {//red
                                ColorTable[3] += 1;
                                mark[i, j] = 3;
                            }
                            else if (Hue <= 50)
                            {//orange
                                ColorTable[4] += 1;
                                mark[i, j] = 4;
                            }
                            else if (Hue <= 60)
                            {//yellow
                                ColorTable[5] += 1;
                                mark[i, j] = 5;
                            }
                            else if (Hue <= 170)
                            {//green
                                ColorTable[6] += 1;
                                mark[i, j] = 6;
                            }
                            else if (Hue <= 254)
                            {//blue
                                ColorTable[7] += 1;
                                mark[i, j] = 7;
                            }
                            else if (Hue <= 354)
                            {//purple
                                ColorTable[8] += 1;
                                mark[i, j] = 8;
                            }
                        }   
                    }
                }
                for (int i = 0; i < ColorCode.Count(); i++)
                    Console.WriteLine(ColorCode[i]+": "+ColorTable[i].ToString());
                int maxval = ColorTable.Max();
                int kind = ColorTable.ToList().IndexOf(maxval);
                
                Form5 input = new Form5();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (input.GetMode() == 1)
                    {
                        //Detect Green
                        for (int i = 0; i < img.Height; i++)
                            for (int j = 0; j < img.Width; j++)
                            {
                                if (imgHSV[i, j, 0] > 60 && imgHSV[i, j, 0] <= 170 && imgHSV[i, j, 1] >= 0.2 && imgHSV[i, j, 2] >= 0.1)
                                    for (int k = 0; k < 3; k++)
                                        data[i, j, k] = 255;
                                else
                                    for (int k = 0; k < 3; k++)
                                        data[i, j, k] = 0;
                            }
                    }
                    else
                    {
                        //Auto Detection
                        for (int i = 0; i < img.Height; i++)
                            for (int j = 0; j < img.Width; j++)
                            {
                                if (mark[i, j] == kind)
                                    for (int k = 0; k < 3; k++)
                                        data[i, j, k] = 255;
                                else
                                    for (int k = 0; k < 3; k++)
                                        data[i, j, k] = 0;
                            }
                    }
                    pictureBox2.Image = img.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }


       
        }

        private void Histogram_Equalization_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Image<Bgr, byte> img; //= new Image<Gray, byte>(beforeBitmap);
                toGray(beforeBitmap, out img);
                Byte[, ,] data = img.Data;
                // Build LookUpTable
                int[,] lut = new int[256,2];
                for (int i = 0; i < 256; i++)
                    lut[i, 0] = 0;
                int g_min = 255;
                for (int i = 0; i < img.Height; i++)
                    for (int j = 0; j < img.Width; j++)
                    {
                        lut[data[i, j, 0], 0] += 1;
                        if (data[i, j, 0] < g_min)
                            g_min = data[i,j,0];
                    }
                //for(int i=0;i<256;i++)
                //    Console.WriteLine(i.ToString() + ": " + lut[i, 0]);
                //return;
                Form6 input = new Form6();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (input.GetMode() == 1)
                    {
                        //with h_min
                        int h_min = lut[g_min,0];
                        //Console.WriteLine(g_min);
                        //Console.WriteLine(h_min);
                        double prob = 255.0 / (double)(img.Height * img.Width - h_min);
                        int[] cumu = new int[256];
                        //First Pass-build Cumulative histogram
                        for (int i = 0; i < 256; i++)
                        {
                            if (i == 0)
                                cumu[i] = lut[i, 0];
                            else
                                cumu[i] = cumu[i - 1] + lut[i, 0];
                        }
                        //for (int i = 0; i < 256; i++)
                        //    Console.WriteLine(i.ToString() + ": " + cumu[i]);
                        //Console.WriteLine(g_min);
                        //Console.WriteLine(h_min);
                        //return;
                        //Second Pass-Look Up Table
                        for (int i = 0; i < 256; i++)
                        {
                            if (cumu[i] - h_min < 0)
                            {
                                lut[i, 1] = -1;
                                continue;
                            }
                            cumu[i] -= h_min;
                            double cdf = Math.Round(prob *  ((double)cumu[i])   );
                            if (cdf > 255)
                                cdf = 255;
                            lut[i, 1] = (int)cdf;
                        }
                        
                    }
                    else if (input.GetMode() == 2)
                    {
                        //without h_min
                        double prob = 255.0 / (double)(img.Height * img.Width);
                        for (int i = 0; i < 256; i++)
                        {
                            if (i != 0)
                                lut[i, 0] += lut[i - 1, 0];
                            //Console.WriteLine(i.ToString() + ": " + lut[i, 0]);
                            double cdf = Math.Round(prob * ((double)lut[i, 0]));
                            if (cdf > 255)
                                cdf = 255;
                            lut[i, 1] = (int)cdf;
                        }
                    }
                    //for (int i = 0; i < 256; i++)
                    //    Console.WriteLine(i.ToString() + ": " + lut[i, 1]);
                    //return;
                    // Generate Result
                    Image<Bgr, byte> result; //= new Image<Bgr, byte>(beforeBitmap);
                    toGray(beforeBitmap, out result);
                    Byte[, ,] rdata = result.Data;
                    for (int i = 0; i < img.Height; i++)
                    {
                        for (int j = 0; j < img.Width; j++)
                        {
                            int val = lut[rdata[i, j, 0], 1];
                            for (int k = 0; k < 3; k++)
                            {
                                rdata[i, j, k] = Convert.ToByte(val);
                            }
                        }
                    }
                    pictureBox2.Image = result.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    // Display
                    Bitmap histogram;
                    int upperbound;
                    GetHistogram(img.ToBitmap(), out upperbound, out histogram);
                    Form2 myhistogram = new Form2(histogram);
                    myhistogram.Show();
                    myhistogram.Text = "Histogram of Input Image";
                    GetHistogram(result.ToBitmap(), out upperbound, out histogram);
                    Form2 myhistogram2 = new Form2(histogram);
                    myhistogram2.Show();
                    myhistogram2.Text = "Histogram of Output Image";
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private void smooth_and_edge_detect_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form7 input = new Form7();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Filter filter = new Filter();
                    pictureBox2.Image = filter.imfilter(beforeBitmap, input.GetMode());
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }else{
                MessageBox.Show("Please select an image to continue...");
            }
        }

        public static void imsharpen(Bitmap greyimg, double cof, out Image<Bgr, byte> resizedImage)
        {
            Image<Bgr, byte> grayimg;
            toGray(greyimg, out grayimg);
            Byte[, ,] data = grayimg.Data;
            Image<Bgr, Byte> afterBitmap = new Image<Bgr, Byte>(grayimg.Width - 2, grayimg.Height - 2, new Bgr(255, 255, 255));
            Byte[, ,] resdata = afterBitmap.Data; // pass by reference to 3d matrix
            double[,] mask = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    mask[i, j] = 1.0 / 9.0;
            //filtering
            for (int i = 1; i < grayimg.Height - 1; i++)
                for (int j = 1; j < grayimg.Width - 1; j++)
                {
                    double val = 0;
                    for (int m = 0; m < 3; m++)
                        for (int n = 0; n < 3; n++)
                        {
                            val += (data[i + m - 1, j + n - 1, 0] * mask[m, n]);
                        }
                    if (val < 0)
                        val = 0;
                    else if (val > 255)
                        val = 255;
                    for (int k = 0; k < 3; k++)
                        resdata[i - 1, j - 1, k] = Convert.ToByte(val);
                }

            Image<Bgr, byte> resizeImage = grayimg.Resize(grayimg.Width - 2, grayimg.Height - 2, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            Byte[, ,] img = resizeImage.Data;
            //double cof = 10;
            for (int i = 0; i < resizeImage.Height; i++)
                for (int j = 0; j < resizeImage.Width; j++)
                {
                    double blur = resdata[i, j, 0];
                    double ori = img[i, j, 0];
                    double val = ori + cof * (ori - blur);
                    if (val < 0)
                        val = 0;
                    else if (val > 255)
                        val = 255;
                    for (int k = 0; k < 3; k++)
                        img[i, j, k] = Convert.ToByte(val);
                }
            resizedImage = resizeImage;
        }


        private void UnSharpenedMask_Button_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form8 input = new Form8();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Image<Bgr, byte> result;
                    imsharpen(beforeBitmap, input.GetCof(), out result);
                    //resizeImage._EqualizeHist();

                    pictureBox2.Image = result.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private void bilater_filter_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("bilat");
            if (flag == true)
            {
                Form9 input = new Form9();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Image<Bgr, byte> inputimg = new Image<Bgr, byte>(beforeBitmap);
                    Byte[, ,] in_img = inputimg.Data;
                    Image<Bgr, byte> outputimg = new Image<Bgr, byte>(beforeBitmap);
                    Byte[, ,] out_img = outputimg.Data;
                    //Pre-compute Gaussian distance weights.
                    int w = (int)input.Getfilterwidth();//int w = 11;
                    double sigma_c = input.Getsigma_c();//double sigma_c = 45;
                    double sigma_s = input.Getsigma_s();//double sigma_s = 45;
                    
                    double[,] X = new double[2 * w + 1, 2 * w + 1];
                    double[,] Y = new double[2 * w + 1, 2 * w + 1];
                    double[,] C = new double[2 * w + 1, 2 * w + 1];
                    for (int i = 0; i < (2 * w + 1); i += 1)
                    {
                        for (int j = 0; j < (2 * w + 1); j += 1)
                        {
                            X[i, j] = j - w;
                        }
                    }
                    for (int i = 0; i < (2 * w + 1); i += 1)
                    {
                        for (int j = 0; j < (2 * w + 1); j += 1)
                        {
                            Y[i, j] = i - w;
                        }
                    }
                    for (int i = 0; i < (2 * w + 1); i += 1)
                    {
                        for (int j = 0; j < (2 * w + 1); j += 1)
                        {
                            C[i, j] = Math.Exp(-1 * (X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * sigma_c * sigma_c));
                            //Console.WriteLine(C[i, j]);
                        }
                    }


                    int m = inputimg.Height;
                    int n = inputimg.Width;
                    //double c = 3;
                    double[,] I = new double[2 * w + 1, 2 * w + 1];
                    double[,] H = new double[2 * w + 1, 2 * w + 1];
                    double[,] F = new double[2 * w + 1, 2 * w + 1];

                    for (int i = 0; i < m; i += 1)
                    {

                        for (int j = 0; j < n; j += 1)
                        {

                            //Extract local region.
                            int iMin = Math.Max(i - w, 0);
                            int iMax = Math.Min(i + w, m - 1);
                            int jMin = Math.Max(j - w, 0);
                            int jMax = Math.Min(j + w, n - 1);
                            for (int p = iMin; p < iMax; p += 1)
                            {
                                for (int q = jMin; q < jMax; q += 1)
                                {
                                    I[p - iMin, q - jMin] = in_img[p, q, 0];
                                }
                            }

                            // Compute Gaussian intensity weights.
                            double tmp = in_img[i, j, 0];
                            for (int p = iMin; p < iMax; p += 1)
                            {
                                for (int q = jMin; q < jMax; q += 1)
                                {
                                    double diff = (I[p - iMin, q - jMin] - tmp);
                                    H[p - iMin, q - jMin] = Math.Exp(-1.0 * diff * diff / (2 * sigma_s * sigma_s));
                                }
                            }
                            //Console.WriteLine("here");

                            //Calculate bilateral filter response.
                            for (int p = iMin; p < iMax; p += 1)
                            {
                                for (int q = jMin; q < jMax; q += 1)
                                {
                                    F[p - iMin, q - jMin] = H[p - iMin, q - jMin] * C[p - i + w + 1, q - j + w + 1];
                                }
                            }
                            double totalA = 0;
                            double totalB = 0;
                            for (int p = iMin; p < iMax; p += 1)
                            {
                                for (int q = jMin; q < jMax; q += 1)
                                {
                                    totalA += (F[p - iMin, q - jMin] * I[p - iMin, q - jMin]);
                                    totalB += F[p - iMin, q - jMin];
                                }
                            }

                            // modify value
                            double res = 0.0;
                            if (totalA != 0 || totalB != 0)
                                res = Math.Round(totalA / totalB);
                            for (int k = 0; k < 3; k += 1)
                            {
                                if (res < 0)
                                {
                                    out_img[i, j, k] = 0;
                                }
                                else if (res > 255)
                                {
                                    out_img[i, j, k] = 255;
                                }
                                else
                                {
                                    out_img[i, j, k] = Convert.ToByte(res);
                                }
                            }
                        }
                    }
                    pictureBox2.Image = outputimg.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;


                    Form form = new Form();
                    form.Height = 500;
                    form.Width = 1000;
                    PictureBox picturebox1 = new PictureBox();
                    picturebox1.Location = new Point(0, 0);
                    picturebox1.Height = 500;
                    picturebox1.Width = 500;
                    picturebox1.Image = inputimg.ToBitmap();
                    picturebox1.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox1);
                    PictureBox picturebox2 = new PictureBox();
                    picturebox2.Location = new Point(500, 0);
                    picturebox2.Height = 500;
                    picturebox2.Width = 500;
                    picturebox2.Image = outputimg.ToBitmap();
                    picturebox2.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox2);
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
            





            /*
            Image<Bgr, byte> inputimg = new Image<Bgr, byte>(beforeBitmap);
            Byte[, ,] ori = inputimg.Data;
            int cof = 10;
            Image<Bgr, byte> bilat = inputimg.SmoothBilatral(11,45,45);
            Byte[, ,] blur = bilat.Data;
            Image<Bgr, byte> result;
            imsharpen(bilat.ToBitmap(), 1,out result);
            //imsharpen(inputimg.ToBitmap(), 2, out result);
            
            pictureBox2.Image = bilat.ToBitmap();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;




            Form form = new Form();
            // full screen
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            form.Height = 500;
            form.Width = 1000;
            PictureBox picturebox1 = new PictureBox();
            picturebox1.Location = new Point(0, 0);
            picturebox1.Height = 500;
            picturebox1.Width = 500;
            picturebox1.Image = inputimg.ToBitmap();
            picturebox1.SizeMode = PictureBoxSizeMode.Zoom;
            form.Controls.Add(picturebox1);
            PictureBox picturebox2 = new PictureBox();
            picturebox2.Location = new Point(500,0);
            picturebox2.Height = 500;
            picturebox2.Width = 500;
            picturebox2.Image = result.ToBitmap();
            picturebox2.SizeMode = PictureBoxSizeMode.Zoom;
            form.Controls.Add(picturebox2);
            form.ShowDialog();*/
        }


        private void InnerBoundaryTracing_Btn_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                MessageBox.Show("Coming Soon.");
                return;
                Image<Bgr, byte> inputimg;
                toGray(beforeBitmap, out inputimg);
                Image<Bgr, Byte> image = new Image<Bgr, Byte>(inputimg.Width + 2, inputimg.Height + 2, new Bgr(255, 255, 255));
                // Copy
                Byte[, ,] d = inputimg.Data;
                Byte[, ,] img = image.Data;
                for (int i = 1; i < inputimg.Height + 1; i++)
                    for (int j = 1; j < inputimg.Width + 1; j++)
                        for (int k = 0; k < 3; k++)
                            img[i, j, k] = d[i - 1, j - 1, k];
                // Mark
                int[,] mark = new int[inputimg.Height,inputimg.Width];
                for(int i=0;i<inputimg.Height;i++)
                    for(int j=0;j<inputimg.Width;j++)
                        mark[i,j] = 0;
                for (int i = 1; i < inputimg.Height+1; i++)
                {
                    for (int j = 1; j < inputimg.Width+1; j++)
                    {
                        if (img[i, j, 0] > 200)
                        {
                            mark[i - 1, j - 1] = 1;
                            // Assign Coordinate
                            List<int> row = new List<int>();
                            List<int> col = new List<int>();
                            row.Add(i);
                            col.Add(j);
                            int dir = 7;
                            int p = i;
                            int q = j;
                            //Console.WriteLine("i: "+(p0[0]-1).ToString());
                            //Console.WriteLine("j: "+(p0[1]-1).ToString());

                            do
                            {
                                if (dir % 2 == 0)
                                    dir = (dir + 7) % 8;
                                else
                                    dir = (dir + 6) % 8;




                            } while (true);

                            /*
                            if (img[i, j + 1, 0] > 200)
                            {//right
                                row.Add(i);
                                col.Add(j+1);
                                dir = 0;
                            }else if(img[i - 1, j + 1, 0] > 200)
                            {//upperright
                                row.Add(i-1);
                                col.Add(j+1);
                                dir = 1;
                            }else if(img[i - 1, j, 0] > 200)
                            {//top
                                row.Add(i-1);
                                col.Add(j);
                                dir = 2;
                            }else if(img[i - 1, j - 1, 0] > 200)
                            {//upperleft
                                row.Add(i-1);
                                col.Add(j-1);
                                dir = 3;
                            }else if(img[i, j - 1, 0] > 200)
                            {//left
                                row.Add(i);
                                col.Add(j-1);
                                dir = 4;
                            }else if(img[i + 1, j - 1, 0] > 200)
                            {//lowerleft
                                row.Add(i+1);
                                col.Add(j-1);
                                dir = 5;
                            }else if(img[i + 1, j, 0] > 200)
                            {//bottom
                                row.Add(i+1);
                                col.Add(j);
                                dir = 6;
                            }else if(img[i + 1, j + 1, 0] > 200)
                            {//lowerright
                                row.Add(i+1);
                                col.Add(j+1);
                                dir = 7;
                            }else
                            {// no neighborhood exists
                                dir = 7;
                                continue;
                            }*/
                            return;

                        }
                        //Console.WriteLine(img[i, j, 0]);
                    }
                }
                /*
                Image<Bgr, byte> image = new Image<Bgr, Byte>(beforeBitmap);
                Image<Bgr, byte> gauss = image.SmoothGaussian(3, 3, 34.3, 45.3);
                Filter filter = new Filter();
                Image<Bgr, byte> grayimg = new Image<Bgr, Byte>(filter.imfilter(gauss.ToBitmap(), 16));
                Byte[,,] img = grayimg.Data;
                for (int i = 0; i < grayimg.Height; i++)
                    for (int j = 0; j < grayimg.Width; j++)
                        if (img[i, j, 0] < 100)
                            for (int k = 0; k < 3; k++)
                                img[i, j, k] = 0;
                // inner border tracing
                for (int i = 0; i < grayimg.Height; i++)
                    for (int j = 0; j < grayimg.Width; j++)
                    {
                        if (img[i, j, 0] != 0)
                        {

                        }
                    }

                pictureBox2.Image = grayimg.ToBitmap();
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                */
                //pictureBox2.Image = filter.imfilter(beforeBitmap, 16);
                //pictureBox2.SizeMode =
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        
        /*
        public static Image<Gray, byte> LabelConnectedComponents(Image<Gray, byte> binary, int startLabel)
        {
            Contour<Point> contours = binary.FindContours(
                Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_CCOMP
            );

            int count = startLabel;
            for (Contour<Point> cont = contours;cont != null;cont = cont.HNext)
            {
                CvInvoke.cvDrawContours(
                binary,
                cont,
                new MCvScalar(count),
                new MCvScalar(0),
                2,
                -1,
                Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED,
                new Point(0, 0));
                ++count;
            }
            return binary;
        }*/

        private void Gray_1_project_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Image<Gray, Byte> image = new Image<Gray, Byte>(beforeBitmap);
                Image<Gray, Byte> ori = image.Clone();  // store original one
                Byte[, ,] d = image.Data;
                Image<Gray, byte> Img_Source_Gray = image.Copy();
                // === auto canny detection ===
                // pre-processing
                Img_Source_Gray._SmoothGaussian(3);
                Image<Gray, byte> Img_Otsu_Gray = image.CopyBlank();
                double CannyAccThresh = CvInvoke.cvThreshold(Img_Source_Gray.Ptr, Img_Otsu_Gray.Ptr, 0, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU | Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);
                double CannyThresh = 0.1 * CannyAccThresh;
                Img_Otsu_Gray.Dispose();
                Img_Source_Gray = Img_Source_Gray.Canny(new Gray(CannyThresh), new Gray(CannyAccThresh));
                // Closing Operator
                Img_Source_Gray._Dilate(4);
                Img_Source_Gray._Erode(1);
                // Replace indicated pixel
                Byte[, ,] mark = Img_Source_Gray.Data;
                Image<Gray, Byte> blur = image.SmoothBlur(13, 13);
                Byte[, ,] blurr = blur.Data;
                int height = image.Height;
                int width = image.Width;
                for (int i = 0; i < height; i += 1)
                {
                    for (int j = 0; j < width; j += 1)
                    {
                        bool enable = false;
                        for (int p = -5; p < 5; p += 1)
                        {
                            for (int q = -5; q < 5; q += 1)
                            {
                                if (i + p < 0 || j + q < 0 || i + p >= height || j + q >= width)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (mark[i + p, j + q, 0] > 10)
                                    {
                                        enable = true;
                                        break;
                                    }
                                }
                            }
                            if (enable)
                            {
                                break;
                            }
                        }

                        if (!enable)
                        {
                            for (int k = 0; k < 1; k += 1)
                            {
                                d[i, j, k] = blurr[i, j, k];
                            }
                        }
                    }
                }
                // sharpen - unsharp mask
                Image<Bgr, byte> result;
                imsharpen(image.ToBitmap(), 1, out result);
                // display result in current form
                pictureBox2.Image = image.ToBitmap();
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                // pass to other form
                Form10 input = new Form10();
                input.ParameterImage1 = ori.ToBitmap();
                input.ParameterImage2 = result.ToBitmap();
                input.ParameterImage3 = Img_Source_Gray.ToBitmap();
                //input.ShowDialog();
                input.Show();
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        private void projectdemo2_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form9 input = new Form9();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Image<Bgr, byte> inputimg = new Image<Bgr, byte>(beforeBitmap);
                    Image<Bgr, byte> bilat = inputimg.SmoothBilatral((int)input.Getfilterwidth(), (int)input.Getsigma_c(), (int)input.Getsigma_s());
                    Image<Bgr, byte> result;
                    imsharpen(bilat.ToBitmap(), 1, out result);
                    pictureBox2.Image = result.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    // show outside
                    Form form = new Form();
                    // full screen
                    //form.FormBorderStyle = FormBorderStyle.None;
                    //form.WindowState = FormWindowState.Maximized;
                    form.Height = 500;
                    form.Width = 1020;
                    PictureBox picturebox1 = new PictureBox();
                    picturebox1.Location = new Point(0, 0);
                    picturebox1.Height = 500;
                    picturebox1.Width = 500;
                    picturebox1.Image = inputimg.ToBitmap();
                    picturebox1.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox1);
                    PictureBox picturebox2 = new PictureBox();
                    picturebox2.Location = new Point(500, 0);
                    picturebox2.Height = 500;
                    picturebox2.Width = 500;
                    picturebox2.Image = result.ToBitmap();
                    picturebox2.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox2);
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }
        public static void bilaterfilter(Bitmap beforeBitmap, int width,double sig_c, double sig_s ,out Bitmap bresult){
            Image<Bgr, byte> inputimg = new Image<Bgr, byte>(beforeBitmap);
            Byte[, ,] img = inputimg.Data;
            Image<Bgr, byte> outputimg = new Image<Bgr, byte>(beforeBitmap);
            Byte[, ,] outimg = outputimg.Data;
            double[, ,] cielabimg = new double[inputimg.Height, inputimg.Width, 3];
            double[, ,] cielabresimg = new double[inputimg.Height, inputimg.Width, 3];
            // CIE-to-XYZ-to-LAB Conversion
            double Xn = 0.9515;
            double Yn = 1.0;
            double Zn = 1.0886;
            for (int i = 0; i < inputimg.Height; i += 1)
            {
                for (int j = 0; j < inputimg.Width; j += 1)
                {
                    double R = (double)(img[i, j, 2]) / 255.0;
                    double G = (double)(img[i, j, 1]) / 255.0;
                    double B = (double)(img[i, j, 0]) / 255.0;
                    double X = B * 0.180423 + R * 0.412453 + G * 0.357580;
                    double Y = B * 0.072169 + R * 0.212671 + G * 0.715160;
                    double Z = B * 0.950227 + R * 0.019334 + G * 0.119193;
                    double tmpY = Y / Yn;
                    double tmpX = X / Xn;
                    double tmpZ = Z / Zn;
                    double L = 0.0;
                    if (tmpY > 0.008856)
                        L = 116.0 * Math.Pow(tmpY, 1.0 / 3.0) - 16.0;
                    else
                        L = 903.3 * tmpY;
                    double a1 = 0.0;
                    double a2 = 0.0;
                    double a = 0.0;
                    if (tmpX > 0.008856)
                        a1 = Math.Pow(tmpX, 1.0 / 3.0);
                    else
                        a1 = 7.787 * tmpX + 16.0 / 116.0;
                    if (tmpY > 0.008856)
                        a2 = Math.Pow(tmpY, 1.0 / 3.0);
                    else
                        a2 = 7.787 * tmpY + 16.0 / 116.0;
                    a = 500.0 * (a1 - a2);

                    double b2 = 0.0;
                    double b = 0.0;
                    if (tmpZ > 0.008856)
                        b2 = Math.Pow(tmpZ, 1.0 / 3.0);
                    else
                        b2 = 7.787 * tmpZ + 16.0 / 116.0;
                    b = 200.0 * (a2 - b2);
                    /*
                    Console.WriteLine(L);
                    Console.WriteLine(a);
                    Console.WriteLine(b);*/
                    cielabimg[i, j, 0] = L;
                    cielabimg[i, j, 1] = a;
                    cielabimg[i, j, 2] = b;
                }
            }
            //Pre-compute Gaussian distance weights.
            int w = width;//int w = 11;
            double sigma_c = sig_c;//double sigma_c = 45;
            double sigma_s = sig_s;//double sigma_s = 45;
            double[,] XX = new double[2 * w + 1, 2 * w + 1];
            double[,] YY = new double[2 * w + 1, 2 * w + 1];
            double[,] C = new double[2 * w + 1, 2 * w + 1];
            for (int i = 0; i < (2 * w + 1); i += 1)
            {
                for (int j = 0; j < (2 * w + 1); j += 1)
                {
                    XX[i, j] = j - w;
                }
            }
            for (int i = 0; i < (2 * w + 1); i += 1)
            {
                for (int j = 0; j < (2 * w + 1); j += 1)
                {
                    YY[i, j] = i - w;
                }
            }
            for (int i = 0; i < (2 * w + 1); i += 1)
            {
                for (int j = 0; j < (2 * w + 1); j += 1)
                {
                    C[i, j] = Math.Exp(-1 * (XX[i, j] * XX[i, j] + YY[i, j] * YY[i, j]) / (2 * sigma_c * sigma_c));
                }
            }
            // Rescale range variance (using maximum luminance).
            sigma_s = 100 * sigma_s;

            // Apply Bilateral Filter
            int m = inputimg.Height;
            int n = inputimg.Width;
            double[, ,] I = new double[2 * w + 1, 2 * w + 1, 3];
            double[,] H = new double[2 * w + 1, 2 * w + 1];
            double[,] F = new double[2 * w + 1, 2 * w + 1];

            for (int i = 0; i < m; i += 1)
            {
                for (int j = 0; j < n; j += 1)
                {
                    //Extract local region.
                    int iMin = Math.Max(i - w, 0);
                    int iMax = Math.Min(i + w, m - 1);
                    int jMin = Math.Max(j - w, 0);
                    int jMax = Math.Min(j + w, n - 1);
                    for (int p = iMin; p < iMax; p += 1)
                    {
                        for (int q = jMin; q < jMax; q += 1)
                        {
                            for (int r = 0; r < 3; r += 1)
                                I[p - iMin, q - jMin, r] = cielabimg[p, q, r];
                        }
                    }

                    // Compute Gaussian intensity weights.
                    double dL = cielabimg[i, j, 0];
                    double da = cielabimg[i, j, 1];
                    double db = cielabimg[i, j, 2];
                    for (int p = iMin; p < iMax; p += 1)
                    {
                        for (int q = jMin; q < jMax; q += 1)
                        {
                            dL = I[p - iMin, q - jMin, 0] - dL;
                            da = I[p - iMin, q - jMin, 1] - da;
                            db = I[p - iMin, q - jMin, 2] - db;
                            H[p - iMin, q - jMin] = Math.Exp(-1.0 * ((dL * dL) + (da * da) + (db * db)) / (2 * sigma_s * sigma_s));
                        }
                    }

                    //Calculate bilateral filter response.
                    for (int p = iMin; p < iMax; p += 1)
                    {
                        for (int q = jMin; q < jMax; q += 1)
                        {
                            F[p - iMin, q - jMin] = H[p - iMin, q - jMin] * C[p - i + w + 1, q - j + w + 1];
                        }
                    }


                    double totalA = 0;
                    double totalB = 0;
                    double totalC = 0;
                    double norm_F = 0;
                    for (int p = iMin; p < iMax; p += 1)
                    {
                        for (int q = jMin; q < jMax; q += 1)
                        {
                            totalA += (F[p - iMin, q - jMin] * I[p - iMin, q - jMin, 0]);
                            totalB += (F[p - iMin, q - jMin] * I[p - iMin, q - jMin, 1]);
                            totalC += (F[p - iMin, q - jMin] * I[p - iMin, q - jMin, 2]);
                            norm_F += F[p - iMin, q - jMin];
                        }
                    }

                    // update
                    double resA = Math.Round(totalA / norm_F);
                    double resB = Math.Round(totalB / norm_F);
                    double resC = Math.Round(totalC / norm_F);
                    cielabresimg[i, j, 0] = resA;
                    cielabresimg[i, j, 1] = resB;
                    cielabresimg[i, j, 2] = resC;
                }
            }
            Console.WriteLine("here");
            // LAB-to-XYZ-to-RGB conversion
            for (int i = 0; i < inputimg.Height; i += 1)
            {
                for (int j = 0; j < inputimg.Width; j += 1)
                {
                    double L = cielabresimg[i, j, 0];
                    double a = cielabresimg[i, j, 1];
                    double b = cielabresimg[i, j, 2];
                    double fy = (L + 16.0) / 116.0;
                    double fx = fy + (a / 500.0);
                    double fz = fy - (b / 200.0);
                    double X = 0.0;
                    double Y = 0.0;
                    double Z = 0.0;
                    if (fy > 0.008856)
                        Y = Yn * fy * fy * fy;
                    else
                        Y = (fy - 16.0) / 116.0 * 3 * 0.008865 * 0.008865 * Yn;
                    if (fx > 0.008856)
                        X = Xn * fx * fx * fx;
                    else
                        X = (fx - 16.0) / 116.0 * 3 * 0.008865 * 0.008865 * Xn;
                    if (fy > 0.008856)
                        Z = Zn * fz * fz * fz;
                    else
                        Z = (fz - 16.0) / 116.0 * 3 * 0.008865 * 0.008865 * Zn;
                    double R = 3.240479 * X - 1.537150 * Y - 0.498535 * Z;
                    double G = -0.969256 * X + 1.875992 * Y + 0.041556 * Z;
                    double B = 0.055648 * X - 0.204043 * Y + 1.057311 * Z;
                    R *= 255;
                    G *= 255;
                    B *= 255;
                    if (R > 255)
                        R = 255;
                    else if (R < 0)
                        R = 0;
                    if (G > 255)
                        G = 255;
                    else if (G < 0)
                        G = 0;
                    if (B > 255)
                        B = 255;
                    else if (B < 0)
                        B = 0;
                    if (double.IsNaN(R))
                        R = 0;
                    if (double.IsNaN(G))
                        G = 0;
                    if (double.IsNaN(B))
                        B = 0;
                    //Console.WriteLine(B);
                    outimg[i, j, 0] = Convert.ToByte(B);
                    outimg[i, j, 1] = Convert.ToByte(G);
                    outimg[i, j, 2] = Convert.ToByte(R);
                }
            }
            bresult = outputimg.ToBitmap();
        }

        //Bitmap outputimg = null;
        //bilaterfilter(beforeBitmap, (int)input.Getfilterwidth(), (int)input.Getsigma_c(), input.Getsigma_s(), out outputimg);

        private void DofColor_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form9 input = new Form9();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(beforeBitmap);
                    Byte[, ,] data = img.Data;
                    Image<Bgr, byte> outputimg = new Image<Bgr, byte>(beforeBitmap);
                    Byte[, ,] outimg = outputimg.Data;
                    outputimg = img.SmoothBilatral((int)input.Getfilterwidth(), (int)input.Getsigma_c(), (int)input.Getsigma_s());
                    //outputimg._SmoothGaussian(3); // preprocessing

                    // SHARPENING
                    // separate channel
                    double[, ,] filteredBGR = new double[img.Height, img.Width, 3];
                    //double[, ,] newBGR = new double[img.Height, img.Width, 3];
                    for (int i = 0; i < img.Height; i += 1)
                        for (int j = 0; j < img.Width; j += 1)
                            for (int k = 0; k < 3; k += 1)
                                filteredBGR[i, j, k] = outimg[i, j, k];
                    // mask
                    double[,] mask = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };
                    for (int i = 1; i < img.Height - 1; i += 1)
                        for (int j = 1; j < img.Width - 1; j += 1)
                            for (int k = 0; k < 3; k += 1)
                            {
                                double val = 0;
                                for (int m = 0; m < 3; m++)
                                {
                                    for (int n = 0; n < 3; n++)
                                    {
                                        val += (filteredBGR[i + m - 1, j + n - 1,k] * mask[m, n]);
                                        /*if (val < 0)
                                            val = 0;
                                        else if (val > 255)
                                            val = 255;*/
                                    }
                                }
                                //newBGR[i, j, k] = val;
                                double fen = outimg[i, j, k];
                                fen -= val;
                                if (fen > 255)
                                    fen = 255;
                                else if (fen < 0)
                                    fen = 0;
                                outimg[i, j, k] = Convert.ToByte(fen);
                            }
                    //outputimg = outputimg.SmoothBilatral((int)input.Getfilterwidth(), (int)input.Getsigma_c(), (int)input.Getsigma_s());
                    pictureBox2.Image = outputimg.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    // show outside
                    Form form = new Form();
                    // full screen
                    //form.FormBorderStyle = FormBorderStyle.None;
                    //form.WindowState = FormWindowState.Maximized;
                    form.Height = 500;
                    form.Width = 1020;
                    PictureBox picturebox1 = new PictureBox();
                    picturebox1.Location = new Point(0, 0);
                    picturebox1.Height = 500;
                    picturebox1.Width = 500;
                    picturebox1.Image = img.ToBitmap();
                    picturebox1.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox1);
                    PictureBox picturebox2 = new PictureBox();
                    picturebox2.Location = new Point(500, 0);
                    picturebox2.Height = 500;
                    picturebox2.Width = 500;
                    picturebox2.Image = outputimg.ToBitmap();
                    picturebox2.SizeMode = PictureBoxSizeMode.Zoom;
                    form.Controls.Add(picturebox2);
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }  


        }

        private void proje_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form9 inputx = new Form9();
                DialogResult drx = inputx.ShowDialog();
                if (drx == DialogResult.OK)
                {
                    Image<Gray, Byte> image = new Image<Gray, Byte>(beforeBitmap);
                    Image<Gray, Byte> ori = image.Clone();  // store original one
                    Byte[, ,] d = image.Data;
                    Image<Gray, byte> Img_Source_Gray = image.Copy();
                    // === auto canny detection ===
                    // pre-processing
                    //Img_Source_Gray._SmoothGaussian(3);
                    //Img_Source_Gray = Img_Source_Gray.SmoothBilatral(9, 45, 45);
                    Img_Source_Gray = Img_Source_Gray.SmoothBilatral((int)inputx.Getfilterwidth(), (int)inputx.Getsigma_c(), (int)inputx.Getsigma_s());
                    ; Image<Gray, byte> Img_Otsu_Gray = image.CopyBlank();
                    double CannyAccThresh = CvInvoke.cvThreshold(Img_Source_Gray.Ptr, Img_Otsu_Gray.Ptr, 0, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU | Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);
                    Console.WriteLine(CannyAccThresh);
                    double CannyThresh = 0.1 * CannyAccThresh;
                    Img_Otsu_Gray.Dispose();
                    Img_Source_Gray = Img_Source_Gray.Canny(new Gray(CannyThresh), new Gray(CannyAccThresh));
                    // Closing Operator
                    Img_Source_Gray._Dilate(4);
                    Img_Source_Gray._Erode(1);
                    // Replace indicated pixel
                    Byte[, ,] mark = Img_Source_Gray.Data;
                    Image<Gray, Byte> blur = image.SmoothBlur(13, 13);
                    Byte[, ,] blurr = blur.Data;
                    int height = image.Height;
                    int width = image.Width;
                    for (int i = 0; i < height; i += 1)
                    {
                        for (int j = 0; j < width; j += 1)
                        {
                            bool enable = false;
                            for (int p = -5; p < 5; p += 1)
                            {
                                for (int q = -5; q < 5; q += 1)
                                {
                                    if (i + p < 0 || j + q < 0 || i + p >= height || j + q >= width)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (mark[i + p, j + q, 0] > 10)
                                        {
                                            enable = true;
                                            break;
                                        }
                                    }
                                }
                                if (enable)
                                {
                                    break;
                                }
                            }

                            if (!enable)
                            {
                                for (int k = 0; k < 1; k += 1)
                                {
                                    d[i, j, k] = blurr[i, j, k];
                                }
                            }
                        }
                    }
                    // sharpen - unsharp mask
                    Image<Bgr, byte> result;
                    imsharpen(image.ToBitmap(), 1, out result);
                    // display result in current form
                    pictureBox2.Image = image.ToBitmap();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    // pass to other form
                    Form10 input = new Form10();
                    input.ParameterImage1 = ori.ToBitmap();
                    input.ParameterImage2 = result.ToBitmap();
                    input.ParameterImage3 = Img_Source_Gray.ToBitmap();
                    //input.ShowDialog();
                    input.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        bool IsGreyScale(Bitmap YourCurrentBitmap)
        {
            Color c;
            for (int i = 0; i < YourCurrentBitmap.Width; i++)
                for (int j = 0; j < YourCurrentBitmap.Height; j++)
                {
                    c = YourCurrentBitmap.GetPixel(i, j);
                    if (!(c.R == c.G && c.R == c.B))
                    {
                        return false;
                    }
                }
            return true;
        }
        private void videomode_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form11 input = new Form11();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    double iteration = input.GetIteration();
                    double interval = input.GetInterval();
                    //int incrfwidth = (int)input.Getfilterwidth();
                    int incrsigma_c = (int)input.Getsigma_c();
                    int incrsigma_d = (int)input.Getsigma_s();
                    //int startfwidth = 3;
                    int fwidth = (int)input.Getfilterwidth();
                    int startsigma_c = 1;
                    int startsigma_d = 1;
                    bool isGreyScale = IsGreyScale(beforeBitmap);
                    for (int i = 0; i < iteration; i++)
                    {
                        Image<Bgr, byte> inputimg = new Image<Bgr, byte>(beforeBitmap);
                        Image<Bgr, byte> bilat = inputimg.SmoothBilatral(fwidth, startsigma_c, startsigma_d);
                        Image<Bgr, byte> result;
                        if (isGreyScale)
                        {
                            imsharpen(bilat.ToBitmap(), 1, out result);
                        }
                        else
                        {
                            result = bilat;
                        }
                        pictureBox2.Image = result.ToBitmap();
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox2.Refresh();
                        label9.Text = "Iter. "+i.ToString() + "    Filter Width: "+ fwidth.ToString() + "    SigmaColor: " + startsigma_c.ToString() + "    SigmaSpace: " + startsigma_d.ToString();
                        label9.Refresh();
                        //startfwidth += incrfwidth;
                        startsigma_c += incrsigma_c;
                        startsigma_d += incrsigma_d;
                        System.Threading.Thread.Sleep((int)interval);
                    }
                    label9.Text = "...";
                }
            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
           
        }

        public struct coord
        {
            public double x;
            public double y;
        }  

        private void resizeBtn_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                Form12 input = new Form12();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    afterBitmap = beforeBitmap;
                    pictureBox2.Image = afterBitmap;
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;


                    Image<Bgr, byte> im = new Image<Bgr, byte>(afterBitmap);
                    Byte[, ,] data2 = im.Data;

                    Image<Bgr, byte> img = new Image<Bgr, byte>(input.GetHeight(), input.GetWidth());
                    Byte[, ,] data = img.Data;

                    coord[,] tmp = new coord[afterBitmap.Height, afterBitmap.Width];
                    for (int i = 0; i < afterBitmap.Height; i++)
                    {
                        for (int j = 0; j < afterBitmap.Width; j++)
                        {
                            tmp[i, j].y = i / (double)afterBitmap.Height;
                            tmp[i, j].x = j / (double)afterBitmap.Width;
                        }
                    }
                    coord[,] ori = new coord[input.GetHeight(), input.GetWidth()];
                    for (int i = 0; i < input.GetHeight(); i++)
                    {
                        for (int j = 0; j < input.GetWidth(); j++)
                        {
                            ori[i, j].y = i / (double)input.GetHeight();
                            ori[i, j].x = j / (double)input.GetWidth();
                        }
                    }
                    int cura = 0;
                    int curb = 0;
                    for (int i = 0; i < input.GetHeight(); i++)
                    {
                        for (int j = 0; j < input.GetWidth(); j++)
                        {
                            for (int a = cura; a < afterBitmap.Height; a++, cura++)
                            {
                                bool xflag = false;

                                for (int b = curb; b < afterBitmap.Width; b++, curb++)
                                {
                                    if (tmp[a, b].x >= ori[i, j].x && tmp[a, b].y >= ori[i, j].y)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            data[i, j, k] += data2[a, b, k];
                                        }
                                        xflag = true;
                                        break;
                                    }

                                }
                                if (xflag == true)
                                    break;
                                else
                                    curb = 0;

                            }
                        }
                    }
                    pictureBox2.Image = img.ToBitmap();
                }

            }
            else
            {
                MessageBox.Show("Please select an image to continue...");
            }
        }

        
        
    }

}
