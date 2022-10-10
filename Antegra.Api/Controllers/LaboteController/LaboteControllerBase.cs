using Labote.Api.BindingModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;
using Labote.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labote.Api.Controllers.LaboteController
{
    [Authorize]
    public class LaboteControllerBase : ControllerBase

    {
       
 

        public LaboteUser CurrentUser
        {
            get
            {
               var UserId=User.Identity.UserId();
                using (var context = new Core.LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        if (UserId != null)
                        {
                            return  context.Users.FirstOrDefault(x => x.Id == UserId);
                        }
                    }

                }

                return null;
            }
        }

        public string CurrentUserTopic
        {
            get
            {
                var UserId = User.Identity.UserId();
                using (var context = new Core.LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        if (UserId != null)
                        {
                            return context.Users.Include(x=>x.UserTopic).Where(x => x.Id == UserId).Select(x=>x.UserTopic).FirstOrDefault().TopicCode;
                        }
                    }

                }

                return null;
            }
        }


        public BaseResponseModel PageResponse { get; set; } = new BaseResponseModel();
    }
}
