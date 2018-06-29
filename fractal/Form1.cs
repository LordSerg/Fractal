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
        Vector3d[] a0;
        Vector3d[] a1;
        Vector3d[] a2;
        private void Form1_Load(object sender, EventArgs e)
        {
            a0 = new Vector3d[7200];
            a1 = new Vector3d[7200];
            a2 = new Vector3d[7200];
            comboBox1.Items.Add("Fractal tree");//+
            comboBox1.Items.Add("Pythaghoras fractal tree");//-
            comboBox1.Items.Add("Dragon curve or Harter - Heighway dragon");//-
            comboBox1.Items.Add("3D fractal");//-+
            g = CreateGraphics();
            p = new Pen(Color.Black,1);
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            double l, x0, y0;
            this.Refresh();
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            trackBar4.Visible = false;
            trackBar5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            glControl1.Visible = false;
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if(comboBox1.Text=="")
            {
                MessageBox.Show("Чтобы построить фрактал - выберете его вид","Подсказка",MessageBoxButtons.OK);
                g.FillRectangle(Brushes.Red, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                Thread.Sleep(100);
                g.FillRectangle(Brushes.White, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                Thread.Sleep(100);
                g.FillRectangle(Brushes.Red, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                Thread.Sleep(100);
                g.FillRectangle(Brushes.White, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                Thread.Sleep(100);
                g.FillRectangle(Brushes.Red, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                Thread.Sleep(100);
                g.FillRectangle(Brushes.White, comboBox1.Location.X - 5, comboBox1.Location.Y - 5, comboBox1.Width + 10, comboBox1.Height + 10);
                comboBox1.Focus();
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (comboBox1.Text == "Fractal tree")
            {
                n = Convert.ToInt32(textBox1.Text);
                l = Convert.ToDouble(textBox5.Text);
                x0 = this.Width / 2;
                y0 = this.Height * 4 / 5;
                
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0), Convert.ToInt32(y0 - l));
                double alpha = Convert.ToDouble(textBox2.Text);
                delta = Convert.ToDouble(textBox3.Text);
                s = Convert.ToDouble(textBox4.Text);
                fractal(n, x0, y0 - l, l / s, alpha, delta);
                fractal(n, x0, y0 - l, l / s, -alpha, delta);

                //fractal(n, x0, y0, l / s, alpha-180, delta);
                //fractal(n, x0, y0, l / s, -alpha-180, delta);
                p = new Pen(Color.Black);
                /*look well:
                n=15;a=45;b=135;s=1.55;l=300;
                n=16;a=72;b=72;s=1.5;l=300;
                n=15;a=130;b=130;s=1.5;l=300;*/
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (comboBox1.Text== "Pythaghoras fractal tree")
            {
                n = Convert.ToInt32(textBox1.Text);
                l = Convert.ToDouble(textBox5.Text);
                double alpha, betta;
                alpha = Convert.ToDouble(textBox2.Text);
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
                    g.FillRectangle(Brushes.Red, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width+10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.White, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.Red, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.White, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.Red, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.White, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.Red, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);
                    Thread.Sleep(100);
                    g.FillRectangle(Brushes.White, label2.Location.X - 5, label2.Location.Y - 5, label2.Width + textBox2.Width + 10, label2.Height * 2 + 25);

                }
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if (comboBox1.Text== "3D fractal")
            {//цветок/шар
                glControl1.Visible = true;
                timer1.Enabled = true;
                trackBar1.Visible = true;
                trackBar2.Visible = true;
                trackBar3.Visible = true;
                trackBar4.Visible = true;
                trackBar5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                //Random r = new Random();
                //a = new Vector3d[5];
                //for(int i=0;i<5;i++)
                //{
                //    a[i].X = 0.001 * r.Next(-500, 500);
                //    a[i].Y = 0.001 * r.Next(-500, 500);
                //    a[i].Z = 0.001 * r.Next(-500, 500);
                //}
                GL.ClearColor(Color.FromArgb(0, 0, 0));
                GL.Clear(ClearBufferMask.ColorBufferBit);
                //GL.Begin(PrimitiveType.Polygon);
                //for(int i=0;i<5;i++)
                //{
                //    GL.Color3(Color.White);
                //    GL.Vertex3(a[i]);
                //}
                //GL.End();

                
            }
            //____________________________________________________________________________________________________________________________________________________________________________________________________________________
            if(comboBox1.Text=="123")
            {
                //создание n-стороннего дерева (снежинки)
            }
        }
        void draw_cube()
        {

        }

        void coord_os()
        {
            xyz[0].X =1;
            xyz[0].Y =0;
            xyz[0].Z =0;
            xyz[1].X =-1;
            xyz[1].Y =0;
            xyz[1].Z =0;
            xyz[2].X =0;
            xyz[2].Y =1;
            xyz[2].Z =0;
            xyz[3].X =0;
            xyz[3].Y =-1;
            xyz[3].Z =0;
            xyz[4].X =0;
            xyz[4].Y =0;
            xyz[4].Z =1;
            xyz[5].X =0;
            xyz[5].Y =0;
            xyz[5].Z =-1;
        }

        Vector3d []xyz;//Координатные оси:
        private void timer1_Tick(object sender, EventArgs e)
        {
            xyz = new Vector3d[6];
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(PrimitiveType.Lines);
            coord_os();
            GL.Color3(Color.Red);//ox
            GL.Vertex3(xyz[0]);
            GL.Vertex3(xyz[1]);
            GL.Color3(Color.Blue);//oy
            GL.Vertex3(xyz[2]);
            GL.Vertex3(xyz[3]);
            GL.Color3(Color.Green);//oz
            GL.Vertex3(xyz[4]);
            GL.Vertex3(xyz[5]);
            GL.End();
            double r = 0.4,A=5;
            r = trackBar4.Value*0.05;//Амплитуда
            A = trackBar5.Value;//Частота
            GL.Begin(PrimitiveType.Points);
            for (int i = -3600; i < 3600; i++)
            {
                a0[i + 3600].X = i * 0.001;
                a0[i + 3600].Y = r * Math.Cos(a0[i + 3600].X * A);
                a0[i + 3600].Z = r * Math.Sin(a0[i + 3600].X * A);
            }
            //r = 0.4;
            //A=5;
            for (int i = -3600; i < 3600; i++)
            {
                a1[i + 3600].X = i * 0.001;
                a1[i + 3600].Y = r * Math.Cos(a1[i + 3600].X * A + 90);
                a1[i + 3600].Z = r * Math.Sin(a1[i + 3600].X * A + 90);//... + 90) - здвиг на фазу
            }
            //r = 0.4;
            //A = 5;
            for (int i = -3600; i < 3600; i++)
            {
                a2[i + 3600].X = i * 0.001;
                a2[i + 3600].Y = r * Math.Cos(a2[i + 3600].X * A + 180);
                a2[i + 3600].Z = r * Math.Sin(a2[i + 3600].X * A + 180);
            }
            GL.Color3(Color.White);
            for (int i = 0; i < 7200; i++)
            {
                GL.Color3(Color.White);
                GL.Vertex3(a0[i]);
                GL.Color3(Color.White);
                GL.Vertex3(a1[i]);
                GL.Color3(Color.White);
                GL.Vertex3(a2[i]);
            }
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.DarkOrange);
            int w=0;
            for (int i = 0; i < 7200; i +=100)
            {
                w++;
                if (w == 10)
                {
                    GL.Color3(Color.Yellow);
                    w = 0;
                }
                else
                    GL.Color3(Color.DodgerBlue);
                GL.Vertex3(a0[i]);
                GL.Vertex3(a1[i]);
                GL.Vertex3(a0[i]);
                GL.Vertex3(a2[i]);
                GL.Vertex3(a1[i]);
                GL.Vertex3(a2[i]);
                GL.Vertex3(a0[i]);
                
            }
            GL.End();
            GL.Begin(PrimitiveType.Patches);
            GL.Color3(Color.White);
            if (!(trackBar1.Value == 0 && trackBar2.Value == 0 && trackBar3.Value == 0))
            {
                GL.Rotate(1, trackBar1.Value, trackBar2.Value, trackBar3.Value);
            }
            //GL.Rotate(1, R.NextDouble(), R.NextDouble(), R.NextDouble());
            GL.End();
            GL.Flush();
            glControl1.SwapBuffers();

            //speed--;
            //GL.ClearColor(Color.FromArgb(0, 0, 0));
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.Begin(PrimitiveType.Lines);
            ////fractal4
            //GL.End();
            //GL.Begin(PrimitiveType.Patches);
            //GL.Rotate(speed/100,(y_down - y_up), (x_down - x_up),0 );
            //GL.End();
            //glControl1.SwapBuffers();
            //if (speed <= 0)
            //{
            //    timer1.Enabled = false;
            //}

        }
       
        void fractal(int n,double x0,double y0,double l,double alpha,double delta)//fractal tree
        {
            if(n>=1)
            {
                //theorem of sin
                //Random w = new Random();                
                //if (n <= 3)
                //    p = new Pen(Color.Green);
                //else
                //    p = new Pen(Color.Brown);
                x = (Math.Sin(alpha*(Math.PI/180)) * l);
                y = -(Math.Sin((90 - alpha )* (Math.PI / 180)) * l);
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + x), Convert.ToInt32(y0 + y));
                fractal(n - 1, x0 + x, y0 + y, l / s, alpha + delta,delta);


                x = (Math.Sin(alpha * (Math.PI / 180)) * l);
                y = -(Math.Sin((90 - alpha) * (Math.PI / 180)) * l);
                g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + x), Convert.ToInt32(y0 + y));
                fractal(n - 1, x0 + x, y0 + y, l / s, alpha - delta, delta);


                //x = (Math.Sin(alpha * (Math.PI / 180)) * l);
                //y = -(Math.Sin((90 - alpha) * (Math.PI / 180)) * l);
                //g.DrawLine(p, Convert.ToInt32(x0), Convert.ToInt32(y0), Convert.ToInt32(x0 + x), Convert.ToInt32(y0 + y));
                //fractal(n - 1, x0 + x, y0 + y, l / 1.2, alpha, delta);
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

        Vector3d[] a;
        int x_down, y_down, x_up, y_up, speed;
        private void glControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            x_down = MousePosition.X;
            y_down = MousePosition.Y;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        double x, y,delta,s;
        private void glControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //x_up = MousePosition.X;
            //y_up = MousePosition.Y;
            //speed = Convert.ToInt32(Math.Sqrt(Math.Abs(x_down - x_up) * Math.Abs(x_down - x_up) + Math.Abs(y_down - y_up) * Math.Abs(y_down - y_up)));
            //timer1.Enabled = true;
            //a = new Vector3d[sum(n)];
        }
        int sum(int n)
        {
            int w=0;
            for(int i=0;i<n;i++)
            {
                w += Convert.ToInt32(Math.Pow(n,i));
            }
            return w;
        }
        void fractal4()//3D fractal(2)
        {
            GL.Begin(PrimitiveType.Lines);

            GL.End();
        }
    }
}
