using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.GenderImplementation;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using OnlineStore.WebApp.Code;
namespace OnlineStore.WebApp.Controllers
{
    public class GenderController : BaseController
    {
        private readonly GenderService GenderService;
        public GenderController(ControllerDependencies dependencies, GenderService genderService) : base(dependencies)
        {
            GenderService = genderService;
        }

        [HttpGet]
        public IActionResult GetGenders()
        {
            return Json(GenderService.GetAllGenders());
        }

        
    }
}
