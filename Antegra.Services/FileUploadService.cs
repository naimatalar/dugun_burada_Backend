using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services
{
    public static class FileUploadService
    {


        public static string UploadFile(this IFormFile f, IHostingEnvironment _hostingEnvironment)
        {
            if (f != null)
            {
                string name = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(f.FileName);
                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/Upload/" + name, FileMode.Create))
                {

                    f.CopyTo(fileStream);
                }

                return name;
            }
            return null;
        }

        public static dynamic UploadImage(this IFormFile f, IHostingEnvironment _hostingEnvironment)
        {
            var extFormat = ImageFormat.Png;
            if (Path.GetExtension(f.FileName) == ".png") { extFormat = ImageFormat.Png; }
            if (Path.GetExtension(f.FileName) == ".jpg") { extFormat = ImageFormat.Jpeg; }
            if (Path.GetExtension(f.FileName) == ".jpeg") { extFormat = ImageFormat.Jpeg; }

            if (f != null)
            {
                
                string name = Guid.NewGuid().ToString().Replace("-", "");
                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    var Image900 = f.ResizeImage(1200);
                    Image900.Save(fileStream, extFormat);
                }

                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + "_x2" + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    var Image100=f.ResizeImage(600);
                    Image100.Save(fileStream, extFormat);
                }
                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + "_x1" + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    var Image400= f.ResizeImage(300);
                    Image400.Save(fileStream, extFormat);
                }
                return new { name , extension=Path.GetExtension(f.FileName) };
            }
            return null;
        }

        public static void DeleteImage(string fileName, IHostingEnvironment _hostingEnvironment)
        {
            var ext = Path.GetExtension(fileName);
            var fileN = fileName.Split('.')[0];
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + fileName);
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + fileN+"_x1"+ext);
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/"+ fileN + "_x2" + ext);
            }
            catch (Exception)
            {
            }

        }


        public static void Delete(string fileName, IHostingEnvironment _hostingEnvironment)
        {
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/Upload/" + fileName);
            }
            catch (Exception)
            {
            }

        }



    }
}
