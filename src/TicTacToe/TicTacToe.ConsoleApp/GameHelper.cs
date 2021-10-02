using System;

namespace TicTacToe.ConsoleApp
{
	public class GameHelper
	{
		public int Minimax(Board board, int depth, bool isMax)
		{
			int score = board.Evaluate();

			if (score == 10 || score == -10 || !board.IsMoveLeft())
			{
				return score;
			}

			//Try each node
			if (isMax)
			{
				score = int.MinValue;

				for (int i = 0, n = board.Size(); i < n; i++)
					for (int k = 0; k < n; k++)
					{
						if (board.IsEmpty(i, k))
						{
							board.Set(i, k, "x");
							score = Math.Max(score, Minimax(board, depth + 1, !isMax) - depth);
							board.Set(i, k);    //clear
						}
					}
			}
			else
			{
				score = int.MaxValue;

				for (int i = 0, n = board.Size(); i < n; i++)
					for (int k = 0; k < n; k++)
					{
						if (board.IsEmpty(i, k))
						{
							board.Set(i, k, "o");
							score = Math.Min(score, Minimax(board, depth + 1, !isMax) + depth);
							board.Set(i, k);
						}
					}
			}

			return score;
		}

		/// <summary>
		/// Computer tries to find best move. It always acts as maximizer
		/// </summary>
		/// <param name="board"></param>
		/// <returns></returns>
		public Tuple<int, int> FindBestMove(Board board)
		{
			int best = int.MinValue;
			Tuple<int, int> points = new Tuple<int, int>(-1, -1);

			for (int i = 0, n = board.Size(); i < n; i++)
				for (int k = 0; k < n; k++)
				{
					if (board.IsEmpty(i, k))
					{
						board.Set(i, k, "x");
						int score = Minimax(board, 1, false);

						if (score > best)
						{
							best = score;
							points = new Tuple<int, int>(i, k);
						}

						board.Set(i, k);
					}
				}

			return points;
		}
	}
}
