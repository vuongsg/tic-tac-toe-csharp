using System;

namespace TicTacToe.ConsoleApp
{
	public enum State
	{
		Computer,
		Human,
		Draw,
		Undefined
	}

	public class Board
	{
		string[,] arr;
		const int SIZE = 3;

		public Board()
		{
			arr = new string[SIZE, SIZE];
		}

		public int Size()
		{
			return SIZE;
		}

		public void Set(int row, int col, string val = "")
		{
			arr[row, col] = val;
		}

		public bool IsEmpty(int row, int col)
		{
			return string.IsNullOrEmpty(arr[row, col]);
		}

		public State CheckGameState(Board board)
		{
			switch (Evaluate())
			{
				case 10:
					return State.Computer;
				case -10:
					return State.Human;
				default:
					return !IsMoveLeft() ? State.Draw : State.Undefined;
			}
		}

		public bool IsMoveLeft()
		{
			for (int i = 0; i < SIZE; i++)
				for (int k = 0; k < SIZE; k++)
				{
					if (string.IsNullOrEmpty(arr[i, k]))
					{
						return true;
					}
				}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>+10 if x wins, -10 if o wins. Otherwise, returns 0</returns>
		public int Evaluate()
		{
			//Check row
			for (int i = 0; i < SIZE; i++)
			{
				if (arr[i, 0] == arr[i, 1] && arr[i, 1] == arr[i, 2] && !string.IsNullOrEmpty(arr[i, 0]))
				{
					return arr[i, 0] == "x" ? 10 : -10;
				}
			}

			//Check col
			for (int k = 0; k < SIZE; k++)
			{
				if (arr[0, k] == arr[1, k] && arr[1, k] == arr[2, k] && !string.IsNullOrEmpty(arr[0, k]))
				{
					return arr[0, k] == "x" ? 10 : -10;
				}
			}

			//Check diagonals
			if (arr[0, 0] == arr[1, 1] && arr[1, 1] == arr[2, 2] && !string.IsNullOrEmpty(arr[0, 0]))
			{
				return arr[0, 0] == "x" ? 10 : -10;
			}

			if (arr[0, 2] == arr[1, 1] && arr[1, 1] == arr[2, 0] && !string.IsNullOrEmpty(arr[0, 2]))
			{
				return arr[0, 2] == "x" ? 10 : -10;
			}

			return 0;
		}

		public void PrintBoard()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;

			for (int i = 0; i < SIZE; i++)
			{
				for (int k = 0; k < SIZE; k++)
				{
					if (!string.IsNullOrEmpty(arr[i, k]))
					{
						Console.Write($"{arr[i, k]}  |  ");
					}
					else
					{
						Console.Write($"   |  ");
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine("================================\n\n\n");
		}
	}
}
