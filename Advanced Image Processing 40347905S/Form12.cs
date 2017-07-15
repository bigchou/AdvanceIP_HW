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
    public partial class Form12 : Form
    {
        private string height = "100";
        private string width = "100";

        public Form12()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public int GetHeight()
        {
            int defaultvalue = 100;
            int return_value;
            if (int.TryParse(height, out return_value))
            {
                return return_value;
            }
            else
            {
                return defaultvalue;
            }
        }
        public int GetWidth()
        {
            int defaultvalue = 100;
            int return_value;
            if (int.TryParse(width, out return_value))
            {
                return return_value;
            }
            else
            {
                return defaultvalue;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           height = textBox1.Text;
           width = textBox2.Text;

        }
    }
}
