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
    public class FaqController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;

        public FaqController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("GetFaqByCompanyId/{companyId}")]
        public async Task<BaseResponseModel> GetFaqByCompanyId(Guid companyId)
        {
            var faq = _context.Faqs.Where(x => x.CompanyId == companyId).ToList();
            PageResponse.Data = faq;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(FaqCreateRequestModel model)
        {

            var faq = _context.Faqs.Add(new Core.Entities.Administrative.Faq
            {
                Ansver = model.Ansver,
                Question = model.Question,
                CompanyId = model.CompanyId
            });
            _context.SaveChanges();
            
            return PageResponse;
        }
        [HttpGet("Delete/{faqId}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Delete(Guid faqId)
        {

            var faq = _context.Faqs.Where(x => x.Id == faqId).FirstOrDefault();
            _context.Remove(faq);
            _context.SaveChanges();
            PageResponse.Data = faq;
            return PageResponse;
        }

    }
}
