using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PlanConverter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void OpenForMpx(object sender, RoutedEventArgs e) {
            var ofd = new OpenFileDialog{Multiselect = true, Filter = "*.mpp|*.mpp"};
            if (ofd.ShowDialog() != true) {
                return;
            }
            if (sender is FrameworkElement fe) {
                if (fe.DataContext is MppToMpxConverter converter) {
                    converter.AddFiles.Execute(ofd.FileNames);
                }
            }
        }

        private void OpenForXml(object sender, RoutedEventArgs e) {
            var ofd = new OpenFileDialog { Multiselect = true, Filter = "*.mpp|*.mpp" };
            if (ofd.ShowDialog() != true) {
                return;
            }
            if (sender is FrameworkElement fe) {
                if (fe.DataContext is MppToMspdiConverter converter) {
                    converter.AddFiles.Execute(ofd.FileNames);
                }
            }
        }
    }
}
