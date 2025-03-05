using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame6
{
    class Snake
    {
        public Brush SnakeColor { get; }
        public Utility.Direction SnakeDirection { get; set; }
        public List<Snakepart> SnakePlayer { get; }

        public int SnakePoints { get; set; }

        public bool SnakeState { get; set; }

        public bool RandomState { get; set; }

        public DateTime Timer { get; set; }


        public Snake(int x, int y, Brush color)
        {
            SnakeColor = color;
            Utility.Direction SnakeDirection = Utility.Direction.Right;
            SnakePlayer = new List<Snakepart>();
            SnakePlayer.Add(new Snakepart(x, y));

            for (int i = 0; i < 5; i++)
            {
                var body = new Snakepart();
                SnakePlayer.Add(body);
                
            }

            SnakePoints = 0;
            RandomState = false;
            SnakeState = true;
        }

        internal void MoveSnake()
        {
            switch (SnakeDirection)
            {
                case Utility.Direction.Up:
                    for (int i = SnakePlayer.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            SnakePlayer[i].Y--;
                        }
                        else
                        {
                            SnakePlayer[i].X = SnakePlayer[i - 1].X;
                            SnakePlayer[i].Y = SnakePlayer[i - 1].Y;
                        }
                    }
                    break;
                case Utility.Direction.Down:
                    for (int i = SnakePlayer.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            SnakePlayer[i].Y++;

                        }
                        else
                        {
                            SnakePlayer[i].X = SnakePlayer[i - 1].X;
                            SnakePlayer[i].Y = SnakePlayer[i - 1].Y;
                        }
                    }
                    break;
                case Utility.Direction.Left:
                    for (int i = SnakePlayer.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            SnakePlayer[i].X--;

                        }
                        else
                        {
                            SnakePlayer[i].X = SnakePlayer[i - 1].X;
                            SnakePlayer[i].Y = SnakePlayer[i - 1].Y;
                        }
                    }
                    break;
                case Utility.Direction.Right:
                    for (int i = SnakePlayer.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            SnakePlayer[i].X++;
                        }
                        else
                        {
                            SnakePlayer[i].X = SnakePlayer[i - 1].X;
                            SnakePlayer[i].Y = SnakePlayer[i - 1].Y;
                        }
                    }
                    break;
            }
        }

        internal void SelfCollision()
        {
            for(int i = 1; i < SnakePlayer.Count; i++)
            {
                if(SnakePlayer[i].X == SnakePlayer[0].X && SnakePlayer[i].Y == SnakePlayer[0].Y)
                {
                    SnakeState = false;
                    break;
                }
            }
        }




    }

    class SnakeGame
    {
        private Random random;
        public Snake PlayerOne { get;  }
        public Snake PlayerTwo { get;  }
        public Snake PlayerThree { get; }

        public FoodLogic FoodManager { get; }

        private int maxHeight;
        private int maxWidth;

        
        public SnakeGame(int board_height, int board_width)
        {

            PlayerOne = new Snake(10, 10, Brushes.Red);
            PlayerTwo = new Snake(20, 20, Brushes.Blue);
            PlayerThree = new Snake(30, 30, Brushes.DarkMagenta);

            maxHeight = board_height;
            maxWidth = board_width;
            FoodManager = new FoodLogic(maxHeight, maxWidth);
            random = new Random();
            if(maxWidth > 0 && maxHeight > 0) FoodManager.RandomizeFood();
        }

        public void ExecuteSnakeGame()
        {
            TimerRandomControl(PlayerOne);
            TimerRandomControl(PlayerTwo);
            TimerRandomControl(PlayerThree);

            PlayerOne.MoveSnake();
            PlayerTwo.MoveSnake();
            PlayerThree.MoveSnake();

            PlayerOne.SelfCollision();
            PlayerTwo.SelfCollision();
            PlayerThree.SelfCollision();

            CheckOutOfBounds(PlayerOne);
            CheckOutOfBounds(PlayerTwo);
            CheckOutOfBounds(PlayerThree);

            CheckFoodCollision(PlayerOne);
            CheckFoodCollision(PlayerTwo);
            CheckFoodCollision(PlayerThree);

            CheckSnakeCollisions();
            ClearSnakes();

            if (FoodManager.Foods.Count < 4)
            {
                FoodManager.RefreshFoods();
                FoodManager.RandomizeFood();
            }
        }
        private void TimerRandomControl(Snake Snake)
        {
            int count_seconds = 0;
            TimeSpan seconds_past = DateTime.Now - Snake.Timer;
            count_seconds = (int)seconds_past.TotalSeconds;
            if(count_seconds == 10)
            {
                Snake.RandomState = false;
            }
        }
        private void CheckFoodCollision(Snake Player)
        {
            for(int i = 0; i < FoodManager.Foods.Count; i++)
            {
                if(Player.SnakePlayer.Count > 0 && Player.SnakePlayer[0].X == FoodManager.Foods[i].X && Player.SnakePlayer[0].Y == FoodManager.Foods[i].Y)
                {
                    FoodManager.HandleFoodCollision(Player, FoodManager.Foods[i]);
                    FoodManager.Foods.Remove(FoodManager.Foods[i]);
                    break;
                }
            }
        }
       
        
        private void CheckOutOfBounds(Snake snake)
        {
            for (int i = 0; i < snake.SnakePlayer.Count; i++)
            {
                if (snake.SnakePlayer[i].X < 0)
                {
                    snake.SnakePlayer[i].X = maxWidth;
                }
                if (snake.SnakePlayer[i].X > maxWidth)
                {
                    snake.SnakePlayer[i].X = 0;
                }
                if (snake.SnakePlayer[i].Y < 0)
                {
                    snake.SnakePlayer[i].Y = maxHeight;
                }
                if (snake.SnakePlayer[i].Y > maxHeight)
                {
                    snake.SnakePlayer[i].Y = 0;
                }
            }
        }
        private void CheckSnakeCollisions()
        {
            if(CheckHeadCollision(PlayerOne, PlayerTwo))
            {
                PlayerOne.SnakeState = false;
                PlayerTwo.SnakeState = false;
            }

            if (CheckHeadCollision(PlayerOne, PlayerThree))
            {
                PlayerOne.SnakeState = false;
                PlayerThree.SnakeState = false;
            }

            if (CheckHeadCollision(PlayerTwo, PlayerThree))
            {
                PlayerTwo.SnakeState = false;
                PlayerThree.SnakeState = false;
            }

            if (CheckBodyCollision(PlayerOne, PlayerTwo))
            {
                PlayerOne.SnakeState = false;
                PlayerTwo.SnakePoints += 5;
            }
            if (CheckBodyCollision(PlayerOne, PlayerThree))
            {
                PlayerOne.SnakeState = false;
                PlayerThree.SnakePoints += 5;
            }

            if (CheckBodyCollision(PlayerTwo, PlayerThree))
            {
                PlayerTwo.SnakeState = false;
                PlayerThree.SnakePoints += 5;
            }
            if (CheckBodyCollision(PlayerTwo, PlayerOne))
            {
                PlayerTwo.SnakeState = false;
                PlayerOne.SnakePoints += 5;
            }

            if (CheckBodyCollision(PlayerThree, PlayerTwo))
            {
                PlayerThree.SnakeState = false;
                PlayerTwo.SnakePoints += 5;
            }
            if (CheckBodyCollision(PlayerThree, PlayerOne))
            {
                PlayerThree.SnakeState = false;
                PlayerOne.SnakePoints += 5;
            }

        }
        private bool CheckBodyCollision(Snake Snake1, Snake Snake2)
        {

            for (int i = 0; i < Snake1.SnakePlayer.Count; i++)
            {
                for (int j = 1; j < Snake2.SnakePlayer.Count; j++)
                {
                    if (Snake1.SnakePlayer[0].X == Snake2.SnakePlayer[j].X && Snake1.SnakePlayer[0].Y == Snake2.SnakePlayer[j].Y)
                    {
                        return true;
                    }
                }
            }
            //snake1 didnt collide with snake2 body
            return false;
        }
        private bool CheckHeadCollision(Snake Snake1, Snake Snake2)
        {
            for (int i = 0; i < Snake1.SnakePlayer.Count; i++)
            {
                for (int j = 0; j < Snake2.SnakePlayer.Count; j++)
                {
                    if (Snake1.SnakePlayer[0].X == Snake2.SnakePlayer[0].X && Snake1.SnakePlayer[0].Y == Snake2.SnakePlayer[0].Y)
                    {
                        return true;
                    }
                }
            }
            //snake1 and snake2 head collision didnt happen
            return false;
        }
        private void ClearSnakes()
        {
            if (PlayerOne.SnakeState == false) PlayerOne.SnakePlayer.Clear();
            if (PlayerTwo.SnakeState == false) PlayerTwo.SnakePlayer.Clear();
            if (PlayerThree.SnakeState == false) PlayerThree.SnakePlayer.Clear();
        }
    }
}