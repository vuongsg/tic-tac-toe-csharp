using System;
using System.Linq;
using TicTacToe.Helpers;

namespace TicTacToe.ConsoleApp;

class Program
{
	static string computerLetter;
	static string humanLetter;
	static Turn turn;
	const int ALPHA = int.MinValue;
	const int BETA = int.MaxValue;

	static void Main(string[] args)
	{
		while (true)
		{
			WelcomeBoard();
			Play(new Board(computerLetter, humanLetter));
		}
	}

	static void WelcomeBoard()
	{
		Console.Write("TIC TAC TOE GAME");
		ConsoleKeyInfo c;

		do
		{
			Console.Write("\nPress 1 if you want to be X, or 2 if you want to be O: ");
			c = Console.ReadKey();
		}
		while (c.KeyChar != '1' && c.KeyChar != '2');

		switch (c.KeyChar)
		{
			case '1':
				humanLetter = "X";
				computerLetter = "O";
				break;
			default:
				humanLetter = "O";
				computerLetter = "X";
				break;
		}

		do
		{
			Console.Write("\nPress 1 if you want to go first, otherwise pressing 2: ");
			c = Console.ReadKey();
		}
		while (c.KeyChar != '1' && c.KeyChar != '2');

		switch (c.KeyChar)
		{
			case '1':
				turn = Turn.Human;
				break;
			default:
				turn = Turn.Computer;
				break;
		}
	}

	static void Play(Board board)
	{
		int row = -1;
		int col = -1;
		GameState state;

		do
		{
			Console.ForegroundColor = ConsoleColor.White;

			switch (turn)
			{
				case Turn.Computer:
					Console.WriteLine("\nComputer's turn");
					var point = GameHelper.FindBestMove(board, ALPHA, BETA);
					board.SetCell(point.Item1, point.Item2, turn);
					turn = Turn.Human;
					break;
				default:
					Console.WriteLine("\nYour turn");
					try
					{
						do
						{
							Console.Write("\nInput point (row 1 -> 3, col 1 -> 3), separated by blank, eg: 1 2: ");
							var points = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(m => int.Parse(m)).ToArray();
							if (points.Length <= 1)
							{
								continue;
							}
							row = points[0] - 1;
							col = points[1] - 1;
						}
						while (row < 0 || row > 2 || col < 0 || col > 2 || !board.IsEmptyCell(row, col));
					}
					catch { }

					board.SetCell(row, col, turn);
					turn = Turn.Computer;
					break;
			}

			board.PrintBoard();
			state = board.CheckState();
		}
		while (state == GameState.NotYet);

		switch (state)
		{
			case GameState.ComputerWin:
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Computer wins");
				break;
			case GameState.HumanWin:
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("You win");
				break;
			default:
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("Draw");
				break;
		}
	}
}