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
using EdgeDetaction.Core.Repasitorys;
using EdgeDetaction.Core.Repasitorys.Enums;
using EdgeDetaction.DAL;
using EdgeDetaction.DAL.Repasitorys;

namespace EdgeDetactionProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EdgeDetectionDal dal = new EdgeDetectionDal(new EdgeDetectionEntities());
        EdgeDetectionCore core = new EdgeDetectionCore();

        public MainWindow()
        {
            //EdgeDetectionEntities db =new EdgeDetectionEntities();
            //var sobelHs = db.MatrixDetections.;
            //foreach (var u in sobelHs)
            //    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name);

            InitializeComponent();

           // compComboBox.DataContext = Enum.GetValues(typeof(CompTypes));
            compComboBox.ItemsSource= Enum.GetValues(typeof(CompTypes));
            compComboBox.SelectedItem = CompTypes.R;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                imgpathtextbox.Text = filename;
            }

        }

        private void CreateAddMatrix_Click(object sender, RoutedEventArgs e)
        {
            var path = $"{imgpathtextbox.Text}";
            var w = 0;
            var h = 0;
            var m = core.Image.ConvertImageToMatrix(path,(CompTypes)compComboBox.SelectedItem, ref w, ref h);
            var a = core.Image.ConvertMatreixToArray(m);
            var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = "R", Height = h, Type = "Defoult", Width = w, Matrix = a };
            dal.ImageDal.Add(image);
            core.Localization.SaveMatrix(m, image.Name);
        }
    }
}
