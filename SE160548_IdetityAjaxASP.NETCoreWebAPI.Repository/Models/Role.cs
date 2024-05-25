using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models
{
    
    public partial class Role
    {
        public Role()
        {
            Members = new HashSet<Member>();
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
