using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Common.DTOs;
using OnlineStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Products.Validations
{
    public class EditProductValidation : AbstractValidator<EditProductDto>
    {

        private readonly UnitOfWork unitOfWork;
        private readonly EditProductDto product;

        public EditProductValidation(UnitOfWork unitOfWork)
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
            RuleFor(p => p.Discount)
                .Must(GoodDiscount).WithMessage("The Discount must be between 0 and 99.");


        }

        private bool NotAlreadyExistName(string arg)
        {
            var product = unitOfWork.Products.Get().Where(p => p.Name == arg ).ToList();
            if (product.Count() > 1)
            {
                return false;
            }
            return true;

        }
        

        private bool GoodDiscount(int discount)
        {
            if (discount < 0 || discount > 99)
            {
                return false;
            }
            return true;
        }

        private bool ToLongDescription(string arg)
        {
            if (!arg.IsNullOrEmpty() && arg.Length > 500)
            {
                return false;
            }
            return true;
        }
    }
}
