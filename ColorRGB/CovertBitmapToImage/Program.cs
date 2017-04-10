using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace CovertBitmapToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] imageData;

            // Create the byte array.
            var originalImage = Image.FromFile(@"E:\original1.bmp");
               var ms = new MemoryStream();
                originalImage.Save(ms, ImageFormat.Bmp);
                imageData = ms.ToArray();
            for (int i = 0; i < imageData.Length; i++)
            {
                Console.Write(imageData[i]+ " ");
                
            }
             // Convert back to image.
            var ms1 = new MemoryStream(imageData);
            Image image = Image.FromStream(ms1);
            image.Save(@"E:\newImage.bmp");

            Console.ReadKey();

        }
    }
}
