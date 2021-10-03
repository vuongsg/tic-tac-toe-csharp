using System;
using System.Threading;

namespace TicTacToe.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("TIC TAC TOE Game");
			Console.WriteLine("Computer takes x. You take o");
			Board board = new Board();
			GameHelper gameHelper = new GameHelper();
			const int ALPHA = int.MinValue;
			const int BETA = int.MaxValue;
			bool isMax = false;     //Human plays first

			Console.WriteLine("NEW GAME\n");
			board.PrintBoard();

			while (true)
			{
				if (!isMax)
				{
					Console.WriteLine("Your turn:");
					Console.Write("Input your position that you want to make, e.g. you want to go in the very first cell, type 0,0:  ");
					string[] pos = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
					board.Set(int.Parse(pos[0]), int.Parse(pos[1]), "o");
				}
				else
				{
					Console.WriteLine("Computer's turn:");
					Tuple<int, int> pos = gameHelper.FindBestMove(board, ALPHA, BETA);
					board.Set(pos.Item1, pos.Item2, "x");
				}

				board.PrintBoard();
				isMax = !isMax;
				State state = board.CheckGameState(board);

				if (state != State.Undefined)
				{
					switch (state)
					{
						case State.Computer:
							Console.WriteLine("OH, COMPUTER WON !!\n\n");
							isMax = true;
							break;
						case State.Human:
							Console.WriteLine("HURA, YOU WON !!\n\n");
							isMax = false;
							break;
						case State.Draw:
							Console.WriteLine("DRAWS !!\n\n");
							isMax = false;
							break;
					}

					Thread.Sleep(100);
					board = new Board();
					Console.WriteLine("NEW GAME\n");
					board.PrintBoard();
				}
			}
		}
	}
}
