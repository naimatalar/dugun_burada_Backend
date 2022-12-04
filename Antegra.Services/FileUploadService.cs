using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
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

        public static string UploadImage(this IFormFile f, IHostingEnvironment _hostingEnvironment)
        {
            if (f != null)
            {
                string name = Guid.NewGuid().ToString().Replace("-", "");
                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    f.ResizeImage(900);
                    f.CopyTo(fileStream);
                }

                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + "_x2" + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    f.ResizeImage(100);
                    f.CopyTo(fileStream);
                }
                using (Stream fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/wwwroot/UploadedImages/" + name + "_x1" + Path.GetExtension(f.FileName), FileMode.Create))
                {
                    f.ResizeImage(400);
                    f.CopyTo(fileStream);
                }
                return name;
            }
            return null;
        }

        public static void DeleteImage(string fileName, IHostingEnvironment _hostingEnvironment)
        {
            var ext = Path.GetExtension(fileName);
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/Upload/" + fileName);
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/Upload/" + fileName.Replace(ext, "_x1" + ext));
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot/Upload/" + fileName.Replace(ext, "_x2" + ext));
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
