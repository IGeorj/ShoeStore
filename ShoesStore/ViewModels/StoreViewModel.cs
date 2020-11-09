using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoesStore.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private string ProductType { get; set; }
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                Filter();
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<Product> _originalProduct { get; set; }

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

        private RelayCommand _filterCommand;

        public RelayCommand FilterCommand
        {
            get
            {
                return _filterCommand ??
                  (_filterCommand = new RelayCommand(obj =>
                  {
                      ProductType = obj.ToString();
                      Filter();
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

        public void Filter()
        {
            //TODO
            TypeFilter();
            CategoryFilter();
            //SearchFilter();
        }

        public void TypeFilter()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (string.IsNullOrEmpty(ProductType))
                {
                    Products = new ObservableCollection<Product>(_originalProduct);
                }
                if (ProductType == "Male")
                {
                    Products = new ObservableCollection<Product>(_originalProduct.Where(x => x.Type == "Male").ToList());
                }
                if (ProductType == "Female")
                {
                    Products = new ObservableCollection<Product>(_originalProduct.Where(x => x.Type == "Female").ToList());
                }
            }
        }

        public void CategoryFilter()
        {
            if (SelectedCategory == null)
            {
                return;
            }
            Products = new ObservableCollection<Product>(Products.Where(x => x.Category == SelectedCategory));
        }

        public StoreViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var items1 = db.Products.Include("Company").ToList();
                Products = new ObservableCollection<Product>(items1);
                _originalProduct = new ObservableCollection<Product>(Products);
                var items2 = db.Categories.ToList();
                Categories = new ObservableCollection<Category>(items2);
            }
        }
    }
}