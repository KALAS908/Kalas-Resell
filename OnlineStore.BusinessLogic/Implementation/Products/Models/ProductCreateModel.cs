using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using OnlineStore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Products.Models
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int MeasureId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int TypeOfClothingId { get; set; }
        public int ColorId { get; set; }
        public List<IFormFile> Images { get; set; }

        public int GenderId { get; set; }
        
    }
}
