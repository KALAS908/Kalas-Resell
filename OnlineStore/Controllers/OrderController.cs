using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.OrderImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class OrderController : BaseController
    {
        public readonly OrderService orderService;
        public OrderController(ControllerDependencies dependencies, OrderService OrderService) : base(dependencies)
        {
            orderService = OrderService;
        }


        public IActionResult OrderDetails(int Id)
        {
            var model = orderService.GetAllOrder(Id);
            return View(model);
        }

    }
}
