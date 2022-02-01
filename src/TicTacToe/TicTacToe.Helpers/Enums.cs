namespace TicTacToe.Helpers;

public enum GameState : byte
{
	ComputerWin,
	HumanWin,
	Draw,
	NotYet
}

public enum Turn : byte
{
	Computer,
	Human
}