//using Jlion.Process.Target.ClientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jlion.Process.Target.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HookService.Start("");
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            var service = new ProcessService();
            this.txtInfo.Text = service.GetProcessInfo();
        }

        private void btnComplateInfo_Click(object sender, RoutedEventArgs e)
        {
            var service = new ProcessService();
            var response = service.GetProcessInfo(new Jlion.Process.Target.Client.Model.ProcessRequest() { Version = "v-Demo 1.0 版本" });
            this.txtInfo.Text = response.Name + response.Version;
        }
    }
}
