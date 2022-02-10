using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TicTacToe.Helpers;

namespace TicTacToe.WPFApp;

internal class TurnToBooleanConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (Turn)Enum.Parse(typeof(Turn), value.ToString()) == Turn.Human;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null || value == DependencyProperty.UnsetValue)
		{
			return Turn.Computer;
		}

		return (bool)value ? Turn.Human : Turn.Computer;
	}
}
