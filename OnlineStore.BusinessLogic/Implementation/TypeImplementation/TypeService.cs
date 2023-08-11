using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.TypeImplementation
{
    public class TypeService : BaseService
    {
        public TypeService(ServiceDependencies dependencies) : base(dependencies)
        {
        }

        public List<TypeDto> GetAllTypes()
        {
            var types = UnitOfWork.Types.Get().ToList();
            var typesDto = new List<TypeDto>();
            foreach(var type in types)
            {
                var typeDto = new TypeDto
                {
                    Id = type.Id,
                    TypeName = type.TypeName
                    };
                typesDto.Add(typeDto);
            }
            return typesDto;
        }
    }
}
