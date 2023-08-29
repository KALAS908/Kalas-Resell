using OnlineStore.BusinessLogic.Base;
using OnlineStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Common.DTOs;
namespace OnlineStore.BusinessLogic.Implementation.GenderImplementation
{
    public class GenderService : BaseService
    {
        public GenderService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }
        public List<GenderDto> GetAllGenders()
        {
            List<Gender> genders = UnitOfWork.Genders.Get().ToList();
            List<GenderDto> genderDtos = new List<GenderDto>();
            foreach (var gender in genders)
            {
                genderDtos.Add(new GenderDto
                {
                    Id = gender.Id,
                    Name = gender.GenderName
                });
                
            }
            return genderDtos;
        }

    }
}

