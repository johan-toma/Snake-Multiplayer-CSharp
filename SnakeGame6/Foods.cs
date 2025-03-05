using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame6
{
    public enum FoodType { ValueableFood, DietFood, RandomControlFood, StandardFood };
    interface IFoods
    {
        Brush FoodColor { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int ValueFood { get; set; }
        int ScoreIncrease { get; set; }
        FoodType FoodType { get; set; }

        void FoodResult(Snake snake);
    }

    abstract class Foods : IFoods
    {
        public Brush FoodColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ValueFood { get; set; }

        public int ScoreIncrease { get; set; }

        public FoodType FoodType { get; set; }

        public abstract void FoodResult(Snake snake);

    }
    class StandardFood : Foods
    {
        public StandardFood()
        {
            FoodColor = Brushes.Yellow;
            ValueFood = 1;
            ScoreIncrease = 1;
            FoodType = FoodType.StandardFood;
        }

        public override void FoodResult(Snake snake)
        {
            snake.SnakePoints += ScoreIncrease;
            for (int i = 0; i < ValueFood; i++)
            {
                Snakepart body = new Snakepart();
                snake.SnakePlayer.Add(body);
            }
        }

    }

    class ValueableFood : Foods
    {
        public ValueableFood()
        {
            FoodColor = Brushes.Magenta;
            ValueFood = 2;
            ScoreIncrease = 5;
            FoodType = FoodType.ValueableFood;
        }
        public override void FoodResult(Snake snake)
        {
            snake.SnakePoints += ScoreIncrease;
            for (int i = 0; i < ValueFood; i++)
            {
                Snakepart body = new Snakepart();
                snake.SnakePlayer.Add(body);
            }
        }
    }

    class DietFood : Foods
    {
        public DietFood()
        {
            FoodColor = Brushes.Green;
            ValueFood = -1;
            ScoreIncrease = 1;
            FoodType = FoodType.DietFood;
        }
        public override void FoodResult(Snake snake)
        {
            snake.SnakePoints += ScoreIncrease;
            if (snake.SnakePlayer.Count > 2) snake.SnakePlayer.RemoveAt(snake.SnakePlayer.Count + ValueFood);
        }
    }

    class RandomControlFood : Foods
    {
        public RandomControlFood()
        {
            ValueFood = 1;
            ScoreIncrease = 1;
            FoodColor = Brushes.White;
            FoodType = FoodType.RandomControlFood;
        }
        public override void FoodResult(Snake snake)
        {
            snake.SnakePoints += ScoreIncrease;
            snake.RandomState = true;

            for (int i = 0; i < ValueFood; i++)
            {
                Snakepart body = new Snakepart();
                snake.SnakePlayer.Add(body);
            }

            snake.Timer = DateTime.Now;
        }
    }

    class FoodLogic
    {
        public List<IFoods> Foods { get;  }
        private Random random;
        private int maxHeight;
        private int maxWidth;

        public FoodLogic(int height, int width)
        {
            maxHeight = height;
            maxWidth = width;
            Foods = new List<IFoods>();
            random = new Random();

            Foods.Add(new DietFood());
            Foods.Add(new ValueableFood());
            Foods.Add(new RandomControlFood());
            Foods.Add(new StandardFood());
        }
        public void RefreshFoods()
        {
            Foods.Clear();
            Foods.Add(new DietFood());
            Foods.Add(new ValueableFood());
            Foods.Add(new RandomControlFood());
            Foods.Add(new StandardFood());
        }
        public void RandomizeFood()
        {
            foreach (var food in Foods)
            {
                food.X = random.Next(5, maxWidth);
                food.Y = random.Next(5, maxHeight);
            }
        }
        public void HandleFoodCollision(Snake Player, IFoods food)
        {
            food.FoodResult(Player);
        }

    }

}
