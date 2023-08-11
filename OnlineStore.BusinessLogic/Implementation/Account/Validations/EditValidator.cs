using FluentValidation;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.Common.DTOs;
using OnlineStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Account.Validations
{
    public class EditValidator : AbstractValidator<ProfileModel>
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CurrentUserDto currentUserDto;
        public EditValidator(UnitOfWork unitOfWork, CurrentUserDto currentUserDto)
        {
            this.unitOfWork = unitOfWork;
            this.currentUserDto = currentUserDto;
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.UserName)
                 .NotEmpty().WithMessage("Camp obligatoriu!")
                 .Must(NotAlreadyExistUsername).WithMessage("Acest UserName exista deja");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExistEmail).WithMessage("Acest email exista deja")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }

        public bool NotAlreadyExistEmail(string email)
        {
            var user = unitOfWork.Users.Get().FirstOrDefault(u => u.Email == email && u.Id.ToString() != currentUserDto.Id);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public bool NotAlreadyExistUsername(string username)
        {
            var user = unitOfWork.Users.Get().FirstOrDefault(u => u.UserName == username && u.Id.ToString() != currentUserDto.Id);
            if (user != null)
            {
                return false;
            }
            return true;
        }
    }
}
