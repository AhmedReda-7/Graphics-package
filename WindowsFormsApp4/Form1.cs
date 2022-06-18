using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public Point center = new Point(256, 201);
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = panel.CreateGraphics();
            Pen pen = new Pen(Color.DarkSlateBlue);


            Point p1 = new Point(center.X + 400, center.Y);
            Point p2 = new Point(center.X - 400, center.Y);
            Point p3 = new Point(center.X, center.Y + 400);
            Point p4 = new Point(center.X, center.Y - 400);

            g.DrawLine(pen, p1, p2);
            g.DrawLine(pen, p3, p4);
        }


        private void addPoint(int x, int y)
        {
            var brush = Brushes.Black;
            var P = panel.CreateGraphics();

            P.FillRectangle(brush, center.X + x, center.Y - y, 2, 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);

            int x2 = Convert.ToInt32(textBox4.Text);
            int y2 = Convert.ToInt32(textBox3.Text);


            panel.Controls.Clear();
            this.Refresh();
             lineDDA(x1, y1, x2, y2);
        }

        int round(float a) { return Convert.ToInt32(a + 0.5); }
        void lineDDA(int x0, int y0, int xEnd, int yEnd)
        {
            int dx = xEnd - x0, dy = yEnd - y0, steps, k;
            float xIncrement, yIncrement, x = x0, y = y0;

            if (Math.Abs(dx) > Math.Abs(dy))
                steps = Math.Abs(dx);
            else
                steps = Math.Abs(dy);
            xIncrement = (float)dx / (float)steps;
            yIncrement = (float)dy / (float)steps;

            addPoint(round(x), round(y));
            for (k = 0; k < steps; k++)
            {
                x += xIncrement;
                y += yIncrement;
                try
                {
                    addPoint(round(x), round(y));
                }
                catch (InvalidOperationException)
                {
                    return ;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox5.Text);
            int y1 = Convert.ToInt32(textBox6.Text);

            int x2 = Convert.ToInt32(textBox7.Text);
            int y2 = Convert.ToInt32(textBox8.Text);

            Point p1 = new Point(x1, y1);

            Point p2 = new Point(x2, y2);
            panel.Refresh();
            Bresenham_Draw(x1, y1, x2, y2, "Def");
        }

        private void swap(ref int x0, ref int y0, ref int xEnd, ref int yEnd)
        {
            int temp1, temp2;
            temp1 = x0;
            x0 = y0;
            y0 = temp1;

            temp2 = xEnd;
            xEnd = yEnd;
            yEnd = temp2;
        }
        private void Bresenham_Draw(int x0, int y0, int xEnd, int yEnd, string Color)
        {

            float deltaX, deltaY, p;
            deltaX = (xEnd - x0);
            deltaY = (yEnd - y0);
            var brush = Brushes.Black;
            if (Color == "Yellow") { brush = Brushes.Yellow; }

            var g = panel.CreateGraphics();

            float slope = deltaY / deltaX;
            if (deltaX == 0) { slope = 99999; }
            // First Ocstant
            if (x0 < xEnd && slope >= 0 && slope <= 1)
            {
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (++X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (++X), center.Y - (++Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Second Ocstant
            else if (y0 < yEnd && slope > 1 && slope < 999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);
                deltaX = xEnd - x0;

                deltaY = yEnd - y0;

                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (++Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Third Ocstant
            else if (y0 < yEnd && slope < -1 && slope > -999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);
                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (--Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Fourth Ocstant
            else if (x0 > xEnd && slope <= 0 && slope >= -1)
            {
                deltaX = -deltaX;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (--X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (--X), center.Y - (++Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }

                }
            }

            // Fifth Ocstant
            else if (x0 > xEnd && slope > 0 && slope <= 1)
            {
                deltaX = -deltaX; deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (--X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (--X), center.Y - (--Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Sixth Ocstant
            else if (y0 > yEnd && slope > 1 && slope < 999999)
            {
                // Swap x1 and y1 ,,, Swap x2 and y2
                swap(ref x0, ref y0, ref xEnd, ref yEnd);

                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaX = -deltaX; deltaY = -deltaY;

                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (--Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Seventh Ocstant
            else if (y0 > yEnd && slope < -1 && slope < 999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);

                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaX = -deltaX;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (++Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Eighth Ocstant
            else if (x0 < xEnd && slope <= 0 && slope >= -1)
            {
                deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        g.FillRectangle(brush, center.X + (++X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        g.FillRectangle(brush, center.X + (++X), center.Y - (--Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox10.Text);
            int y = Convert.ToInt32(textBox9.Text);
            int Radius = Convert.ToInt32(textBox11.Text);
            panel.Refresh();
            Draw_CenterCircle(x, y, Radius);
        }
        private void Draw_CenterCircle(int center_X, int center_Y, int radius)
        {
            int x = 0;
            int y = radius;
            int p = 1 - radius;

            var g = panel.CreateGraphics();
            Draw_Circle(g, center_X, center_Y, x, y);
            while (x <= y)
            {
                x++;
                if (p < 0)
                    p += (2 * x) + 1;
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                Draw_Circle(g, center_X, center_Y, x, y);
            }
        }
        private void Draw_Circle(Graphics graphics, int center_X, int center_Y, int x, int y)
        {
            addPoint( center_X + x, center_Y + y);
            addPoint( center_X - x, center_Y + y);
            addPoint( center_X + x, center_Y - y);
            addPoint( center_X - x, center_Y - y);
            addPoint( center_X + y, center_Y + x);
            addPoint( center_X - y, center_Y + x);
            addPoint( center_X + y, center_Y - x);
            addPoint( center_X - y, center_Y - x);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox12.Text);
            int y = Convert.ToInt32(textBox19.Text);
            int x_r = Convert.ToInt32(textBox18.Text);
            int y_r = Convert.ToInt32(textBox17.Text);
            panel.Refresh();
            Ellipse_Algo(x, y, x_r, y_r);
        }
        private void Ellipse_Algo(int Xcenter, int Ycenter, int Rx, int Ry)
        {
            var brush = Brushes.Black;
            var g = panel.CreateGraphics();
            int Rx2 = Rx * Rx;
            int Ry2 = Ry * Ry;
            int TwoRx2 = 2 * Rx2;
            int TwoRy2 = 2 * Ry2;
            int p;
            int x = 0;
            int y = Ry;
            int px = 0;
            int py = TwoRx2 * y;

            ellipsePlotPoint1(Xcenter, Ycenter, x, y, g);
            /* Region 1 */

            p = round(Ry2 - (Rx2 * Ry) + (int)(0.25 * Rx2));
            while (px < py)
            {
                x++;
                px += TwoRy2;
                if (p < 0)
                {
                    p += Ry2 + px;
                }
                else
                {
                    y--;
                    py -= TwoRx2;
                    p += Ry2 + px - py;

                }
                ellipsePlotPoint2(Xcenter, Ycenter, x, y, g);

            }
            /* Region 2 */
            p = round(Ry2 * (int)(x + 0.5) * (int)(x + 0.5) + Rx2 * (y - 1) * (y - 1) - Rx2 * Ry2);
            while (y > 0)
            {
                y--;
                py -= TwoRx2;
                if (p > 0)
                {
                    p += Rx2 - py;
                }
                else
                {
                    x++;
                    px += TwoRy2;
                    p += Rx2 - py + px;
                }

                ellipsePlotPoint1(Xcenter, Ycenter, x, y, g);
            }


        }
        private void ellipsePlotPoint1(int xCenter, int yCenter, int x, int y, Graphics graphics)
        {

            addPoint( xCenter + x, yCenter + y);

            addPoint( xCenter - x, yCenter + y);

            addPoint( xCenter + x, yCenter - y);

            addPoint( xCenter - x, yCenter - y);
        }

        private void ellipsePlotPoint2(int xCenter, int yCenter, int x, int y, Graphics graphics)
        {

            addPoint(xCenter + x, yCenter + y);

            addPoint(xCenter - x, yCenter + y);

            addPoint(xCenter + x, yCenter - y);

            addPoint(xCenter - x, yCenter - y);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            panel.Refresh();
            Bresenham_Draw(x1, y1, x2, y2, "Def");
            Bresenham_Draw(x2, y2, x3, y3, "Def");
            Bresenham_Draw(x3, y3, x4, y4, "Def");
            Bresenham_Draw(x4, y4, x1, y1, "Def");
        }
        public void Transiation(ref int X, ref int Y, int X_Translation, int Y_Translation)
        {
            X += X_Translation;
            Y += Y_Translation;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int X_Translation = Convert.ToInt32(textBox32.Text);
            int Y_Translation = Convert.ToInt32(textBox33.Text);

            Transiation(ref x1, ref y1, X_Translation, Y_Translation);
            Transiation(ref x2, ref y2, X_Translation, Y_Translation);
            Transiation(ref x3, ref y3, X_Translation, Y_Translation);
            Transiation(ref x4, ref y4, X_Translation, Y_Translation);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");
        }

        public void Scaling(ref int X, ref int Y, int X_Scaling, int Y_Scaling)
        {
            X = X * X_Scaling;
            Y = Y * Y_Scaling;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int X_Scaling = Convert.ToInt32(textBox32.Text);
            int Y_Scaling = Convert.ToInt32(textBox33.Text);

            Scaling(ref x1, ref y1, X_Scaling, Y_Scaling);
            Scaling(ref x2, ref y2, X_Scaling, Y_Scaling);
            Scaling(ref x3, ref y3, X_Scaling, Y_Scaling);
            Scaling(ref x4, ref y4, X_Scaling, Y_Scaling);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");
        }
        private void button17_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Angel = Convert.ToInt32(textBox35.Text);

            Rotation(ref x1, ref y1, Angel);
            Rotation(ref x2, ref y2, Angel);
            Rotation(ref x3, ref y3, Angel);
            Rotation(ref x4, ref y4, Angel);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");
        }
        public double Cos(double Angel)
        {
            double angel = Convert.ToInt32(Math.Cos(Math.PI * Angel / 180) * 100);
            angel = Convert.ToDouble(angel / 100);
            return angel;
        }
        public void Rotation(ref int X, ref int Y, double Angel)
        {

            double x, y, con, sin;
            con = Cos(Angel);
            sin = Math.Sin(Math.PI * Convert.ToDouble(Angel / 180));

            x = (X * con) + (Y * sin);
            y = (X * sin) - (Y * con);

            X = Convert.ToInt32(Math.Round(x));
            Y = Convert.ToInt32(Math.Round(y));

        }

        private void button21_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Shearing_shx = Convert.ToInt32(textBox39.Text);

            Shearing_X(ref x1, ref y1, Shearing_shx);
            Shearing_X(ref x2, ref y2, Shearing_shx);
            Shearing_X(ref x3, ref y3, Shearing_shx);
            Shearing_X(ref x4, ref y4, Shearing_shx);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");
        }
        public void Shearing_X(ref int X, ref int Y, int Y_Shearing)
        {
            int x, y;
            x = X + Y_Shearing * Y;
            y = Y;
            X = x;
            Y = y;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Shearing_shy = Convert.ToInt32(textBox40.Text);

            Shearing_Y(ref x1, ref y1, Shearing_shy);
            Shearing_Y(ref x2, ref y2, Shearing_shy);
            Shearing_Y(ref x3, ref y3, Shearing_shy);
            Shearing_Y(ref x4, ref y4, Shearing_shy);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");
        }
        public void Shearing_Y(ref int X, ref int Y, int X_Shearing)
        {
            int x, y;
            x = X * 1;
            y = X * X_Shearing + Y;
            X = x;
            Y = y;
        }


        private void button23_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            Bresenham_Draw(-x1, y1, -x2, y2, "Yellow");
            Bresenham_Draw(-x2, y2, -x3, y3, "Yellow");
            Bresenham_Draw(-x3, y3, -x4, y4, "Yellow");
            Bresenham_Draw(-x4, y4, -x1, y1, "Yellow");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = -Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = -Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = -Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = -Convert.ToInt32(textBox30.Text);

            Bresenham_Draw(x1, y1, x2, y2, "Yellow");
            Bresenham_Draw(x2, y2, x3, y3, "Yellow");
            Bresenham_Draw(x3, y3, x4, y4, "Yellow");
            Bresenham_Draw(x4, y4, x1, y1, "Yellow");

        }
        private void button25_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = -Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = -Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = -Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = -Convert.ToInt32(textBox30.Text);


            Bresenham_Draw(-x1, y1, -x2, y2, "Yellow");
            Bresenham_Draw(-x2, y2, -x3, y3, "Yellow");
            Bresenham_Draw(-x3, y3, -x4, y4, "Yellow");
            Bresenham_Draw(-x4, y4, -x1, y1, "Yellow");
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            panel.Refresh();

        }
    }
}


    

