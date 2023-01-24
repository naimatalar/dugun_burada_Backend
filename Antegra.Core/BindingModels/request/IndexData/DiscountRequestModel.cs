using Labote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request.IndexData
{
    public class DiscountRequestModel
    {
        public int Discount { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
    }
}
