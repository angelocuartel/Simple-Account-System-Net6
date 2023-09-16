using AutoFixture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAccountSystem.Mvc.Attributes;
using SimpleAccountSystem.Mvc.Commons;
using SimpleAccountSystem.Mvc.Dto;
using System.Reflection;

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
            IEnumerable<IdentityUser>? users = null;

            if (!string.IsNullOrEmpty(extractedDataTableParameters.SearchValue))
            {
                users = FilterUsers(extractedDataTableParameters.SearchValue);
            }
            else
            {
                users = _userManager.Users;
            }

            var result = new GenericResultDto<IdentityUser>
            {
                draw = extractedDataTableParameters.Draw,
                data = users.Take(extractedDataTableParameters.Length).ToList(),
                recordsTotal = users.Count(),
                recordsFiltered = users.Count()
            };

            return new JsonResult(result);
        }

        private IEnumerable<IdentityUser> FilterUsers(string filter)
        {
            IEnumerable<IdentityUser>? result = null;

            result = _userManager.Users.Where(i => i.UserName.Contains(filter)
            || i.Email.Contains(filter)
            || i.TwoFactorEnabled.ToString().Contains(filter))
                .Select(i => new IdentityUser { Email = i.Email, UserName = i.UserName, TwoFactorEnabled = i.TwoFactorEnabled });

            return result;
        }
    }
}
