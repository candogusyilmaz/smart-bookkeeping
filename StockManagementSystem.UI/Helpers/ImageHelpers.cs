using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace StockManagementSystem.UI
{
    public static class ImageHelpers
    {
        public static byte[] ToByteArray(string imagePath)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Image img = Image.FromFile(imagePath);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static void SaveBitmap(this byte[] value, string filename)
        {
            if (!(filename.EndsWith(".jpg") || filename.EndsWith(".jpeg")))
                filename += ".jpeg";

            using (MemoryStream ms = new MemoryStream(value))
            {
                new Bitmap(ms).Save(filename, ImageFormat.Jpeg);
            }
        }
    }
}
