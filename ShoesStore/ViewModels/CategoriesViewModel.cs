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
