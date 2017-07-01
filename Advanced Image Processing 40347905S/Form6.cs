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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private int mode = 1;// 1: hmin , 2. without hmin

        public int GetMode()
        {
            return mode;
        }

        private void h_min_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void without_hmin_Click(object sender, EventArgs e)
        {
            mode = 2;
        }
    }
}
