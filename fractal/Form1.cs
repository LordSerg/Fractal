using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform.Windows;
using System.Threading;
using System.Drawing.Drawing2D;

namespace fractal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Pen p;
        int n;
        double x, y, s;

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.Items.Add("Fractal tree");//+
            //comboBox1.Items.Add("Pythaghoras fractal tree");//-
            ////comboBox1.Items.Add("Dragon curve or Harter - Heighway dragon");//-
            //comboBox1.Items.Add("3D fractal");//-+
            //comboBox1.Items.Add("Точечный фрактал");
            g = CreateGraphics();
            p = new Pen(Color.Black,1);
            //clkx = new int[4];
            //clky = new int[4];
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Color = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            double l, x0, y0;
            if (checkBox1.Checked)
                this.Refresh();
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (tabControl1.SelectedIndex == 0)
            {
                g = tabPage1.CreateGraphics();
                n = Convert.ToInt32(textBox1.Text);
                l = Convert.ToDouble(textBox5.Text);
                x0 = this.Width / 2;
                y0 = this.Height * 4 / 5;
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0), Convert.ToInt32(y0 - l));
                double alpha = Convert.ToDouble(textBox2.Text);
                double alpha0 = alpha;
                s = Convert.ToDouble(textBox4.Text);
                fractal(n, x0, y0 - l, l / s, alpha, alpha0);
                fractal(n, x0, y0 - l, l / s, -alpha, alpha0);
                p = new Pen(Color.Black);
                /*
                look well:
                n=15; a=45;  s=1.55; l=300;
                n=16; a=72;  s=1.5;  l=300;
                n=15; a=130; s=1.5;  l=600;
                n=16; a=130; s=1.4;  l=400;
                n=17; a=60; s=1.38;  l=150;
                n=60; a=40; s=1.416;  l=250;
                */
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (tabControl1.SelectedIndex == 2)//
            {
                g = tabPage3.CreateGraphics();
                n = Convert.ToInt32(textBox16.Text);
                l = Convert.ToDouble(textBox19.Text);
                double alpha, betta;
                alpha = Convert.ToDouble(textBox17.Text);
                betta = Convert.ToDouble(textBox3.Text);
                if (alpha + betta < 180)
                {
                    bool r;
                    x0 =this.Width/2-l/2;
                    y0 =this.Height*2/3+l/2;
                    //drawing the first cube
                    g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + l), Convert.ToInt32(y0));
                    g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0), Convert.ToInt32(y0-l));
                    g.DrawLine(p, Convert.ToInt32(x0+l), Convert.ToInt32(y0), Convert.ToInt32(x0 + l), Convert.ToInt32(y0-l));
                    g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0-l), Convert.ToInt32(x0 + l), Convert.ToInt32(y0-l));
                    //{a=l*sin(betta)/sin(180-(alpha+betta))  =>{h=sin(alpha)*l*sin(betta)/sin(180-(alpha+betta))  =>  y1=y0-l-sin(alpha)*sin(betta)*l/sin(180-(alpha+betta))
                    //{h=sin(alpha)*a/sin(90)                   {y1=y0-l-h
                    double y1 = y0 - l - (Math.Sin(betta * Math.PI / 180) *Math.Sin((alpha) * Math.PI / 180) * l) / ((-1) * Math.Sin((alpha + betta) * Math.PI / 180));
                    //{l1=a*sin(90-alpha)  => {x1=x0+a*sin(90-alpha)                  =>  x1=x0+sin(90-alpha)*sin(betta)*l/sin(180-(alpha+betta))   =>   x1=x0+(cos(alpha)*sin(betta)*l)/-1*(sin(alpha+betta))
                    //{x1=x0+l1               {a=l*sin(betta)/sin(180-(alpha+betta))
                    double x1 = x0 + (Math.Cos((alpha) * Math.PI / 180) * Math.Sin(betta * Math.PI / 180) * l) / ((-1) * Math.Sin((alpha + betta) * Math.PI / 180));
                    
                    //r=true - right
                    //r=false - left

                    g.DrawLine(p, Convert.ToInt32(x1), Convert.ToInt32(y1), Convert.ToInt32(x0 + l), Convert.ToInt32(y0 - l));
                    g.DrawLine(p, Convert.ToInt32(x1), Convert.ToInt32(y1), Convert.ToInt32(x0), Convert.ToInt32(y0 - l));

                    r = false;
                    //fractal1(n, x0, y0 - l, x1, y1, alpha, betta,r);
                    r = true;
                    //fractal1(n, x0 + l, y0 - l, x1, y1, alpha, betta,r);
                }
                else
                {
                    MessageBox.Show("","'a' + 'b' Shouldn't be more than 180!!!",MessageBoxButtons.OK);
                    for (int i = 0; i < 4; i++)
                    {
                        g.FillRectangle(Brushes.Red, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                        Thread.Sleep(100);
                        g.FillRectangle(Brushes.White, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                        Thread.Sleep(100);
                    }
                }
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (tabControl1.SelectedIndex == 1)
            {//3д дерево
                g = tabPage2.CreateGraphics();
                glControl1.Visible = true;
                n = Convert.ToInt32(textBox1.Text);
                l = Convert.ToDouble(textBox5.Text)/1000;
                double alpha = Convert.ToDouble(textBox2.Text);//угол, относительно центральной вертикали
                double betta = Convert.ToDouble(textBox3.Text);//угол подвыподветурности следующей ветви
                Vector3d a;
                a.X = 0;
                a.Y = l - 1;    
                a.Z = 0;
                double s = Convert.ToDouble(textBox4.Text);
                
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.ClearColor(this.BackColor);
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(p.Color);
                GL.Vertex3(0,-1,0);
                GL.Vertex3(a);

                //GL.Vertex3(a);
                //GL.Vertex3(a.X + (l / s) * Math.Sin(alpha), a.Y + (l / s) * Math.Cos(alpha), a.Z + 0);

                //GL.Vertex3(a);
                //GL.Vertex3(a.X - (l / s) * Math.Sin(alpha)/2, a.Y + (l / s) * Math.Cos(alpha), a.Z - (l/s)*Math.Sin(alpha)*(Math.Sqrt(3)/2));

                //GL.Vertex3(a);
                //GL.Vertex3(a.X - (l / s) * Math.Sin(alpha)/2, a.Y + (l / s) * Math.Cos(alpha), a.Z + (l / s) * Math.Sin(alpha) * (Math.Sqrt(3) / 2));
                //GL.End();


                fractal4(n, a, alpha, betta, l, s, alpha);

                alpha = Convert.ToDouble(textBox2.Text);
                betta = Convert.ToDouble(textBox3.Text);
                fractal4(n, a, alpha, betta + 120, l, s, alpha);

                alpha = Convert.ToDouble(textBox2.Text);
                betta = Convert.ToDouble(textBox3.Text);
                fractal4(n, a, alpha, betta + 240, l, s, alpha);
                GL.End();
                glControl1.SwapBuffers();
                //timer1.Interval = 1;
                //timer1.Enabled = true;
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            /*if(tabControl1.SelectedIndex == "")//создание n-стороннего дерева
            {

            }*/
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (tabControl1.SelectedIndex == 3)//создание фрактала по точкам
            {
                g = tabPage4.CreateGraphics();
                
                int k = Convert.ToInt32(textBox1.Text);//k - количество итераций
                //MessageBox.Show("выберите первую точку;", "Подсказка", MessageBoxButtons.OK);
                Point[] coord;
                coord = new Point[5];
                coord[0] = new Point(Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text));
                coord[1] = new Point(Convert.ToInt32(textBox8.Text), Convert.ToInt32(textBox9.Text));
                coord[2] = new Point(Convert.ToInt32(textBox10.Text), Convert.ToInt32(textBox11.Text));
                //coord[3] = new Point(Convert.ToInt32(textBox12.Text), Convert.ToInt32(textBox13.Text));
                coord[3] = new Point(0, 0);
                g.DrawLine(p, coord[0], coord[1]);
                g.DrawLine(p, coord[1], coord[2]);
                g.DrawLine(p, coord[2], coord[0]);
                //g.DrawLine(p, coord[3], coord[0]);
                for (int i = 0; i < 5; i++)
                {
                    g.DrawEllipse(p, coord[i].X, coord[i].Y, 1, 1);
                }
                Random r = new Random();
                int rk;

                for(int i=0;i<k;i++)
                {
                    rk = r.Next(1,4);
                    if(rk==1)
                    {
                        coord[3] = fractal5(coord[0], coord[3]);
                    }
                    if (rk == 2)
                    {
                        coord[3] = fractal5(coord[1], coord[3]);
                    }
                    if (rk == 3)
                    {
                        coord[3] = fractal5(coord[2], coord[3]);
                    }
                    //if (rk == 4)
                    //{
                    //    coord[4] = fractal5(coord[3], coord[4]);
                    //}
                    g.DrawEllipse(p, coord[3].X, coord[3].Y, 1, 1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Опции↓")
            {//показать опции
                button2.Text = "Опции↑";
                panel1.Height = 0;
                panel1.Visible = true;
                while(panel1.Height<=200)
                {
                    panel1.Height++;
                }
                label7.BackColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
                label7.ForeColor = Color.FromArgb(250 - trackBar4.Value, 250 - trackBar5.Value, 250 - trackBar6.Value);
            }
            else
            {//спрятать опции
                button2.Text = "Опции↓";
                while (panel1.Height>0)
                {
                    panel1.Height--;
                }
                panel1.Visible = false;
            }
        }
       
        void fractal(int n,double x0,double y0,double l,double alpha, double alpha0)//fractal tree
        {
            if(n>=1)
            {
                x = (Math.Sin(alpha*(Math.PI/180)) * l);
                y = -(Math.Sin((90 - alpha )* (Math.PI / 180)) * l);
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + x), Convert.ToInt32(y0 + y));
                fractal(n - 1, x0 + x, y0 + y, l / s, alpha + alpha0, alpha0);

                x = (Math.Sin(alpha * (Math.PI / 180)) * l);
                y = -(Math.Sin((90 - alpha) * (Math.PI / 180)) * l);
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + x), Convert.ToInt32(y0 + y));
                fractal(n - 1, x0 + x, y0 + y, l / s, alpha - alpha0, alpha0);
            }
        }

        void fractal1(int n, double x0, double y0, double x1, double y1,double alpha,double betta,bool r)//Pythaghoras fractal tree
        {
            //1.вычисляем длинну прямой
            double l1 = Math.Sqrt(Math.Pow(Math.Abs(x0 - x1), 2) + Math.Pow(Math.Abs(y0 - y1), 2));
            //2.вычисляем координаты вершин треугольника
            double h = Math.Sin(alpha * Math.PI / 180) * l1 * Math.Sin(alpha * Math.PI / 180) / Math.Sin((180 - (alpha + betta)) * Math.PI / 180);
            //l2=b*cos(betta)                    =>  l2=l*sin(alpha)*cos(betta)/(-sin(alpha+betta))
            //b = l1 * sin(alpha) / (-sin(alpha + betta))
            double lx;
            if (r == true)
            {
                lx = (l1 * Math.Sin(alpha * Math.PI / 180) * Math.Cos(betta * Math.PI / 180)) / ((-1) * Math.Sin((alpha + betta) * Math.PI / 180));
            }
            else
            {
                //l1=a*cos(alpha)                    =>  l1=l*sin(betta)*cos(alpha)/(-sin(alpha+betta))
                //a=l*sin(betta)/(-sin(alpha+betta))
                lx = l1 * Math.Sin(betta * Math.PI / 180) * Math.Cos(alpha * Math.PI / 180) / ((-1) * Math.Sin((alpha + betta) * Math.PI / 180));
            }
            g.DrawLine(p, Convert.ToInt32(x1), Convert.ToInt32(y1), Convert.ToInt32(x1 + h), Convert.ToInt32(y1 - lx));
            g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + h), Convert.ToInt32(y0 - lx));
            g.DrawLine(p, Convert.ToInt32(x1 + h), Convert.ToInt32(y1 - lx), Convert.ToInt32(x0 + h), Convert.ToInt32(y0 - lx));

            //x0 = x1 + h;
            //y0 = y1 + lx;

            //x1 = x0 + (Math.Cos(alpha*Math.PI/180)*Math.Sin(betta*Math.PI/180)*l)/((-1)*Math.Sin((alpha+betta)*Math.PI/180));
            //x1 =;
            //y1 = y0 - l - (Math.Sin(betta * Math.PI / 180) *Math.Sin(alpha * Math.PI / 180) * l) / ((-1) * Math.Sin((alpha + betta) * Math.PI / 180));            
            //y1 =;
            //4.вызываем новые 2 фрактала

        }
        
        void fractal2()//Dragon curve or Harter - Heytway dragon
        {

        }

        void fractal3()//Snowflake
        {

        }

        void fractal4(int n,Vector3d a, double alpha,double betta,double l,double s,double alpha0)//3D fractal
        {
            //вектор а - конечная точка придыдущей линии
            //alpha - 
            //betta - 
            //n - 
            //l - 
            //s - 
            //betta0 - 
            if (n >= 1)
            {
                GL.Vertex3(a);
                //Вычисления следующих координат;

                a.X += (l / s) * Math.Sin(alpha* (Math.PI / 180)) * Math.Sin(Math.Abs(betta - 90)* (Math.PI / 180));
                a.Y += (l / s) * Math.Cos(alpha* (Math.PI / 180));
                a.Z += (l / s) * Math.Sin(alpha* (Math.PI / 180)) * Math.Cos(Math.Abs(betta - 90)* (Math.PI / 180));
                GL.Vertex3(a);

                //вызов 3-х функций:
                n--;
                fractal4(n, a, alpha + alpha0, betta, l, s, alpha0);
                fractal4(n, a, alpha + alpha0, betta + 120, l, s, alpha0);
                fractal4(n, a, alpha + alpha0, betta + 240, l, s, alpha0);
            }
        }

        Point fractal5(Point a,Point a1)//
        {
            //1.наxодим расстояние, на которое передвигаемся;
            Point S1 = a;
            S1.X += (a1.X - a.X) / 2;
            S1.Y -= (a1.Y - a.Y) / 2;
            return S1;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!(trackBar1.Value == 0 && trackBar2.Value == 0 && trackBar3.Value == 0))
            {
                GL.Begin(PrimitiveType.Patches);
                GL.Rotate(1, trackBar1.Value, trackBar2.Value, trackBar3.Value);
                GL.End();
                glControl1.SwapBuffers();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(this.BackColor);
            //button1_Click(sender, e);
            GL.Begin(PrimitiveType.Patches);
            GL.Rotate(15, 0, 1, 0);
            GL.End();
            glControl1.SwapBuffers();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.ClearColor(this.BackColor);
            //button1_Click(sender, e);
            
            GL.Rotate(90,1,0,0);
            //glControl1.SwapBuffers();
        }
        int []clkx, clky;
        
        int iclk =0;
        private void Form1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            /*if (iclk == 0)
            {
                clkx[iclk] = e.X;
                clky[iclk] = e.Y;
                g.DrawEllipse(p, clkx[iclk], clky[iclk], 2, 2);
                iclk++;
                
            }
            else if (iclk == 1)
            {
                clkx[iclk] = e.X;
                clky[iclk] = e.Y;
                g.DrawEllipse(p, clkx[iclk], clky[iclk], 2, 2);
                iclk++;
                
            }
            else if (iclk == 2)
            {
                clkx[iclk] = e.X;
                clky[iclk] = e.Y;
                g.DrawEllipse(p, clkx[iclk], clky[iclk], 2, 2);
                iclk++;
                
            }
            else if (iclk == 3)
            {
                clkx[iclk] = e.X;
                clky[iclk] = e.Y;
                g.DrawEllipse(p, clkx[iclk], clky[iclk], 2, 2);
                iclk++;
                
            }*/

        }

        private void trackBar5_Scroll_1(object sender, EventArgs e)
        {//green
            label7.BackColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            label7.ForeColor = Color.FromArgb(250 - trackBar4.Value, 250 - trackBar5.Value, 250 - trackBar6.Value);
        }
        private void trackBar6_Scroll_1(object sender, EventArgs e)
        {//blue
            label7.BackColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            label7.ForeColor = Color.FromArgb(250 - trackBar4.Value, 250 - trackBar5.Value, 250 - trackBar6.Value);
        }
        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {//red
            label7.BackColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            label7.ForeColor = Color.FromArgb(250 - trackBar4.Value, 250 - trackBar5.Value, 250 - trackBar6.Value);
        }
    }
}
