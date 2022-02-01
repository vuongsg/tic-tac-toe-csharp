namespace TicTacToe.Helpers;

public class Board
{
	string[,] board;

	public string ComputerLetter { get; set; }
	public string HumanLetter { get; set; }

	public Board(string computerLetter, string humanLetter)
	{
		board = new string[Constants.SIZE, Constants.SIZE];
		ResetBoard(computerLetter, humanLetter);
	}

	/// <summary>
	/// Check this specified cell is empty or not
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <returns></returns>
	public bool IsEmptyCell(int row, int col)
	{
		return string.IsNullOrEmpty(board[row, col]);
	}

	/// <summary>
	/// Check whether board has move left
	/// </summary>
	/// <returns></returns>
	public bool HasMoveLeft()
	{
		for (int i = 0; i < Constants.SIZE; i++)
			for (int k = 0; k < Constants.SIZE; k++)
			{
				if (string.IsNullOrEmpty(board[i, k]))
				{
					return true;
				}
			}

		return false;
	}

	/// <summary>
	/// Reset board
	/// </summary>
	public void ResetBoard(string computerLetter, string humanLetter)
	{
		for (int i = 0; i < Constants.SIZE; i++)
			for (int k = 0; k < Constants.SIZE; k++)
			{
				board[i, k] = string.Empty;
			}

		ComputerLetter = computerLetter;
		HumanLetter = humanLetter;
	}

	/// <summary>
	/// Compare cells
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr"></param>
	/// <returns></returns>
	public bool CompareCells<T>(params T[] arr) where T : IComparable
	{
		if (arr == null || arr.Length <= 1)
		{
			return false;
		}

		for (int i = 1, n = arr.Length; i < n; i++)
		{
			if (arr[0].CompareTo(arr[i]) != 0)
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Evaluate board
	/// </summary>
	/// <returns></returns>
	public int EvaluateBoard()
	{
		//Check cols
		for (int row = 0; row < Constants.SIZE; row++)
		{
			if (!string.IsNullOrEmpty(board[row, 0]) && CompareCells(board[row, 0], board[row, 1], board[row, 2]))
			{
				return board[row, 0] == ComputerLetter ? Constants.COMPUTER_SCORE : Constants.HUMAN_SCORE;
			}
		}

		//Check rows
		for (int col = 0; col < Constants.SIZE; col++)
		{
			if (!string.IsNullOrEmpty(board[0, col]) && CompareCells(board[0, col], board[1, col], board[2, col]))
			{
				return board[0, col] == ComputerLetter ? Constants.COMPUTER_SCORE : Constants.HUMAN_SCORE;
			}
		}

		//Check backward diagonal
		if (!string.IsNullOrEmpty(board[0, 0]) && CompareCells(board[0, 0], board[1, 1], board[2, 2]))
		{
			return board[0, 0] == ComputerLetter ? Constants.COMPUTER_SCORE : Constants.HUMAN_SCORE;
		}

		//Check forward diagonal
		if (!string.IsNullOrEmpty(board[0, 2]) && CompareCells(board[0, 2], board[1, 1], board[2, 0]))
		{
			return board[0, 2] == ComputerLetter ? Constants.COMPUTER_SCORE : Constants.HUMAN_SCORE;
		}

		return Constants.ZERO_SCORE;
	}

	/// <summary>
	/// Check board state
	/// </summary>
	/// <returns></returns>
	public GameState CheckState()
	{
		switch (EvaluateBoard())
		{
			case Constants.COMPUTER_SCORE:
				return GameState.ComputerWin;
			case Constants.HUMAN_SCORE:
				return GameState.HumanWin;
			default:
				return HasMoveLeft() ? GameState.NotYet : GameState.Draw;
		}
	}

	/// <summary>
	/// Set cell
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="turn"></param>
	public void SetCell(int row, int col, Turn turn)
	{
		board[row, col] = turn == Turn.Computer ? ComputerLetter : HumanLetter;
	}

	/// <summary>
	/// Clear cell
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	public void ClearCell(int row, int col)
	{
		board[row, col] = String.Empty;
	}

	/// <summary>
	/// Print board
	/// </summary>
	public void PrintBoard()
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine();

		for (int i = 0; i < Constants.SIZE; i++)
		{
			for (int k = 0; k < Constants.SIZE; k++)
			{
				if (!string.IsNullOrEmpty(board[i, k]))
				{
					Console.Write($"{board[i, k]}  |  ");
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