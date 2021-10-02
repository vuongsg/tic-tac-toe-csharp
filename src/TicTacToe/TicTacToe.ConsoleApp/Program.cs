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
			bool isMax = false;     //Human plays first
			board.PrintBoard();

			while (true)
			{
				if (!isMax)
				{
					Console.Write("Input your position that you want to make, e.g. you want to go in the very first cell, type 0,0");
					string[] pos = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
					board.Set(int.Parse(pos[0]), int.Parse(pos[1]), "o");
				}
				else
				{
					Tuple<int, int> pos = gameHelper.FindBestMove(board);
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
							Console.WriteLine("Computer won");
							isMax = true;
							break;
						case State.Human:
							Console.WriteLine("Congrats, you won");
							isMax = false;
							break;
						case State.Draw:
							Console.WriteLine("Draws");
							isMax = false;
							break;
					}

					Thread.Sleep(100);
					board = new Board();
					board.PrintBoard();
				}
			}
		}
	}
}
