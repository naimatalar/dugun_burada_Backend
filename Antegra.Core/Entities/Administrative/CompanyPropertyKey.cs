using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labote.Core.Constants.Enums;

namespace Labote.Core.Entities.Administrative
{
    public class CompanyPropertyKey:BaseEntity
    {
        public CompanyPropertyKind CompanyPropertyKind { get; set; }
        public ICollection<CompanyPropertyValue> CompanyPropertyValues { get; set; }
        public string Key { get; set; }
        public bool IsOnlyValue { get; set; } = false;
        public bool IsPrimary { get; set; }
        public bool IsDefault { get; set; }
        public CompanyPropertyValueType CompanyPropertyValueType { get; set; }
    }
}
