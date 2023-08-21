using OnlineStore.BusinessLogic.Base;
using OnlineStore.BusinessLogic.Implementation.Account.Models;
using OnlineStore.BusinessLogic.Implementation.Account.Validations;
using OnlineStore.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Common.Extesnsions;
using OnlineStore.Entities.Entities;
using AutoMapper;
using System.Security.Cryptography;
using Polly.Utilities;

namespace OnlineStore.BusinessLogic.Implementation.Account
{
    public class UserAccountService : BaseService
    {

        private readonly RegisterUserValidator validationRules;
        private readonly EditValidator editValidationRules;

        public UserAccountService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            validationRules = new RegisterUserValidator(serviceDependencies.UnitOfWork);
            editValidationRules = new EditValidator(serviceDependencies.UnitOfWork, CurrentUser);
        }

        public CurrentUserDto Login(string email, string password)
        {



            var user = UnitOfWork.Users.Get().
                FirstOrDefault(u => u.Email == email);
            password = StringToHash(password);
            var randomString = UnitOfWork.UsersStrings.Get().
                FirstOrDefault(u => u.UserId == user.Id);
            var hashedPasswordInput = StringToHash(password + randomString.RandomString);

            if (user.Password != hashedPasswordInput)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            if (user == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            var CurrentUser = new CurrentUserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId.ToString(),
                CountryId = user.CountryId.ToString(),
                UserName = user.UserName,
                IsAuthenticated = true
            };
            return CurrentUser;

        }

        public void RegisterNewUser(RegisterModel model)
        {

            validationRules.Validate(model).ThenThrow();

            var user = Mapper.Map<RegisterModel, User>(model);

            var stringRandom = GenerateRandomString(10);
            user.Password = StringToHash(user.Password);
            user.Password = StringToHash(user.Password + stringRandom);
            user.Id = Guid.NewGuid();
            user.RoleId = 2;
            var userString = new UserString
            {
                UserId = user.Id,
                RandomString = stringRandom
            };
            UnitOfWork.Users.Insert(user);
            UnitOfWork.UsersStrings.Insert(userString);

            UnitOfWork.SaveChanges();
        }


        private string StringToHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert input string to bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Compute hash
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert hash bytes to hexadecimal representation
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // "x2" formats as hexadecimal with 2 digits
                }

                return builder.ToString();
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new StringBuilder(length);
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[sizeof(uint)];
                while (length-- > 0)
                {
                    rng.GetBytes(buffer);
                    uint num = BitConverter.ToUInt32(buffer, 0);
                    result.Append(chars[(int)(num % (uint)chars.Length)]);
                }
            }
            return result.ToString();
        }

        public ProfileModel GetUserProfile(Guid id)
        {
            var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.Id == id);
            var model = new ProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Country = UnitOfWork.Countries.Get().FirstOrDefault(c => c.Id == user.CountryId).Name,
                CountryId = (int)user.CountryId

            };

            return model;
        }

        public void UpdateUserProfile(ProfileModel model)
        {


            var modelcopy = new ProfileModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                CountryId = model.CountryId
            };
            var result = editValidationRules.Validate(modelcopy);
            if (result.IsValid == false)
            {
                model = modelcopy;
                result.ThenThrow();
            }

            var currentUser = CurrentUser;
            var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.Id.ToString() == currentUser.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.CountryId = model.CountryId;
            UnitOfWork.Users.Update(user);
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<ShoppingCartDto> GetUserShoppingCart()
        {
            var currentUser = CurrentUser;
            var ShoppingCartProducts = UnitOfWork.ShoppingCarts.Get().Where(s => s.UserId.ToString() == currentUser.Id).ToList();
            var ShoppingCartDto = new List<ShoppingCartDto>();
            foreach (var item in ShoppingCartProducts)
            {
                var product = UnitOfWork.Products.Get().FirstOrDefault(p => p.Id == item.ProductId);
                var images = UnitOfWork.ProductImages.Get().Where(x => x.ProductId == product.Id).ToList();
                var productDto = new ShoppingCartDto
                {
                    ProductId = item.ProductId,
                    ProductName = product.Name,
                    ProductPrice = (double)product.Price,
                    Quantity = (int)item.Quantity,
                    ProductImage = images.Select(x => x.Picture).FirstOrDefault(),
                    ProductSize = UnitOfWork.Measures.Get().FirstOrDefault(x => x.Id == item.MeasureId).MeasureValue,
                    ProductDiscount = (int)product.Discount

                };
                ShoppingCartDto.Add(productDto);
            }
            return ShoppingCartDto;
        }

    }
}
