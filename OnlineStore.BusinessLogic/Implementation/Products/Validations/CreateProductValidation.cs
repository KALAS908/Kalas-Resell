using FluentValidation;
using Microsoft.IdentityModel.Tokens;
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
                .NotEmpty().WithMessage("Required!")
                .Must(NotAlreadyExistName).WithMessage("This name alreasdy exist");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Required!")
                .Must(ToLongDescription).WithMessage("Maximum 500 characters!");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Required!");
            RuleFor(p => p.GenderId)
                .NotEmpty().WithMessage("Required!");
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Required!");
            RuleFor(p => p.ColorId)
                .NotEmpty().WithMessage("Required!");
           

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

        private bool ToLongDescription(string arg)
        {
            if ( !arg.IsNullOrEmpty() && arg.Length > 500)
            {
                return false;
            }
            return true;
        }
    }
}
