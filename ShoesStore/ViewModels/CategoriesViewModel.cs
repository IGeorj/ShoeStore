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
            }
            LoadDataAsync();
        }

        public CategoriesViewModel()
        {
            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var categories = await Task.Run(() => db.Categories.ToListAsync());
                Categories = new ObservableCollection<Category>(categories);
            }
        }
    }
}