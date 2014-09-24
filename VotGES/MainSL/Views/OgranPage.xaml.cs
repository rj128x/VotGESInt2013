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

namespace MainSL.Views {
    public partial class OgranPage : Page {
        protected OgranGAAnswer currentAnswer;

        public OgranGAAnswer CurrentAnswer {
            get { return currentAnswer; }
            set { currentAnswer = value; }
        }

        OgranGAContext context;

        public OgranPage() {
            InitializeComponent();
            context = new OgranGAContext();
        }

        // Выполняется, когда пользователь переходит на эту страницу.
        protected override void OnNavigatedTo(NavigationEventArgs e) {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            GlobalStatus.Current.StopLoad();
        }

        protected void loadGAInfo(int ga) {
            InvokeOperation currentOper = context.getOgranGAAnswer(ga, oper => {
                if (oper.IsCanceled) {
                    return;
                }
                try {
                    GlobalStatus.Current.StartProcess();
                    CurrentAnswer = oper.Value;
                    chartControl.Create(CurrentAnswer.ChartAnswer);
                    grdAnswer.DataContext = CurrentAnswer;
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
