using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VotGES.Chart;
using VotGES.Web.Services;

namespace MainSL.Views {
	public partial class PuskStopGAFull : ChildWindow {
		OgranGAContext context;
		public DateTime DateStart;
		public DateTime DateEnd;

		public PuskStopGAFull() {
			InitializeComponent();
			context = new OgranGAContext();			
		}

		public void refresh() {
			InvokeOperation currentOper = context.getPuskStopFull(DateStart,DateEnd, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					ChartAnswer answer = (ChartAnswer)oper.Value;
					cntrlChart.Create(answer);
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
			this.Width = MainPage.Current.ActualWidth;
			this.Height = MainPage.Current.ActualHeight;
			this.Margin = new Thickness(0, 0, 0, 0);
		}

		private void OKButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = false;
		}
	}
}

