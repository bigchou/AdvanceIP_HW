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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private int mode = 1;// 1 for Detect Green, 2 for Auto

        public int GetMode()
        {
            return mode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = 2;
        }
    }
}
