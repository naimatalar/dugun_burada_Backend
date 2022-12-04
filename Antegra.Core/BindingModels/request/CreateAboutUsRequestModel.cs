using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class CreateAboutUsRequestModel
    {
        public Guid CompanyId { get; set; }
        public string Text { get; set; }

    }
}
