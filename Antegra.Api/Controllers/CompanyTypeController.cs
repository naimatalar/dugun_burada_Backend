using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels;
using Labote.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTypeController : LaboteControllerBase
    {
        private const string pageName = "firma-turleri";
        private readonly LaboteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CompanyTypeController(LaboteContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetCompanyType")]
        public async Task<BaseResponseModel> GetProperyType()
        {
            var data = _context.CompanyTypes.Where(x => !x.IsDelete)
                .Select(x => new
                {
                    x.Name,
                    CompanyCount = x.Companies.Count(),
                    x.Id,
                  
                });
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CreateWithStringRequestModel model)
        {
            var data = new Core.Entities.Administrative.CompanyType { Name = model.Name };
            _context.CompanyTypes.Add(data);
          
            _context.SaveChanges(); 
            PageResponse.Data = data;
            return PageResponse;

        }

        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CreateWithStringRequestModel model)
        {

            var data = _context.CompanyTypes.FirstOrDefault(x => x.Id == model.Id);
            data.Name = model.Name;
            _context.Update(data);
            _context.SaveChanges();
            PageResponse.Data = data;
            return PageResponse;

        }

        [HttpPost("UploadFile")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> UploadFile(FileUploadControllerModel model)
        {

            var dd = _context.CompanyTypes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (!string.IsNullOrEmpty(dd.LogoUrl))
            {
                FileUploadService.Delete(dd.LogoUrl, _hostingEnvironment);
            }

            dd.LogoUrl = model.FileName;
            _context.Update(dd);
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpPost("FileDelete")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> FileDelete(FileUploadControllerModel model)
        {

            var dd = _context.CompanyTypes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (!string.IsNullOrEmpty(dd.LogoUrl))
            {
                FileUploadService.Delete(dd.LogoUrl, _hostingEnvironment);
            }

            dd.LogoUrl = null;
            _context.Update(dd);
            _context.SaveChanges();
            return PageResponse;
        }


        [HttpGet("GetById/{Id}")]
        public async Task<BaseResponseModel> GetById(Guid Id)
        {
            var data = _context.CompanyTypes.FirstOrDefault(x => x.Id == Id);
            PageResponse.Data = data;
            return PageResponse;

        }

        [HttpGet("Delete/{Id}")]
        public async Task<BaseResponseModel> Delete(Guid Id)
        {

            var data = _context.CompanyTypes.FirstOrDefault(x => x.Id == Id);
            data.IsDelete = true;
            _context.Update(data);
            _context.SaveChanges();
            return PageResponse;

        }


    }
}
