namespace Snake
{
    internal class Program
    {
        // Define the width and height of the game area
        const int width = 80;
        const int height = 40;

        // Define the symbols used for the snake and food
        const char snakeSymbol = '*';
        const char foodSymbol = '@';

        // Define the directions for the snake
        enum Direction { Up, Down, Left, Right };

        // Define a structure for a point in the game area
        struct Point
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            // Initialize the game
            Console.CursorVisible = false;
            Console.SetWindowSize(width + 1, height + 1);

            // Initialize the snake
            List<Point> snake = new List<Point>();
            snake.Add(new Point { X = width / 2, Y = height / 2 });
            Direction snakeDirection = Direction.Right;

            // Initialize the food
            Point food = GenerateFood(snake);

            // Game loop
            while (true)
            {
                // Check for input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (snakeDirection != Direction.Down)
                                snakeDirection = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            if (snakeDirection != Direction.Up)
                                snakeDirection = Direction.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (snakeDirection != Direction.Right)
                                snakeDirection = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            if (snakeDirection != Direction.Left)
                                snakeDirection = Direction.Right;
                            break;
                    }
                }

                // Move the snake
                Point head = snake[0];
                Point newHead = new Point();
                switch (snakeDirection)
                {
                    case Direction.Up:
                        newHead = new Point { X = head.X, Y = head.Y - 1 };
                        break;
                    case Direction.Down:
                        newHead = new Point { X = head.X, Y = head.Y + 1 };
                        break;
                    case Direction.Left:
                        newHead = new Point { X = head.X - 1, Y = head.Y };
                        break;
                    case Direction.Right:
                        newHead = new Point { X = head.X + 1, Y = head.Y };
                        break;
                }

                // Check for collisions with walls or itself
                if (newHead.X < 0 || newHead.X >= width || newHead.Y < 0 || newHead.Y >= height
                    || snake.Contains(newHead))
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.ReadKey();
                    return;
                }

                snake.Insert(0, newHead);

                // Check if the snake eats the food
                if (newHead.X == food.X && newHead.Y == food.Y)
                {
                    // Increase the length of the snake
                    food = GenerateFood(snake);
                }
                else
                {
                    // Remove the tail of the snake
                    snake.RemoveAt(snake.Count - 1);
                }

                // Draw the game
                DrawGame(snake, food);

                // Delay for smoother gameplay
                Thread.Sleep(100);
            }

            static void DrawGame(List<Point> snake, Point food)
            {
                Console.Clear();

                // Draw the snake
                foreach (Point point in snake)
                {
                    Console.SetCursorPosition(point.X, point.Y);
                    Console.Write(snakeSymbol);
                }

                // Draw the food
                Console.SetCursorPosition(food.X, food.Y);
                Console.Write(foodSymbol);
            }

            static Point GenerateFood(List<Point> snake)
            {
                Random random = new Random();
                Point food;
                do
                {
                    food = new Point { X = random.Next(width), Y = random.Next(height) };
                } while (snake.Contains(food));

                return food;
            }
        }
    }
}
