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
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;
using VotGES.Piramida.Report;

namespace MainSL.Views
{
	public partial class SutVedPage : Page
	{
		public SettingsBase settings;
		ReportBaseDomainContext context;

		public SutVedPage() {
			InitializeComponent();
			settings = new SettingsBase();
			settings.Date = DateTime.Now.Date;
			pnlSettings.DataContext = settings;
			context = new ReportBaseDomainContext();
			string user = WebContext.Current.User.Name.ToString().ToLower();
			//MessageBox.Show("_"+user+"_");
			if (user.Contains("chekunovamv") || user.Length<3) {
				btnKotmi.Visibility = Visibility.Visible;
			} else {
				btnKotmi.Visibility = Visibility.Collapsed;
			}
			
		}
		
		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {

		}	

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {			
			string uri=String.Format("Reports/SutVed?year={0}&month={1}&day={2}",settings.Date.Year,settings.Date.Month,settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPBR_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/PBR?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnPrikaz20_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/Prikaz20?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnVER_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/VERReport?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnVERMonth_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/VERReportMonth?year={0}&month={1}", settings.Date.Year, settings.Date.Month);
			FloatWindow.OpenWindow(uri);
		}

		private void btnVERDir_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("http://sr-votges-int:8074");
			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPBRMC_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/PBRFromMC?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnGet53500MC_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/MaketFromMC?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnCheckMaket_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/CheckMaket?year={0}&month={1}&day={2}&ikm=false", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnCheckMaketOKM_Click(object sender, RoutedEventArgs e) {
			string uri = String.Format("Reports/CheckMaket?year={0}&month={1}&day={2}&ikm=true", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnKotmi_Click(object sender, RoutedEventArgs e) {
			(new KOTMIWindow()).Show();
		}
	}
}
