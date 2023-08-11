using FluentValidation;
using OnlineStore.BusinessLogic.Implementation.Products.Models;
using OnlineStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Products.Validations
{
    public class CreateProductValidation : AbstractValidator<ProductCreateModel>
    {
        private readonly List<string> _validImageExtensions = new List<string> { ".jpg", ".png", ".gif", ".jpeg" };
        private readonly UnitOfWork unitOfWork;

        public CreateProductValidation(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExistName).WithMessage("Acest nume exista deja");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(p => p.GenderId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(p => p.ColorId)
                .NotEmpty().WithMessage("Camp obligatoriu!");

        }

        private bool NotAlreadyExistName(string arg)
        {
                var product = unitOfWork.Products.Get().FirstOrDefault(p => p.Name == arg);
                if (product != null)
                {
                    return false;
                }
                return true;
            
        }
    }
}
