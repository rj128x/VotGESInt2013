using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace MainSL.Views
{
    public partial class OgranPage : Page
    {
        public OgranPage()
        {
            InitializeComponent();
        }

        // Выполняется, когда пользователь переходит на эту страницу.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<KeyValuePair<int, int>> lst = new List<KeyValuePair<int, int>>();
            lst.Add(new KeyValuePair<int, int>(5, 5));

        }

    }
}
