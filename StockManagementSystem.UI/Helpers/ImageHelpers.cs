using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
