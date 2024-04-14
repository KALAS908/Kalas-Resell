using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineStore.BusinessLogic.Implementation.EmailImplementation;
using OnlineStore.BusinessLogic.Implementation.OrderImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;
using System.Net.Mail;
using System.Web.Mvc;
using IronPdf;

namespace OnlineStore.WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService orderService;
        private readonly EmailService emailService;

        public OrderController(ControllerDependencies dependencies, OrderService orderService, EmailService emailService) : base(dependencies)
        {
            this.orderService = orderService;
            this.emailService = emailService;
        }

        public IActionResult OrderDetails(int Id)
        {
            var model = orderService.GetAllOrder(Id);
            return View(model);
        }


    }
}
