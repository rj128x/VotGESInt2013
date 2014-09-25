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
using VotGES.Web.Models;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Threading;
using VotGES.Chart;

namespace MainSL.Views {
	public partial class OgranPage : Page {
		protected DispatcherTimer timer;
		public SettingsGraphVyab settings;
		protected OgranGAAnswer currentAnswer;
		protected int CurrentGA;
		protected ChartDataSerie CurrentChartData;
		

		public OgranGAAnswer CurrentAnswer {
			get { return currentAnswer; }
			set { currentAnswer = value; }
		}

		OgranGAContext context;

		public OgranPage() {
			InitializeComponent();
			context = new OgranGAContext();
			settings = new SettingsGraphVyab();
			settings.Second = 60;
			settings.AutoRefresh = false;
			pnlRefresh.DataContext = settings;
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
		}

		void timer_Tick(object sender, EventArgs e) {
			if (!GlobalStatus.Current.IsBusy && settings.AutoRefresh) {
				settings.Second--;
				if (settings.Second == 0) {
					refresh();
				}
			}
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			timer.Start();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
			GlobalStatus.Current.StopLoad();
			timer.Stop();
		}

		protected void refresh() {
			InvokeOperation currentOper = context.getOgranGAData(CurrentGA, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					OgranGAAnswer answer = (OgranGAAnswer)oper.Value;
					chartControl.ChartSeries.Last().refresh(answer.CurrentData);
					CurrentChartData = answer.CurrentData;
					grdCurrent.DataContext = answer;
					manualPaintCurrent(CurrentGA);
				}
				catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				}
				finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnRefresh_Click(object sender, RoutedEventArgs e) {
			refresh();
		}

		protected void manualPaintCurrent(int ga) {
			canvas.Children.Clear();
			Rectangle rect = new Rectangle();

			double wid = imgHar.ActualWidth;
			double hei = imgHar.ActualHeight;
			
			double left = wid * 0.055;
			double top = hei * 0.025;
			double bot = hei * 0.89;
			double right = wid * 0.975;

			switch (ga) {
				case 1:
					break;
				case 2:
					top = hei * 0.04;
					break;
				case 3:
					top = hei * 0.05;
					bot = hei * 0.89;
					break;
				case 4:
					top = hei * 0.04;
					bot = hei * 0.90;
					break;
				case 5:
					top = hei * 0.04;
					bot = hei * 0.90;
					break;
				case 6:
					top = hei * 0.04;
					bot = hei * 0.90;
					break;
				case 7:
					left = wid * 0.06;
					top = hei * 0.05;
					bot = hei * 0.90;
					right = wid * 0.98;
					break;
				case 8:
					top = hei * 0.05;
					bot = hei * 0.90;
					break;
				case 9:
					top = hei * 0.035;
					bot = hei * 0.90;
					break;
				case 10:
					top = hei * 0.03;
					bot = hei * 0.90;
					right = wid * 0.97;
					break;
			}			

			double recWidth = right - left;
			double recHeight = bot - top;

			double stepPower = (ga==5 || ga==6)?(recWidth / 90.0):(recWidth/100.0);
			double stepNapor = recHeight / 6.0;

			


			rect.Width = recWidth;
			rect.Height = recHeight;
			rect.Stroke = new SolidColorBrush(Colors.Blue);
			rect.StrokeThickness = 2;			
			rect.Margin = new Thickness(left,top,right,bot);

			
			foreach (ChartDataPoint point in CurrentChartData.Points) {
				
				Ellipse el = new Ellipse();
				el.Width = 10;
				el.Height = 10;
				el.Fill = new SolidColorBrush(Colors.Blue);


				double pointLeft = (point.XValDouble - 20) * stepPower;
				double pointTop = rect.Height-(point.YVal-16) * stepNapor;

				el.Margin = new Thickness(left+pointLeft,top+pointTop,0,0);
				

				canvas.Children.Add(el);
				
			}

			//canvas.Children.Add(rect);
		}

		protected void loadGAInfo(int ga) {
			CurrentGA = ga;
			ImageSourceConverter src=new ImageSourceConverter();
			imgHar.Source=(ImageSource)src.ConvertFromString(String.Format("/MainSL;component/Images/gaHar/ga{0}.jpg",ga));			
			InvokeOperation currentOper = context.getOgranGAAnswer(ga, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					CurrentAnswer = oper.Value;
					chartControl.Create(CurrentAnswer.ChartAnswer);
					grdNarab.DataContext = CurrentAnswer;
					grdCurrent.DataContext = CurrentAnswer;
					CurrentChartData = CurrentAnswer.CurrentData;
					manualPaintCurrent(ga);
				}
				catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				}
				finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void ga1Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(1);
		}

		private void ga2Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(2);
		}

		private void ga3Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(3);
		}

		private void ga4Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(4);
		}

		private void ga5Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(5);
		}

		private void ga6Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(6);
		}

		private void ga7Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(7);
		}

		private void ga8Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(8);
		}

		private void ga9Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(9);
		}

		private void ga10Btn_Click(object sender, RoutedEventArgs e) {
			loadGAInfo(10);
		}

	}


}
