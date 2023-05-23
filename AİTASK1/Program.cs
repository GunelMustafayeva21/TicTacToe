using System;

class TicTacToe
{
    private char[,] board;
    private const int SIZE = 3;
    private const char PLAYER = 'X';
    private const char AI = 'O';

    public TicTacToe()
    {
        board = new char[SIZE, SIZE];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                board[row, col] = ' ';
            }
        }
    }

    public void Play()
    {
        Console.WriteLine("Welcome to Tic Tac Toe!");

        while (!IsGameOver())
        {
            PrintBoard();

            if (IsPlayerTurn())
            {
                PlayerMove();
            }
            else
            {
                AIMove();
            }
        }

        PrintBoard();
        DeclareResult();
    }

    private void PrintBoard()
    {
        Console.WriteLine("---------");
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Console.Write($"| {board[row, col]} ");
            }
            Console.WriteLine("|");
            Console.WriteLine("---------");
        }
    }

    private bool IsPlayerTurn()
    {
        // Count the number of empty cells
        int emptyCells = 0;
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (board[row, col] == ' ')
                {
                    emptyCells++;
                }
            }
        }

        // If the number of empty cells is even, it's player's turn
        return emptyCells % 2 == 0;
    }

    private void PlayerMove()
    {
        Console.WriteLine("Your turn. Enter row (0-2): ");
        int row = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter column (0-2): ");
        int col = int.Parse(Console.ReadLine());

        if (IsValidMove(row, col))
        {
            board[row, col] = PLAYER;
        }
        else
        {
            Console.WriteLine("Invalid move. Try again.");
            PlayerMove();
        }
    }

    private bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < SIZE && col >= 0 && col < SIZE && board[row, col] == ' ';
    }

    private void AIMove()
    {
        int bestScore = int.MinValue;
        int bestRow = -1;
        int bestCol = -1;

        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = AI;
                    int score = Minimax(board, 0, false);
                    board[row, col] = ' ';

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestRow = row;
                        bestCol = col;
                    }
                }
            }
        }

        board[bestRow, bestCol] = AI;
        Console.WriteLine("AI's turn. AI placed its move.");
    }

    private int Minimax(char[,] board, int depth, bool isMaximizing)
    {
        if (IsGameOver())
        {
            int score = Evaluate();
            return score;
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        board[row, col] = AI;
                        int score = Minimax(board, depth + 1, false);
                        board[row, col] = ' ';
                        bestScore = Math.Max(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        board[row, col] = PLAYER;
                        int score = Minimax(board, depth + 1, true);
                        board[row, col] = ' ';
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
    }

    private bool IsGameOver()
    {
        return IsPlayerWinner() || IsAIWinner() || IsBoardFull();
    }

    private bool IsPlayerWinner()
    {
        return CheckRows(PLAYER) || CheckColumns(PLAYER) || CheckDiagonals(PLAYER);
    }

    private bool IsAIWinner()
    {
        return CheckRows(AI) || CheckColumns(AI) || CheckDiagonals(AI);
    }

    private bool IsBoardFull()
    {
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (board[row, col] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckRows(char player)
    {
        for (int row = 0; row < SIZE; row++)
        {
            if (board[row, 0] == player && board[row, 1] == player && board[row, 2] == player)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckColumns(char player)
    {
        for (int col = 0; col < SIZE; col++)
        {
            if (board[0, col] == player && board[1, col] == player && board[2, col] == player)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDiagonals(char player)
    {
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
        {
            return true;
        }

        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
        {
            return true;
        }

        return false;
    }

    private int Evaluate()
    {
        if (IsPlayerWinner())
        {
            return -1;
        }
        else if (IsAIWinner())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void DeclareResult()
    {
        if (IsPlayerWinner())
        {
            Console.WriteLine("Congratulations! You won!");
        }
        else if (IsAIWinner())
        {
            Console.WriteLine("You lost! Better luck next time!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }

    static void Main(string[] args)
    {
        TicTacToe game = new TicTacToe();
        game.Play();
    }
}

