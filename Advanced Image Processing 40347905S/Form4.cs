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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private int mode = 1;// 1 for RGB, 2 for XYZ
        private void button1_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        public int GetMode()
        {
            return mode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mode = 4;
        }
    }
}
