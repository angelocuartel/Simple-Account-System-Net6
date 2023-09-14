using AutoFixture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAccountSystem.Mvc.Attributes;
using SimpleAccountSystem.Mvc.Commons;
using SimpleAccountSystem.Mvc.Dto;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    //[StepUpAuthentication]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var rawRequestQuery = HttpContext.Request.Query;
            var extractedDataTableParameters = rawRequestQuery.ExtractGenericQueryData();

            var users = _userManager.Users;
            var result = new GenericResultDto<IdentityUser>
            {
                draw = extractedDataTableParameters.Draw,
                data = users.Take(extractedDataTableParameters.Length).ToList(),
                recordsTotal = users.Count(),
                recordsFiltered = users.Count()
            };

            return new JsonResult(result);
        }
    }
}
