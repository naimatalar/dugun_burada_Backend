using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
   public class PropertySelectList:BaseEntity
    {
        public CompanyPropertyKey CompanyPropertyKey { get; set; }
        public Guid CompanyPropertyKeyId { get; set; }
        public string Item { get; set; }
    }
}
