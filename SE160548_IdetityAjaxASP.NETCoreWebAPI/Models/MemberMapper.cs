using AutoMapper;
using AutoMapper.Execution;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Models
{
    public class MemberMapper
    {
        public MemberMapper() { }
        public MemberMapper(int id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
