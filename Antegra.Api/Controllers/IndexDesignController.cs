using Labote.Core.BindingModels.request;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Labote.Core;
using Labote.Api.BindingModel;
using Labote.Api.Controllers.LaboteController;
using System.Linq;
using Labote.Api.BindingModel.RequestModel;
using Labote.Core.BindingModels.request.IndexData;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexDesignController : LaboteControllerBase
    {
        private const string pageName = "anasayfa-dizayn";
        private readonly LaboteContext _context;

        public IndexDesignController(LaboteContext context)
        {
            _context = context;
        }
        #region indexCategory
        [HttpGet("getIndexCategories")]
        public async Task<BaseResponseModel> getIndexCategories()
        {

            var data = _context.IndexCategories.Select(x => new
            {
                x.Id,
                x.Description,
                x.CompanyType.Name,
                x.CompanyTypeId,
                x.CompanyType.LogoUrl
            });
            PageResponse.Data = data;
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpGet("createIndexCategory/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> createIndexCategory(Guid Id)
        {

            var data = _context.IndexCategories.Add(new Core.Entities.IndexCategory
            {
                CompanyTypeId = Id,
            });
            _context.SaveChanges();
            
            return PageResponse;
        }
        [HttpGet("removeIndexCategory/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> removeIndexCategory(Guid Id)
        {

            var data = _context.IndexCategories.Where(x => x.Id == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
        #endregion

        #region DiscountCompany
        [HttpGet("getDiscountCompany")]
        public async Task<BaseResponseModel> getDiscountCompany()
        {

            var data = _context.DiscountCompanies.Select(x => new
            {
                x.Id,
                x.Description,
                x.CompanyId,
                x.Company.Name,
                x.Discount,
                x.Company.LogoUrl
            });
            PageResponse.Data = data;
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpPost("createDiscountCompany")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> createDiscountCompany(DiscountRequestModel model)
        {

            _context.DiscountCompanies.Add(new Core.Entities.DiscountCompany
            {
                CompanyId = model.CompanyId,
                Discount = model.Discount,
                Description = model.Description,
            });
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpGet("removeDiscountCompany/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> removeDiscountCompany(Guid Id)
        {

            var data = _context.DiscountCompanies.Where(x => x.Id == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
        #endregion

        #region SelectedCompany
        [HttpGet("getSelectedCompany")]
        public async Task<BaseResponseModel> getSelectedCompany()
        {

            var data = _context.SelectedCompanies.Select(x => new
            {
                x.Id,
                x.Description,
                x.CompanyId,
                x.Company.Name,
                x.Company.UrlName,
                x.Company.LogoUrl
            });
            PageResponse.Data = data;
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpPost("createSelectedCompany")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> createSelectedCompany(DiscountRequestModel model)
        {

            _context.SelectedCompanies.Add(new Core.Entities.SelectedCompany
            {
                CompanyId = model.CompanyId,
                Description = model.Description,
            });
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpGet("removeSelectedCompany/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> removeSelectedCompany(Guid Id)
        {

            var data = _context.SelectedCompanies.Where(x => x.Id == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
        #endregion

        #region SelectedCompanyTypeCompany
        [HttpGet("getSelectedCompanyTypeCompany")]
        public async Task<BaseResponseModel> getSelectedCompanyTypeCompany()
        {

            var data = _context.SelectedCompanyTypeCompanies.Select(x => new
            {
                x.Id,
                x.Description,
                x.CompanyId,
                x.Company.Name,
                x.Company.LogoUrl
            });
            PageResponse.Data = data;
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpPost("createSelectedCompanyTypeCompany")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> creategetSelectedCompanyTypeCompany(SelectedCompanyTypeCompany model)
        {
            var data = _context.CompanyTypes.Where(x => x.Id == model.CompanyTypeId).FirstOrDefault();
            _context.SelectedCompanyTypeCompanies.Add(new Core.Entities.SelectedCompanyTypeCompany
            {
                CompanyId = model.CompanyId,
                Description = model.Description,
                Title = data.Name
            });
            _context.SaveChanges();
            return PageResponse;
        }
        [HttpGet("removeSelectedCompanyTypeCompany/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> removeSelectedCompanyTypeCompany(Guid Id)
        {

            var data = _context.SelectedCompanyTypeCompanies.Where(x => x.Id == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
        #endregion
    }
}
