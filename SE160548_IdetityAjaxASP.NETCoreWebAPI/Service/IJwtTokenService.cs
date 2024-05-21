using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Service
{
    public interface IJwtTokenService
    {
        string GenerateToken(Member user);
    }
}
