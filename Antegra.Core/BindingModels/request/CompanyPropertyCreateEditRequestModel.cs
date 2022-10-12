using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels.request
{
    public class CompanyPropertyCreateEditRequestModel
    {
        public Guid? Id { get; set; }
        public int CompanyPropertyKind { get; set; }
        public string Key { get; set; }
        public bool IsOnlyValue { get; set; } = false;
        public bool IsPrimary { get; set; }
        public bool IsDefault { get; set; }
        public int CompanyPropertyValueType { get; set; }
    }
}
