using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
    public class CompanyTypePropertyValue:BaseEntity
    {
        public CompanyPropertyKey CompanyPropertyKey { get; set; }
        public Guid CompanyPropertyKeyId { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Value { get; set; }

    }
}
