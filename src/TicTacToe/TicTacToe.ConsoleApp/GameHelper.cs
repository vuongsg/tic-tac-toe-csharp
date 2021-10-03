using System;

namespace TicTacToe.ConsoleApp
{
	public class GameHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="board"></param>
		/// <param name="depth"></param>
		/// <param name="isMax"></param>
		/// <param name="alpha">the best value that maximizer can get at that level or above</param>
		/// <param name="beta">the best value that minimizer can get at that level or above</param>
		/// <returns></returns>
		public int Minimax(Board board, int depth, bool isMax, int alpha, int beta)
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
							score = Math.Max(score, Minimax(board, depth + 1, !isMax, alpha, beta) - depth);
							alpha = Math.Max(alpha, score);
							board.Set(i, k);    //clear

							if (beta <= alpha)
							{
								break;
							}
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
							score = Math.Min(score, Minimax(board, depth + 1, !isMax, alpha, beta) + depth);
							beta = Math.Min(beta, score);
							board.Set(i, k);

							if (beta <= alpha)
							{
								break;
							}
						}
					}
			}

			return score;
		}

		/// <summary>
		/// Computer tries to find best move. It always acts as maximizer
		/// </summary>
		/// <param name="board"></param>
		/// <param name="alpha"></param>
		/// <param name="beta"></param>
		/// <returns></returns>
		public Tuple<int, int> FindBestMove(Board board, int alpha, int beta)
		{
			int best = int.MinValue;
			Tuple<int, int> points = new Tuple<int, int>(-1, -1);

			for (int i = 0, n = board.Size(); i < n; i++)
				for (int k = 0; k < n; k++)
				{
					if (board.IsEmpty(i, k))
					{
						board.Set(i, k, "x");
						int score = Minimax(board, 1, false, alpha, beta);

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
