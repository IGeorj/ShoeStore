using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;
using ShoesStore.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore
{
    internal class DatabaseController
    {
        public async static Task<ObservableCollection<User>> GetUsersAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Users.ToListAsync());
                return new ObservableCollection<User>(items);
            }
        }

        public async static Task<ObservableCollection<User>> SearchUsersAsync(string text)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() =>
                db.Users.Where(x => x.Name.StartsWith(text) || x.Email.StartsWith(text) || x.Login.StartsWith(text) ||
                               x.Password.StartsWith(text) || x.Proffesion.StartsWith(text))
                );
                return new ObservableCollection<User>(items);
            }
        }

        public async static Task<ObservableCollection<Product>> GetProductsAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Products.Include("Company").Include("Category").ToListAsync());
                return new ObservableCollection<Product>(items);
            }
        }

        public async static Task<ObservableCollection<Category>> GetCategoriesAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Categories.ToListAsync());
                return new ObservableCollection<Category>(items);
            }
        }

        public async static Task<ObservableCollection<Company>> GetCompaniesAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Companies.ToListAsync());
                return new ObservableCollection<Company>(items);
            }
        }

        public async static Task<ObservableCollection<Order>> GetOrdersAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Orders.Include("User").ToListAsync());
                return new ObservableCollection<Order>(items);
            }
        }

        public async static Task<ObservableCollection<OrderDetail>> GetCartAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Status == "Pending" && x.User.Id == GlobalInfo.CurrentUser.Id);
                if (order is null)
                {
                    db.Orders.Add(new Order { Date = "", Status = "Pending", TotalPrice = 0, User = db.Users.FirstOrDefault(x => x.Id == GlobalInfo.CurrentUser.Id) });
                    db.SaveChanges();
                }
                var items = await Task.Run(() => db.OrderDetails.Include("Product").Where(x => x.Order.Status == "Pending" && x.Order.User.Id == GlobalInfo.CurrentUser.Id));
                return new ObservableCollection<OrderDetail>(items);
            }
        }

        public static void AddProductToCart(int productId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product product = db.Products.Find(productId);
                if (product.Quantity == 0)
                {
                    throw new Exception("Out of stock");
                }
                else
                {
                    product.Quantity -= 1;
                    OrderDetail details = db.OrderDetails.Where(x => x.Order.Status == "Pending" && x.Order.User.Id == GlobalInfo.CurrentUser.Id).FirstOrDefault(x => x.Product.Id == product.Id);
                    if (details == null)
                    {
                        db.OrderDetails.Add(new OrderDetail { Product = product, Order = db.Orders.FirstOrDefault(x => x.Status == "Pending" && x.User.Id == GlobalInfo.CurrentUser.Id), Quantity = 1, TotalPrice = product.Price });
                    }
                    else
                    {
                        details.Quantity += 1;
                        details.TotalPrice += product.Price;
                    }
                }
                db.SaveChanges();
            }
        }

        public static void RemoveProductFromCart(int productId, int quantity)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product product = db.Products.Find(productId);
                product.Quantity += quantity;
                db.SaveChanges();
                db.OrderDetails.Remove(db.OrderDetails.FirstOrDefault(x => x.Order.User.Id == GlobalInfo.CurrentUser.Id && x.Product.Id == productId));
                db.SaveChanges();
            }
        }

        public static void ConfirmOrder(int orderPrice)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int counter = db.OrderDetails.Include("Product").Where(x => x.Order.Status == "Pending").Count();
                if (counter <= 0)
                {
                    throw new Exception("Your cart is empty");
                }
                else
                {
                    Order order = db.Orders.FirstOrDefault(x => x.Status == "Pending" && x.User.Id == GlobalInfo.CurrentUser.Id);
                    order.TotalPrice = orderPrice;
                    order.Status = "Confirmed";
                    order.Date = DateTime.Now.ToString();
                    db.SaveChanges();
                }
            }
        }
    }
}