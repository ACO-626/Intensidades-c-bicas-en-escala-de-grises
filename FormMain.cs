using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace TemasSelectosdeProgramacióneImagenes
{
    public partial class FormMain : Form
    {
        Image<Bgr, Byte> My_Image;
        Image<Bgr, Byte> DemoImage;
        int y = 0;
        double ygraf = 0;
     

        public FormMain()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                My_Image = new Image<Bgr, byte>(Openfile.FileName);
                Image<Gray, Byte> My_gray = My_Image.Convert<Gray, byte>();
                Image<Bgr, Byte> My_Demo = My_Image.Convert<Bgr, byte>();

                pictureBox1.Image = My_gray.ToBitmap();
                pictureBox2.Image = My_gray.ToBitmap();
                pictureBoxDemo.Image = My_Demo.ToBitmap();

                xtres.Text = "0";
                xDos.Text = "0";
                xUno.Text = "1";
                xZero.Text = "0";

                btnTransformar.Enabled = true;
                grafica.Visible = true;
                graficar();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                   Image<Gray, byte> My_gray2 = My_Image.Convert<Gray, byte>();
                    for (int i=0;i<My_Image.Height;i++)
                    {
                        for(int j=0;j<My_Image.Width; j++)
                        {
                        //My_gray2[i, j] = new Gray(nuevogris(float.Parse(xtres.Text),float.Parse(xDos.Text),(int)My_gray2[i,j].Intensity));
                        My_gray2[i, j] = new Gray(nuevogris((int)My_gray2[i, j].Intensity));
                        }
                       
                    }
                    pictureBox2.Image = My_gray2.ToBitmap();
                    graficar();

                       
               
            }catch(IOException)
            {
                MessageBox.Show("Error al cargar");
            }

        }
        private int nuevogris(int i)
        {
            y = (int)(Math.Pow(i, 3) * float.Parse(xtres.Text) + Math.Pow(i, 2) * float.Parse(xDos.Text) + Math.Pow(i, 1) * float.Parse(xUno.Text) + float.Parse(xZero.Text));

            if (y>=255)
            {
                y = 255;
            }
            if(y<=0)
            {
                y = 0;
            }
            return y;
        }

        private void graficar()
        {
            foreach(var series in grafica.Series)
            {
                series.Points.Clear();
            }

            for(int i=0;i<255;i++)
            {
                //grafica.Visible = true;
                ygraf = Math.Pow(i, 3) * float.Parse(xtres.Text) + Math.Pow(i, 2) * float.Parse(xDos.Text) + Math.Pow(i, 1) * float.Parse(xUno.Text) + float.Parse(xZero.Text);
                grafica.Series["f"].Points.AddXY(i, ygraf);
                /*if(ygraf>255|ygraf<0)
                {
                    
                }else
                {
                    grafica.Series["f"].Points.AddXY(i, ygraf);
                }*/
            }
        }

        private void xtres_TextChanged(object sender, EventArgs e)
        {
            if (xtres.Text == "") 
                xtres.Text = "0";
            
        }

        private void xDos_TextChanged(object sender, EventArgs e)
        {
            if (xDos.Text == "")
                xDos.Text = "0";
           
           
        }

        private void xUno_TextChanged(object sender, EventArgs e)
        {
            if (xUno.Text == "")
                xUno.Text = "1";
        }

        private void xZero_TextChanged(object sender, EventArgs e)
        {
            if (xZero.Text == "")
                xZero.Text = "0";
        }
    }
}
