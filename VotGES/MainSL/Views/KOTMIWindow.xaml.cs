using KotmiLib;
using MainSL.Logging;
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
using VotGES.Web.Services;

namespace MainSL.Views
{
	public partial class KOTMIWindow : ChildWindow
	{
		public Dictionary<string, ArcField> KotmiFields;
		public Dictionary<string, ArcField> FilteredKotmiFields;
		ReportBaseDomainContext Context { get; set; }
		public KOTMIWindow() {
			InitializeComponent();
			Context = new ReportBaseDomainContext();
			loadRoot();
			dtStart.SelectedDate = DateTime.Now.Date;
			dtEnd.SelectedDate = DateTime.Now.Date;
			tmEnd.Text = DateTime.Now.Hour.ToString("0");
			tmStart.Text = "0";
			FilteredKotmiFields = new Dictionary<string, ArcField>();
		}

		private void OKButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = false;
			
		}

		protected void loadRoot() {
			InvokeOperation currentOper = Context.GetKOTMI(oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {					
					GlobalStatus.Current.StartProcess();
					List<ArcField> result = oper.Value;
					KotmiFields = new Dictionary<string, ArcField>();
					
					foreach (ArcField field in result) {
						KotmiFields.Add(field.Code, field);
					}
					//MessageBox.Show(result.Count.ToString());
					grdItems.ItemsSource = KotmiFields.Values;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка при получении дерева");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnShow_Click(object sender, RoutedEventArgs e) {
			List<string> selected = new List<string>();
			foreach (Object item in grdItems.ItemsSource) {
				ArcField field = item as ArcField;
				if (field.Sel)
					selected.Add(field.Code);
			}
			DateTime dateStart = dtStart.SelectedDate.Value.Date.AddHours(Int32.Parse(tmStart.Text));
			DateTime dateEnd = dtEnd.SelectedDate.Value.Date.AddHours(Int32.Parse(tmEnd.Text));

			string uri = String.Format("Reports/GetKotmiData?year1={0}&month1={1}&day1={2}&hh1={3}&year2={4}&month2={5}&day2={6}&hh2={7}&mode={8}&stepSeconds={9}&negPos={10}&Fields={11}",
				dateStart.Year, dateStart.Month, dateStart.Day, dateStart.Hour,
				dateEnd.Year, dateEnd.Month, dateEnd.Day, dateEnd.Hour, 
				rbHH.IsChecked.Value?"HH":"Step",txtStep.Text,chbNegPos.IsChecked, String.Join("~", selected));
			FloatWindow.OpenWindow(uri);
			//MessageBox.Show(String.Join("~", selected));
		}


		private void txtFilter_TextChanged(object sender, TextChangedEventArgs e) {
			FilteredKotmiFields.Clear();
			string filter = txtFilter.Text.ToLower();
			foreach (KeyValuePair<string, ArcField> de in KotmiFields) {
				if (de.Value.Name.ToLower().Contains(filter) || de.Value.Sel) {
					FilteredKotmiFields.Add(de.Key, de.Value);
				}
			}
			grdItems.ItemsSource = FilteredKotmiFields.Values;
		}

		private void dtStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
			checkDateStart();
		}

		private void tmStart_TextChanged(object sender, TextChangedEventArgs e) {
			int h = Int32.Parse(tmStart.Text);
			h = h < 0 ? 0:h;
			h = h > 24 ? 24 : h;
			tmStart.Text = h.ToString("0");
			checkDateStart();
		}

		private void dtEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
			checkDateEnd();
		}

		private void tmEnd_TextChanged(object sender, TextChangedEventArgs e) {
			int h = Int32.Parse(tmEnd.Text);
			h = h < 0 ? 0 : h;
			h = h > 24 ? 24 : h;
			tmEnd.Text = h.ToString("0");
			checkDateEnd();
		}

		protected void checkDateEnd() {
			DateTime dateStart = dtStart.SelectedDate.Value.Date.AddHours(Int32.Parse(tmStart.Text));
			DateTime dateEnd = dtEnd.SelectedDate.Value.Date.AddHours(Int32.Parse(tmEnd.Text));
			if (dateStart.AddHours(24) < dateEnd) {
				dateEnd = dateStart.AddHours(24);
				dtEnd.SelectedDate = dateEnd.Date;
				tmEnd.Text = dateEnd.Hour.ToString("0");
			}
		}

		protected void checkDateStart() {
			DateTime dateStart = dtStart.SelectedDate.Value.Date.AddHours(Int32.Parse(tmStart.Text));
			DateTime dateEnd = dtEnd.SelectedDate.Value.Date.AddHours(Int32.Parse(tmEnd.Text));
			if (dateStart.AddHours(24) < dateEnd) {
				dateStart = dateEnd.AddHours(-24);
				dtStart.SelectedDate = dateStart.Date;
				tmStart.Text = dateStart.Hour.ToString("0");
			}
		}
	}
}

