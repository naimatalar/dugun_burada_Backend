using Labote.Core.Entities.Administrative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<FirmUserLaboteUser> FirmUserLaboteUsers { get; set; }
        public virtual ICollection<CompanyPropertyValue> CompanyPropertyValues { get; set; }

    }
}
