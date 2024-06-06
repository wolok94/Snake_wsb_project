using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class Game
{
    private int screenWidth = 40;
    private int screenHeight = 20;
    private int snakeX;
    private int snakeY;
    private int fruitX;
    private int fruitY;
    private int score;
    private bool gameOver;
    private List<int[]> snakeBody;
    private Random random;
    private Direction currentDirection;

    public Game()
    {
        snakeX = screenWidth / 2;
        snakeY = screenHeight / 2;
        score = 0;
        gameOver = false;
        snakeBody = new List<int[]>();
        random = new Random();
        currentDirection = Direction.Right;
    }

    public void Start()
    {
        Console.CursorVisible = false;
        snakeBody.Add(new int[] { snakeX, snakeY });
        SpawnFruit();

        while (!gameOver)
        {
            Draw();
            Input();
            Logic();
            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Game Over! Final Score: " + score);
        Console.ReadKey();
    }

    private void Draw()
    {
        Console.Clear();
        for (int i = 0; i < screenHeight; i++)
        {
            for (int j = 0; j < screenWidth; j++)
            {
                if (i == 0 || i == screenHeight - 1 || j == 0 || j == screenWidth - 1)
                {
                    Console.Write("#");
                }
                else if (i == snakeY && j == snakeX)
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
                    foreach (var part in snakeBody)
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

    private void Logic()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                snakeY--;
                break;
            case Direction.Down:
                snakeY++;
                break;
            case Direction.Left:
                snakeX--;
                break;
            case Direction.Right:
                snakeX++;
                break;
        }

        if (snakeX == fruitX && snakeY == fruitY)
        {
            score++;
            SpawnFruit();
            snakeBody.Add(new int[] { snakeX, snakeY });
        }

        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i][0] = snakeBody[i - 1][0];
            snakeBody[i][1] = snakeBody[i - 1][1];
        }
        if (snakeBody.Count > 0)
        {
            snakeBody[0][0] = snakeX;
            snakeBody[0][1] = snakeY;
        }

        if (snakeX <= 0 || snakeX >= screenWidth - 1 || snakeY <= 0 || snakeY >= screenHeight - 1)
        {
            gameOver = true;
        }

        for (int i = 1; i < snakeBody.Count; i++)
        {
            if (snakeX == snakeBody[i][0] && snakeY == snakeBody[i][1])
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




    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}