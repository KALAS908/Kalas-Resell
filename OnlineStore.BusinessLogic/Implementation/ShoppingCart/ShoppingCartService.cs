using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;


namespace OnlineStore.BusinessLogic.Implementation.NewFolder
{
    public class ShoppingCartService : BaseService
    {
        private readonly CurrentUserDto currentUser;

        public ShoppingCartService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            currentUser = serviceDependencies.CurrentUser;
        }


        public void RemoveItem(Guid productId, string measure)
        {
            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            UnitOfWork.ShoppingCarts.Delete(tem);
            UnitOfWork.SaveChanges();
        }

        public void IncreaseQuantity(Guid productId, string measure)
        {

            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            if (measureId == null)
            {
                throw new Exception("Measure not found");
            }
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            if (tem == null)
            {
                throw new Exception("Quantity not found");
            }
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
            if (measureId == null)
            {
                throw new Exception("Measure not found");
            }
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId && x.UserId.ToString() == currentUser.Id);
            if (tem == null)
            {
                throw new Exception("Quantity not found");
            }
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
            return $"{tem.Quantity} {SubTotal}";
        }


        public void RemoveFromDataBase(Guid productId, string measure, int quantity)
        {

            var measureId = UnitOfWork.Measures.Get().FirstOrDefault(x => x.MeasureValue == measure).Id;
            if (measureId == null)
            {
                throw new Exception("Measure not found");
            }
            var productMeasure = UnitOfWork.ProductMeasures.Get().FirstOrDefault(x => x.ProductId == productId && x.MeasureId == measureId);
            if (productMeasure == null)
            {
                throw new Exception("Product not found");
            }

            productMeasure.Quantity -= quantity;
            UnitOfWork.ProductMeasures.Update(productMeasure);
            UnitOfWork.SaveChanges();

        }

        public double GetTotalPrice()
        {
            var cart = UnitOfWork.ShoppingCarts.Get().Where(x => x.UserId.ToString() == currentUser.Id);
            double TotalPrice = 0;
            foreach (var item in cart)
            {
                TotalPrice = (double)(TotalPrice + (item.Quantity * (item.Product.Price - item.Product.Price * item.Product.Discount / 100)));
            }
            return TotalPrice;
        }

        public void AddReceipt(Receipt receipt)
        {
            UnitOfWork.Receipts.Insert(receipt);
            UnitOfWork.SaveChanges();
        }

        public void AddOrderedItem(OrderedItems orderedItem, int Id)
        {
            var exist = UnitOfWork.Orders.Get().FirstOrDefault(x => x.ProductId == orderedItem.ProductId && x.OrderId == Id);

            if (exist != null)
            {
                exist.Quantity += orderedItem.Quantity;
                UnitOfWork.Orders.Update(exist);
                UnitOfWork.SaveChanges();
                return;
            }

            UnitOfWork.Orders.Insert(orderedItem);
            UnitOfWork.SaveChanges();
        }
    }
}
