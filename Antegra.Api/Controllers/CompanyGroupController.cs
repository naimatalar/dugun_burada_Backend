using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGroupController : LaboteControllerBase
    {
        private const string pageName = "firma-gruplari";
        private readonly LaboteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CompanyGroupController(LaboteContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("UploadFile")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> UploadFile(FileUploadControllerModel model)
        {

            var dd = _context.CompanyGroups.Where(x => x.Id == model.Id).FirstOrDefault();
            if (!string.IsNullOrEmpty(dd.ImageUrl))
            {
                FileUploadService.Delete(dd.ImageUrl, _hostingEnvironment);
            }

            dd.ImageUrl = model.FileName;
            _context.Update(dd);
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpPost("FileDelete")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> FileDelete(FileUploadControllerModel model)
        {

            var dd = _context.CompanyGroups.Where(x => x.Id == model.Id).FirstOrDefault();
            if (!string.IsNullOrEmpty(dd.ImageUrl))
            {
                FileUploadService.Delete(dd.ImageUrl, _hostingEnvironment);
            }

            dd.ImageUrl = null;
            _context.Update(dd);
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CompanyGroupCreateModel model)
        {
            var mdl = new Core.Entities.CompanyGroup
            {
                Name = model.Name
            };
            _context.CompanyGroups.Add(mdl);
            _context.SaveChanges();
            PageResponse.Data = mdl;
            return PageResponse;
        }



        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CompanyGroupCreateModel model)
        {
            var data = _context.CompanyGroups.Where(x => x.Id == model.Id).FirstOrDefault();
            data.Name = model.Name;
            _context.Update(data);
            _context.SaveChanges();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("Delete/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Delete(Guid Id)
        {
            var data = _context.CompanyGroups.Where(x => x.Id == Id).FirstOrDefault();
            data.IsDelete = true;
            _context.CompanyGroups.Update(data);
            _context.SaveChanges();
            return PageResponse;
        }


        [HttpGet("GetAll")]
        public async Task<BaseResponseModel> GetAll()
        {
            try
            {
                var data = _context.CompanyGroups.Include(x => x.CompanyTypes).Where(x => x.IsActive && !x.IsDelete).Select(x => new
                {
                    x.Name,
                    x.Id,
                }).ToList();
                PageResponse.Data = data;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }


        [HttpGet("GetById/{Id}")]
        public async Task<BaseResponseModel> GetById(Guid Id)
        {
            var data = _context.CompanyGroups.Where(x => x.IsActive && !x.IsDelete).Select(x => new
            {
                x.Name,
                x.ImageUrl,
                x.Id,
            }).FirstOrDefault();
            PageResponse.Data = data;
            return PageResponse;
        }

    }
}
