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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private string Msg = "1";

        private void button1_Click(object sender, EventArgs e)
        {
            Msg = textBox1.Text;
        }
        public double GetCof()
        {
            double defaultvalue = 1.0; // (If K = 1 Unsharp, If K > 1 Highboost)
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
