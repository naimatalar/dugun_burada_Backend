using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
   public class CompanyImage:BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public string ImageUrl { get; set; }
    }
}
