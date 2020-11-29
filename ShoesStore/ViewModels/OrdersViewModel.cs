using ShoesStore.Models;
using System.Collections.ObjectModel;

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

        public async void LoadDataAsync()
        {
            Orders = await DatabaseController.GetOrdersAsync();
        }
    }
}