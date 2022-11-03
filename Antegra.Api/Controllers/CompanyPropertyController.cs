using Labote.Api.BindingModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels.request.company;
using Labote.Core.Constants;
using Labote.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                CompanyTypeId = model.CompanyTypeId,
                CompanyPropertyValueType = (Enums.CompanyPropertyValueType)model.CompanyPropertyValueType
            });
            _context.SaveChanges();

            return PageResponse;
        }

        [HttpPost("CreateList")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> CreateList(CompanyPropertyListRequestModel model)
        {

            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var property = new Core.Entities.Administrative.CompanyPropertyKey
                    {
                        CompanyPropertyKind = (Enums.CompanyPropertyKind)model.CompanyPropertyKind,
                        IsOnlyValue = model.IsOnlyValue,
                        IsPrimary = model.IsPrimary,
                        IsDefault = model.IsDefault,
                        Key = model.Key,
                        CompanyTypeId = model.CompanyTypeId,
                        CompanyPropertyValueType = (Enums.CompanyPropertyValueType)model.CompanyPropertyValueType
                    };
                    context.CompanyPropertyKeys.Add(property);
                    foreach (var item in model.ValueList)
                    {
                        context.PropertySelectLists.Add(new Core.Entities.Administrative.PropertySelectList
                        {
                            CompanyPropertyKeyId = property.Id,
                            Item = item
                        });
                    }



                    context.SaveChanges();
                    transaction.Commit();
                }
            }




            return PageResponse;
        }

        [HttpPost("Edit")]
        [PermissionCheck(Action = pageName)]
        public async Task<BaseResponseModel> Edit(CompanyPropertyListRequestModel model)
        {
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var data = context.CompanyPropertyKeys.Include(x => x.PropertySelectLists).Where(x => x.Id == model.Id).FirstOrDefault();

                    data.CompanyPropertyKind = (Enums.CompanyPropertyKind)model.CompanyPropertyKind;
                    data.IsOnlyValue = model.IsOnlyValue;
                    data.IsPrimary = model.IsPrimary;
                    data.IsDefault = model.IsDefault;
                    data.Key = model.Key;
                    data.CompanyTypeId = model.CompanyTypeId;
                    data.CompanyPropertyValueType = (Enums.CompanyPropertyValueType)model.CompanyPropertyValueType;
                    context.CompanyPropertyKeys.Update(data);
                    var slList = data.PropertySelectLists.ToList();
                    foreach (var item in model.ValueList)
                    {
                        if (!slList.Any(x => x.Item == item))
                        {
                            context.PropertySelectLists.Add(new Core.Entities.Administrative.PropertySelectList
                            {
                                CompanyPropertyKeyId = data.Id,
                                Item = item
                            });
                        }
                    }
                    foreach (var item in slList)
                    {
                        if (!model.ValueList.Any(x => x == item.Item))
                        {
                            item.IsDelete = true;
                            context.PropertySelectLists.Update(item);
                        } 
                        
                    }

                   


                    context.SaveChanges();
                    transaction.Commit();
                }

            }
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
                CompanyPropertyValueTypeString = x.CompanyPropertyValueType.GetDisiplayDescription(),
                x.Id,
            }).ToList();
            PageResponse.Data = data;
            return PageResponse;
        }
        [HttpGet("GetAllByCompanyPropertyKind/{Kind}/{TypeId}")]
        public async Task<BaseResponseModel> GetAllByCompanyPropertyKind(int Kind, Guid TypeId)
        {
            var data = _context.CompanyPropertyKeys.Where(x => x.IsActive && !x.IsDelete && x.CompanyPropertyKind == (Enums.CompanyPropertyKind)Kind && x.CompanyTypeId == TypeId)
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
            var data = _context.CompanyPropertyKeys.Include(x=>x.PropertySelectLists).Where(x => x.Id == Id && x.IsActive && !x.IsDelete)
                .Select(x => new
                {
                    x.Key,
                    x.IsDefault,
                    x.IsOnlyValue,
                    x.IsPrimary,
                    CompanyPropertyValueType = (int)x.CompanyPropertyValueType,
                    CompanyPropertyValueTypeString = x.CompanyPropertyValueType.GetDisiplayDescription(),
                    x.Id,
                    ValueList = x.PropertySelectLists.Where(x =>  x.IsActive && !x.IsDelete).Select(y => new { y.Item, y.Id })
                })
                .FirstOrDefault();


            PageResponse.Data = data;
            return PageResponse;
        }




    }
}
