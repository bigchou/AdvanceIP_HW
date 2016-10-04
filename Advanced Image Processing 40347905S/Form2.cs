using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Advanced_Image_Processing_40347905S
{
    public partial class Form2 : Form
    {
        public Form2(Bitmap pic)
        {
            InitializeComponent();
            pictureBox2.Image = pic;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void save_img_Click_Click(object sender, EventArgs e)
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
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
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
    }
}
