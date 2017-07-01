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
    public partial class Form10 : Form
    {
        public Bitmap ParameterImage1 = null;
        public Bitmap ParameterImage2 = null;
        public Bitmap ParameterImage3 = null;

        public Form10()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = this.ParameterImage1;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = this.ParameterImage2;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = this.ParameterImage3;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
