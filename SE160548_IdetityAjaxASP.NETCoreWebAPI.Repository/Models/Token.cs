using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models
{
    public partial class Token
    {
        public Token() 
        {
            Members = new HashSet<Member>();
        }
        public int TokenId { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public string TokenValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
