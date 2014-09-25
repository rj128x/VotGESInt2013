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
					grdCurrent.DataContext = answer;
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
