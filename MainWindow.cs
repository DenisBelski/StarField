using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarField
{
    public partial class MainWindow : Form
    {
        private Star[] stars = new Star[15000];
        private Random random = new Random();
        private Graphics graphics;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            foreach (var star in stars)
            {
                DrawStar(star);
                MoveStar(star);
            }

            pictureBox.Refresh();
        }

        private void MoveStar(Star star)
        {
            star.Z -= 10;
            if (star.Z < 1)
            {
                star.X = random.Next(-pictureBox.Width, pictureBox.Width);
                star.Y = random.Next(-pictureBox.Height, pictureBox.Height);
                star.Z = random.Next(1, pictureBox.Width);
            }
        }

        private void DrawStar(Star star)
        {
            float starSize = Map(star.Z, 0, pictureBox.Width, 5, 0);
            float x = Map(star.X / star.Z, 0, 1, 0, pictureBox.Width) + pictureBox.Width / 2;
            float y = Map(star.Y / star.Z, 0, 1, 0, pictureBox.Height) + pictureBox.Height / 2;

            graphics.FillEllipse(Brushes.White, x, y, starSize, starSize);
        }

        private float Map (float currentCoordinate, float sourceCoordinateStart, 
            float sourceCoordinateStop, float shiftCoordinateStart, float shiftCoordinateStop)
        {
            return ((currentCoordinate - sourceCoordinateStart) 
                    / (sourceCoordinateStop - sourceCoordinateStart)) 
                    * (shiftCoordinateStop - shiftCoordinateStart) 
                    + shiftCoordinateStart;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star()
                {
                    X = random.Next(-pictureBox.Width, pictureBox.Width),
                    Y = random.Next(-pictureBox.Height, pictureBox.Height),
                    Z = random.Next(1, pictureBox.Width)
                };
            }

            timer.Start();
        }
    }
}
