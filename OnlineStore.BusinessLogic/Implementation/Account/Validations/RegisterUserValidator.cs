using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.DataAccess;
using OnlineStore.Entities.Entities;
using OnlineStore.Common.Extesnsions;
using OnlineStore.Common.DTOs;

namespace OnlineStore.BusinessLogic.Implementation.Account.Validations
{
    public class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CurrentUserDto currentUser;

        public RegisterUserValidator(UnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
          
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExistEmail)
                .WithMessage("Acest email exista deja")
                .EmailAddress(FluentValidation
                .Validators
                .EmailValidationMode
                .AspNetCoreCompatible);
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Camp obligatoriu!");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Camp obligatoriu!")
                .Equal(r => r.Password)
                .WithMessage("Parolele nu coincid!");
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Camp obligatoriu!");
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExistUsername)
                .WithMessage("Acest UserName exista deja");

        }

        public bool NotAlreadyExistEmail(string email)
        {
            var user = unitOfWork.Users
                .Get()
                .FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public bool NotAlreadyExistUsername(string username)
        {
            var user = unitOfWork.Users
                .Get()
                .FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                return false;
            }
            return true;
        }   
    }
}
