using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.NewFolder;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public readonly ShoppinCartService ShoppinCartService;
        public readonly CurrentUserDto CurrentUser;
        public ShoppingCartController(ControllerDependencies dependencies, ShoppinCartService shoppinCartService) : base(dependencies)
        {
            ShoppinCartService = shoppinCartService;
            CurrentUser = dependencies.CurrentUser;
        }

        [HttpPost]
        public IActionResult RemoveItem(Guid productId, string measure)
        {

            ShoppinCartService.RemoveItem(productId, measure);
            return Ok();
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(Guid productId, string measure)
        {
            ShoppinCartService.IncreaseQuantity(productId, measure);
            return Ok();
            
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(Guid productId, string measure)
        {
            ShoppinCartService.DecreaseQuantity(productId, measure);
            return Ok();
        }
    }
}
