using Labote.Core.Entities.Administrative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class IndexCategory:BaseEntity
    {
        public Guid CompanyTypeId { get; set; }
        public string Description { get; set; }
        public CompanyType CompanyType { get; set; }
    }
}
