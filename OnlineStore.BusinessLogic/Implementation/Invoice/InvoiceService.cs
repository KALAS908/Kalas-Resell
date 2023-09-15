using OnlineStore.BusinessLogic.Base;
using OnlineStore.BusinessLogic.Implementation.EmailImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;


namespace OnlineStore.BusinessLogic.Implementation.Invoice
{
    public class InvoiceService : BaseService
    {
        private readonly EmailService EmailService;


        public InvoiceService(ServiceDependencies serviceDependencies, EmailService emailService) : base(serviceDependencies)
        {
            this.EmailService = emailService;
            EmailService = emailService;
        }
    }
   
}
