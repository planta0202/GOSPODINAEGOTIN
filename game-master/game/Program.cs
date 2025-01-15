using System;
using System.Threading;

class DinoRunGame
{
    private const char Dino = '^';    // Dino representation
    private const char Obstacle = '#'; // Obstacle representation
    private const char EmptySpace = ' '; // Empty space

    private static int dinoX = 5;    // Dino's X position
    private static int dinoY = 10;   // Dino's Y position (ground level)
    private static int obstacleX = 50; // Obstacle's starting X position
    private static int obstacleY = 10; // Obstacle's Y position (ground level)
    private static bool isJumping = false;
    private static int jumpHeight = 4; // How high the dino can jump
    private static int currentJumpHeight = 0;
    private static bool gameOver = false;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(80, 20); // Set a fixed window size for the console game
        Console.SetBufferSize(80, 20); // Ensure the buffer is the same size

        while (!gameOver)
        {
            Update();
            Draw();
            Input();
            Thread.Sleep(100); // Slow down the game loop for a smooth experience
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Game Over! Press any key to exit.");
        Console.ReadKey();
    }

    static void Update()
    {
        if (isJumping)
        {
            if (currentJumpHeight < jumpHeight)
            {
                currentJumpHeight++;
                dinoY--;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            if (currentJumpHeight > 0)
            {
                currentJumpHeight--;
                dinoY++;
            }
        }

        // Move the obstacle leftwards
        obstacleX--;

        if (obstacleX < 0)
        {
            obstacleX = 79; // Reset obstacle to the far right
        }

        // Check for collision
        if (dinoX == obstacleX && dinoY == obstacleY)
        {
            gameOver = true;
        }
    }

    static void Draw()
    {
        Console.Clear();

        // Draw the Dino and Obstacle
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (x == dinoX && y == dinoY)
                {
                    Console.Write(Dino);
                }
                else if (x == obstacleX && y == obstacleY)
                {
                    Console.Write(Obstacle);
                }
                else
                {
                    Console.Write(EmptySpace);
                }
            }
            Console.WriteLine();
        }
    }

    static void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;

            // Jump when the space bar is pressed
            if (key == ConsoleKey.Spacebar && !isJumping)
            {
                isJumping = true;
            }
        }
    }
}
