using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ShoesStore.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private int _orderTotalPrice;

        public int OrderTotalPrice
        { 
            get => _orderTotalPrice;
            set 
            {
                _orderTotalPrice = value;
                OnPropertyChanged();
            }
        }

        private OrderDetail _selectedDetails;

        public OrderDetail SelectedDetails
        {
            get
            {
                return _selectedDetails;
            }
            set
            {
                _selectedDetails = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrderDetail> _cart;

        public ObservableCollection<OrderDetail> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand _removeProductCommand;

        public RelayCommand RemoveProductCommand
        {
            get
            {
                return _removeProductCommand ??
                  (_removeProductCommand = new RelayCommand(obj =>
                  {
                      RemoveProduct();
                  }));
            }
        }
        public void RemoveProduct()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Product product = db.Products.Find(SelectedDetails.Product.Id);
                product.Quantity += SelectedDetails.Quantity;
                db.SaveChanges();
                db.OrderDetails.Remove(db.OrderDetails.Find(SelectedDetails.Id));
                db.SaveChanges();
                LoadData();
            }
        }
        public CartViewModel()
        {
            LoadData();
        }
        public void LoadData()
        {
            //TODO CurrentUser
            using (ApplicationContext db = new ApplicationContext())
            {
                OrderTotalPrice = 0;
                var order = db.Orders.FirstOrDefault(x => x.Status == "Pending");
                if (order is null)
                {
                    db.Orders.Add(new Order { Date = DateTime.Now.ToString(), Status = "Pending", TotalPrice = 0, User = db.Users.First() });
                    db.SaveChanges();
                }
                Cart = new ObservableCollection<OrderDetail>(db.OrderDetails.Include("Product").Where(x => x.Order.Status == "Pending"));
                foreach (var item in Cart)
                {
                    OrderTotalPrice += item.TotalPrice;
                }
            }
        }
    }
}
