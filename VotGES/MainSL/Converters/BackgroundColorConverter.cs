﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using MainSL.Logging;

namespace MainSL.Converters
{
	public class BackgroundColorConverter : IValueConverter
	{
		#region IValueConverter Members
		/// <summary>
		/// Convert a Date into DD/MMM/YYYY HH:mm format
		/// </summary>
		/// <param name="value">object: value</param>
		/// <param name="targetType">Type: targetType</param>
		/// <param name="parameter">object: parameter</param>
		/// <param name="culture">System.Globalization.CultureInfo: culture</param>
		/// <returns>System.Object</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				Color color=Colors.Transparent;
				string param=parameter.ToString();
				switch (param) {
					case "SerieSelected":
						bool sel=(bool)value;
						color=sel?Colors.Gray:Colors.Transparent;
						break;
				}
					
				return new SolidColorBrush(color);
			} catch {
				return new SolidColorBrush(Colors.Transparent);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}

		#endregion
	}
}
