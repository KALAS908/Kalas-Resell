using OnlineStore.Common;
using OnlineStore.DataAccess.EntityFramework.Context;
using OnlineStore.Entities;
using OnlineStore.Entities.Entities;
using System;
using Type = OnlineStore.Entities.Entities.Type;

namespace OnlineStore.DataAccess
{
    public class UnitOfWork  
    {
        private readonly StoreDataBaseContext Context;

        public UnitOfWork(StoreDataBaseContext context)
        {
            this.Context = context;
        }

        private IRepository<Brand> brands;
        public IRepository<Brand> Brands => brands ??= new BaseRepository<Brand>(Context);

        private IRepository<Category> categories;
        public IRepository<Category> Categories => categories ??= new BaseRepository<Category>(Context);

        private IRepository<Product> products;
        public IRepository<Product> Products => products ??= new BaseRepository<Product>(Context);

        private IRepository<Image> productImages;
        public IRepository<Image> ProductImages => productImages ??= new BaseRepository<Image>(Context);

        public IRepository<Measure> measures;
        public IRepository<Measure> Measures => measures ??= new BaseRepository<Measure>(Context);

        private IRepository<OrderedItems> orderedProducts;
        public IRepository<OrderedItems> OrderedProducts => orderedProducts ??= new BaseRepository<OrderedItems>(Context);

        public IRepository<Color> colors;
        public IRepository<Color> Colors => colors ??= new BaseRepository<Color>(Context);

        public IRepository<Country> countries ;
        public IRepository<Country> Countries => countries ??= new BaseRepository<Country>(Context);

        public IRepository<Goods> goods;
        public IRepository<Goods> Goods => goods ??= new BaseRepository<Goods>(Context);

        public IRepository<Gender> genders;
        public IRepository<Gender> Genders => genders ??= new BaseRepository<Gender>(Context);

        public IRepository<Provider> providers;
        public IRepository<Provider> Providers => providers ??= new BaseRepository<Provider>(Context);

        public IRepository<Receipt> receipts;
        public IRepository<Receipt> Receipts => receipts ??= new BaseRepository<Receipt>(Context);

        public IRepository<Role> roles;
        public IRepository<Role> Roles => roles ??= new BaseRepository<Role>(Context);

        public IRepository<ShoppingCart> shoppingCarts;
        public IRepository<ShoppingCart> ShoppingCarts => shoppingCarts ??= new BaseRepository<ShoppingCart>(Context);

        public IRepository<User> users;
        public IRepository<User> Users => users ??= new BaseRepository<User>(Context);

        public IRepository<WishList> wishLists;
        public IRepository<WishList> WishLists => wishLists ??= new BaseRepository<WishList>(Context);

        public IRepository<UserString> usersStrings;
        public IRepository<UserString> UsersStrings => usersStrings ??= new BaseRepository<UserString>(Context);

        public IRepository<Type> types;
        public IRepository<Type> Types => types ??= new BaseRepository<Type>(Context);

        public IRepository<ProductMeasure> productMeasures;
        public IRepository<ProductMeasure> ProductMeasures => productMeasures ??= new BaseRepository<ProductMeasure>(Context);

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
