using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAccountSystem.Domain.Service;
using SimpleAccountSystem.Dto.Request;
using SimpleAccountSystem.Mvc.Commons;
using SimpleAccountSystem.Mvc.Controllers.Base;
using SimpleAccountSystem.Mvc.Models;
using SimpleAccountSystem.Mvc.Validations;

namespace SimpleAccountSystem.Mvc.Controllers
{
    [Authorize]
    //[StepUpAuthentication]
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
                var mappedUser = _mapper.Map<UserModel, IdentityUserRequestDto>(user);

                var result = await _userService.AddUserAsync(mappedUser);

                return Ok(result);
            }

            return FluentBadRequest(modelValidation.Errors);
        }

        private IEnumerable<IdentityUser> ExcludeCurrentUser(IEnumerable<IdentityUser> users)
        {
            var currentUserId = _userService.GetUserId(User);

            return users.Where(i => i.Id != currentUserId);
        }

    }
}
