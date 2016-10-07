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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private string Msg;
        private int mode = 1;// 1 for boxmuller, 2 for center limit theorem


        private void button1_Click(object sender, EventArgs e)
        {
            Msg = textBox1.Text;
            mode = 1;
            Console.WriteLine("BM");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Msg = textBox1.Text;
            mode = 2;
            Console.WriteLine("CLT");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Msg = textBox1.Text;
            mode = 3;
            Console.WriteLine("GA");
        }
        public int GetMode()
        {
            return mode;
        }
        public double GetSigma()
        {
            double defaultvalue = 60.0;
            double sigma;
            if (double.TryParse(Msg, out sigma))
            {
                return sigma;
            }
            else
            {
                return defaultvalue;
            }
        }

        

        
    }
}
