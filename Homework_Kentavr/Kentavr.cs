using System;

const int SIZE = 8;
int[,] board = new int[SIZE, SIZE];


int[] rookDx = new int[] { 1, -1, 0, 0 };
int[] rookDy = new int[] { 0, 0, 1, -1 };


int[] knightDx = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
int[] knightDy = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

void PrintBoard()
{
    for (int i = 0; i < SIZE; i++)
    {
        for (int j = 0; j < SIZE; j++)
        {
            if (board[i, j] == 1)
            {
                Console.Write("K ");
            }
            else if (board[i, j] == -1)
            {
                Console.Write("x ");
            }
            else
            {
                int covered = Count(i, j);
                Console.Write($"{covered} ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void Cover(int[,] boardd, int x, int y)
{
 
    for (int i = 0; i < 4; i++)
    {
        int nx = x + rookDx[i], ny = y + rookDy[i];
        while (nx >= 0 && nx < SIZE && ny >= 0 && ny < SIZE)
        {
            if (boardd[nx, ny] == 1) 
            {
                break;
            }
            if (boardd[nx, ny] == 0)
            {
                boardd[nx, ny] = -1;
            }
            nx += rookDx[i];
            ny += rookDy[i];
        }
    }


    for (int i = 0; i < 8; i++)
    {
        int nx = x + knightDx[i], ny = y + knightDy[i];
        if (nx >= 0 && nx < SIZE && ny >= 0 && ny < SIZE && boardd[nx, ny] == 0)
        {
            boardd[nx, ny] = -1;  
        }
    }
}

int Count(int x, int y)
{
    int count = 0;
    int[,] tempBoard = (int[,])board.Clone();
    Cover(tempBoard, x, y);  
    foreach (int cell in tempBoard)
    {
        if (cell == 0) count++;  
    }
    return count;
}

Console.Write("Enter start X (0-7): ");
int startX = int.Parse(Console.ReadLine());

Console.Write("Enter start Y (0-7): ");
int startY = int.Parse(Console.ReadLine());

Console.WriteLine();
board[startX, startY] = 1;
Cover(board, startX, startY);
PrintBoard();

while (true)
{
    int maxFreedom = -1;
    int nextX = -1, nextY = -1;

    for (int i = 0; i < SIZE; i++)
    {
        for (int j = 0; j < SIZE; j++)
        {
            if (board[i, j] == 0)
            {
                int freedom = Count(i, j);
                if (freedom > maxFreedom)
                {
                    maxFreedom = freedom;
                    nextX = i;
                    nextY = j;
                }
            }
        }
    }

    if (nextX == -1 || nextY == -1)
    {
        Console.WriteLine("No more valid moves.");
        break;
    }

    board[nextX, nextY] = 1;
    Cover(board, nextX, nextY);
    Console.WriteLine($"Placed Kentavr at ({nextX},{nextY})");
    PrintBoard();
}
