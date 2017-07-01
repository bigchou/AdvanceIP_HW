using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;

namespace Advanced_Image_Processing_40347905S
{
    class Filter
    {
        public Bitmap imfilter(Bitmap beforeBitmap, int mode)
        {
            Image<Bgr, byte> grayimg = new Image<Bgr, byte>(beforeBitmap);
            Byte[, ,] img = grayimg.Data; // pass by reference to 3d matrix
            // toGray
            for (int i = 0; i < grayimg.Rows; i++)
            {
                for (int j = 0; j < grayimg.Cols; j++)
                {
                    int val = 0;
                    for (int k = 0; k < 3; k++)
                        val += img[i, j, k];
                    byte result = Convert.ToByte(val / 3);
                    for (int k = 0; k < 3; k++)
                        img[i, j, k] = result;
                }
            }

            //Image<Bgr, byte> afterBitmap = new Image<Bgr, byte>(beforeBitmap);
            Image<Bgr, Byte> afterBitmap = new Image<Bgr, Byte>(grayimg.Width-2, grayimg.Height-2, new Bgr(255, 255, 255));
            Byte[, ,] resdata = afterBitmap.Data; // pass by reference to 3d matrix
            double[,] mask = new double[3, 3];
            if (mode == 1)
            {//Laplace Operator for 4-Nbrhd
                mask[0, 0] = mask[0, 2] = mask[2, 0] = mask[2, 2] = 0;
                mask[0, 1] = mask[1, 0] = mask[1, 2] = mask[2, 1] = 1;
                mask[1, 1] = -4;
            }
            else if (mode == 2)
            {//Laplace Operator for 8-Nbrhd
                for (int m = 0; m < 3; m++)
                    for (int n = 0; n < 3; n++)
                        mask[m, n] = 1;
                mask[1, 1] = -8;
            }
            else if (mode == 3)
            {//Prewitt_horizontal
                
                for (int m = 2; m >= 0; m--)
                    for (int n = 0; n < 3; n++)
                        mask[m, n] = m - 1;
            }
            else if (mode == 4)
            {//Prewitt_oblique
                for(int m=0;m<3;m++)
                    for (int n = 0; n < 3; n++)
                    {
                        if (m == n)
                            mask[m, n] = 0;
                        else if (m < n)
                            mask[m, n] = 1;
                        else
                            mask[m, n] = -1;
                    }
            }
            else if (mode == 5)
            {//Prewitt_vertical
                for (int m = 0; m < 3; m++)
                    for (int n = 0; n < 3; n++)
                        mask[m, n] = n - 1;
            }
            else if (mode == 6)
            {//Sobel_horizontal
                for(int m=0;m<3;m++){
                    mask[0, m] = m + 1;
                    mask[1, m] = 0;
                    mask[2, m] = -1 * (m + 1);
                }
                mask[0, 2] = 1;
                mask[2, 2] = -1;
            }
            else if (mode == 7)
            {//Sobel_oblique
                for (int m = 0; m < 3; m++)
                    for (int n = 0; n < 3; n++)
                    {
                        mask[m, n] = n-m;
                    }
            }
            else if (mode == 8)
            {//Sobel_vertical
                for (int m = 0; m < 3; m++)
                {
                    mask[m, 0] = -1 * (m + 1);
                    mask[m, 1] = 0;
                    mask[m, 2] = m+1;
                }
                mask[2, 0] = -1;
                mask[2, 2] = 1;
            }
            else if (mode == 9)
            {//Kirsch_horizontal
                for (int m = 0; m < 3; m++)
                {
                    mask[0, m] = 3;
                    mask[1, m] = 3;
                    mask[2, m] = -5;
                }
                mask[1, 1] = 0;
            }
            else if (mode == 10)
            {
                for (int m = 0; m < 3; m++)
                    for (int n = 0; n < 3; n++)
                    {
                        if (m == n)
                            mask[m, n] = 3;
                        else if (m < n)
                            mask[m, n] = 3;
                        else
                            mask[m, n] = -5;
                    }
                mask[1, 1] = 0;
            }
            else if (mode == 11)
            {//Kirsch_vertical
                for (int m = 0; m < 3; m++)
                {
                    mask[m, 0] = -5;
                    mask[m, 1] = 3;
                    mask[m, 2] = 3;
                }
                mask[1, 1] = 0;
            }
            else if (mode == 12)
            {//Averaging filters
                for (int m = 0; m < 3; m++)
                    for (int n = 0; n < 3; n++)
                        mask[m, n] = (1.0) / (9.0);
            }
            else if (mode == 13)
            {//generalized weighted smoothing filter
                mask[0, 0] = mask[0, 2] = mask[2, 0] = mask[2, 2] = (1.0) / (16.0);
                mask[0, 1] = mask[1, 0] = mask[1, 2] = mask[2, 1] = (2.0) / (16.0);
                mask[1, 1] = (4.0) / (16.0);
            }
            else if (mode == 14)
            {//Maximum filter
                for (int i = 1; i < grayimg.Height - 1; i++)
                    for (int j = 1; j < grayimg.Width - 1; j++)
                    {
                        double max = 0;
                        for (int m = 0; m < 3; m++)
                            for (int n = 0; n < 3; n++)
                                if (img[i + m - 1, j + n - 1, 0] > max)
                                    max = img[i + m - 1, j + n - 1, 0];
                        if (max < 0)
                            max = 0;
                        else if (max > 255)
                            max = 255;
                        for (int k = 0; k < 3; k++)
                            resdata[i - 1, j - 1, k] = Convert.ToByte(max);
                    }
                return afterBitmap.ToBitmap();
            }
            else if (mode == 15)
            {//Rotating_Mask
                for(int i=2;i<grayimg.Height-2;i++)
                    for(int j=2;j<grayimg.Width-2;j++)
                    {
                        //Rotating
                        double[] avgs = new double[9];
                        double[] vars = new double[9];
                        for(int p=0;p<3;p++)
                            for (int q = 0; q < 3; q++)
                            {
                                // avg.
                                double sum = 0.0;
                                for (int m = 2; m >= 0; m--)
                                    for (int n = 2; n >= 0; n--)
                                    {
                                        sum += img[i - m +p, j - n +q, 0];
                                    }
                                double avg = sum / 9.0;
                                avgs[3 * p + q] = avg;
                                // var.
                                sum = 0.0;
                                for (int m = 2; m >= 0; m--)
                                    for (int n = 2; n >= 0; n--)
                                    {
                                        double val = (double)(img[i - m + p, j - n + q, 0]) - avg;
                                        sum += (val*val);
                                    }
                                double var = sum / 9.0;
                                vars[3 * p + q] = var;
                            }
                        // find min
                        int minind = 0;
                        double small = vars[0];
                        for(int x=0;x<9;x++)
                            if (small > vars[x])
                            {
                                small = vars[x];
                                minind = x;
                            }
                        int v = (int)Math.Ceiling(avgs[minind]);
                        //Console.WriteLine(v);
                        if (v > 255)
                            v = 255;
                        else if (v < 0)
                            v = 0;
                        for (int k = 0; k < 3; k++)
                            resdata[i - 2, j - 2, k] = Convert.ToByte(v);
                    }
                return afterBitmap.ToBitmap();
            }
            else if (mode == 16)
            {//Laplace_of_Gaussian
                double[,] mask2 = { { 0, 0, -1, 0, 0 }, { 0, -1, -2, -1, 0 }, { -1, -2, 16, -2, -1 }, { 0, 0, -1, 0, 0 }, { 0, -1, -2, -1, 0 } };
                for (int i = 2; i < grayimg.Height - 2; i++)
                    for (int j = 2; j < grayimg.Width - 2; j++)
                    {
                        double val = 0;
                        for (int m = 0; m < 5; m++)
                            for (int n = 0; n < 5; n++)
                            {
                                val += (img[i + m - 2, j + n - 2, 0] * mask2[m, n]);
                            }
                        if (val < 0)
                            val = 0;
                        else if (val > 255)
                            val = 255;
                        for (int k = 0; k < 3; k++)
                            resdata[i - 1, j - 1, k] = Convert.ToByte(val);
                    }
                return afterBitmap.ToBitmap();
            }
            else if (mode == 17)
            {//3×3 Prewitt kernels in 8 compass directions
                double[, ,] filter = {{{-1, 0, 1},
                                      {-1,0,1},
                                      {-1,0,1}},
                                      {{-1,-1,0},
                                      {-1,0,1},
                                      {0,1,1}},
                                      {{-1, -1, -1},
                                      {0,0,0},
                                      {1,1,1}},
                                      {{0,-1,-1},
                                      {1,0,-1},
                                      {1,1,0}},
                                      {{1, 0, -1},
                                      {1,0,-1},
                                      {1,0,-1}},
                                      {{1, 1, 0},
                                      {1,0,-1},
                                      {0,-1,-1}},
                                      {{1, 1, 1},
                                      {0,0,0},
                                      {-1,-1,-1}},
                                      {{0, 1, 1},
                                      {-1,0,1},
                                      {-1,-1,0}}
                                    };

                for (int i = 1; i < grayimg.Height - 1; i++)
                    for (int j = 1; j < grayimg.Width - 1; j++)
                    {
                        double big = -3000;
                        for (int x = 0; x < 8; x++)
                        {
                            double val = 0;
                            for (int m = 0; m < 3; m++)
                                for (int n = 0; n < 3; n++)
                                {
                                    val += (img[i + m - 1, j + n - 1, 0] * filter[x, m, n]);
                                }
                            if (val > big)
                                big = val;
                        }

                        if (big < 0)
                            big = 0;
                        else if (big > 255)
                            big = 255;
                        for (int k = 0; k < 3; k++)
                            resdata[i - 1, j - 1, k] = Convert.ToByte(big);
                    }
                return afterBitmap.ToBitmap();
            }
            else if (mode == 18)
            {//Sobel Operator used as a detector of horizontality and verticality of edges
                double[, ,] filter = {{{1, 2, 1},
                                      {0,0,0},
                                      {-1,-2,-1}},
                                      {{-1,0,1},
                                      {-2,0,2},
                                      {-1,0,1}}
                                     };
                for (int i = 1; i < grayimg.Height - 1; i++)
                    for (int j = 1; j < grayimg.Width - 1; j++)
                    {
                        double x = 0, y = 0;
                        double val = 0;
                        for (int m = 0; m < 3; m++)
                            for (int n = 0; n < 3; n++)
                            {
                                val += (img[i + m - 1, j + n - 1, 0] * filter[0, m, n]);
                            }
                        x = val;
                        val = 0;
                        for (int m = 0; m < 3; m++)
                            for (int n = 0; n < 3; n++)
                            {
                                val += (img[i + m - 1, j + n - 1, 0] * filter[1, m, n]);
                            }
                        y = val;
                        val = Math.Sqrt(x * x + y * y);
                        if (val < 0)
                            val = 0;
                        else if (val > 255)
                            val = 255;
                        for (int k = 0; k < 3; k++)
                            resdata[i - 1, j - 1, k] = Convert.ToByte(val);
                    }
                return afterBitmap.ToBitmap();
            }
            else if (mode == 19)
            {//Median Filter
                double[] tmp = new double[9];
                for (int i = 1; i < grayimg.Height - 1; i++)
                    for (int j = 1; j < grayimg.Width - 1; j++)
                    {
                        for (int m = 0; m < 3; m++)
                            for (int n = 0; n < 3; n++)
                                tmp[m * 3 + n] = img[i + m - 1, j + n - 1, 0];
                        Array.Sort(tmp);
                        for (int k = 0; k < 3; k++)
                            resdata[i - 1, j - 1, k] = Convert.ToByte(tmp[4]);
                    }
                return afterBitmap.ToBitmap();
            }
            // filtering
            for (int i = 1; i < grayimg.Height - 1; i++)
                for (int j = 1; j < grayimg.Width - 1; j++)
                {
                    double val = 0;
                    for (int m = 0; m < 3; m++)
                        for (int n = 0; n < 3; n++)
                        {
                            val += (img[i+m-1,j+n-1,0]*mask[m,n]);
                        }
                    if (val < 0)
                        val = 0;
                    else if (val > 255)
                        val = 255;
                    for (int k = 0; k < 3; k++)
                        resdata[i-1, j-1, k] = Convert.ToByte(val);
                }
            return afterBitmap.ToBitmap();
        }
    }
    
}
