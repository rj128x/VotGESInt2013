﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainSL
{
	public partial class About : Page
	{
		public About() {
			InitializeComponent();
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("docs/Instruction8075.doc");
			FloatWindow.OpenWindow(uri);
		}
	}
}