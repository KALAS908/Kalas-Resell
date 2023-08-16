using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.NewFolder
{
    public class ShoppinCartService : BaseService
    {
        private readonly CurrentUserDto currentUser;
        public ShoppinCartService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            currentUser = serviceDependencies.CurrentUser;
        }


        public void RemoveItem(Guid productId, string  measure)
        {
            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            UnitOfWork.ShoppingCarts.Delete(tem);
            UnitOfWork.SaveChanges();
        }

        public void IncreaseQuantity(Guid productId, string measure)
        {
            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            tem.Quantity++;
            var Productmeasure = UnitOfWork.ProductMeasures.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId);
            if (tem.Quantity > Productmeasure.Quantity)
            {
                tem.Quantity = Productmeasure.Quantity;
                
            }
            UnitOfWork.ShoppingCarts.Update(tem);
            UnitOfWork.SaveChanges();
        }

        public void DecreaseQuantity(Guid productId, string measure)
        {
            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            tem.Quantity--;

            if (tem.Quantity == 0)
            {
                UnitOfWork.ShoppingCarts.Delete(tem);
                UnitOfWork.SaveChanges();
                return;
            }

            UnitOfWork.ShoppingCarts.Update(tem);
            UnitOfWork.SaveChanges();
        }

        public string GetShoppingCartItem(Guid productId, string measure)
        {
            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            var SubTotal = tem.Quantity * tem.Product.Price;
            return tem.Quantity.ToString() + " " + SubTotal.ToString();

        }
    }
}
