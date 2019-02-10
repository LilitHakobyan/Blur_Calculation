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
using EdgeDetaction.Core;
using EdgeDetaction.DAL;

namespace EdgeDetactionProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            EdgeDetectionEntities db =new EdgeDetectionEntities();
            var sobelHs = db.SobelHs;
            foreach (var u in sobelHs)
                Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name);

            InitializeComponent();
        }
    }
}
