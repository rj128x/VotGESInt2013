using KotmiLib;
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
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка при получении дерева");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}
	}
}

