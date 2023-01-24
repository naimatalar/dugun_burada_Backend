using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request;
using Labote.Core.BindingModels.request.company;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CompanyController(LaboteContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetCompanyType")]
        public async Task<BaseResponseModel> GetProperyType()
        {
            var data = _context.CompanyTypes.Where(x => !x.IsDelete && x.IsActive)
                .Select(x => new
                {
                    x.Name,
                    x.Id,

                });
            PageResponse.Data = data;
            return PageResponse;
        }
        [HttpPost("GetCompanyByName")]
        public async Task<BaseResponseModel> GetCompanyByName(GetByNameRequestModel model)
        {
            var data = _context.Companies.Where(x => !x.IsDelete && x.IsActive && x.Name.Contains(model.Name))
                .Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.UrlName,
                }).Take(8);
            PageResponse.Data = data;
            return PageResponse;
        }
        [HttpPost("GetCompanyByNameAndCompanyType")]
        public async Task<BaseResponseModel> GetCompanyByNameAndCompanyType(NameAndIdRequestModel model)
        {
            var data = _context.Companies.Where(x => !x.IsDelete && x.IsActive && x.Name.Contains(model.Name)&&x.CompanyTypeId==model.Id)
                .Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.UrlName,
                }).Take(8);
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("SetProperty")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> SetProperty(PropertyValueSetRequestModel model)
        {
            var dda = _context.CompanyPropertyValues.FirstOrDefault(x => x.CompanyId == model.CompanyId && x.CompanyPropertyKeyId == model.ProperyId);

            if (dda == null)
            {
                if (!string.IsNullOrEmpty(model.Value?.ToString()))
                {
                    _context.CompanyPropertyValues.Add(new Core.Entities.Administrative.CompanyTypePropertyValue
                    {
                        Value = model.Value.ToString(),
                        CompanyPropertyKeyId = model.ProperyId,
                        CompanyId = model.CompanyId

                    });
                }

            }
            else
            {
                if (string.IsNullOrEmpty(model.Value?.ToString()))
                {
                    _context.Remove(dda);
                    _context.SaveChanges();
                }
                else
                {
                    dda.Value = model.Value?.ToString();
                    _context.Update(dda);
                }

            }
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpPost("UploadImage")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> UploadImage(FileUploadWithExtension model)
        {
            var dataCount = _context.CompanyImages.Where(x=>x.CompanyId==model.Id&&x.ImageUrl.Contains("_x1")).Count();
            if (dataCount > 9)
            {
                PageResponse.Message = "En Fazla 10 Adet Resim Yüklenebilir";
                PageResponse.IsError = true;
                return PageResponse;
            }
            _context.CompanyImages.Add(new Core.Entities.Administrative.CompanyImage { CompanyId = model.Id, ImageUrl = model.FileName+model.Extension });
            _context.CompanyImages.Add(new Core.Entities.Administrative.CompanyImage { CompanyId = model.Id, ImageUrl = model.FileName+ "_x2" + model.Extension });
            _context.CompanyImages.Add(new Core.Entities.Administrative.CompanyImage { CompanyId = model.Id, ImageUrl = model.FileName+ "_x1" + model.Extension });

            _context.SaveChanges();
            return PageResponse;
        }

        [HttpGet("GetImages/{companyId}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> GetImages(Guid companyId)
        {
            var resultd = _context.CompanyImages.Where(x => x.CompanyId == companyId);
            PageResponse.Data = resultd;
            return PageResponse;
        }


        [HttpPost("RemoveImage")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> RemoveImage(FileUploadControllerModel model)
        {

            FileUploadService.DeleteImage(model.FileName, _hostingEnvironment);
            var fls = model.FileName.Split('.')[0];
            var files = _context.CompanyImages.Where(x => x.ImageUrl.Contains(fls));
            _context.CompanyImages.RemoveRange(files);
            _context.SaveChanges();
            return PageResponse;
        }


        [HttpPost("SetPropertyList")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> SetPropertyList(PropertyValueSetRequestModel model)
        {
            //var dda = _context.PropertySelectLists.FirstOrDefault(x => x. == model.CompanyId && x.CompanyPropertyKeyId == model.ProperyId);

            //if (dda == null)
            //{
            //    _context.CompanyPropertyValues.Add(new Core.Entities.Administrative.CompanyTypePropertyValue
            //    {
            //        Value = model.Value.ToString(),
            //        CompanyPropertyKeyId = model.ProperyId,
            //        CompanyId = model.CompanyId

            //    });
            //}
            //else
            //{
            //    dda.Value = model.Value.ToString();
            //    _context.Update(dda);
            //}
            //_context.SaveChanges();
            return PageResponse;
        }
        [HttpGet("GetCompanyPropertyTypeById/{Id}")]

        public async Task<BaseResponseModel> GetCompanyPropertyType(Guid Id)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.CompanyTypeId == Id).Where(x => !x.IsDelete)
                .Select(x => new
                {
                    x.Key,
                    x.Id,
                    CompanyPropertyKind = (int)x.CompanyPropertyKind
                });
            PageResponse.Data = data.ToList();
            return PageResponse;
        }

        [HttpPost("AddCompanyProperty")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> AddCompanyProperty(AddCompanyPropertyRequestModel model)
        {
            var hasData = _context.CompanyPropertyValues.Where(x => x.CompanyId == model.CompanyId && x.CompanyPropertyKeyId == model.CompanyPropertyKeyId).FirstOrDefault();
            if (hasData == null)
            {
                var data = _context.CompanyPropertyValues.Add(new Core.Entities.Administrative.CompanyTypePropertyValue
                {
                    CompanyId = model.CompanyId,
                    CompanyPropertyKeyId = model.CompanyPropertyKeyId,
                    Value = model.Value,

                });
                _context.SaveChanges();
            }
            else
            {
                hasData.Value = model.Value;
                _context.Update(hasData);
                _context.SaveChanges();
            }



            return PageResponse;
        }
        [HttpPost("AddCompanyListProperty")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> AddCompanyListProperty(AddCompanyListPropertyRequestModel model)
        {
            var data = _context.PropertySelectListValue.Where(x => x.CompanyId == model.CompanyId && x.PropertySelectListId == model.ItemId).FirstOrDefault();

            if (model.IsActive)
            {
                if (data == null)
                {
                    _context.PropertySelectListValue.Add(new Core.Entities.Administrative.PropertySelectListValue
                    {
                        CompanyId = model.CompanyId,
                        PropertySelectListId = model.ItemId
                    });
                    _context.SaveChanges();
                }
                else
                {
                    _context.Remove(data);

                    _context.SaveChanges();
                }

            }
            else
            {

                _context.Remove(data);

                _context.SaveChanges();
            }

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

        public async Task<BaseResponseModel> GetByCurrentUser()
        {
            var data = _context.FirmUserLaboteUsers.Include(x => x.Company).ThenInclude(x => x.CompanyType).Include(x => x.Company.CompanyPropertyValues).Where(x => x.LaboteUserId == CurrentUser.Id && !x.Company.IsDelete)
                .Select(x => new
                {
                    CompanyName = x.Company.Name,
                    CompanyLogo = x.Company.LogoUrl,
                    CompanyType = x.Company.CompanyType.Name,
                    CompanyTypeId = x.Company.CompanyType.Id,
                    x.Company.IsPublish,
                    x.CompanyId,
                    CompanyPropertyValuesCount = x.Company.CompanyPropertyValues.Count(),
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
            var dd = IlIlceJson.GetIlIlce().Where(x=>x.plaka==model.IlPlaka).FirstOrDefault();
            var comp = new Company();
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var urlName = model.Name.Replace(" ", "-").TurkishCharReplace();
            urlName = rgx.Replace(urlName, "").ToLower();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    comp = new Company
                    {
                        Name = model.Name,
                        CompanyTypeId = model.CompanyTypeId,
                        IlPlaka=model.IlPlaka,
                        Il=dd.il,
                        Ilce=model.Ilce,
                        UrlName=urlName
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
            comp.FirmUserLaboteUsers = null;
            PageResponse.Data = comp;
            return PageResponse;
        }

        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CompanyCreateRequestModel model)
        {
         
            var dd = IlIlceJson.GetIlIlce().Where(x => x.plaka == model.IlPlaka).FirstOrDefault();
            var comp = new Company();
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var urlName = model.Name.Replace(" ","-").TurkishCharReplace();
            urlName = rgx.Replace(urlName, "").ToLower();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    comp = context.Companies.Where(x => x.Id == model.Id).FirstOrDefault();

                    comp.Name = model.Name;
                    comp.CompanyTypeId = model.CompanyTypeId;
                    comp.IlPlaka = model.IlPlaka;
                    comp.Il = dd.il;
                    comp.Ilce = model.Ilce;
                    comp.UrlName = urlName;
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

            var dd = _context.Companies.Where(x => x.Id == model.Id).FirstOrDefault();
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

        [HttpGet("GetFullDataCompanyById/{Id}")]
        public async Task<BaseResponseModel> GetWithCompanyTypeById(Guid Id)
        {
            var data = _context.Companies.Select(x => new
            {
                x.Id,
                x.IsPublish,
                x.Name,
                x.LogoUrl,
                CompanyType = x.CompanyType.Name,
                x.CompanyTypeId,

            }).FirstOrDefault(x => x.Id == Id);
            PageResponse.Data = data;
            return PageResponse;

        }
        [HttpGet("GetCompanyPropertyByCompanyAndPropertyId/{CompanyId}/{PropertyKind}")]
        public async Task<BaseResponseModel> GetCompanyPropertyByCompanyAndPropertyId(Guid CompanyId, int PropertyKind)
        {
            var compnay = _context.Companies.Include(x => x.CompanyPropertyValues).Where(x => x.Id == CompanyId).FirstOrDefault();
            var keys = _context.CompanyPropertyKeys.Where(x => x.CompanyTypeId == compnay.CompanyTypeId && x.CompanyPropertyKind == (Core.Constants.Enums.CompanyPropertyKind)PropertyKind && x.IsDelete == false).Select(x => new
            {
                x.Key,
                CompanyPropertyValueType = (int)x.CompanyPropertyValueType,
                x.Id,
                PropertySelectLists = x.PropertySelectLists.Where(x => !x.IsDelete).Select(z => new
                {
                    z.Item,
                    z.Id,
                    Values = z.PropertySelectListValues.Where(x => !x.IsDelete).Select(x => new
                    {
                        x.Id,
                        x.PropertySelectListId

                    })
                }),

                CompanyPropertyValues = x.CompanyPropertyValues.Where(x => !x.IsDelete).Select(y => new
                {
                    y.Value,

                }).FirstOrDefault()
            }).OrderBy(x => x.CompanyPropertyValueType); ;

            PageResponse.Data = keys;
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
