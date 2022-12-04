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
    public class ContactController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;

        public ContactController(LaboteContext context)
        {
            _context = context;
        }

        [HttpGet("GetContactUsByCompanyId/{id}")]
        public async Task<BaseResponseModel> GetContactUsByCompanyId(Guid id)
        {
            var data = _context.ContactUses.Where(x => x.CompanyId == id).ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(ContactUsCreateResponseModel model)
        {
            var dd = _context.ContactUses.Where(x => x.ContactType == (Core.Constants.Enums.ContactType)model.ContactType).FirstOrDefault();
            if (dd == null)
            {
                if (!string.IsNullOrEmpty(model.Value))
                {
                    var faq = _context.ContactUses.Add(new Core.Entities.Administrative.ContactUs
                    {
                        ContactType = (Core.Constants.Enums.ContactType)model.ContactType,
                        Value = model.Value,
                        CompanyId = model.CompanyId
                    });
                    _context.SaveChanges();
                }

            }
            else
            {
                if (string.IsNullOrEmpty(model.Value))
                {
                    _context.Remove(dd);
                    _context.SaveChanges();
                }
                else
                {
                    dd.Value = model.Value;
                    _context.Update(dd);
                    _context.SaveChanges();
                }

            }


            return PageResponse;
        }

        [HttpGet("DeleteContactUsByCompanyId")]
        public async Task<BaseResponseModel> DeleteContactUsByCompanyId(Guid Id)
        {
            var data = _context.ContactUses.Where(x => x.CompanyId == Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();

            return PageResponse;
        }




    }


}
