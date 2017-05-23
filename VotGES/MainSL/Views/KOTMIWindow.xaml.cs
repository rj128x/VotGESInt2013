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
		ReportBaseDomainContext Context { get; set; }
		public KOTMIWindow() {
			InitializeComponent();
			Context = new ReportBaseDomainContext();
			loadRoot();
			
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
			DateTime date = dtCalend.SelectedDate.Value;
			string uri = String.Format("Reports/GetKotmiData?year={0}&month={1}&day={2}&mode={3}&stepSeconds={4}&Fields={5}", date.Year, date.Month, date.Day,"HH",txtStep.Text, String.Join("~", selected));
			FloatWindow.OpenWindow(uri);
			//MessageBox.Show(String.Join("~", selected));
		}
	}
}

