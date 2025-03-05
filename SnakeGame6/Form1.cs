namespace SnakeGame6
{
    public partial class Form1 : Form
    {
        int maxHeight, maxWidth;
        SnakeGame Snakegame;
        public Form1()
        {
            InitializeComponent();
            new Utility();
            Snakegame = new SnakeGame(maxHeight, maxWidth); 
            KeyPreview = true;
        }
        private void RefreshScore()
        {
            player1score.Text = Convert.ToString(Snakegame.PlayerOne.SnakePoints);
            player2score.Text = Convert.ToString(Snakegame.PlayerTwo.SnakePoints);
            player3score.Text = Convert.ToString(Snakegame.PlayerThree.SnakePoints);
        }
        private void Timer_Event(object sender, EventArgs e)
        {
            Snakegame.ExecuteSnakeGame();
            RefreshScore();
            GameOver();
            pictureBox1.Invalidate();
        }
      
        //startgame/restart game button
        private void button1_Click(object sender, EventArgs e)
        {
            maxHeight = pictureBox1.Height / Utility.Height - 1;
            maxWidth = pictureBox1.Width / Utility.Width - 1;
            Snakegame = new SnakeGame(maxHeight, maxWidth);
            button1.Text = "RESTART GAME";
            VictoryPrompt.Text = "";
            Timer.Start();
        }
        private void RefreshGraphics(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            foreach(var snakepart in Snakegame.PlayerOne.SnakePlayer)
            {
                graphics.FillRectangle(Snakegame.PlayerOne.SnakeColor, new Rectangle(
                    snakepart.X * Utility.Width, snakepart.Y * Utility.Height, Utility.Width, Utility.Height));
            }
            foreach (var snakepart in Snakegame.PlayerTwo.SnakePlayer)
            {
                graphics.FillRectangle(Snakegame.PlayerTwo.SnakeColor, new Rectangle(
                    snakepart.X * Utility.Width, snakepart.Y * Utility.Height, Utility.Width, Utility.Height));
            }
            foreach (var snakepart in Snakegame.PlayerThree.SnakePlayer)
            {
                graphics.FillRectangle(Snakegame.PlayerThree.SnakeColor, new Rectangle(
                    snakepart.X * Utility.Width, snakepart.Y * Utility.Height, Utility.Width, Utility.Height));
            }
            foreach (var food in Snakegame.FoodManager.Foods)
            {
                graphics.FillRectangle(food.FoodColor, new Rectangle(
                    food.X * Utility.Width, food.Y * Utility.Height, Utility.Width, Utility.Height));
            }
        }

        private void GameOver()
        {
            if(Snakegame.PlayerOne.SnakeState == false && Snakegame.PlayerTwo.SnakeState == false && Snakegame.PlayerThree.SnakeState == false)
            {
                if (Snakegame.PlayerOne.SnakePoints > Snakegame.PlayerTwo.SnakePoints && Snakegame.PlayerOne.SnakePoints > Snakegame.PlayerThree.SnakePoints)
                {
                    VictoryPrompt.Text = "Player1 is Victorious!";
                }
                else if (Snakegame.PlayerTwo.SnakePoints > Snakegame.PlayerOne.SnakePoints && Snakegame.PlayerTwo.SnakePoints > Snakegame.PlayerThree.SnakePoints)
                {
                    VictoryPrompt.Text = "Player2 is Victorious!";
                }
                else if (Snakegame.PlayerThree.SnakePoints > Snakegame.PlayerTwo.SnakePoints && Snakegame.PlayerThree.SnakePoints > Snakegame.PlayerOne.SnakePoints)
                {
                    VictoryPrompt.Text = "Player3 is Victorious!";
                }
                else if (Snakegame.PlayerThree.SnakePoints == Snakegame.PlayerOne.SnakePoints)
                {
                    VictoryPrompt.Text = "Player1 and Player3 Draw";
                }
                else if (Snakegame.PlayerTwo.SnakePoints == Snakegame.PlayerOne.SnakePoints)
                {
                    VictoryPrompt.Text = "Player1 and Player2 Draw";
                }
                else if (Snakegame.PlayerTwo.SnakePoints == Snakegame.PlayerThree.SnakePoints)
                {
                    VictoryPrompt.Text = "Player3 and Player2 Draw";
                }
                else
                {
                    VictoryPrompt.Text = "No one wins its a complete Draw";
                }

                Timer.Stop();
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (Snakegame.PlayerOne.RandomState == true || Snakegame.PlayerTwo.RandomState == true || Snakegame.PlayerThree.RandomState == true)
            {
                int current_key = -1;
                var random_control = new Random();
                current_key = random_control.Next(4);
                switch (current_key)
                {
                    case 0:
                        current_key = 0;
                        break;
                    case 1:
                        current_key = 1;
                        break;
                    case 2:
                        current_key = 2;
                        break;
                    case 3:
                        current_key = 3;
                        break;
                }

                if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D && Snakegame.PlayerOne.RandomState == true)
                {
                    if (current_key == 0 && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Left)
                    {
                        Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Right;
                    }
                    else if (current_key == 1 && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Right)
                    {
                        Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Left;
                    }
                    else if (current_key == 2 && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Down)
                    {
                        Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Up;
                    }
                    else if (current_key == 3 && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Up)
                    {
                        Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Down;
                    }
                }
                if (e.KeyCode == Keys.I || e.KeyCode == Keys.J || e.KeyCode == Keys.K || e.KeyCode == Keys.L && Snakegame.PlayerTwo.RandomState == true)
                {
                    if (current_key == 0 && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Left)
                    {
                        Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Right;
                    }
                    else if (current_key == 1 && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Right)
                    {
                        Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Left;
                    }
                    else if (current_key == 2 && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Down)
                    {
                        Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Up;
                    }
                    else if (current_key == 3 && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Up)
                    {
                        Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Down;
                    }
                }
                if (e.KeyCode == Keys.T || e.KeyCode == Keys.G || e.KeyCode == Keys.F || e.KeyCode == Keys.H && Snakegame.PlayerThree.RandomState == true)
                {
                    if (current_key == 0 && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Left)
                    {
                        Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Right;
                    }
                    else if (current_key == 1 && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Right)
                    {
                        Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Left;
                    }
                    else if (current_key == 2 && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Down)
                    {
                        Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Up;
                    }
                    else if (current_key == 3 && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Up)
                    {
                        Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Down;
                    }
                }
            }
            //player1
            if (e.KeyCode == Keys.W && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Down && Snakegame.PlayerOne.RandomState == false)
            {
                Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Up;
            }
            else if (e.KeyCode == Keys.S && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Up && Snakegame.PlayerOne.RandomState == false)
            {
                Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Down;
            }
            else if (e.KeyCode == Keys.A && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Right && Snakegame.PlayerOne.RandomState == false)
            {
                Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Left;
            }
            else if (e.KeyCode == Keys.D && Snakegame.PlayerOne.SnakeDirection != Utility.Direction.Left && Snakegame.PlayerOne.RandomState == false)
            {
                Snakegame.PlayerOne.SnakeDirection = Utility.Direction.Right;
            }
            //player2
            if (e.KeyCode == Keys.I && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Down && Snakegame.PlayerTwo.RandomState == false)
            {
                Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Up;
            }
            else if (e.KeyCode == Keys.K && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Up && Snakegame.PlayerTwo.RandomState == false)
            {
                Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Down;
            }
            else if (e.KeyCode == Keys.J && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Right && Snakegame.PlayerTwo.RandomState == false)
            {
                Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Left;
            }
            else if (e.KeyCode == Keys.L && Snakegame.PlayerTwo.SnakeDirection != Utility.Direction.Left && Snakegame.PlayerTwo.RandomState == false)
            {
                Snakegame.PlayerTwo.SnakeDirection = Utility.Direction.Right;
            }
            //Player3
            if (e.KeyCode == Keys.T && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Down && Snakegame.PlayerTwo.RandomState == false)
            {
                Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Up;
            }
            else if (e.KeyCode == Keys.G && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Up && Snakegame.PlayerThree.RandomState == false)
            {
                Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Down;
            }
            else if (e.KeyCode == Keys.F && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Right && Snakegame.PlayerThree.RandomState == false)
            {
                Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Left;
            }
            else if (e.KeyCode == Keys.H && Snakegame.PlayerThree.SnakeDirection != Utility.Direction.Left && Snakegame.PlayerThree.RandomState == false)
            {
                Snakegame.PlayerThree.SnakeDirection = Utility.Direction.Right;
            }

        }
       
    }
}