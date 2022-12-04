using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class FaqCreateRequestModel
    {
        public string Ansver { get; set; }
        public string Question { get; set; }
        public Guid CompanyId { get; set; }

    }
}
