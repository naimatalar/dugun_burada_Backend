using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class CompanyGroup:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public string ImageUrl { get; set; }
    }
}
