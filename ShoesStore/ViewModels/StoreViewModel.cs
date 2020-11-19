using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> _originalProduct { get; set; }

        public ObservableCollection<Product> OriginalProducts
        {
            get { return _originalProduct; }
            set
            {
                _originalProduct = value;
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
        private RelayCommand _buyProductCommand;

        public RelayCommand BuyProductCommand
        {
            get
            {
                return _buyProductCommand ??
                  (_buyProductCommand = new RelayCommand(obj =>
                  {
                      BuyProduct();
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
        public void BuyProduct()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product product = db.Products.Find(SelectedProduct.Id);
                if(product.Quantity == 0)
                {
                    MessageBox.Show("Out of stock");
                    return;
                }
                else
                {
                    product.Quantity -= 1;
                    OrderDetail details = db.OrderDetails.Where(x => x.Order.Status == "Pending").FirstOrDefault(x => x.Product.Id == product.Id);
                    if (details == null)
                    {
                        db.OrderDetails.Add(new OrderDetail { Product = product, Order = db.Orders.FirstOrDefault(x => x.Status == "Pending"), Quantity = 1, TotalPrice = product.Price });
                    }
                    else
                    {
                        details.Quantity += 1;
                        details.TotalPrice += product.Price;
                    }
                }
                db.SaveChanges();
                Refresh();
            }
        }
        public void Filter()
        {
            //TODO
            TypeFilter();
            CategoryFilter();
            //SearchFilter();
            //SortFilter();
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
            Products = new ObservableCollection<Product>(Products.Where(x => x.Category.Name == SelectedCategory.Name));
        }

        public StoreViewModel()
        {
            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var products = await Task.Run(() => db.Products.Include("Company").Include("Category").ToListAsync());
                Products = new ObservableCollection<Product>(products);
                _originalProduct = new ObservableCollection<Product>(Products);
                var categories = await Task.Run(() => db.Categories.ToListAsync());
                Categories = new ObservableCollection<Category>(categories);
            }
        }

        public void Refresh()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Products = new ObservableCollection<Product>(db.Products.Include("Company").Include("Category").ToList());
                _originalProduct = new ObservableCollection<Product>(Products);
            }
            Filter();
        }
    }
}