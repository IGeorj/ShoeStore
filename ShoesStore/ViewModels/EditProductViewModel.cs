using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public class EditProductViewModel : BaseViewModel
    {
        public enum SexType
        {
            Male,
            Female
        }
        private SexType _sex;

        public SexType Sex
        {
            get { return _sex; }
            set
            {
                if (_sex == value)
                    return;

                _sex = value;
                OnPropertyChanged("SexType");
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
        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                SelectedCategory = _product.Category;
                SelectedCompany = _product.Company;
                _sex = _product.Type == "Male" ? SexType.Male : SexType.Female;
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

        public EditProductViewModel()
        {
            LoadData();
        }
        public void LoadData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Categories = new ObservableCollection<Category>(db.Categories.ToList());
                Companies = new ObservableCollection<Company>(db.Companies.ToList());
            }
        }
    }
}
