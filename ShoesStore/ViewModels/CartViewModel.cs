using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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
                RefreshOrderPrice();
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

        private RelayCommand _acceptCommand;

        public RelayCommand AcceptCommand
        {
            get
            {
                return _acceptCommand ??
                  (_acceptCommand = new RelayCommand(obj =>
                  {
                      AcceptOrder();
                  }));
            }
        }

        public void AcceptOrder()
        {
            try
            {
                DatabaseController.ConfirmOrder(OrderTotalPrice);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            LoadDataAsync();
        }

        public void RemoveProduct()
        {
            DatabaseController.RemoveProductFromCart(SelectedDetails.Product.Id, SelectedDetails.Quantity);
            Cart.Remove(SelectedDetails);
            RefreshOrderPrice();
        }

        public void RefreshOrderPrice()
        {
            OrderTotalPrice = 0;
            foreach (var item in Cart)
            {
                OrderTotalPrice += item.TotalPrice;
            }
        }

        public CartViewModel()
        {
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            Cart = await DatabaseController.GetCartAsync();
        }
    }
}