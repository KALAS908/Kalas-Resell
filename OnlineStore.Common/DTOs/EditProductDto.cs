using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
     public class EditProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int MeasureId { get; set; }

        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int TypeOfClothingId { get; set; }
        public int ColorId { get; set; }
     
        public int GenderId { get; set; }
        public int Discount { get; set; }

    }
}
