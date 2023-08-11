using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.ColorImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class ColorController : BaseController
    {
        public readonly ColorService ColorService;
        public ColorController(ControllerDependencies dependencies, ColorService colorService) : base(dependencies)
        {
            ColorService = colorService;
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            var colorList = ColorService.GetAllColors();
            var myJson = Json(colorList);

            return Json(colorList);
        }
        
    }
}
