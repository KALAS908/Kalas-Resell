using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Account;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.BusinessLogic.Implementation.Countries;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.WebApp.Code;
using System.Security.Claims;
using PagedList;
using OnlineStore.Entities.Enums;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.WebApp.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly UserAccountService UserAccountService;
        private readonly CountriesService CountriesService;
        private readonly IEmailSender EmailSender;

        public UserAccountController(ControllerDependencies dependencies, UserAccountService userAccountService, CountriesService countriesService, IEmailSender emailSender)
            : base(dependencies)
        {
            UserAccountService = userAccountService;
            CountriesService = countriesService;
            EmailSender = emailSender;
        }


        public ViewResult AllUsers(string searchString, int? page)
        {
            double pagesize = 15;
            ViewBag.ModelCount = UserAccountService.GetUserCount(searchString);
            ViewBag.PageCount = Math.Ceiling(ViewBag.ModelCount / pagesize);
            if (ViewBag.PageCount < page)
            {
                ViewBag.page = Convert.ToInt32(ViewBag.PageCount);
            }
            else
            {
                ViewBag.page = page;
            }

            if (ViewBag.page == null)
            {
                ViewBag.page = 1;

            }
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pagesize;

            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");
            }

            var model = UserAccountService.GetAllUsers(searchString, (int)pagesize, ViewBag.page);

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            return View("Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            UserAccountService.RegisterNewUser(model);

            LoginModel loginModel = new LoginModel();
            loginModel.Email = model.Email;
            loginModel.Password = model.Password;
            _ = Login(loginModel);
            var receiver = model.Email;
            var subject = "Welcome to our store";
            var message = "Welcome to our store";
            await EmailSender.SendEmailAsync(receiver, subject, message);
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
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("IsAdmin", user.IsAdmin.ToString()),

            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

           

            await HttpContext.SignInAsync(
                    scheme: "OnlineStoreCookies",
                    principal: principal);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var Id = new Guid(CurrentUser.Id);
            var model = UserAccountService.GetUserProfile(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var Id = new Guid(CurrentUser.Id);
            var model = UserAccountService.GetUserProfile(Id);
            ViewBag.CountryId = model.CountryId;
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

        [HttpGet]
        public IActionResult WishList()
        {

            var model = UserAccountService.GetUserWishList();
            return View(model);
        }

        public IActionResult DeleteByAdmin(Guid UserId)
        {
            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");
            }

            UserAccountService.DeleteUser(UserId);
            return Ok();
        }


        public IActionResult DeleteAccount()
        {

            UserAccountService.DeleteAccount();
            _ = Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MakeAdmin(Guid UserId)
        {
            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");

            }
            UserAccountService.MakeAdmin(UserId);
            return Ok();
        }
        public IActionResult MakeUser(Guid UserId)
        {
            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");

            }
            UserAccountService.MakeUser(UserId);
            return Ok();
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "OnlineStoreCookies");
        }


        [HttpGet]
        public IActionResult Orders()
        {

            var model = UserAccountService.GetUserOrders();
            return View(model);
        }

        [HttpGet]

        public IActionResult Top10Users()
        {
            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");

            }
            var model = UserAccountService.GetTopUsers();
            return View(model);
        }



        [HttpGet]
        public IActionResult Medals()
        {
            if (CurrentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");

            }
            return View();
        }
    }
}
