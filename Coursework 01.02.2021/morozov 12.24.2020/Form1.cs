using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace morozov_12._24._2020
{
    public partial class Form1 : Form
    {
        int d = 0;
        int x, y;
        int xOld, yOld;
        int xLine, yLine;
        double tInt;
        int x0, y0;
        double alpha = 0, alphaLine;
        int R0, R;
        double t = 0;
        double w, wl;
        double μ, g;
        Pen pen = new Pen(Color.Gray, 3);
        Point[] p;

        int count = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Point[4];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        double rt, wt, rtOld;

        private void timer3_Tick(object sender, EventArgs e)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            count++;
            wt = w + wl * t;
            if (μ * g < Math.Pow(wt * R0, 2) / R0)
            {
                rt += Math.Pow(wt * R0, 2) / R0 + R0 / (1 - Math.Pow(wt, 2) * Math.Pow(t, 2) / 2);
                alpha += ((Math.PI * wt) - 0.03) / 4;
            }
            else
            {
                alpha += (Math.PI * wt) / 4;
            }
            alphaLine += (Math.PI * wt) / 4;
            x = x0 + (int)(rt * Math.Sin(alpha));
            y = y0 - (int)(rt * Math.Cos(alpha));
            if (count % 4 == 1)
            {
                p[(count % 4) - 1].X = x;
                p[(count % 4) - 1].Y = y;
            }
            else if (count % 4 == 2)
            {
                p[(count % 4) - 1].X = x;
                p[(count % 4) - 1].Y = y;
            }
            else if (count % 4 == 3)
            {
                p[(count % 4) - 1].X = x;
                p[(count % 4) - 1].Y = y;
            }
            else if (count % 4 == 0)
            {
                p[3].X = x;
                p[3].Y = y;
                if (Math.Sqrt(Math.Pow(x - pictureBox1.Width / 2, 2) + Math.Pow(y - pictureBox1.Height / 2, 2)) >= d || wt < 0)
                {
                    timer3.Stop();
                }
                else
                {
                    gr.DrawCurve(pen, p);
                    draw_circle(Pens.White, xOld, yOld, R);
                    gr.DrawLine(Pens.White, x0, y0, xLine, yLine);
                    xLine = x0 + (int)(d * Math.Sin(alphaLine));
                    yLine = y0 - (int)(d * Math.Cos(alphaLine));
                    gr.DrawLine(Pens.Black, x0, y0, xLine, yLine);
                    draw_circle(Pens.Black, x, y, R);
                    xOld = x;
                    yOld = y;
                    t += tInt / 4;
                    rtOld = rt;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }



        private void button4_Click_1(object sender, EventArgs e)
        {
            timer2.Stop();
            timer3.Stop();
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            d = Convert.ToInt32(textBox1.Text);
            R0 = Convert.ToInt32(textBox2.Text);
            R = Convert.ToInt32(textBox3.Text);
            w = Convert.ToDouble(textBox4.Text);
            wl = Convert.ToDouble(textBox5.Text);
            μ = Convert.ToDouble(textBox7.Text);
            g = Convert.ToDouble(textBox8.Text);
            x0 = pictureBox1.Width / 2;
            y0 = pictureBox1.Height / 2;
            draw_circle(Pens.Black, x0, y0, d);
            alpha = 0;
            alphaLine = 0;
            t = 0;
            tInt = Convert.ToDouble(textBox6.Text) * 1000;
            tInt = tInt / 1000;
            rtOld = R0 / (1 - Math.Pow(wt, 2) * Math.Pow(t, 2) / 2);
            rt = R0 / (1 - Math.Pow(wt, 2) * Math.Pow(t, 2) / 2);
            timer2.Interval = 1000/Convert.ToInt32(textBox9.Text);
            timer3.Interval = 250/Convert.ToInt32(textBox9.Text);
            x = x0 + (int)(rt * Math.Sin(alpha));
            y = y0 - (int)(rt * Math.Cos(alpha));
            p[0].X = x;
            p[0].Y = y;
            p[1].X = x;
            p[1].Y = y;
            p[2].X = x;
            p[2].Y = y;
            p[3].X = x;
            p[3].Y = y;
            if (checkBox2.Checked == true)
            {
                timer3.Start();
            }
            else
            {
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            wt = w*tInt + wl * t;
            rt = R0 / (1 - Math.Pow(wt, 2) * Math.Pow(t, 2) / 2);
            x = x0 + (int)(rtOld * Math.Sin(alpha));
            y = y0 - (int)(rtOld * Math.Cos(alpha));
            alpha += (Math.PI * wt);
            if (Math.Sqrt(Math.Pow(x - pictureBox1.Width / 2, 2) + Math.Pow(y - pictureBox1.Height / 2, 2)) >= d || wt < 0)
            {
                timer2.Stop();
            }
            else
            {
                draw_circle(Pens.LightGray, x, y, R);
                gr.DrawLine(Pens.White, x0, y0, xLine, yLine);
                xLine = x0 + (int)(d * Math.Sin(alpha));
                yLine = y0 - (int)(d * Math.Cos(alpha));
                x = x0 + (int)(rt * Math.Sin(alpha));
                y = y0 - (int)(rt * Math.Cos(alpha));
                gr.DrawLine(Pens.Black, x0, y0, xLine, yLine);
                draw_circle(Pens.Black, x, y, R);
                t+= tInt;
                rtOld = rt;
            }
        }
        private void draw_circle(Pen pen, int x, int y, int R)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawEllipse(pen, x-R, y-R, R*2, R*2);
        }
    }
}
