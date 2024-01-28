using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Domain.Service;
using SimpleAccountSystem.Dto.Request;
using SimpleAccountSystem.Mvc.Commons;
using SimpleAccountSystem.Mvc.Controllers.Base;
using SimpleAccountSystem.Mvc.Models;
using SimpleAccountSystem.Mvc.Validations;
using System.Drawing.Text;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    //[StepUpAuthentication]
    public class UserController : CustomControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userService = new UserService(userManager);
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

            var users = _userService.GetUsers(extractedDataTableParameters.Length, 
                extractedDataTableParameters.SearchValue);
            var excludedCurrentUser = ExcludeCurrentUser(users);

            return DataTableResult(excludedCurrentUser, extractedDataTableParameters.Draw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserAsync(UserModel user)
        {
            var modelValidation = await ValidateModelAsync<UserModel, UserModelValidator>(user);
            if (modelValidation.IsValid)
            {
                var userRequest = new IdentityUserRequestDto(user.UserName,
                    user.Email,  
                    user.Password,
                    user.ConfirmPassword,
                    user.EmailConfirmed
                    );
                
                return Ok(await _userService.AddUserAsync(userRequest));
            }

            return FluentBadRequest(modelValidation.Errors);
        }

        private  IEnumerable<IdentityUser> ExcludeCurrentUser(IEnumerable<IdentityUser> users)
        {
            var currentUserId = _userService.GetUserId(User);

            return users.Where(i => i.Id != currentUserId);
        }

    }
}
