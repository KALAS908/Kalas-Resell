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
using Color = OnlineStore.Entities.Entities.Color;

namespace OnlineStore.BusinessLogic.Implementation.ColorImplementation
{
    public class ColorService : BaseService
    {
       
        public ColorService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<ColorDto> GetAllColors()
        {
            List<Color> colors = UnitOfWork.Colors.Get().ToList();
            List<ColorDto> colorsDto = new List<ColorDto>();
            foreach (var color in colors)
            {
                colorsDto.Add(new ColorDto
                {
                    Id = color.Id,
                    Name = color.Name,
                   
                });
            }
            return colorsDto;
        }
    }
}
