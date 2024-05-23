using System.ComponentModel.DataAnnotations;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
