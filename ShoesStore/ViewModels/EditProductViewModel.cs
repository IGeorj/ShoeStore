using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ShoesStore.ViewModels
{
    public class EditProductViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        private SexType _sex;

        public SexType Sex
        {
            get => _sex;
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

        public bool IsMale
        {
            get { return Sex == SexType.Male; }
            set { Sex = value ? SexType.Male : Sex; }
        }

        public bool IsFemale
        {
            get { return Sex == SexType.Female; }
            set { Sex = value ? SexType.Female : Sex; }
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
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
            }
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
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
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

        private RelayCommand _changeProductCommand;

        public RelayCommand ChangeProductCommand
        {
            get
            {
                return _changeProductCommand ??
                  (_changeProductCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          Product pr = db.Products.Include("Category").Include("Company").FirstOrDefault(x => x.Id == Id);
                          pr.Name = Name;
                          pr.Image = $"{Image.UriSource}";
                          pr.Quantity = (int)Quantity;
                          pr.Price = (int)Price;
                          pr.Type = GetSexType;
                          pr.Company = db.Companies.FirstOrDefault(x => x.Id == SelectedCompany.Id);
                          pr.Category = db.Categories.FirstOrDefault(x => x.Id == SelectedCategory.Id);
                          db.SaveChanges();
                      }
                  }));
            }
        }

        public EditProductViewModel()
        {
            LoadData();
        }

        public EditProductViewModel(int Id)
        {
            LoadData();
            this.Id = Id;
            SetData();
        }

        public void SetData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product pr = db.Products.Include("Category").Include("Company").FirstOrDefault(x => x.Id == Id);
                Name = pr.Name;
                Image = new BitmapImage(new Uri(pr.Image, UriKind.Absolute));
                Quantity = pr.Quantity;
                Price = pr.Price;
                _sex = pr.Type == "Female" ? SexType.Female : SexType.Male;
                SelectedCategory = Categories.FirstOrDefault(x => x.Id == pr.Category.Id);
                SelectedCompany = Companies.FirstOrDefault(x => x.Id == pr.Company.Id);
            }
        }

        public void LoadData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Companies = new ObservableCollection<Company>(db.Companies.ToList());
                Categories = new ObservableCollection<Category>(db.Categories.ToList());
            }
        }
    }
}