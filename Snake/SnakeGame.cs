using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class SnakeGame
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

    public SnakeGame()
    {
        snakeX = screenWidth / 2;
        snakeY = screenHeight / 2;
        score = 0;
        gameOver = false;
        snakeBody = new List<int[]>();
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

    public void Start()
    {
        Console.CursorVisible = false;
        snakeBody.Add(new int[] { snakeX, snakeY });


        while (!gameOver)
        {
            Draw();
            Thread.Sleep(100);
        }
    }
}
