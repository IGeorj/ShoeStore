using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShoesStore.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private Product _selectedProduct;
        public Product SelectedProduct 
        { 
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _deleteProductCommand;

        public RelayCommand DeleteProductCommand
        {
            get
            {
                return _deleteProductCommand ??
                  (_deleteProductCommand = new RelayCommand(obj =>
                  {
                      DeleteProduct();
                  }));
            }
        }

        public void DeleteProduct()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Products.Remove(SelectedProduct);
                db.SaveChanges();
                Products.Remove(SelectedProduct);
            }
        }
        public StoreViewModel()
        {
            LoadDataAsync();
        }
        public async void LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                var items = await Task.Run(() => db.Products.Include("Company").ToListAsync());
                Products = new ObservableCollection<Product>(items);
            }
        }
    }
}
