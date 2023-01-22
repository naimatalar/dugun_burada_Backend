using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels
{
    public class CreateWithStringRequestModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? CompanyGroupId { get; set; }
        public bool ShowMenu { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }

    }
}
