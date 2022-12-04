using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class ContactUsCreateResponseModel
    {
        public int ContactType { get; set; }
        public string Value { get; set; }
        public Guid CompanyId { get; set; }
    }
}
