using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private string ProductType { get; set; }
        private ObservableCollection<string> _sortType;

        public ObservableCollection<string> SortType
        {
            get => _sortType;
            set
            {
                _sortType = value;
                OnPropertyChanged();
            }
        }

        private string _searchedText;

        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                _searchedText = value;
                OnPropertyChanged();
                Filter();
            }
        }

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

        private string _selectedSort;

        public string SelectedSort
        {
            get
            {
                return _selectedSort;
            }
            set
            {
                _selectedSort = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private ObservableCollection<Product> _unfilteredProduct { get; set; }

        public ObservableCollection<Product> UnfilteredProducts
        {
            get { return _unfilteredProduct; }
            set
            {
                _unfilteredProduct = value;
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
                //Remove from database
                db.Products.Remove(SelectedProduct);
                db.SaveChanges();
                //Remove from UI
                Products.Remove(SelectedProduct);
            }
        }

        public void BuyProduct()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    DatabaseController.AddProductToCart(SelectedProduct.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Refresh();
            }
        }

        public void Filter()
        {
            ObservableCollection<Product> temp = new ObservableCollection<Product>(UnfilteredProducts);
            //TODO
            temp = TypeFilter(temp);
            temp = CategoryFilter(temp);
            temp = SearchFilter(temp);
            temp = SortFilter(temp);
            Products = temp;
        }

        public ObservableCollection<Product> TypeFilter(ObservableCollection<Product> temp)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (string.IsNullOrEmpty(ProductType))
                {
                    return temp;
                }
                else
                {
                    if (ProductType == "Male")
                    {
                        return new ObservableCollection<Product>(temp.Where(x => x.Type == "Male").ToList());
                    }
                    else
                    {
                        return new ObservableCollection<Product>(temp.Where(x => x.Type == "Female").ToList());
                    }
                }
            }
        }

        public ObservableCollection<Product> CategoryFilter(ObservableCollection<Product> temp)
        {
            if (SelectedCategory == null)
            {
                return temp;
            }
            else
            {
                return new ObservableCollection<Product>(temp.Where(x => x.Category.Name == SelectedCategory.Name));
            }
        }

        public ObservableCollection<Product> SearchFilter(ObservableCollection<Product> temp)
        {
            if (string.IsNullOrEmpty(SearchedText))
            {
                return temp;
            }
            else
            {
                var items = temp.Where(x => x.Name.StartsWith(SearchedText, StringComparison.OrdinalIgnoreCase) || x.Company.Name.StartsWith(SearchedText, StringComparison.OrdinalIgnoreCase));
                return new ObservableCollection<Product>(items);
            }
        }

        public ObservableCollection<Product> SortFilter(ObservableCollection<Product> temp)
        {
            if (string.IsNullOrEmpty(SelectedSort))
            {
                return temp;
            }
            else
            {
                if (SelectedSort == "Price↓")
                {
                    return new ObservableCollection<Product>(temp.OrderBy(x => x.Price));
                }
                else
                {
                    return new ObservableCollection<Product>(temp.OrderByDescending(x => x.Price));
                }
            }
        }

        public StoreViewModel()
        {
            LoadDataAsync();
            SortType = new ObservableCollection<string>() { "Price↑", "Price↓" };
            //With a underscore so as not to trigger Filter;
            _searchedText = "";
        }

        public async void LoadDataAsync()
        {
            Products = await DatabaseController.GetProductsAsync();
            _unfilteredProduct = new ObservableCollection<Product>(Products);
            Categories = await DatabaseController.GetCategoriesAsync();
        }

        public async void Refresh()
        {
            Products = await DatabaseController.GetProductsAsync();
            _unfilteredProduct = new ObservableCollection<Product>(Products);
            Filter();
        }
    }
}