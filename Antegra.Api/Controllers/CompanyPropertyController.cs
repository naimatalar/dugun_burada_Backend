using Labote.Api.BindingModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request;
using Labote.Core.Constants;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyPropertyController : LaboteControllerBase
    {
        private const string pageName = "ozellik-tanimlari";
        private readonly LaboteContext _context;

        public CompanyPropertyController(LaboteContext context)
        {
            _context = context;
        }


        [HttpPost("Create")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Create(CompanyPropertyCreateEditRequestModel model)
        {

            _context.CompanyPropertyKeys.Add(new Core.Entities.Administrative.CompanyPropertyKey
            {
                CompanyPropertyKind = (Enums.CompanyPropertyKind)model.CompanyPropertyKind,
                IsOnlyValue = model.IsOnlyValue,
                IsPrimary = model.IsPrimary,
                IsDefault = model.IsDefault,
                Key = model.Key,
                CompanyPropertyValueType=(Enums.CompanyPropertyValueType)model.CompanyPropertyValueType
            });
            _context.SaveChanges();

            return PageResponse;
        }
        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CompanyPropertyCreateEditRequestModel model)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.Id == model.Id).FirstOrDefault();

            data.CompanyPropertyKind = (Enums.CompanyPropertyKind)model.CompanyPropertyKind;
            data.IsOnlyValue = model.IsOnlyValue;
            data.IsPrimary = model.IsPrimary;
            data.IsDefault = model.IsDefault;   
            data.Key = model.Key;
            data.CompanyPropertyValueType = (Enums.CompanyPropertyValueType)model.CompanyPropertyValueType;
            _context.CompanyPropertyKeys.Update(data);
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpGet("Delete/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Delete(Guid Id)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.Id == Id).FirstOrDefault();
            data.IsDelete = true;
            _context.CompanyPropertyKeys.Update(data);
            _context.SaveChanges();
            return PageResponse;
        }


        [HttpGet("GetAll")]
        public async Task<BaseResponseModel> GetAll()
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.IsActive && !x.IsDelete).Select(x => new
            {
                x.Key,
                x.IsDefault,
                x.IsOnlyValue,
                x.IsPrimary,
                x.CompanyPropertyValueType,
                CompanyPropertyValueTypeString=x.CompanyPropertyValueType.GetDisiplayDescription(),
                x.Id,
            }).ToList();
            PageResponse.Data = data;
            return PageResponse;
        }
        [HttpGet("GetAllByCompanyPropertyKind/{Kind}")]
        public async Task<BaseResponseModel> GetAllByCompanyPropertyKind(int Kind)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.IsActive && !x.IsDelete && x.CompanyPropertyKind== (Enums.CompanyPropertyKind)Kind)
                .Select(x => new
                {
                    x.Key,
                    x.IsDefault,
                    x.IsOnlyValue,
                    x.IsPrimary,
                    x.CompanyPropertyValueType,
                    CompanyPropertyValueTypeString = x.CompanyPropertyValueType.GetDisiplayDescription(),
                    x.Id,
                })
                .ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("GetById/{Id}")]
        public async Task<BaseResponseModel> GetById(Guid Id)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.Id == Id)
                .Select(x => new
                {
                    x.Key,
                    x.IsDefault,
                    x.IsOnlyValue,
                    x.IsPrimary,
                    CompanyPropertyValueType=(int)x.CompanyPropertyValueType,
                    CompanyPropertyValueTypeString = x.CompanyPropertyValueType.GetDisiplayDescription(),
                    x.Id,
                })
                .FirstOrDefault();
            PageResponse.Data = data;
            return PageResponse;
        }




    }
}
