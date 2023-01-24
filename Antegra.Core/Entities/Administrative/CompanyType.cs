using Labote.Core.Entities.Administrative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
    public class CompanyType : BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public CompanyGroup CompanyGroup { get; set; }
        public Guid? CompanyGroupId { get; set; }
        public bool ShowMenu { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CompanyPropertyKey> CompanyPropertyKeys { get; set; }
        public virtual ICollection<IndexCategory> IndexCategory { get; set; }

    }
}
