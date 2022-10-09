using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class FirmUserLaboteUser:BaseEntity
    {
        public LaboteUser LaboteUser { get; set; }
        public Company Company { get; set; }
        public Guid LaboteUserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
