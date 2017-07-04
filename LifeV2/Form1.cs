using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeV2
{
    public partial class Form1 : Form
    {
        Graphics g;
        const int CellSize = 15;
        int cols, rows;
        int generation = 0;
        int[,] prevArr;
        int[,] arr;//массив клеток поля,0--белый, 1--черный
        public void DrawGrid()
        {
            int x=0,y=0;
            for(int i=0;i<rows;i++)
            {
               
                g.DrawLine(Pens.Black,0,y,pictureBox1.Width,y);
                 y+=CellSize;
            }
            for(int j=0;j<cols;j++)
            {
                g.DrawLine(Pens.Black, x, 0, x, pictureBox1.Height);
                x += CellSize;
            }
        }
        void Form1_Paint(object sender,PaintEventArgs e)
        {
           
        }
        public Form1()
        {
            InitializeComponent();
            //g = pictureBox1.CreateGraphics();
            pictureBox1.BackColor = Color.White;
            cols = pictureBox1.Width / CellSize;
            rows = pictureBox1.Height / CellSize;
            arr = new int[rows, cols];
            prevArr = new int[rows, cols];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            int x = e.X/CellSize;
            int y = e.Y / CellSize;
            int ltX = x * CellSize;
            int ltY = y * CellSize;
            Brush b;
            if (arr[y, x] == 0)
            {
                b = Brushes.Black;
                arr[y, x] = 1;
            }
            else
            {
                b = Brushes.White;
                arr[y, x] = 0;
            }
                g.FillRectangle(b, ltX+1, ltY+1, CellSize-1, CellSize-1);
        }

       public void FillFromArray()
        {
            g = pictureBox1.CreateGraphics();
           for(int i=0;i<rows;i++)
               for(int j=0;j<cols;j++)
               {
                   int ltX=j*CellSize,ltY=i*CellSize;
                   Brush b;
                   if (arr[i, j] == 1)
                   {
                       b = Brushes.Black;
                   }
                   else b = Brushes.White;
                   g.FillRectangle(b, ltX + 1, ltY + 1, CellSize - 1, CellSize - 1);
               }
        }
        public void Step()
        {
            generation++;
            label1.Text = generation.ToString();
            prevArr = (int[,])arr.Clone();
            for(int i=0;i<rows;i++)
                for(int j=0;j<cols;j++)
                {
                    int sum = 0;
                    for (int k = i - 1; k < i+2; k++)//окрестные точки
                    {
                        
                        int r,c;
                        if (k < 0)
                        {
                            r = rows - 1;
                        }
                        else
                            if (k >= rows)
                                r = 0;
                            else
                                r = k;
                        for (int l = j - 1; l < j+2; l++)
                        {
                           

                            if (l < 0)
                            {
                                c = cols - 1;
                            }
                            else
                                if (l >= cols)
                                    c = 0;
                                else
                                    c = l;

                            if(!(r==i&&c==j))
                            {
                                if (prevArr[r, c] == 1)
                                    sum++;
                            }
                        }
                    }
                    if (sum == 3 && prevArr[i, j] == 0)
                        arr[i, j] = 1;
                    else
                    if (prevArr[i, j] == 1 && (sum < 2 || sum > 3))
                        arr[i, j] = 0;
                }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            DrawGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Step();
            FillFromArray();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Step();
            FillFromArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for(int i=0;i<rows;i++)
                for(int j=0;j<cols;j++)
                {
                    arr[i, j] = 0;
                }
            FillFromArray();
        }
    }
}
