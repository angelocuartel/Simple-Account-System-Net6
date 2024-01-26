using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Mvc.Commons;
using SimpleAccountSystem.Mvc.Controllers.Base;
using SimpleAccountSystem.Mvc.Dto;
using SimpleAccountSystem.Mvc.Models;
using SimpleAccountSystem.Mvc.Validations;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    //[StepUpAuthentication]
    public class UserController : CustomControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private string _currentUserId;

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
            _currentUserId = _userManager.GetUserId(User);

            var rawRequestQuery = HttpContext.Request.Query;
            var extractedDataTableParameters = rawRequestQuery.ExtractGenericQueryData();
            IEnumerable<IdentityUser>? users = null;

            if (!string.IsNullOrEmpty(extractedDataTableParameters.SearchValue))
            {
                users = GetFilteredUsers(extractedDataTableParameters.SearchValue);
            }
            else
            {
                users = GetUsers(extractedDataTableParameters.Length);
            }

            var result = new GenericResultDto<IdentityUser>
            {
                draw = extractedDataTableParameters.Draw,
                data = users,
                recordsTotal = users.Count(),
                recordsFiltered = users.Count()
            };

            return new JsonResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserAsync(UserModel user)
        {
            var modelValidation = await ValidateModelAsync<UserModel, UserModelValidator>(user);
            if (modelValidation.IsValid)
            {
                var resultCreation = await _userManager.CreateAsync(new IdentityUser
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    EmailConfirmed = user.EmailConfirmed
                }
                  , user.Password);

                if (resultCreation.Succeeded)
                {
                    return Ok();
                }
            }

            return FluentBadRequest(modelValidation.Errors);
        }

        private IEnumerable<IdentityUser> GetFilteredUsers(string filter)
        {
            IEnumerable<IdentityUser>? result = null;

            result = _userManager.Users.Where(i => (i.UserName.Contains(filter)
            || i.Email.Contains(filter)
            || i.UserName.Contains(filter))
            && i.Id != _currentUserId)
                .Select(i => new IdentityUser { Email = i.Email, UserName = i.UserName, TwoFactorEnabled = i.TwoFactorEnabled });

            return result;
        }

        private IEnumerable<IdentityUser> GetUsers(int recordCount)
        =>_userManager.Users
            .Where(i => i.Id != _currentUserId)
            .Take(recordCount);
        
    }
}
