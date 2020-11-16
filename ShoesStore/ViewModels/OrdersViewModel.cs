using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
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
            LoadDataAsync();
        }
        public async Task LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var orders = await Task.Run(() => db.Orders.Include("User").ToListAsync());
                Orders = new ObservableCollection<Order>(orders);
            }
        }
    }
}
