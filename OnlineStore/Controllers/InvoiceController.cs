using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.EmailImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace OnlineStore.WebApp.Controllers
{
    public class InvoiceController : BaseController
    {

        private readonly EmailService EmailService;
        public InvoiceController(ControllerDependencies dependencies , EmailService emailService) : base(dependencies)
        {
            this.EmailService = emailService;
        }

    }
}
