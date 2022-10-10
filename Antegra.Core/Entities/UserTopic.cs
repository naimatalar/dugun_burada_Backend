using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class UserTopic:BaseEntity
    {
        //public Guid CreatorUserId { get; set; }
        public virtual ICollection<LaboteUser> LaboteUsers { get; set; }

        public string TopicCode { get; set; } = DateTime.Now.ToString("yy-HHmmMMss-dd");
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
