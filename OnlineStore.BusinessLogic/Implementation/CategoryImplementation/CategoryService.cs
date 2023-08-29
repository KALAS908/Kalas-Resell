using OnlineStore.BusinessLogic.Base;
using OnlineStore.DataAccess;
using OnlineStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineStore.DataAccess.EntityFramework.Context;
using OnlineStore.Common.DTOs;

namespace OnlineStore.WebApp.Controllers
{
    public class CategoryService : BaseService
    {

        public CategoryService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            
        }

        public List<CategoryDto> GetAllCategories()
        {
            List<Category> categories = UnitOfWork.Categories.Get().ToList();
            List<CategoryDto> categoryList = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoryList.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    GenderId = category.GenderId,
                    TypeId = category.TypeId
                });
            }
            return categoryList;
        }

        public List<CategoryDto> GetCategories(int genderId,int typeId) 
        { 
            List<Category> categories = UnitOfWork.Categories.Get().Where(categories => categories.GenderId == genderId && categories.TypeId == typeId).ToList();
            List<CategoryDto> categoryList = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoryList.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return categoryList;

        }
    }
}