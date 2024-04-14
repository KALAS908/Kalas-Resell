using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Account;
using OnlineStore.BusinessLogic.Implementation.NewFolder;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.DataAccess;
using OnlineStore.Entities.Entities;
using OnlineStore.WebApp.Code;
using Stripe.Checkout;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineStore.BusinessLogic.Implementation.EmailImplementation;
using MimeKit;
using MimeKit.Text;
using MimeKit.IO;
using MailKit.Net.Smtp;
using System;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Products;

namespace OnlineStore.WebApp.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public readonly ShoppingCartService ShoppingCartService;
        public new readonly CurrentUserDto CurrentUser;
        public readonly UserAccountService UserAccountService;
        public readonly EmailService EmailService;
        public readonly ProductService ProductService;
        public ShoppingCartController(ControllerDependencies dependencies, ShoppingCartService shoppingCartService, UserAccountService userAccountService, EmailService emailService, ProductService productService) : base(dependencies)
        {
            ShoppingCartService = shoppingCartService;
            CurrentUser = dependencies.CurrentUser;
            UserAccountService = userAccountService;
            EmailService = emailService;
            ProductService = productService;
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

        public IActionResult OrderConfirmation()
        {
            var cart = UserAccountService.GetUserShoppingCart();
            var receipt = new Receipt();
            Guid Id = new Guid(CurrentUser.Id);
            receipt.TotalPrice = ShoppingCartService.GetTotalPrice();
            receipt.UserId = Id;
            ShoppingCartService.AddReceipt(receipt);
            Document document = new Document(PageSize.A4, 50, 50, 50, 50); 
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PdfWriter.GetInstance(document, new FileStream(path + "/Receipt.pdf", FileMode.Create));
            document.Open();

           
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK);
            var headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

            
            var table = new PdfPTable(4);
            table.WidthPercentage = 100;

            
            var titleCell = new PdfPCell(new Phrase("Your Order Receipt", titleFont));
            titleCell.Colspan = 4;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.Border = 0; 
            table.AddCell(titleCell);

           
            var headers = new string[] { "Product", "Size", "Price", "Quantity" };
            foreach (var header in headers)
            {
                var headerCell = new PdfPCell(new Phrase(header, headingFont));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(headerCell);
            }

            foreach (var item in cart)
            {
                var product = ProductService.GetProductById(item.ProductId);

               
                table.AddCell(new PdfPCell(new Phrase(product.Name, normalFont)));
                table.AddCell(new PdfPCell(new Phrase(item.ProductSize, normalFont)));
                table.AddCell(new PdfPCell(new Phrase("$" + (product.Price - product.Price * product.Discount / 100) * item.Quantity, normalFont)));
                table.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString(), normalFont)));

                var OrderedItem = new OrderedItems();
                OrderedItem.ProductId = item.ProductId;
                OrderedItem.OrderId = receipt.Id;
                OrderedItem.Quantity = item.Quantity;

                ShoppingCartService.AddOrderedItem(OrderedItem, receipt.Id);
                ShoppingCartService.RemoveItem(item.ProductId, item.ProductSize);
                ShoppingCartService.RemoveFromDataBase(item.ProductId, item.ProductSize, item.Quantity);
            }

           
            var totalCell = new PdfPCell(new Phrase("Total Price: $" + receipt.TotalPrice, headingFont));
            totalCell.Colspan = 4;
            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalCell.Border = 0;
            table.AddCell(totalCell);

            
            document.Add(table);

            document.Close();
            _ = EmailService.SendDocument(CurrentUser.Email, "Order Receipt", "Thank you for your order", path + "/Receipt.pdf");
            return View("OrderConfirmation", cart);
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

                        UnitAmount = (long?)(item.ProductPrice - item.ProductPrice * item.ProductDiscount / 100) * 100,
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
