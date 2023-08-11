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
using OnlineStore.Common.Extesnsions;

namespace OnlineStore.BusinessLogic.Implementation.Countries
{
    public  class CountriesService : BaseService
    {

        public CountriesService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            

        }

        public List<CountryDto> GetAllCountries()
        {
            List<Country> countries = UnitOfWork.Countries.Get().ToList();
            List<CountryDto> countriesDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countriesDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            return countriesDto;
        }
    }
}
