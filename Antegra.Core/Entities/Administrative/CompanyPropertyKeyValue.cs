using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labote.Core.Constants.Enums;

namespace Labote.Core.Entities.Administrative
{
    public class CompanyPropertyKeyValue:BaseEntity
    {
        public CompanyPropertyKind CompanyPropertyKind { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Key { get; set; }
        public bool IsOnlyValue { get; set; } = false;
        public bool IsPrimary { get; set; }
        public string Value { get; set; }

    }
}
