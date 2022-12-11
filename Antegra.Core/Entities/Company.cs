using Labote.Core.Entities.Administrative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public bool IsPublish { get; set; } = false;
        public CompanyType CompanyType { get; set; }
        public Guid CompanyTypeId { get; set; }
        public virtual ICollection<CompanyUserLaboteUser> FirmUserLaboteUsers { get; set; }
        public virtual ICollection<CompanyTypePropertyValue> CompanyPropertyValues { get; set; }
        public virtual ICollection<PropertySelectListValue> PropertySelectListValues { get; set; }
        public virtual ICollection<Faq> Faqs { get; set; }
        public virtual ICollection<AboutUs> AboutUses { get; set; }
        public virtual ICollection<ContactUs> ContactUses { get; set; }
        public virtual ICollection<CompanyImage> CompanyImages { get; set; }


    }
}
