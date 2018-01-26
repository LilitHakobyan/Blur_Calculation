using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var img = new Image();
            img.Height = 100;
            img.Width = 100;
            img.Visibility = Visibility.Visible;

            var th = new Thickness(100, 100, 0, 0);
            img.Margin = th;
            var uri = new Uri(@"E:\12.jpg");
            img.Source = new BitmapImage(uri);
        }
    }
}
