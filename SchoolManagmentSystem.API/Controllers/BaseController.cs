using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SchoolManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IActionContextAccessor _accessor;

        public BaseController(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
