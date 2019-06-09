using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EdgeDetaction.Core;
using EdgeDetaction.Core.Repasitorys;
using EdgeDetaction.Core.Repasitorys.Enums;
using EdgeDetaction.DAL;
using EdgeDetaction.DAL.Repasitorys;
using Microsoft.Win32;


namespace EdgeDetactionProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EdgeDetectionDal dal;
        private EdgeDetectionCore core;
        private List<MatrixDetection> listOfImage;
        public MainWindow()
        {
            //EdgeDetectionEntities db =new EdgeDetectionEntities();
            //var sobelHs = db.MatrixDetections.;
            //foreach (var u in sobelHs)
            //    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name);

            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dal = new EdgeDetectionDal(new EdgeDetectionEntities());
            core = new EdgeDetectionCore();
            compComboBox.ItemsSource = Enum.GetValues(typeof(CompTypes));
            compComboBox.SelectedItem = CompTypes.R;
            listOfImage = dal.ImageDal.GetAll();//.GetAllRoots();
            imgDataGrid.ItemsSource = listOfImage;
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (FromFolderCheckBox.IsChecked == true)
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                // Set validate names and check file exists to false otherwise windows will
                // not let you select "Folder Selection."
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Folder Selection";
                if (folderBrowser.ShowDialog() == true)
                {
                    string folderPath = System.IO.Path.GetDirectoryName(folderBrowser.FileName).Replace("Folder Selection", "");
                    imgpathtextbox.Text = folderPath;

                }
            }
            else
            {
                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".bmp";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp";
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
            

        }
        

        private void CreateAddMatrix_Click(object sender, RoutedEventArgs e)
        {
            var path = $"{imgpathtextbox.Text}";
            var w = 0;
            var h = 0;
            var matrix = core.Image.ConvertImageToMatrixByte(path, (CompTypes)compComboBox.SelectedItem, ref w, ref h);
            var arrM = core.Image.ConvertMatreixToArray(matrix);
            var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = "R", Height = h, Type = Convert.ToString(MatrixTypes.Default), Width = w, Matrix = arrM };
            dal.ImageDal.Add(image);
            core.Localization.SaveMatrix(matrix, image.Name);
            Button_Click(sender, e);

        }

        private void Magnitude_Click(object sender, RoutedEventArgs e)
        {
            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                MatrixDetection rowobj = (MatrixDetection)imgDataGrid.Items[i];  // this give you access to the row
                                                                                 //var sobelVM= dal.ImageDal.GetSobelV(rowobj.Parent1.GetValueOrDefault());
                                                                                 //var sobelHM = dal.ImageDal.GetSobelH(rowobj.Parent2.GetValueOrDefault());

                //var matrixV = core.Image.ConvertArrayToMatrix(sobelVM.Matrix, sobelVM.Width, sobelVM.Height);
                //var matrixH = core.Image.ConvertArrayToMatrix(sobelHM.Matrix, sobelHM.Width, sobelHM.Height);
                var matrix = core.Image.ConvertArrayToMatrixDoubles(rowobj.Matrix, rowobj.Width, rowobj.Height);
                var sobelMagMatrix = core.Image.MagnitudeFromDefoultMatrixForVisualization(matrix);

                var arrM = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelMagMatrix));
                var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = rowobj.Component, Height = sobelMagMatrix.GetLength(1), Type = "SobelM", Width = sobelMagMatrix.GetLength(0), Matrix = arrM };
                dal.ImageDal.Add(image);
                core.Localization.SaveMatrix(sobelMagMatrix, image.Name + "_SobelM");
                dal.ImageDal.SetMagnitude(rowobj.Id, image.Id);
                Bitmap img = core.Image.ConvertArrayToImage(arrM, rowobj.Width-2, rowobj.Height-2);
                core.Localization.SaveImage(img, image.Name);

            }
            else
            {

            }
        }

        private void SobelV_Click(object sender, RoutedEventArgs e)
        {

            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                MatrixDetection rowobj = (MatrixDetection)imgDataGrid.Items[i];  // this give you access to the row
                var matrix = core.Image.ConvertArrayToMatrixDoubles(rowobj.Matrix, rowobj.Width, rowobj.Height);
                var sobelvMatrix = core.Image.SobelVOperationDForVisualization(matrix);
                var arrM = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelvMatrix));
                var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = rowobj.Component, Height = rowobj.Height, Type = "SobelV", Width = rowobj.Width, Matrix = arrM };
                dal.ImageDal.Add(image);
                core.Localization.SaveMatrix(sobelvMatrix, image.Name + "_SobelV");
                dal.ImageDal.SetSobelV(rowobj.Id, image.Id);
                Bitmap img = core.Image.ConvertArrayToImage(arrM, rowobj.Width, rowobj.Height);
                core.Localization.SaveImage(img, image.Name);

            }
            else
            {

            }
        }
        private void SobelH_Click(object sender, RoutedEventArgs e)
        {
            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                MatrixDetection rowobj = (MatrixDetection)imgDataGrid.Items[i];  // this give you access to the row
                var matrix = core.Image.ConvertArrayToMatrixDoubles(rowobj.Matrix, rowobj.Width, rowobj.Height);
                var sobelhMatrix = core.Image.SobelHOperationDoubleForVisualization(matrix);
                var arrM = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelhMatrix));
                var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = rowobj.Component, Height = rowobj.Height, Type = "SobelH", Width = rowobj.Width, Matrix = arrM };
                dal.ImageDal.Add(image);
                core.Localization.SaveMatrix(sobelhMatrix, image.Name+"_SobelH");
                dal.ImageDal.SetSobelH(rowobj.Id, image.Id);
                Bitmap img = core.Image.ConvertArrayToImage(arrM, rowobj.Width, rowobj.Height);
                core.Localization.SaveImage(img, image.Name);

            }
            else
            {

            }

        }

        private void Estimation_Click(object sender, RoutedEventArgs e)
        {
            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                MatrixDetection rowobj = (MatrixDetection)imgDataGrid.Items[i];  // this give you access to the row
                //var sobelM = dal.ImageDal.GetById(rowobj.BaseId.GetValueOrDefault());//GetMagnitude(rowobj.BaseId.GetValueOrDefault());

                //var matrixM = core.Image.ConvertArrayToMatrix(sobelM.Matrix, sobelM.Width, sobelM.Height);
                var matrix = core.Image.ConvertArrayToMatrixDoubles(rowobj.Matrix, rowobj.Width, rowobj.Height);
                var matrixM = core.Image.MagnitudeFromDefoultMatrix(matrix);
                var estimaion = core.Image.EstimationDouble(matrixM);
                dal.ImageDal.SetEstimation(rowobj.Id,estimaion);

            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imgDataGrid.ItemsSource = null;
            compComboBox.ItemsSource = Enum.GetValues(typeof(CompTypes));
            compComboBox.SelectedItem = CompTypes.R;
            listOfImage = dal.ImageDal.GetAllRoots();
            imgDataGrid.ItemsSource = listOfImage;
        }

        private void CalcAll_Click(object sender, RoutedEventArgs e)
        {
            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                SobelV_Click(sender, e);
                SobelH_Click(sender,e);
                Magnitude_Click(sender,e);
                Estimation_Click(sender,e);
                Button_Click(sender,e);

            }
            else
            {

            }
        }

        private void ImgDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = imgDataGrid.SelectedIndex;
            if (i > -1)
            {
                MatrixDetection rowobj = (MatrixDetection)imgDataGrid.Items[i];  // this give you access to the row
                Bitmap img = core.Image.ConvertArrayToImage(rowobj.Matrix, rowobj.Width, rowobj.Height);

                Imagebox.Source = ImageSourceForBitmap(img);
            }
            else
            {

            }
        }
        //convet bitmap to imagesource need to be there ((
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);

        private ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            imgDataGrid.ItemsSource = null;
            compComboBox.ItemsSource = Enum.GetValues(typeof(CompTypes));
            compComboBox.SelectedItem = CompTypes.R;
            listOfImage = dal.ImageDal.GetAll();
            imgDataGrid.ItemsSource = listOfImage;
        }
        private void LoadAndCalcFromFolder_Click(object sender, RoutedEventArgs e)
        {
            var path = $"{imgpathtextbox.Text}";
            var images = core.Localization.GetImagePathsFromFolder(path);
            foreach (var img in images)
            {
                var w = 0;
                var h = 0;
                var matrix = core.Image.ConvertImageToMatrixByte(img, (CompTypes)compComboBox.SelectedItem, ref w, ref h);
                var arrD = core.Image.ConvertMatreixToArray(matrix);
                var imageD = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = "R", Height = h, Type = Convert.ToString(MatrixTypes.Default), Width = w, Matrix = arrD };
                dal.ImageDal.Add(imageD);
                core.Localization.SaveMatrix(matrix, imageD.Name);
                //SobelV
                var matrixD = core.Image.ConvertImageToMatrix(img, (CompTypes)compComboBox.SelectedItem, ref w, ref h);

                var sobelvMatrix = core.Image.SobelVOperationDForVisualization(matrixD);
                var arrSV = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelvMatrix));
                var imageSV = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = imageD.Component, Height = h, Type = "SobelV", Width = w, Matrix = arrSV};
                dal.ImageDal.Add(imageSV);
                core.Localization.SaveMatrix(sobelvMatrix, imageSV.Name + "_SobelV");
                dal.ImageDal.SetSobelV(imageD.Id, imageSV.Id);
                Bitmap imgSV = core.Image.ConvertArrayToImage(arrSV, w, h);
                core.Localization.SaveImage(imgSV, imageSV.Name);

                //SobelH
                var sobelhMatrix = core.Image.SobelHOperationDoubleForVisualization(matrixD);
                var arrSH = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelhMatrix));
                var imageSH = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = imageD.Component, Height = h, Type = "SobelH", Width = w, Matrix = arrSH};
                dal.ImageDal.Add(imageSH);
                core.Localization.SaveMatrix(sobelhMatrix, imageSH.Name + "_SobelH");
                dal.ImageDal.SetSobelH(imageD.Id, imageSH.Id);
                Bitmap imgSH = core.Image.ConvertArrayToImage(arrSH, w, h);
                core.Localization.SaveImage(imgSH, imageSH.Name);

                //Mag
                var sobelMagMatrix = core.Image.MagnitudeFromDefoultMatrixForVisualization(matrixD);

                var arrM = core.Image.ConvertMatreixToArray(core.Image.Normalize(sobelMagMatrix));
                var imageM = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = imageD.Component, Height = sobelMagMatrix.GetLength(1), Type = "SobelM", Width = sobelMagMatrix.GetLength(0), Matrix = arrM };
                dal.ImageDal.Add(imageM);
                core.Localization.SaveMatrix(sobelMagMatrix, imageM.Name + "_SobelM");
                dal.ImageDal.SetMagnitude(imageD.Id, imageM.Id);
                Bitmap imgM = core.Image.ConvertArrayToImage(arrM, w - 2, h - 2);
                core.Localization.SaveImage(imgM, imageM.Name);
                //est
                var matrixM = core.Image.MagnitudeFromDefoultMatrix(matrixD);
                var estimaion = core.Image.EstimationDouble(matrixM);
                dal.ImageDal.SetEstimation(imageD.Id, estimaion);
            }
            
            Button_Click(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (FromFolderCheckBox.IsChecked==true)
            {
                imgpathtextbox.Text = "Folder Path";
            }
            else
            {
                imgpathtextbox.Text = "Image Path";
            }
        }
    }
}
