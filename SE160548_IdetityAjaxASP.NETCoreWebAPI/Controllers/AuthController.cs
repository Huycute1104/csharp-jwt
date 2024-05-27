using Microsoft.AspNetCore.Mvc;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Service;

using System.Linq;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUnitOfwork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _unitOfWork.MemberRepo.Get()
                               .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }
}
