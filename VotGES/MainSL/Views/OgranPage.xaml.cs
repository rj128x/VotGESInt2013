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
using VotGES.Piramida.Report;

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

			SettingsControl.InitOnlyDates();
			SettingsControl.Settings.ReportType = ReportTypeEnum.day;
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
			
			double left = 0;
			double top = 0;
			double bot = hei;
			double right = wid;

			switch (ga) {
				case 1:
					left = wid * 0.058;
					top = hei * 0.035;
					bot = hei * 0.894;
					right = wid * 0.9632;
					break;
				case 2:
					left = wid * 0.142;
					top = hei * 0.015;
					bot = hei * 0.898;
					right = wid * 0.968;
					break;				
				case 3:
					left = wid * 0.0598;
					top = hei * 0.056;
					bot = hei * 0.888;
					right = wid * 0.969;
					break;
				case 4:
					left = wid * 0.059;
					top = hei * 0.056;
					bot = hei * 0.876;
					right = wid * 0.967;
					break;
				case 5:
					left = wid * 0.062;
					top = hei * 0.051;
					bot = hei * 0.875;
					right = wid * 0.970;
					break;
				case 6:
					left = wid * 0.060;
					top = hei * 0.045;
					bot = hei * 0.888;
					right = wid * 0.962;
					break;
				case 7:
					left = wid * 0.063;
					top = hei * 0.062;
					bot = hei * 0.899;
					right = wid * 0.966;
					break;
				case 8:
					left = wid * 0.060;
					top = hei * 0.057;
					bot = hei * 0.841;
					right = wid * 0.969;
					break;
				case 9:
					left = wid * 0.141;
					top = hei * 0.022;
					bot = hei * 0.896;
					right = wid * 0.971;
					break;
				case 10:
					left = wid * 0.070;
					top = hei * 0.036;
					bot = hei * 0.893;
					right = wid * 0.953;
					break;
			}			

			double recWidth = right - left;
			double recHeight = bot - top;


			double stepPower =recWidth/100.0;
			if (ga == 6)
				stepPower = recWidth / 90.0;
			if (ga == 8)
				stepPower = recWidth / 110.0;			
			double stepNapor = recHeight / 6.0;
			if (ga==2 || ga==9)
				stepNapor = recHeight / 11.0;
			rect.Width = recWidth;
			rect.Height = recHeight;
			rect.Stroke = new SolidColorBrush(Colors.Blue);
			rect.StrokeThickness = 2;			
			rect.Margin = new Thickness(left,top,right,bot);


			double prevX = -1;
			double prevY = -1;

			int index=0;
			int count=CurrentChartData.Points.Count;
			foreach (ChartDataPoint point in CurrentChartData.Points) {
				index++;
				byte ccc=(byte)(100+(byte.MaxValue-100)*index/count);
				Color color = Color.FromArgb(ccc, 0, 0, byte.MaxValue);
				int sz = 10;

				Ellipse el = new Ellipse();
				el.Width = sz;
				el.Height = sz;

				el.Fill = new SolidColorBrush(color);
				el.Stroke = new SolidColorBrush(Colors.Blue);
				el.StrokeThickness = 1;
				if (point == CurrentChartData.Points.Last()) {
					el.Stroke = new SolidColorBrush(Colors.Red);
					el.StrokeThickness = 2;
				}
				


				double pointLeft = (point.XValDouble - 20.0) * stepPower;
				if (ga == 8 ||ga==5)
					pointLeft = (point.XValDouble - 10.0) * stepPower;

				double pointTop = rect.Height-(point.YVal-16.0) * stepNapor;
				if (ga == 2 ||ga==9) {
					pointTop = rect.Height - (point.YVal - 12.0) * stepNapor;
				}


				double x=left+pointLeft;
				double y=top+pointTop;
				if (x < left)
					x =  10;

				el.Margin = new Thickness(x-5,y-5,x+5,y+5);

				if (prevX != -1) {
					Line line = new Line();										
					line.Stroke = new SolidColorBrush(color);
					line.StrokeThickness = 2;
					line.X1 = prevX;
					line.X2 = x;
					line.Y1 = prevY;
					line.Y2 = y;
					
					//line.Margin = new Thickness(prevX, prevY, x, y);
					canvas.Children.Add(line);
				}
				prevX = x;
				prevY = y;
				canvas.Children.Add(el);
				
			}

			//canvas.Children.Add(rect);
		}

		protected void loadGAInfo(int ga) {
			textGenerator.Text = "Генератор №" + ga;
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

		private void btnGetPuskStop_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des = ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			string uri = String.Format("Reports/PuskStop?year1={0}&month1={1}&day1={2}&year2={3}&month2={4}&day2={5}",
				des.DateStart.Year, des.DateStart.Month, des.DateStart.Day,
				des.DateEnd.Year, des.DateEnd.Month, des.DateEnd.Day);

			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPuskStopFull_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des = ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			PuskStopGAFull window = new PuskStopGAFull();
			window.DateStart = des.DateStart;
			window.DateEnd = des.DateEnd;
			window.Show();
			window.refresh();
		}

		private void btnGetPuskStopByDays_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des = ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			string uri = String.Format("Reports/PuskStopByDays?year1={0}&month1={1}&day1={2}&year2={3}&month2={4}&day2={5}",
				des.DateStart.Year, des.DateStart.Month, des.DateStart.Day,
				des.DateEnd.Year, des.DateEnd.Month, des.DateEnd.Day);

			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPuskStopKotmi_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des = ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			string uri = String.Format("Reports/PuskStopKOTMI?year1={0}&month1={1}&day1={2}&year2={3}&month2={4}&day2={5}",
				des.DateStart.Year, des.DateStart.Month, des.DateStart.Day,
				des.DateEnd.Year, des.DateEnd.Month, des.DateEnd.Day);

			FloatWindow.OpenWindow(uri);
		}
	}


}
