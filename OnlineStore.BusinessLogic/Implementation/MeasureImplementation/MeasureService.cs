using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.MeasureImplementation
{
    public class MeasureService: BaseService
    {
        public MeasureService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<MeasureDto> GetMeasures (int typeId)
        {
            var measures = UnitOfWork.Measures.Get().Where(measures => measures.TypeId == typeId).ToList();
            List<MeasureDto> measuresDto = new List<MeasureDto>();

            foreach (var measure in measures)
            {
                var measureDto = new MeasureDto
                {
                    Id = measure.Id,
                    Name = measure.MeasureValue
                };
                measuresDto.Add(measureDto);
            }
            return measuresDto;
        }

        public List<QuantityMeasureDto> GetProductMeasures(Guid productId)
        {

            var productMeasures = UnitOfWork.ProductMeasures.Get().Where(x => x.ProductId == productId &&  x.Quantity!=0 ).ToList();
            List<QuantityMeasureDto> productMeasuresDto = new List<QuantityMeasureDto>();

            foreach (var productMeasure in productMeasures)
            {
                var measure = UnitOfWork.Measures.Get().FirstOrDefault(x => x.Id == productMeasure.MeasureId);

                var measureDto = new QuantityMeasureDto
                {
                    MeasureId = measure.Id,
                    MeasureName = measure.MeasureValue,
                    Quantity = (int)productMeasure.Quantity
                };
                productMeasuresDto.Add(measureDto);
            }
            return productMeasuresDto;
        }

        

    }
}
