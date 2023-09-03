using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Mvc.Attributes;
using SimpleAccountSystem.Mvc.Commons;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    [StepUpAuthentication]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            var genericResult = new GenericListResultDto<IdentityUser>();
            genericResult.List = users;

            return View(genericResult);
        }

        //[HttpGet]
        //public IActionResult FilterByRoles()
        //{

        //}
    }
}
