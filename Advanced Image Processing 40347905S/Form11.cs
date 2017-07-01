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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        private string filterwidth = "1";
        private string sigma_s = "1";
        private string sigma_c = "1";
        private string iteration = "1";
        private string interval = "1";
        public double GetInterval()
        {
            double defaultvalue = 1.0;
            double minterval;
            if (double.TryParse(interval, out minterval))
            {
                return minterval;
            }
            else
            {
                return defaultvalue;
            }
        }
        public double GetIteration()
        {
            double defaultvalue = 1.0;
            double dur;
            if (double.TryParse(iteration, out dur))
            {
                return dur;
            }
            else
            {
                return defaultvalue;
            }
        }
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
            iteration = textBox1.Text;
            interval = textBox2.Text;
            filterwidth = textBox3.Text;
            sigma_c = textBox4.Text;
            sigma_s = textBox5.Text;
            
            Console.WriteLine("width: " + filterwidth.ToString());
            Console.WriteLine("sigma_c: " + sigma_c.ToString());
            Console.WriteLine("sigma_s: " + sigma_s.ToString());
            Console.WriteLine("iteration: " + iteration.ToString());
            Console.WriteLine("interval: " + interval.ToString());
        }
    }
}
