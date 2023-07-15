using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Mvc.Attributes;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    [StepUpAuthentication]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
