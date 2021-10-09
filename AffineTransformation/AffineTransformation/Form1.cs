using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffineTransformation
{
    public partial class Form1 : Form
    {

		//Matrix for figure
		float[,] figure = new float[,] {
			{ -65, -65, 1 },
			{ 65, -65, 1 },
			{ -25, -25, 1 },
			{ -65, 65, 1}
		};

		//Matrix for saving the original figure
		float[,] reset = new float[,] {
			{ -65, -65, 1 },
			{ 65, -65, 1 },
			{ -25, -25, 1 },
			{ -65, 65, 1}
		};

		//Matrix for rotation
		float[,] rotate = new float[,]
		{
			{ 0, 1, 0 },
			{ -1, 0, 0 },
			{ 0, 0, 1 }
		};

		//Matrix for scaling down
		float[,] scaleDown = new float[,]
		{
			{ 0.5F, 0, 0 },
			{ 0, 0.5F, 0 },
			{ 0, 0, 1 }
		};

		//Matrix for scaling down
		float[,] scaleUp = new float[,]
		{
			{ 2, 0, 0 },
			{ 0, 2, 0 },
			{ 0, 0, 1 }
		};

		//Matrix for reflection
		float[,] reflect = new float[,]
		{
			{ 1, 0, 0 },
			{ 0, -1, 0 },
			{ 0, 0, 1 }
		};

		//Matrix for transferring
		float[,] transfer = new float[,]
		{
			{ 1, 0, 0 },
			{ 0, 1, 0 },
			{ 10, -10, 1 }
		};


		public Form1()
        {
            InitializeComponent();
			pictureBox1.Paint += PictureBox1_Paint;
		}

		private void PictureBox1_Paint(object sender, PaintEventArgs e)
		{
			int w = pictureBox1.ClientSize.Width / 2;
			int h = pictureBox1.ClientSize.Height / 2;
			e.Graphics.TranslateTransform(w, h);
			DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
			DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);
			e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
		}

		//Drawing ticks along x axis
		private void DrawXAxis(Point start, Point end, Graphics g)
		{
			g.DrawLine(Pens.Black, start, end);
			for (int i = start.X; i < end.X; i += 20)
			{
				g.DrawLine(Pens.Black, i, -3, i, 3);
			}
		}

		//Drawing ticks along y axis
		private void DrawYAxis(Point start, Point end, Graphics g)
		{
			g.DrawLine(Pens.Black, start, end);
			for (int i = start.Y; i > end.Y; i -= 20)
			{
				g.DrawLine(Pens.Black, -3, i, 3, i);
			}
		}

		//Method for drawing the figure inside the pictureBox
		private void DrawFigure()
		{
			Graphics g = pictureBox1.CreateGraphics();
			Pen pen = new Pen(Color.Red);
			int w = pictureBox1.ClientSize.Width / 2;
			int h = pictureBox1.ClientSize.Height / 2;
			g.TranslateTransform(w, h);
			for (int i = 0; i < 4; i++)
			{
				if (i == 3)
				{
					g.DrawLine(pen, figure[i, 0], figure[i, 1], figure[0, 0], figure[0, 1]);
				}
				else
				{
					g.DrawLine(pen, figure[i, 0], figure[i, 1], figure[i + 1, 0], figure[i + 1, 1]);
				}

			}
			g.Dispose();
		}

		//Method for multiplying two matrixes
		static float[,] Multiplication(float[,] a, float[,] b)
		{
			float[,] result = new float[a.GetLength(0), b.GetLength(1)];
			for (int i = 0; i < a.GetLength(0); i++)
			{
				for (int j = 0; j < b.GetLength(1); j++)
				{
					for (int k = 0; k < b.GetLength(0); k++)
					{
						result[i, j] += a[i, k] * b[k, j];
					}
				}
			}
			return result;
		}

		//Button that draws the figure
		private void button1_Click(object sender, EventArgs e)
        {
			DrawFigure();
		}

		//Button that rotates the figure
		private void button2_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = Multiplication(figure, rotate);
			DrawFigure();
		}

		//Button that scalesUp the figure
		private void button3_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = Multiplication(figure, scaleUp);
			DrawFigure();
		}

		//Button that scalesDown the figure
		private void button4_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = Multiplication(figure, scaleDown);
			DrawFigure();
		}

		//Button that reflects the figure
		private void button5_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = Multiplication(figure, reflect);
			DrawFigure();
		}

		//Button that transfers the figure
		private void button6_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = Multiplication(figure, transfer);
			DrawFigure();
		}

		//Button that resets the figure
		private void button7_Click(object sender, EventArgs e)
        {
			Refresh();
			figure = reset;
			DrawFigure();
		}
    }
}
