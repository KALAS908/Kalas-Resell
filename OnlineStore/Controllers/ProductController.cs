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
using OnlineStore.Entities.Enums;
using OnlineStore.WebApp.Code;
using PagedList;
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
        public IActionResult Top10Products()
        {
            var model = ProductService.GetTopProducts();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (currentUser.RoleId == (int)RolesEnum.Admin)
            {
                var model = new ProductCreateModel();
                return View("Create", model);
            }
            return View("Error_NotFound");
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

        [HttpGet("/Prduct/ClothesView/")]
        public IActionResult ClothesView(int genderId, string searchString, int? page, string selectedBrands, int maxPrice, string selectedMeasures)
        {

            if (maxPrice == 0)
            {
                maxPrice = 10000;
            }
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
            }
            var measuresId = ProductService.TransformStringToInt(selectedMeasures);
            var brandsId = ProductService.TransformStringToInt(selectedBrands);
            double pagesize = 15;
            ViewBag.SelectedMeasures = selectedMeasures;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pagesize;
            ViewBag.GenderId = genderId;
            ViewBag.SelectedBrands = selectedBrands;
            var model = ProductService.GetAllClothes(genderId, searchString, ViewBag.page, (int)pagesize, brandsId, maxPrice, measuresId);
            ViewBag.ModelCount = ProductService.GetClothesCount(searchString, genderId, brandsId, maxPrice, measuresId);
            ViewBag.PageCount = Math.Ceiling(ViewBag.ModelCount / pagesize);
            return View(model);
        }

        [HttpGet("/Prduct/ShoesView/")]
        public ViewResult ShoesView(int genderId, string searchString, int? page, string selectedBrands, int maxPrice, string selectedMeasures)
        {
            if (maxPrice == 0)
            {
                maxPrice = 10000;
            }
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
            }
            var price = maxPrice;
            var measuresId = ProductService.TransformStringToInt(selectedMeasures);
            var brandsId = ProductService.TransformStringToInt(selectedBrands);
            double pagesize = 15;

            ViewBag.MaxPrice = maxPrice;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pagesize;
            ViewBag.GenderId = genderId;
            ViewBag.SelectedBrands = selectedBrands;
            ViewBag.SelectedMeasures = selectedMeasures;
            var model = ProductService.GetAllShoes(genderId, searchString, ViewBag.page, (int)pagesize, brandsId, price, measuresId);
            ViewBag.ModelCount = ProductService.GetShoesCount(searchString, genderId, brandsId, price, measuresId);
            ViewBag.PageCount = Math.Ceiling(ViewBag.ModelCount / pagesize);
            return View(model);
        }

        [HttpGet("/ProductByCategory/")]
        public IActionResult CategoryView(int categoryId, string searchString, int? page, string selectedBrands, int maxPrice, string selectedMeasures)
        {

            if (maxPrice == 0)
            {
                maxPrice = 10000;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
            }
            double pagesize = 15;
            var measuresId = ProductService.TransformStringToInt(selectedMeasures);
            var brandsId = ProductService.TransformStringToInt(selectedBrands);
            if (page == null)
            {
                page = 1;
            }
            ViewBag.SelectedMeasures = selectedMeasures;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pagesize;
            ViewBag.SelectedBrands = selectedBrands;
            ViewBag.CategoryId = categoryId;
            var model = ProductService.GetProuctsByCategory(categoryId, searchString, ViewBag.page, (int)pagesize, brandsId, maxPrice, measuresId);
            ViewBag.ModelCount = ProductService.GetCategoryCount(searchString, categoryId, brandsId, maxPrice, measuresId);
            ViewBag.PageCount = Math.Ceiling(ViewBag.ModelCount / pagesize);
            return View(model);
        }


        [HttpGet("/Product/GenderView/")]
        public IActionResult GenderView(int genderId, string searchString, int? page, string selectedBrands, int maxPrice, string selectedMeasures)
        { ///strip
            if (maxPrice == 0)
            {
                maxPrice = 10000;
            }
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
            }
            var measuresId = ProductService.TransformStringToInt(selectedMeasures);
            var brandsId = ProductService.TransformStringToInt(selectedBrands);
            double pagesize = 15;
            ViewBag.SelectedMeasures = selectedMeasures;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pagesize;
            ViewBag.GenderId = genderId;
            ViewBag.SelectedBrands = selectedBrands;
            var model = ProductService.GetProuctsByGender(genderId, searchString, ViewBag.page, (int)pagesize, brandsId, maxPrice, measuresId);
            ViewBag.ModelCount = ProductService.GetGenderCount(searchString, genderId, brandsId, maxPrice, measuresId);
            ViewBag.PageCount = Math.Ceiling(ViewBag.ModelCount / pagesize);
            return View(model);
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
            if (currentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");
            }

            var model = new MeasureQuantityModel();
            model.ProductId = id;
            model.ProductName = ProductService.GetProductById(id).Name;
            model.TypeId = ProductService.GetProductType(id);

            return View("AddProductMeasure", model);

        }

        [HttpPost]
        public IActionResult AddProductMeasure(MeasureQuantityModel model)
        {
            string referringUrl = Request.Headers["Referer"].ToString();
            if (model == null)
            {
                return View("Error_NotFound");
            }

            ProductService.AddProductMeasure(model);
            return Redirect(referringUrl);
        }


        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            if (currentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");
            }
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
            return RedirectToAction("ProductDetails", "Product", new { id = model.Id });
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

        [HttpPost]
        public IActionResult AddProductToWishList(Guid productId)
        {
            try
            {

                if (currentUser.IsAuthenticated)
                {
                    ProductService.AddProductToWishList(productId);
                    return Ok("Product added to wishlist successfully.");
                }
                else
                {
                    return RedirectToAction("Login", "UserAccount");

                }
            }
            catch (System.Exception)
            {
                return BadRequest("Product already in wishlist.");

            }

        }
        [HttpPost]
        public IActionResult RemoveFromWishList(Guid productId)
        {
            try
            {
                ProductService.RemoveProductFromWishList(productId);
                return Ok("Product removed from wishlist successfully.");
            }
            catch (System.Exception)
            {
                return BadRequest("Product already removed from wishlist.");

            }

        }

        public IActionResult DeleteProduct(Guid id)
        {
            if (currentUser.RoleId != (int)RolesEnum.Admin)
            {
                return View("Error_NotFound");
            }
            try
            {
                ProductService.DeleteProduct(id);
                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                return View("Error_NotFound");
            }
        }
    }
}
