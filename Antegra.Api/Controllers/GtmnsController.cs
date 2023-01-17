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

           var dd= _context.CompanyGroups.Select(x => new {
                CompanyTypes= x.CompanyTypes.Where(y=>y.ShowMenu==true).Select(z=>new { 
                    z.Id,
                  z.LogoUrl,
                  z.Name,
                }),
                x.Name,
                x.Id
            }).ToList();
            var ddsa = _context.CompanyTypes.Where(y => y.ShowMenu == true).Select(x => new {x.Name,x.Id,x.LogoUrl});
            
             PageResponse.Data= new { groupMenu = dd.Where(x => x.CompanyTypes.Count() > 0) ,singleMenu=ddsa};
            return PageResponse;
        }
    }
}
