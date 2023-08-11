using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.TypeImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class TypeController : BaseController
    {
        public readonly TypeService TypeService;
        public TypeController(ControllerDependencies dependencies, TypeService typeService) : base(dependencies)
        {
            TypeService = typeService;
        }

        [HttpGet]
        public IActionResult GetTypes()
        {
            return Json(TypeService.GetAllTypes());
        }

    }
}
