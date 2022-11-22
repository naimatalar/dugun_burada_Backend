using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
   public  class PropertyValueSetRequestModel
    {
        public object Value { get; set; }
        public Guid ProperyId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
