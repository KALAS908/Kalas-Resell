using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.GenderImplementation;
using OnlineStore.BusinessLogic.Implementation.Products;
using OnlineStore.BusinessLogic.Implementation.TypeImplementation;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.Models;
using OnlineStore.WebApp;
using OnlineStore.WebApp.Code;
using OnlineStore.WebApp.Controllers;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly TypeService _typeService;
        private readonly GenderService _genderService;
        private readonly IEmailSender _emailSender;


        public HomeController(ControllerDependencies dependencies, ILogger<HomeController> logger, ProductService productService, GenderService genderService, CategoryService categoryService, TypeService typeService, IEmailSender emailSender)
            : base(dependencies)
        {
            _logger = logger;
            _productService = productService;
            _genderService = genderService;
            _categoryService = categoryService;
            _typeService = typeService;
            _emailSender = emailSender;

        }

        public IActionResult Index()
        {
            var topDeals = _productService.GetTopDeals();
            var topProducts = _productService.GetTopSoldItems();

            ViewBag.TopDeals = topDeals;
            ViewBag.TopProducts = topProducts;

            return View("HomePage");
        }

        public ActionResult RenderMenu()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.Genders = _genderService.GetAllGenders();
            ViewBag.Types = _typeService.GetAllTypes();
            return PartialView("_MenuBar");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}