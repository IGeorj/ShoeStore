using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<OrderDetail> _orderDetails;

        public ObservableCollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                _orderDetails = value;
                OnPropertyChanged();
            }
        }
        public OrdersViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SaveChanges();
                OrderDetails = new ObservableCollection<OrderDetail>(db.OrderDetails.Include("Order.User").ToList());
            }
        }
    }
}
