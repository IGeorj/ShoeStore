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
        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }
        public OrdersViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SaveChanges();
                Orders = new ObservableCollection<Order>(db.Orders.Include("User").ToList());
            }
        }
    }
}
