using ShoesStore.Commands;
using ShoesStore.Models;

namespace ShoesStore.ViewModels
{
    public class AddEditCategoryViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => _id = value;
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

        private RelayCommand _addCategoryCommand;

        public RelayCommand AddCategoryCommand
        {
            get
            {
                return _addCategoryCommand ??
                  (_addCategoryCommand = new RelayCommand(obj =>
                  {
                      AddCategory();
                  }));
            }
        }

        private RelayCommand _changeCategoryCommand;

        public RelayCommand ChangeCategoryCommand
        {
            get
            {
                return _changeCategoryCommand ??
                  (_changeCategoryCommand = new RelayCommand(obj =>
                  {
                      ChangeCategory();
                  }));
            }
        }

        public void AddCategory()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Categories.Add(new Category { Name = Name });
                db.SaveChanges();
            }
        }

        public void ChangeCategory()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                Category category = db.Categories.Find(Id);
                category.Name = Name;
                db.SaveChanges();
            }
        }

        public AddEditCategoryViewModel()
        {
        }

        public AddEditCategoryViewModel(int id)
        {
            this.Id = id;
            using (ApplicationContext db = new ApplicationContext())
            {
                Category category = db.Categories.Find(id);
                Name = category.Name;
            }
        }
    }
}