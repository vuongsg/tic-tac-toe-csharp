namespace TicTacToe.Helpers;

public class GameHelper
{
	/// <summary>
	/// 
	/// Computer always try to maximize score
	/// Human always try to minimize score
	/// </summary>
	/// <param name="board"></param>
	/// <param name="turn"></param>
	/// <param name="depth"></param>
	/// <param name="alpha">best value that maximizer can achieve at depth level and above</param>
	/// <param name="beta">best value that maximizer can achieve at depth level and above</param>
	/// <returns></returns>
	public static int Minimax(Board board, Turn turn, int depth, int alpha, int beta)
	{
		int score = board.EvaluateBoard();
		if (score != Constants.ZERO_SCORE || !board.HasMoveLeft())
		{
			return score;
		}

		if (turn == Turn.Computer)
		{
			score = int.MinValue;

			for (int row = 0; row < Constants.SIZE; row++)
				for (int col = 0; col < Constants.SIZE; col++)
				{
					if (board.IsEmptyCell(row, col))
					{
						board.SetCell(row, col, turn);  //try
						score = Math.Max(score, Minimax(board, Turn.Human, depth + 1, alpha, beta) - depth);    //subtract depth to choose the victory which takes least number of moves
						alpha = Math.Max(alpha, score);
						board.ClearCell(row, col);      //undo

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

			for (int row = 0; row < Constants.SIZE; row++)
				for (int col = 0; col < Constants.SIZE; col++)
				{
					if (board.IsEmptyCell(row, col))
					{
						board.SetCell(row, col, turn);  //try
						score = Math.Min(score, Minimax(board, Turn.Computer, depth + 1, alpha, beta) + depth); //plus depth to prolong the grame and play as many moves as possible
						beta = Math.Min(beta, score);
						board.ClearCell(row, col);      //undo

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
	/// Find best move for computer
	/// </summary>
	/// <param name="board"></param>
	/// <param name="alpha">best value that maximizer can achieve at depth level and above</param>
	/// <param name="beta">best value that maximizer can achieve at depth level and above</param>
	/// <returns></returns>
	public static Tuple<int, int> FindBestMove(Board board, int alpha, int beta)
	{
		int chosenScore = int.MinValue;
		int chosenRow = -1;
		int chosenCol = -1;

		for (int row = 0; row < Constants.SIZE; row++)
			for (int col = 0; col < Constants.SIZE; col++)
			{
				if (board.IsEmptyCell(row, col))
				{
					board.SetCell(row, col, Turn.Computer);
					int score = Minimax(board, Turn.Human, 0, alpha, beta);

					if (score > chosenScore)
					{
						chosenScore = score;
						chosenRow = row;
						chosenCol = col;
					}

					board.ClearCell(row, col);
				}
			}

		return new Tuple<int, int>(chosenRow, chosenCol);
	}
}
