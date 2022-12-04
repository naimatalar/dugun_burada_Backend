using Labote.Api.Controllers.LaboteController;
using Labote.Core;
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
    public class CompanyImageController : LaboteControllerBase
    {
        private const string pageName = "firma-listesi";
        private readonly LaboteContext _context;

        public CompanyImageController(LaboteContext context)
        {
            _context = context;
        }
    }
}
