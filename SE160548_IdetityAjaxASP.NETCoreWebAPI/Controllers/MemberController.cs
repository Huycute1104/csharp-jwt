using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public MemberController(IUnitOfwork unitOfwork, IConfiguration configuration, IMapper mapper)
        {
            this.unitOfwork = unitOfwork;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var members = unitOfwork.MemberRepo.Get();
            if (members == null)
            {
                return NotFound();
            }

            var memberDtos = mapper.Map<IEnumerable<MemberMapper>>(members);
            return Ok(memberDtos);
        }
    }
}
