using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class SnakeGame
{
    private int screenWidth = 40;
    private int screenHeight = 20;
    private int fruitX;
    private int fruitY;
    private int score;
    private Snake snake = new Snake();
    private bool gameOver;
    private Random random;
    private Direction currentDirection;

    public SnakeGame()
    {
        snake.SnakeX = screenWidth / 2;
        snake.SnakeY = screenHeight / 2;
        score = 0;
        gameOver = false;
        snake.SnakeBody = new List<int[]>();
        random = new Random();
        currentDirection = Direction.Right;
    }


    private void Draw()
    {
        Console.Clear();
        for (int i = 0; i < screenHeight; i++)
        {
            for (int j = 0; j < screenWidth; j++)
            {
                if (i == 0 || i == screenHeight - 1)
                {
                    Console.Write("-");
                }
                else if (j == 0 || j == screenWidth - 1)
                {
                    Console.Write("I");
                }
                else if (i == snake.SnakeY && j == snake.SnakeX)
                {
                    Console.Write("O");
                }
                else if (i == fruitY && j == fruitX)
                {
                    Console.Write("*");
                }
                else
                {
                    bool isBody = false;
                    foreach (var part in snake.SnakeBody)
                    {
                        if (part[0] == j && part[1] == i)
                        {
                            Console.Write("o");
                            isBody = true;
                            break;
                        }
                    }
                    if (!isBody)
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("Score: " + score);
    }

    public void Start()
    {
        Console.CursorVisible = false;
        snake.SnakeBody.Add(new int[] { snake.SnakeX, snake.SnakeY });
        SpawnFruit();

        while (!gameOver)
        {
            Draw();
            Input();
            Move();
            Thread.Sleep(100);
        }
    }

    private void Move()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                snake.SnakeY--;
                break;
            case Direction.Down:
                snake.SnakeY++;
                break;
            case Direction.Left:
                snake.SnakeX--;
                break;
            case Direction.Right:
                snake.SnakeX++;
                break;
        }

        if (snake.SnakeX == fruitX && snake.SnakeY == fruitY)
        {
            score++;
            SpawnFruit();
            snake.SnakeBody.Add(new int[] { snake.SnakeX, snake.SnakeY });
        }

        for (int i = snake.SnakeBody.Count - 1; i > 0; i--)
        {
            snake.SnakeBody[i][0] = snake.SnakeBody[i - 1][0];
            snake.SnakeBody[i][1] = snake.SnakeBody[i - 1][1];
        }
        if (snake.SnakeBody.Count > 0)
        {
            snake.SnakeBody[0][0] = snake.SnakeX;
            snake.SnakeBody[0][1] = snake.SnakeY;
        }

        if (snake.SnakeX <= 0 || snake.SnakeX >= screenWidth - 1 || snake.SnakeY <= 0 || snake.SnakeY >= screenHeight - 1)
        {
            gameOver = true;
        }

        for (int i = 1; i < snake.SnakeBody.Count; i++)
        {
            if (snake.SnakeX == snake.SnakeBody[i][0] && snake.SnakeY == snake.SnakeBody[i][1])
            {
                gameOver = true;
                break;
            }
        }


    }

    private void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                    break;
            }
        }
    }

    private void SpawnFruit()
    {
        fruitX = random.Next(1, screenWidth - 1);
        fruitY = random.Next(1, screenHeight - 1);
    }
}
