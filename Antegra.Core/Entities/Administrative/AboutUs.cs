using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
   public class AboutUs:BaseEntity
    {
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Text { get; set; }
        
    }
}
