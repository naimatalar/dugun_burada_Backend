using Labote.Core.Entities.Administrative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class IndexCategoryCompany:BaseEntity
    {
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
