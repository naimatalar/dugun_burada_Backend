using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request.IndexData
{
    public class SelectedCompanyTypeCompany
    {

        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CompanyTypeId { get; set; }
    }
}
