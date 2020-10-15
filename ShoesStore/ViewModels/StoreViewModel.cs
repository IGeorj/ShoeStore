using Microsoft.EntityFrameworkCore;
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
        public string ImagePath
        {
            get
            {
                return @"D:\512x512.jpg";
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
        public StoreViewModel()
        {
            LoadDataAsync();
        }
        public async void LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items = await Task.Run(() => db.Products.ToListAsync());
                Products = new ObservableCollection<Product>(items);
            }
        }
    }
}
