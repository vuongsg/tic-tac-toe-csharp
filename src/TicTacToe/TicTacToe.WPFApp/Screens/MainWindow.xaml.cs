using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicTacToe.Helpers;

namespace TicTacToe.WPFApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private Board board;
	public Turn turn { get; set; }

	public MainWindow()
	{
		InitializeComponent();
		this.DataContext = this;
		turn = Turn.Computer;
	}

	private void Reset()
	{
		board = new Board("O", "X");
	}

	private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
	{
		string tag = (sender as Border)?.Tag.ToString();
	}
}
