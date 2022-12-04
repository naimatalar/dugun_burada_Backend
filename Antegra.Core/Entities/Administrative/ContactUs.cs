using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labote.Core.Constants.Enums;

namespace Labote.Core.Entities.Administrative
{
    public class ContactUs:BaseEntity
    {
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
