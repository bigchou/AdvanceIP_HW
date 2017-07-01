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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        private int mode = 1;
        public int GetMode()
        {
            return mode;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = 1;//Laplace Operator for 4-Nbrhd
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mode = 2;//Laplace Operator for 8-Nbrhd
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = 3;//Prewitt_horizontal
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mode = 4;//Prewitt_oblique
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            mode = 5;//Prewitt_vertical
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            mode = 6;//Sobel_horizontal
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            mode = 7;//Sobel_oblique
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            mode = 8;//Sobel_vertical
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            mode = 9;//Kirsch_horizontal
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            mode = 10;//Kirsch_oblique
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            mode = 11;//Kirsch_vertical
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            mode = 12;//Averaging filters
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            mode = 13;//generalized weighted smoothing filter
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            mode = 14;//Maximum filter
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            mode = 15;//Rotating_Mask
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            mode = 16;//Laplace_of_Gaussian
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            mode = 17;//3×3 Prewitt kernels in 8 compass directions
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            mode = 18;//Sobel Operator used as a detector of horizontality and verticality of edges
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            mode = 19;//Median Filter
        }



        

        

        
    }
}
