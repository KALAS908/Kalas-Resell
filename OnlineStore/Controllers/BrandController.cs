using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.BrandImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class BrandController : BaseController
    {
        public readonly BrandService BrandService;
        public BrandController(ControllerDependencies dependencies, BrandService brandService) : base(dependencies)
        {
            BrandService = brandService;
        }

        [HttpGet]
        public IActionResult GetBrands()
        {
            var brandList = BrandService.GetAllBrands();
            var myJson = Json(brandList);

            return Json(brandList);
        }
    }
}
