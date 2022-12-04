using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services
{
    public static class ImageResize
    {
        public  static Image ResizeImage(this IFormFile file, int width)
        {
            using (var memoryStream = new MemoryStream())
            {
                 file.CopyTo(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    return img.Resize(width);
                }
            }
        }

        public static Image Resize(this Image image, int width)
        {

            float aspect = image.Width / (float)image.Height;
            int newWidth, newHeight;

            //calculate new dimensions based on aspect ratio
            newWidth = (int)(width);
            newHeight = (int)(newWidth / aspect);

            var res = new Bitmap(newWidth, newHeight);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return res;
        }
    }
}
