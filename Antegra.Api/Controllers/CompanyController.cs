﻿using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request.company;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CompanyController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("GetCompanyType")]
        public async Task<BaseResponseModel> GetProperyType()
        {
            var data = _context.CompanyTypes.Where(x => !x.IsDelete&&x.IsActive                                                                       )
                .Select(x => new
                {
                    x.Name,
                    x.Id,

                });
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("GetCompanyPropertyTypeById/{Id}")]
        public async Task<BaseResponseModel> GetCompanyPropertyType(Guid Id)
        {
            var data = _context.CompanyPropertyKeys.Where(x=>x.CompanyTypeId==Id).Where(x => !x.IsDelete)
                .Select(x => new
                {
                    x.Key,
                    x.Id,
                    CompanyPropertyKind=(int)x.CompanyPropertyKind
                });
            PageResponse.Data = data.ToList();
            return PageResponse;
        }

        [HttpPost("AddCompanyProperty")]
        public async Task<BaseResponseModel> AddCompanyProperty(AddCompanyPropertyRequestModel model)
        {
            var data = _context.CompanyPropertyValues.Add(new Core.Entities.Administrative.CompanyTypePropertyValue { 
            CompanyId=model.CompanyId,
            CompanyPropertyKeyId=model.CompanyPropertyKeyId,
            Value=model.Value,
            
            });
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpGet("GetCompanyPropertyValueByCompanyId/{id}")]
        public async Task<BaseResponseModel> GetCompanyPropertyValueByCompanyId(Guid Id)
        {
            var data = _context.CompanyPropertyValues.Where(x => x.CompanyId == Id).Select(x => new
            {
                x.CompanyPropertyKey.Key,
                x.Value,
               
            });
            _context.SaveChanges();
            return PageResponse;
        }



        [HttpGet("GetByCurrentUser")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> GetByCurrentUser()
        {
            var data = _context.FirmUserLaboteUsers.Include(x=>x.Company).ThenInclude(x=>x.CompanyType).Include(x=>x.Company.CompanyPropertyValues).Where(x => x.LaboteUserId == CurrentUser.Id && !x.Company.IsDelete)
                .Select(x => new
                {
                    CompanyName = x.Company.Name,
                    CompanyLogo = x.Company.LogoUrl,
                    CompanyType=x.Company.CompanyType.Name,
                    CompanyTypeId= x.Company.CompanyType.Id,
                    x.Company.IsPublish,
                    x.CompanyId,
                    CompanyPropertyValuesCount=x.Company.CompanyPropertyValues.Count(),
                    x.Id
                })
                .ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CompanyCreateRequestModel model)
        {
            var comp = new Company();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                     comp = new Company
                    {
                        Name = model.Name,
                         CompanyTypeId = model.CompanyTypeId
                     };
                    context.Companies.Add(comp);
                    context.FirmUserLaboteUsers.Add(new Core.Entities.CompanyUserLaboteUser
                    {
                        CompanyId = comp.Id,
                        LaboteUserId = CurrentUser.Id
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            PageResponse.Data = comp;
            return PageResponse;
        }

        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CompanyCreateRequestModel model)
        {
            var comp = new Company();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    comp=context.Companies.Where(x => x.Id == model.Id).FirstOrDefault();

                    comp. Name = model.Name;
                    comp.CompanyTypeId = model.CompanyTypeId;
                    
                    context.Companies.Update(comp);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            PageResponse.Data = comp;
            return PageResponse;
        }

        [HttpPost("UploadFile")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> UploadFile(FileUploadControllerModel model)
        {

            var dd = _context.Companies.Where(x => x.Id == model.Id).FirstOrDefault();
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
            var data = _context.Companies.FirstOrDefault(x => x.Id == Id);
            PageResponse.Data = data;
            return PageResponse;

        }

        [HttpGet("Delete/{Id}")]
        public async Task<BaseResponseModel> Delete(Guid Id)
        {

            var data = _context.Companies.FirstOrDefault(x => x.Id == Id);
            data.IsDelete = true;
            _context.Update(data);
            _context.SaveChanges();
            return PageResponse;

        }
    }
}
