using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ShoesStore.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private Category _selectedCategory;
        public Category SelectedCategory 
        { 
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
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
        private RelayCommand _deleteCategoryCommand;

        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return _deleteCategoryCommand ??
                  (_deleteCategoryCommand = new RelayCommand(obj =>
                  {
                      DeleteCategory();
                  }));
            }
        }
        public void DeleteCategory()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Categories.Remove(SelectedCategory);
                db.SaveChanges();
            }
        }
        public CategoriesViewModel()
        {
            LoadData();
        }
        public void LoadData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Categories = new ObservableCollection<Category>(db.Categories.ToList());
            }
        }
    }
}
