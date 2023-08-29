using OnlineStore.BusinessLogic.Base;
using OnlineStore.BusinessLogic.Implementation.Products.Models;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineStore.BusinessLogic.Implementation.Products.Validations;
using OnlineStore.Common.Extesnsions;
using Microsoft.EntityFrameworkCore;
using Azure;
using System.Drawing.Printing;

namespace OnlineStore.BusinessLogic.Implementation.Products
{
    public class ProductService : BaseService
    {
        private readonly CreateProductValidation validationRules;
        private readonly CurrentUserDto currentUser;
        public ProductService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            validationRules = new CreateProductValidation(serviceDependencies.UnitOfWork);
            currentUser = serviceDependencies.CurrentUser;
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = UnitOfWork.Products
                .Get()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Color)
                .Include(x => x.Image)
                .Take(30)
                .ToList();

            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };



                productsDto.Add(productDto);
            }
            return productsDto;
        }

        public int GetClothesCount(string searchString, int genderId, List<int> brandsId)
        {

            var products = UnitOfWork.Products
                 .Get()
                 .Include(x => x.Brand)
                 .Include(x => x.Category)
                 .Include(x => x.Color)
                 .Include(x => x.Image)
                 .Where(x => x.Category.TypeId == 2 && x.Category.GenderId == genderId)
                 .ToList();


            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return products.Count;
        }

        public List<ProductDto> GetAllClothes(int genderId, string searchString, int page, int pageSize, List<int> brandsId)
        {

            var products = new List<Product>();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = UnitOfWork.Products
                    .Get()
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Color)
                    .Include(x => x.Image)
                    .Where(x => x.Category.TypeId == 2 && x.Category.GenderId == genderId && x.Name.ToLower().Contains(searchString.ToLower()))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                products = UnitOfWork.Products
                     .Get()
                     .Include(x => x.Brand)
                     .Include(x => x.Category)
                     .Include(x => x.Color)
                     .Include(x => x.Image)
                     .Where(x => x.Category.TypeId == 2 && x.Category.GenderId == genderId)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
            }

            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }


            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }
            return productsDto;
        }


        public int GetShoesCount(string searchString, int genderId, List<int> brandsId)
        {

            var products = UnitOfWork.Products
                 .Get()
                 .Include(x => x.Brand)
                 .Include(x => x.Category)
                 .Include(x => x.Color)
                 .Include(x => x.Image)
                 .Where(x => x.Category.TypeId == 1 && x.Category.GenderId == genderId)
                 .ToList();


            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return products.Count;
        }

        public List<ProductDto> GetAllShoes(int genderId, string searchString, int page, int pageSize, List<int> brandsId)
        {
            var products = new List<Product>();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = UnitOfWork.Products
                    .Get()
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Color)
                    .Include(x => x.Image)
                    .Where(x => x.Category.TypeId == 1 && x.Category.GenderId == genderId && x.Name.ToLower().Contains(searchString.ToLower()))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                products = UnitOfWork.Products
                     .Get()
                     .Include(x => x.Brand)
                     .Include(x => x.Category)
                     .Include(x => x.Color)
                     .Include(x => x.Image)
                     .Where(x => x.Category.TypeId == 1 && x.Category.GenderId == genderId)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
            }

            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }


            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }
            return productsDto;
        }


        public List<ProductDto> GetProductsByCategory(int Id)
        {
            var products = UnitOfWork.Products
                  .Get()
                  .Where(x => x.CategoryId == Id)
                  .Include(x => x.Brand)
                  .Include(x => x.Category)
                  .Include(x => x.Color)
                  .Include(x => x.Image)
                  .ToList();

            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }
            return productsDto;
        }


        public int GetGenderCount(string searchString, int genderId, List<int> brandsId)
        {

            var products = UnitOfWork.Products
                 .Get()
                 .Include(x => x.Brand)
                 .Include(x => x.Category)
                 .Where(x => x.Category.GenderId == genderId)
                 .ToList();


            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return products.Count;
        }

        public List<ProductDto> GetProuctsByGender(int genderId, string searchString, int page, int pageSize, List<int> brandsId)
        {
            var products = UnitOfWork.Products
                .Get()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Color)
                .Include(x => x.Image)
                .Where(x => x.Category.GenderId == genderId)
                .ToList();

           
            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }


            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }
            return productsDto;
        }


        public int GetCategoryCount(string searchString, int categoryrId, List<int> brandsId)
        {

            var products = UnitOfWork.Products
                 .Get()
                 .Include(x => x.Brand)
                 .Include(x => x.Category)
                 .Where(x => x.Category.Id == categoryrId)
                 .ToList();


            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return products.Count;
        }

        public List<ProductDto> GetProuctsByCategory(int categoryId, string searchString, int page, int pageSize, List<int> brandsId)
        {
            var products = UnitOfWork.Products
                .Get()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Color)
                .Include(x => x.Image)
                .Where(x => x.Category.Id == categoryId)
                .ToList();


            if (brandsId.Count > 0)
            {
                products = products.Where(x => brandsId.Contains(x.BrandId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }


            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Image.Select(x => x.Picture).ToList(),
                    CoverImage = product.Image.FirstOrDefault().Picture,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }
            return productsDto;
        }

        public void CreateProduct(ProductCreateModel productCreateModel)
        {

            validationRules.Validate(productCreateModel).ThenThrow();

            var productTest = productCreateModel;
            var images = productCreateModel.Images;

            List<byte[]> productImages = new List<byte[]>();
            List<int> colorsIds = new List<int>();


            foreach (var image in images)
            {
                var picture = new Image();
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    productImages.Add(fileBytes);
                }

            }


            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreateModel.Name,
                Description = productCreateModel.Description,
                Price = productCreateModel.Price,
                BrandId = productCreateModel.BrandId,
                ColorId = productCreateModel.ColorId,
                CategoryId = productCreateModel.CategoryId,
                Discount = 0

            };

            foreach (var image in productImages)
            {
                var picture = new Image
                {

                    Picture = image,
                    ProductId = product.Id
                };

                UnitOfWork.ProductImages.Insert(picture);
            }
            UnitOfWork.Products.Insert(product);
            UnitOfWork.SaveChanges();
        }

        public ProductWithMeasureDto GetProductById(Guid id)
        {
            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var productDto = new ProductWithMeasureDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = UnitOfWork.Brands.Get().FirstOrDefault(x => x.Id == product.BrandId).Name,
                Color = UnitOfWork.Colors.Get().FirstOrDefault(x => x.Id == product.ColorId).Name,
                Category = UnitOfWork.Categories.Get().FirstOrDefault(x => x.Id == product.CategoryId).Name,
                Images = UnitOfWork.ProductImages.Get().Where(x => x.ProductId == product.Id).Select(x => x.Picture).ToList(),
                CoverImage = UnitOfWork.ProductImages.Get().FirstOrDefault(x => x.ProductId == product.Id).Picture,
                Discount = product.Discount
            };
            return productDto;

        }

        public EditProductDto GetEditProductDto(Guid id)
        {
            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var editProductDto = new EditProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Discount = product.Discount,

            };
            return editProductDto;
        }


        public void DeleteProduct(Guid id)
        {
            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == id);
            UnitOfWork.Products.Delete(product);
            UnitOfWork.SaveChanges();
        }


        public int GetProductType(Guid dd)
        {

            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == dd);
            var category = UnitOfWork.Categories.Get().FirstOrDefault(x => x.Id == product.CategoryId);
            var type = UnitOfWork.Types.Get().FirstOrDefault(x => x.Id == category.TypeId);
            return type.Id;
        }

        public void AddProductMeasure(MeasureQuantityModel model)
        {
            var ExistingProductMeasure = UnitOfWork.ProductMeasures.Get().FirstOrDefault(x => x.ProductId == model.ProductId && x.MeasureId == model.MeasureId);
            if (ExistingProductMeasure != null)
            {
                ExistingProductMeasure.Quantity += model.Quantity;
                UnitOfWork.ProductMeasures.Update(ExistingProductMeasure);
                UnitOfWork.SaveChanges();
                return;
            }
            else
            {
                var productMeasure = new ProductMeasure
                {
                    ProductId = model.ProductId,
                    MeasureId = model.MeasureId,
                    Quantity = model.Quantity
                };
                UnitOfWork.ProductMeasures.Insert(productMeasure);
                UnitOfWork.SaveChanges();
            }

        }

        public void EditProduct(EditProductDto model)
        {

            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == model.Id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Discount = model.Discount;
            UnitOfWork.Products.Update(product);
            UnitOfWork.SaveChanges();

        }

        public void AddProductToCart(Guid productId, int measureId)
        {

            var productInShoppingCart = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            if (currentUser == null)
            {
                throw new Exception("You must be logged in to add products to cart");
            }
            if (productInShoppingCart != null)
            {
                productInShoppingCart.Quantity += 1;
                UnitOfWork.ShoppingCarts.Update(productInShoppingCart);
                UnitOfWork.SaveChanges();

            }
            else
            {
                var product = new ShoppingCart
                {
                    UserId = new Guid(currentUser.Id),
                    ProductId = productId,
                    MeasureId = measureId,
                    Quantity = 1
                };

                UnitOfWork.ShoppingCarts.Insert(product);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
