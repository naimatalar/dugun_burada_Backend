using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities.Administrative
{
   public class Faq:BaseEntity
    {
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Question { get; set; }
        public string Ansver { get; set; }
    }
}
