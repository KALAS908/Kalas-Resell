using OnlineStore.BusinessLogic.Base;
using OnlineStore.DataAccess;
using OnlineStore.Entities.Entities;
using OnlineStore.Common.DTOs;

namespace OnlineStore.BusinessLogic.Implementation.BrandImplementation
{
    public class BrandService : BaseService
    {

       
        public BrandService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
           
        }
        
        public List<BrandDto> GetAllBrands()
        {
            List<Brand> result = UnitOfWork.Brands.Get().ToList();
            List<BrandDto> resultDto = new List<BrandDto>();
            foreach (var brand in result)
            {
                resultDto.Add(new BrandDto
                {
                    Id = brand.Id,
                    Name = brand.Name
                });
            }
            return resultDto;
        }

    }
}
