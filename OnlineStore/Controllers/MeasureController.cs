using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.MeasureImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class MeasureController : BaseController
    {
        public readonly MeasureService MeasureService;
        public MeasureController(ControllerDependencies dependencies, MeasureService measureService) : base(dependencies)
        {
            MeasureService = measureService;
        }


        [HttpGet]
        public IActionResult GetMeasures(int typeId)
        {
            return Json(MeasureService.GetMeasures(typeId));
        }

        [HttpGet]
        public IActionResult GetAllMeasures()
        {
            return Json(MeasureService.GetAllMeasures());
        }
        [HttpGet]
        public IActionResult GetProductMeasures(Guid productId)
        {
            var productMeasures = MeasureService.GetProductMeasures(productId);
            productMeasures = productMeasures.OrderBy(x => x.MeasureId).ToList();
            return Json(productMeasures);
        }
    }
}
