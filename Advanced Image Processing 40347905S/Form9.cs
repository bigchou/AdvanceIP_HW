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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        private string filterwidth = "1";
        private string sigma_s = "1";
        private string sigma_c = "1";
        public double Getfilterwidth()
        {
            double defaultvalue = 1.0; // (If K = 1 Unsharp, If K > 1 Highboost)
            double width;
            if (double.TryParse(filterwidth, out width))
            {
                return width;
            }
            else
            {
                return defaultvalue;
            }
        }
        public double Getsigma_s()
        {
            double defaultvalue = 1.0; // (If K = 1 Unsharp, If K > 1 Highboost)
            double sigma;
            if (double.TryParse(sigma_s, out sigma))
            {
                return sigma;
            }
            else
            {
                return defaultvalue;
            }
        }
        public double Getsigma_c()
        {
            double defaultvalue = 1.0; // (If K = 1 Unsharp, If K > 1 Highboost)
            double sigma;
            if (double.TryParse(sigma_c, out sigma))
            {
                return sigma;
            }
            else
            {
                return defaultvalue;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            filterwidth = textBox1.Text;
            sigma_c = textBox2.Text;
            sigma_s = textBox3.Text;
            Console.WriteLine("width: " + filterwidth.ToString());
            Console.WriteLine("sigma_c: " + sigma_c.ToString());
            Console.WriteLine("sigma_s: " + sigma_s.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // color
            filterwidth = "25";
            sigma_c = "80";
            sigma_s = "100";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Grey
            filterwidth = "25";
            sigma_c = "50";
            sigma_s = "40";
        }
    }
}
