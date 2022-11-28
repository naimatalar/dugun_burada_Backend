using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class AddCompanyListPropertyRequestModel
    {
        public Guid CompanyId { get; set; }
        public Guid ItemId { get; set; }
        public bool IsActive { get; set; }
    }
}
