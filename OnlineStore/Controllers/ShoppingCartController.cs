using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Account;
using OnlineStore.BusinessLogic.Implementation.NewFolder;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.WebApp.Code;
using Stripe.Checkout;

namespace OnlineStore.WebApp.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public readonly ShoppingCartService ShoppingCartService;
        public readonly CurrentUserDto CurrentUser;
        public readonly UserAccountService UserAccountService;
        public ShoppingCartController(ControllerDependencies dependencies, ShoppingCartService shoppingCartService, UserAccountService userAccountService) : base(dependencies)
        {
            ShoppingCartService = shoppingCartService;
            CurrentUser = dependencies.CurrentUser;
            UserAccountService = userAccountService;
        }

        [HttpPost]
        public IActionResult RemoveItem(Guid productId, string measure)
        {

            ShoppingCartService.RemoveItem(productId, measure);
            return Ok();
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(Guid productId, string measure)
        {
            try
            {
                ShoppingCartService.IncreaseQuantity(productId, measure);
                return Ok();
            }
            catch (System.Exception)
            {
                return View("Error_NoutFound");
            }

        }

        [HttpPost]
        public IActionResult DecreaseQuantity(Guid productId, string measure)
        {
            try
            {
                ShoppingCartService.DecreaseQuantity(productId, measure);
                return Ok();
            }
            catch (System.Exception)
            {
                return View("Error_NoutFound");
            }
        }


        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            var cart = UserAccountService.GetUserShoppingCart();

            foreach(var item in cart)
            {
                ShoppingCartService.RemoveItem(item.ProductId, item.ProductSize);
                ShoppingCartService.RemoveFromDataBase(item.ProductId, item.ProductSize, item.Quantity);
                
            }

            return View("OrderConfirmation",cart);
        }

        public IActionResult CheckOut()
        {

            var domain = "https://localhost:7108/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"ShoppingCart/OrderConfirmation",
                CancelUrl = domain + $"UserAccount/ShoppingCart",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",

            };

            var cart = UserAccountService.GetUserShoppingCart();
            foreach (var item in cart)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        
                        UnitAmount = (long?)(item.ProductPrice - item.ProductPrice*item.ProductDiscount/100) *100 ,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{item.ProductName}  {item.ProductSize}"

                        },
                    },
                    Quantity = item.Quantity,
                };

                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
