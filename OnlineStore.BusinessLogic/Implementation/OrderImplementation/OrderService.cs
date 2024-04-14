using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.OrderImplementation
{
    public class OrderService : BaseService
    {
        private readonly CurrentUserDto currentUser;
        public OrderService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            currentUser = serviceDependencies.CurrentUser;
        }  

        public List<OrderedItemDto> GetAllOrder(int orderId)
        {

            var order = UnitOfWork.Orders.Get().Where(x => x.OrderId == orderId).ToList();
            var orderedItems = new List<OrderedItemDto>();
            foreach (var item in order)
            {
                var orderedItem = new OrderedItemDto();
                orderedItem.ProductId = item.ProductId;
                orderedItem.ProductName = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == item.ProductId).Name;
                orderedItem.Quantity = (int)item.Quantity;
                orderedItems.Add(orderedItem);
            }
            return orderedItems;
        }
    }
}
