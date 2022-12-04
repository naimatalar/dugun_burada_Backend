using Labote.Api.BindingModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;

        public AboutUsController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("GetAboutUsByCompanyId/{Id}")]
        public async Task<BaseResponseModel> GetAboutUsByCompanyId(Guid Id)
        {
            var data = _context.AboutUses.Where(x => x.CompanyId == Id).FirstOrDefault();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CreateAboutUsRequestModel model)
        {
            var exist = _context.AboutUses.Where(x => x.CompanyId == model.CompanyId).FirstOrDefault();
            if (exist == null)
            {
                var faq = _context.AboutUses.Add(new Core.Entities.Administrative.AboutUs
                {
                    Text = model.Text,
                    CompanyId = model.CompanyId
                });
                _context.SaveChanges();
            }
            else
            {
                exist.Text = model.Text;
                _context.Update(exist);
                _context.SaveChanges();
            }

            return PageResponse;
        }
        [HttpGet("Delete")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Delete(Guid Id)
        {

            var data = _context.AboutUses.Where(x => x.Id == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }


    }
}
