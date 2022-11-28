using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
    public class PropertySelectListValue:BaseEntity
    {
        public PropertySelectList PropertySelectList { get; set; }
        public Guid PropertySelectListId { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        
    }
}
