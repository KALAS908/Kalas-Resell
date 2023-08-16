using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Account;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.BusinessLogic.Implementation.Countries;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.WebApp.Code;
using System.Security.Claims;
 
namespace OnlineStore.WebApp.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly UserAccountService UserAccountService;
        private readonly CountriesService CountriesService;

        public UserAccountController(ControllerDependencies dependencies, UserAccountService userAccountService, CountriesService countriesService)
            : base(dependencies)
        {
            UserAccountService = userAccountService;
            CountriesService = countriesService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            //model.CountryList = CountriesService.GetAllCountries();
           
            return View("Register", model);
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }
            //model.CountryList = CountriesService.GetAllCountries();
            UserAccountService.RegisterNewUser(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = UserAccountService.Login(model.Email, model.Password);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }

        private async Task LogIn(CurrentUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "OnlineStoreCookies",
                    principal: principal);
        }

        [HttpGet]
        public IActionResult Profile(Guid Id)
        {
           
           var model = UserAccountService.GetUserProfile(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditProfile(Guid Id)
        {
            var model = UserAccountService.GetUserProfile(Id);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(ProfileModel model)
        {

            UserAccountService.UpdateUserProfile(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {

            var model = UserAccountService.GetUserShoppingCart();
            return View(model);
        }


        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "OnlineStoreCookies");
        }

    }
}
