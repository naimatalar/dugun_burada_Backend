using Labote.Api.BindingModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request.company;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : LaboteControllerBase
    {
        private const string pageName = "yetki-islemleri";
        private readonly LaboteContext _context;

        public CompanyController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("GetProperyType")]
        public async Task<BaseResponseModel> GetProperyType()
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.IsDefault);
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("GetByCurrentUser")]
        public async Task<BaseResponseModel> GetByCurrentUser()
        {
            var data = _context.FirmUserLaboteUsers.Where(x => x.LaboteUserId == CurrentUser.Id && !x.IsDelete)
                .Select(x => new
                {
                    CompanyName = x.Company.Name,
                    CompanyLogo = x.Company.Logo,
                    x.Company.IsPublish,
                    x.Id,
                })
                .ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CompanyCreateRequestModel model)
        {
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var comp = new Core.Entities.Company
                    {
                        Name = model.Name,
                        Logo = model.Logo,
                    };
                    context.Companies.Add(comp);
                    context.FirmUserLaboteUsers.Add(new Core.Entities.CompanyUserLaboteUser
                    {
                        CompanyId = comp.Id,
                        LaboteUserId = CurrentUser.Id
                    });

              

                }
            }


            return PageResponse;

        }
    }
}
