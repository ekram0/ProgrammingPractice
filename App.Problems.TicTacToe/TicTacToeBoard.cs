﻿using System;

namespace App.Problems.TicTacToe
{
  class TicTacToeBoard
  {
    public TicTacToeBoard(int size)
    {
      Size = size;
      Cells = new int[size, size];
      EraseBoard();
    }

    public int Size { get; private set; }

    public int TotalMovesMade { get; private set; }

    public int RemainingMoves => Size * Size - TotalMovesMade;

    private int[,] Cells { get; set; }

    private bool CheckForWinAtRow(int row, int playerId)
    {
      // I could do this with a LINQ expression too, but for
      // clarity and explicitness, I've chosen to use a standard
      // for loop to keep it free of extraneous dependencies. I
      // think this is in keeping with the intent of the challenge.

      for (int col=0; col < Size; col++)
      {
        if (Cells[row, col] != playerId)
          return false;
      }

      return true;
    }

    private bool CheckForWinAtColumn(int col, int playerId)
    {
      for (int row=0; row < Size; row++)
      {
        if (Cells[row, col] != playerId)
          return false;
      }

      return true;
    }

    private bool IsCellAlongDiagnoal1(int row, int col)
    {
      return row == col;
    }

    private bool IsCellAlongDiagonal2(int row, int col)
    {
      return row == Size - 1 - col;
    }

    private bool CheckForWinAtDiagonal1(int playerId)
    {
      for (int row=0; row < Size; row++)
      {
        if (Cells[row, row] != playerId)
          return false;
      }

      return true;
    }

    private bool CheckForWinAtDiagonal2(int playerId)
    {
      for (int row=Size - 1; row >= 0; row--)
      {
        if (Cells[row, Size - 1 - row] != playerId)
          return false;
      }

      return true;
    }

    private bool HasPlayerWonAfterMove(int row, int col, int playerId)
    {
      return (
        CheckForWinAtRow(row, playerId) ||
        CheckForWinAtColumn(col, playerId) ||
        IsCellAlongDiagnoal1(row, col) && CheckForWinAtDiagonal1(playerId) ||
        IsCellAlongDiagonal2(row, col) && CheckForWinAtDiagonal2(playerId)
      );
    }

    public void EraseBoard()
    {
      for (int row=0; row < Size; row++)
      {
        for (int col=0; col < Size; col++)
          Cells[row, col] = 0;
      }

      TotalMovesMade = 0;
    }

    public bool MakeMoveAndCheckForWin(int row, int col, int playerId)
    {
      if (Cells[row, col] != 0)
        throw new Exception("The cell is not available");

      if (TotalMovesMade == Size * Size)
        throw new Exception("The board is full");

      Cells[row, col] = playerId;
      TotalMovesMade++;

      return HasPlayerWonAfterMove(row, col, playerId);
    }
  }
}
