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
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
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
                            this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 2:
                            this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            this.pictureBox2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }
                    fs.Close();
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
    }
}
