using System;
using System.Collections.Generic;

namespace Tic_tac_toe.Models
{
    internal class TicTacToeGame
    {
        public const int Size = 3;
        public const char Empty = ' ';
        public const char Human = 'X';
        public const char Computer = 'O';

        public char[,] Board { get; private set; } = new char[Size, Size];
        public bool GameOver { get; private set; }

        public TicTacToeGame()
        {
            NewGame();
        }

        public void NewGame()
        {
            for (int row = 0; row < Size; row++)
                for (int col = 0; col < Size; col++)
                    Board[row, col] = Empty;

            GameOver = false;
        }

        public bool MakeMove(int row, int col, char player)
        {
            if (GameOver || Board[row, col] != Empty)
                return false;

            Board[row, col] = player;

            if (CheckWinner(player) || IsDraw())
                GameOver = true;

            return true;
        }

        public List<(int row, int col)> GetAvailableMoves()
        {
            var moves = new List<(int, int)>();

            for (int row = 0; row < Size; row++)
                for (int col = 0; col < Size; col++)
                    if (Board[row, col] == Empty)
                        moves.Add((row, col));

            return moves;
        }

        public bool CheckWinner(char player)
        {
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, 0] == player &&
                    Board[i, 1] == player &&
                    Board[i, 2] == player)
                    return true;

                if (Board[0, i] == player &&
                    Board[1, i] == player &&
                    Board[2, i] == player)
                    return true;
            }

            return
                Board[0, 0] == player &&
                Board[1, 1] == player &&
                Board[2, 2] == player
                ||
                Board[0, 2] == player &&
                Board[1, 1] == player &&
                Board[2, 0] == player;
        }

        public bool IsDraw()
        {
            return GetAvailableMoves().Count == 0;
        }

        public (int row, int col)? ChooseComputerMove(int level)
        {
            var moves = GetAvailableMoves();

            if (moves.Count == 0)
                return null;

            
            if (level == 3)
                foreach (var move in moves)
                    if (TestMove(move, Computer))
                        return move;

            
            if (level >= 2)
                foreach (var move in moves)
                    if (TestMove(move, Human))
                        return move;

            
            return moves[new Random().Next(moves.Count)];
        }

        private bool TestMove((int row, int col) move, char player)
        {
            Board[move.row, move.col] = player;
            bool win = CheckWinner(player);
            Board[move.row, move.col] = Empty;

            return win;
        }
    }
}