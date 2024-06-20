using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Media;
using System.Threading;

namespace Sector_Twelve
{
    public partial class Form1 : Form
    {
        int c = 0;
        int m;
        int x, y;
        int a;

        //SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.HELL);
        //SoundPlayer soundPlayer2 = new SoundPlayer(Properties.Resources.TaDa);

        Rectangle background = new Rectangle(0, 0, 800, 800);

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Pen blackPen = new Pen(Color.Black);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        new List<Rectangle> ballList = new List<Rectangle>();
        new List<string> ballColour = new List<string>();

        Rectangle Ralph = new Rectangle(160, 360, 40, 40);

        Rectangle ZombA = new Rectangle(120, 280, 40, 40);
        Rectangle ZombB = new Rectangle(240, 360, 40, 40);
        Rectangle ZombC = new Rectangle(240, 200, 40, 40);
        Rectangle ZombD = new Rectangle(120, 120, 40, 40);
        Rectangle ZombABite = new Rectangle(160, 280, 40, 40);
        Rectangle ZombBBite = new Rectangle(200, 360, 40, 40);
        Rectangle ZombBBite2 = new Rectangle(240, 320, 40, 40);
        Rectangle ZombCBite = new Rectangle(200, 200, 40, 40);
        Rectangle ZombDBite = new Rectangle(160, 120, 40, 40);
        public Form1()
        {
            InitializeComponent();
        }

        public void InitializeGame()
        {
            gameTimer.Enabled = true;
            descLabel.Enabled = false;
            titleLabel.Enabled = false;
            subtitleLabel.Enabled = false;
            descLabel.Visible = false;
            titleLabel.Visible = false;
            subtitleLabel.Visible = false;
            titleLabel.Text = "";
            subtitleLabel.Text = "";

            //Planned Time Limit & Zombie Kill system
            //time = 500;
            //score = 0;

            ballColour.Clear();

            int y = 0;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col <4; col++)
                {
                    int x = 120 + col * 40;

                    ballList.Add(new Rectangle(x, y, 40, 40));
                    ballColour.Add("white");
                }

                y += 40;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle yellOne = new Rectangle(Ralph.X + 40, Ralph.Y, 40, 40);
            Rectangle yellTwo = new Rectangle(Ralph.X - 40, Ralph.Y, 40, 40);
            Rectangle yellThree = new Rectangle(Ralph.X, Ralph.Y - 40, 40, 40);
            Rectangle yellFour = new Rectangle(Ralph.X, Ralph.Y + 40, 40, 40);

            // Checking where mouse was
            //label1.Text = $"{x}, {y}";

            int yOne = Ralph.Y + 40;
            int yTwo = Ralph.Y - 40;

            if (gameTimer.Enabled == false)
            {
                e.Graphics.FillRectangle(blackBrush, background);
                titleLabel.Text = "Sector Twelve";
                subtitleLabel.Text = "Press Space to Begin";
            }
            else if (m == 2)
            {
                subtitleLabel.Visible = true;
                titleLabel.Visible = true;
                subtitleLabel.Text = "Thank you for playing\nPress Space to Exit";
                titleLabel.Text = "You Escaped! Victory!";
            }
            else if (m == 3)
            {
                titleLabel.Visible = true;
                subtitleLabel.Visible = true;
                titleLabel.Text = "Zombies ate you!";
                subtitleLabel.Text = "Game Over...\nPress Space to Exit";
            }
            else if (c == 1)
            {
                descLabel.Visible = true;
                descLabel.Text = "In an alternate 1981, HIV has mutated into a deadly virus which takes over their victim's mind before turning them into mindless husks of their former selves that attack all and spread the infection. You play as Ralph who is making his way to a military checkpoint to reach a safe zone established by the government. Don't get caught and make through the streets without being touched by these filths.\n\nPress Return to Start";
            }
            else if (gameTimer.Enabled == true && c == 2)
            {
                descLabel.Text = "";
                e.Graphics.FillRectangle(blackBrush, 0, 0, 120, this.Height);
                e.Graphics.FillRectangle(whiteBrush, 120, 0, 160, this.Height);
                e.Graphics.FillRectangle(blackBrush, 280, 0, 120, this.Height);

                for (int i = 0; i < ballList.Count; i++)
                {
                    if (ballColour[i] == "white")
                    {
                        e.Graphics.FillRectangle(whiteBrush, ballList[i]);
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    int x = i * 40;
                    e.Graphics.DrawLine(blackPen, x, 0, x, this.Height);
                }
                for (int i = 0; i < 10; i++)
                {
                    int y = i * 40;
                    e.Graphics.DrawLine(blackPen, 0, y, this.Width, y);
                }

                e.Graphics.FillRectangle(blueBrush, Ralph);
                e.Graphics.FillRectangle(redBrush, ZombA);
                e.Graphics.FillRectangle(redBrush, ZombB);
                e.Graphics.FillRectangle(redBrush, ZombC);

                if (a == 1)
                {
                    e.Graphics.FillRectangle(redBrush, ZombD);
                }

                if (m == 1)
                {
                    e.Graphics.FillRectangle(yellowBrush, yellOne);
                    e.Graphics.FillRectangle(yellowBrush, yellTwo);
                    e.Graphics.FillRectangle(yellowBrush, yellThree);
                    if (yellFour.Y < this.Height)
                    {
                        e.Graphics.FillRectangle(yellowBrush, yellFour);
                    }
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            x = MousePosition.X - this.Location.X;
            y = MousePosition.Y - this.Location.Y;

            if (Ralph.Y == 160)
            {
                a = 1;
            }

            if (Ralph.Y == 0)
            {
                m = 2;
            }

            if (Ralph.IntersectsWith(ZombABite) || Ralph.IntersectsWith(ZombBBite) || Ralph.IntersectsWith(ZombBBite2) || Ralph.IntersectsWith(ZombCBite) || Ralph.IntersectsWith(ZombDBite))
            {
                m = 3;
            }

            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Left):
                    if (y > Ralph.Y && x > Ralph.X && y < (Ralph.Y + 40) && x < (Ralph.X + 40) && c == 2)
                    {
                        m = 1;
                    }
                    else if (y > Ralph.Y && x > Ralph.X + 40 && y < (Ralph.Y + 40) && x < (Ralph.X + 80) && m == 1 && x < 280)
                    {
                        Ralph.X += 40;
                    }
                    else if (y > Ralph.Y && x > Ralph.X - 40 && y < (Ralph.Y + 40) && x < Ralph.X && m == 1 && x > 120)
                    {
                        Ralph.X -= 40;
                    }
                    else if (y < Ralph.Y && x > Ralph.X && y > (Ralph.Y - 40) && x < (Ralph.X + 40) && m == 1)
                    {
                        Ralph.Y -= 40;
                    }
                    else if (y > Ralph.Y + 40 && x > Ralph.X && y < (Ralph.Y + 80) && x < (Ralph.X + 40) && m == 1 && x < 400)
                    {
                        Ralph.Y += 40;
                    }
                    break;
                case (MouseButtons.Right):
                    m = 0;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (c == 1)
                    {
                        c = 2;
                    }
                    break;
                case Keys.Space:
                    if (c == 0)
                    {
                        c = 1;
                        InitializeGame();
                    }
                    else if (m == 3 || m == 2)
                    {
                        Application.Exit();
                    }
                    break;
            }
        }
    }
}