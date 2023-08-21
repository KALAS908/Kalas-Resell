using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Account;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.BusinessLogic.Implementation.BrandImplementation;
using OnlineStore.BusinessLogic.Implementation.ColorImplementation;
using OnlineStore.BusinessLogic.Implementation.Countries;
using OnlineStore.BusinessLogic.Implementation.MeasureImplementation;
using OnlineStore.BusinessLogic.Implementation.Products;
using OnlineStore.BusinessLogic.Implementation.Products.Models;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using OnlineStore.WebApp.Code;
using System.Security.Claims;


namespace OnlineStore.WebApp.Controllers
{
    public class ProductController : BaseController
    {

        public readonly BrandService BrandService;
        public readonly ColorService ColorService;
        public readonly CategoryService CategoryService;
        public readonly ProductService ProductService;
        public readonly MeasureService MeasureService;
        public readonly CurrentUserDto currentUser;

        public ProductController(ControllerDependencies dependencies, BrandService brandService, ColorService colorService, CategoryService categoryService, ProductService productService, MeasureService measureService)
            : base(dependencies)
        {

            BrandService = brandService;
            ColorService = colorService;
            CategoryService = categoryService;
            ProductService = productService;
            MeasureService = measureService;
            currentUser = dependencies.CurrentUser;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateModel();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateModel model)
        {

            if (model == null)
            {
                return View("Error_NotFound");
            }



            ProductService.CreateProduct(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ProductsView()
        {
            var model = ProductService.GetAllProducts();
            return View("ProductsView", model);
        }

        [HttpGet]
        public IActionResult ProductDetails(Guid id)
        {
            try
            {
                var model = ProductService.GetProductById(id);
                return View("ProductDetails", model);
            }
            catch (System.Exception)
            {
                return View("Error_NotFound");
            }
        }

        [HttpGet]
        public IActionResult AddProductMeasure(Guid id)
        {

            var model = new MeasureQuantityModel();
            model.ProductId = id;
            model.ProductName = ProductService.GetProductById(id).Name;
            model.TypeId = ProductService.GetProductType(id);

            return View("AddProductMeasure", model);

        }

        [HttpPost]
        public IActionResult AddProductMeasure(MeasureQuantityModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            ProductService.AddProductMeasure(model);
            return RedirectToAction("ProductsView", "Product");
        }


        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            try
            {
                var model = ProductService.GetEditProductDto(id);
                return View("EditProduct", model);
            }
            catch (System.Exception)
            {
                return View("Error_NotFound");
            }
        }

        [HttpPost]
        public IActionResult EditProduct(EditProductDto model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            ProductService.EditProduct(model);
            return RedirectToAction("ProductsView", "Product");
        }

        [HttpPost]
        public IActionResult AddProductToCart(Guid productId, int measureId)
        {
            if (currentUser.IsAuthenticated)
            {
                ProductService.AddProductToCart(productId, measureId);
                return Ok("Product added to cart successfully.");
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
   
            }
          
        }

    }
}
