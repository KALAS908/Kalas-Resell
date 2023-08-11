using Microsoft.AspNetCore.Mvc;
using OnlineStore.Code;
using OnlineStore.Entities.Entities;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        public readonly CategoryService CategoryService;
        public CategoryController(ControllerDependencies dependencies, CategoryService categoryService) : base(dependencies)
        {
            CategoryService = categoryService;
        }


        [HttpGet]
        public IActionResult GetCategories(int genderId,int typeId)
        {
            return Json(CategoryService.GetCategories(genderId,typeId));
        }
        
    }
}
