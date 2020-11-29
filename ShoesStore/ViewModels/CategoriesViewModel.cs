using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Categories.Remove(SelectedCategory);
                db.SaveChanges();
                Categories.Remove(SelectedCategory);
            }
        }

        public CategoriesViewModel()
        {
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            Categories = await DatabaseController.GetCategoriesAsync();
        }
    }
}