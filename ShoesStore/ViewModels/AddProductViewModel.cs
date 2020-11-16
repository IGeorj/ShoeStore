using Microsoft.Win32;
using ShoesStore.Commands;
using ShoesStore.Enums;
using ShoesStore.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ShoesStore.ViewModels
{

    public class AddProductViewModel : BaseViewModel
    {
        private SexType _sex = SexType.Male;

        public SexType Sex
        {
            get { return _sex; }
            set
            {
                if (_sex == value)
                    return;

                _sex = value;
                OnPropertyChanged("Sex");
                OnPropertyChanged("IsFemale");
                OnPropertyChanged("IsMale");
            }
        }

        public string GetSexType
        {
            get
            {
                switch (Sex)
                {
                    case SexType.Male:
                        return "Male";

                    case SexType.Female:
                        return "Female";
                }
                return "";
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int? _price;

        public int? Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private int? _quantity;

        public int? Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage image;

        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Company> _companies;

        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

        private Company _selectedCompany;

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; }
        }

        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; }
        }

        private RelayCommand _changeImageCommand;

        public RelayCommand ChangeImageCommand
        {
            get
            {
                return _changeImageCommand ??
                  (_changeImageCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog file = new OpenFileDialog();
                      file.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG) | *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.*";
                      Nullable<bool> result = file.ShowDialog();
                      if (File.Exists(file.FileName))
                      {
                          Image = new BitmapImage(new Uri(file.FileName, UriKind.Absolute));
                      }
                  }));
            }
        }

        private RelayCommand _addProductCommand;

        public RelayCommand AddProductCommand
        {
            get
            {
                return _addProductCommand ??
                  (_addProductCommand = new RelayCommand(obj =>
                  {
                      AddProduct();
                  }));
            }
        }

        public void AddProduct()
        {
            if (Image == null || SelectedCategory == null || SelectedCompany == null || string.IsNullOrEmpty(Name) || Price == 0)
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Products.Add(new Product { Image = $"{Image.UriSource}", Name = Name, Company = db.Companies.FirstOrDefault(x => x.Name == SelectedCompany.Name), Category = db.Categories.FirstOrDefault(x => x.Name == SelectedCategory.Name), Price = (int)Price, Quantity = (int)Quantity, Type = GetSexType });
                db.SaveChanges();
            }
        }

        public AddProductViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Companies = new ObservableCollection<Company>(db.Companies.ToList());
                Categories = new ObservableCollection<Category>(db.Categories.ToList());
            }
        }
    }
}