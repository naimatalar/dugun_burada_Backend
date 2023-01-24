using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class SelectedCompany:BaseEntity
    {
        public string Description  { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
     
    }
}
