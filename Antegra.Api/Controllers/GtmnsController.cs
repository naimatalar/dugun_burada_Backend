using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Labote.Api.BindingModel;
using Labote.Core;
using System.Linq;
using Labote.Core.Entities.Administrative;
using Labote.Core.Constants;
using static Labote.Core.Constants.Enums;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtmnsController : LaboteControllerBase
    {
        private readonly LaboteContext _context;

        public GtmnsController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("algt")]
        [AllowAnonymous]
        public async Task<BaseResponseModel> getMenu()
        {
            var dd = _context.CompanyGroups.Select(x => new
            {
                CompanyTypes = x.CompanyTypes.Where(y => y.ShowMenu == true).Select(z => new
                {
                    z.Id,
                    z.LogoUrl,
                    z.Name,
                    z.UrlName
                }),
                x.Name,
                x.Id
            }).ToList();
            var ddsa = _context.CompanyTypes.Where(y => y.ShowMenu == true).Select(x => new { x.Name, x.Id, x.LogoUrl, x.UrlName });

            PageResponse.Data = new { groupMenu = dd.Where(x => x.CompanyTypes.Count() > 0), singleMenu = ddsa };
            return PageResponse;
        }

        [HttpGet("sctk")]
        [AllowAnonymous]
        public async Task<BaseResponseModel> getSearchMenu()
        {

            var dd = _context.CompanyGroups.Select(x => new
            {
                CompanyTypes = x.CompanyTypes.Select(z => new
                {
                    z.Id,
                    z.LogoUrl,
                    z.Name,
                    z.UrlName
                }),
                x.Name,
                x.Id,
                x.UrlName
            }).ToList();
            var ddsa = _context.CompanyTypes.Select(x => new { x.Name, x.Id, x.LogoUrl, x.UrlName });

            PageResponse.Data = new { groupMenu = dd.Where(x => x.CompanyTypes.Count() > 0), singleMenu = ddsa };
            return PageResponse;
        }
        [HttpGet("rytm")]
        [AllowAnonymous]
        public async Task<BaseResponseModel> GetIndexPage()
        {
            var discount = _context.DiscountCompanies.Select(x => new
            {
                x.Id,
                x.Company.Name,
                x.Discount,
                CompanyImagesCount=x.Company.CompanyImages.Count(),
                x.Company.LogoUrl,
                x.Company.UrlName,
                x.Description,
            }).Take(4).ToList();
            var category = _context.IndexCategories.Select(x => new
            {
                x.Id,
                x.CompanyType.Name,
                CompaniesCount=x.CompanyType.Companies.Count(),
                x.Description,
                x.CompanyType.UrlName,
                x.CompanyType.LogoUrl
            }).Take(3).ToList();

            var selectedCompanyTypeCompanies = _context.SelectedCompanyTypeCompanies.Select(x => new
            {
                x.Id,
                x.Description,
                CompanyName=x.Company.Name,
                x.Company.UrlName,
                CompanyPropertyValues = x.Company.CompanyPropertyValues.Select(y => new
                {
                    y.CompanyPropertyKey.Key,
                    y.Value,
                    CompanyPropertyKind=y.CompanyPropertyKey.CompanyPropertyKind.GetDisiplayDescription()
                })
            }).Take(4).ToList();
            var selectedCompany = _context.SelectedCompanies.Select(x => new
            {
                x.Id,
                x.Description,
                CompanyName = x.Company.Name,
                x.Company.UrlName
            }).ToList();

            PageResponse.Data = new {discount,category, selectedCompanyTypeCompanies,selectedCompany };
            return PageResponse;
        }


    }
}
