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
using OnlineStore.Entities.Enums;
using Microsoft.EntityFrameworkCore;

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
            if (email == null || password == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            var user = UnitOfWork.Users.Get().
                FirstOrDefault(u => u.Email == email);
            password = StringToHash(password);
            var randomString = UnitOfWork.UsersStrings.Get().
                FirstOrDefault(u => u.UserId == user.Id);
            var hashedPasswordInput = StringToHash(password + randomString.RandomString);


            if (user == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            if (user.Password != hashedPasswordInput)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }



            var CurrentUser = new CurrentUserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = (int)user.RoleId,
                CountryId = user.CountryId.ToString(),
                UserName = user.UserName,
                IsAuthenticated = true
            };
            if (CurrentUser.RoleId == 1)
            {
                CurrentUser.IsAdmin = true;
            }
            else
            {
                CurrentUser.IsAdmin = false;
            }

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

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);


                byte[] hashBytes = sha256.ComputeHash(inputBytes);


                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
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
                var availableQuantity = UnitOfWork.ProductMeasures.Get().FirstOrDefault(x => x.ProductId == product.Id && x.MeasureId == item.MeasureId).Quantity;
                if (availableQuantity == 0)
                {
                    UnitOfWork.ShoppingCarts.Delete(item);
                    UnitOfWork.SaveChanges();
                }
                else
                {
                    if (availableQuantity < item.Quantity)
                    {
                        item.Quantity = availableQuantity;
                        UnitOfWork.ShoppingCarts.Update(item);
                        UnitOfWork.SaveChanges();
                    }
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
            }
            return ShoppingCartDto;
        }

        public IEnumerable<WishListDto> GetUserWishList()
        {
            var currentUser = CurrentUser;
            var WishListProducts = UnitOfWork.WishLists.Get()
                .Include(x => x.Product)
                .Where(s => s.UserId.ToString() == currentUser.Id)
                .ToList();
            var WishListDto = new List<WishListDto>();

            foreach (var product in WishListProducts)
            {
                var image = UnitOfWork.ProductImages.Get().FirstOrDefault(x => x.ProductId == product.ProductId);
                var productDto = new WishListDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.Product.Name,
                    ProductPrice = (double)product.Product.Price,
                    ProductImage = image.Picture,
                    ProductDiscount = (int)product.Product.Discount
                };
                WishListDto.Add(productDto);
            }
            return WishListDto;
        }

        public int GetUserCount(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return UnitOfWork.Users.Get().Where(x => x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                x.LastName.ToLower().Contains(searchString.ToLower()) ||
                x.Email.ToLower().Contains(searchString.ToLower()) ||
                x.UserName.ToLower().Contains(searchString.ToLower()))
                    .Count();
            }
            return UnitOfWork.Users.Get().Count();
        }

        public IEnumerable<UserDto> GetAllUsers(string searchString, int pageSize, int page)
        {

            var users = new List<User>();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = UnitOfWork.Users
              .Get()
              .Where(x => x.FirstName.ToLower().Contains(searchString.ToLower()) || x.LastName.ToLower().Contains(searchString.ToLower()) || x.Email.ToLower().Contains(searchString.ToLower()) || x.UserName.ToLower().Contains(searchString.ToLower()))
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .ToList();

            }
            else
            {
                users = UnitOfWork.Users
                   .Get()
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
            }


            var usersDto = new List<UserDto>();
            foreach (var item in users)
            {
                var userDto = new UserDto
                {
                    UserId = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    UserName = item.UserName,
                    Role = UnitOfWork.Roles.Get().FirstOrDefault(r => r.Id == item.RoleId).Role1
                };
                usersDto.Add(userDto);
            }
            return usersDto;
        }

        public void DeleteUser(Guid Id)
        {
            var user = UnitOfWork.Users.Get().FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                UnitOfWork.Users.Delete(user);
                UnitOfWork.SaveChanges();
            }
        }


        public void DeleteAccount()
        {
            var currentUser = CurrentUser;
            var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.Id.ToString() == currentUser.Id);
            UnitOfWork.Users.Delete(user);
            UnitOfWork.SaveChanges();
        }
        public void MakeAdmin(Guid Id)
        {
            var user = UnitOfWork.Users.Get().FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                user.RoleId = (int)RolesEnum.Admin;
                UnitOfWork.Users.Update(user);
                UnitOfWork.SaveChanges();
            }
        }
        public void MakeUser(Guid Id)
        {
            var user = UnitOfWork.Users.Get().FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                user.RoleId = (int)RolesEnum.User;
                UnitOfWork.Users.Update(user);
                UnitOfWork.SaveChanges();
            }
        }

        public List<OrderDto> GetUserOrders()
        {
            var orders = UnitOfWork.Receipts.Get().Where(x => x.UserId.ToString() == CurrentUser.Id).ToList();
            var ordersDto = new List<OrderDto>();
            foreach (var item in orders)
            {
                var orderDto = new OrderDto
                {
                    Id = item.Id,
                    TotalPrice = (double)item.TotalPrice,
                };
                ordersDto.Add(orderDto);
            }
            return ordersDto;
        }


        public IEnumerable<RankingDto> GetTopUsers()
        {
            var users = UnitOfWork.Receipts.Get()
                .GroupBy(x => x.UserId)
                .Select(x => new { UserId = x.Key, TotalPrice = x.Sum(y => y.TotalPrice) })
                .OrderByDescending(x => x.TotalPrice)
                .Take(10)
                .ToList();

            var usersDto = new List<RankingDto>();
            foreach (var item in users)
            {
                var user = UnitOfWork.Users.Get().FirstOrDefault(x => x.Id == item.UserId);
                var userDto = new RankingDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = UnitOfWork.Roles.Get().FirstOrDefault(r => r.Id == user.RoleId).Role1,
                    TotalPrice = (double)item.TotalPrice
                };
                usersDto.Add(userDto);
            }
            return usersDto;
        }


    }



}

