using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Domain.Service;
using SimpleAccountSystem.Mvc.Commons;

namespace SimpleAccountSystem.Mvc.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUserService _userService;

        public RoleController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var dataTableRequestData = HttpContext.Request.Query.ExtractGenericQueryData();

            var roles = _userService.GetRoles(dataTableRequestData.Length, dataTableRequestData.SearchValue);

            return Ok(roles);
        }
    }
}
