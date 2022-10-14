using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request.company
{
    public class AddCompanyPropertyRequestModel
    {
        public Guid CompanyId { get; set; }
        public Guid CompanyPropertyKeyId { get; set; }
        public string Value { get; set; }
    }
}
