using ShoesStore.Commands;
using ShoesStore.Models;

namespace ShoesStore.ViewModels
{
    public class AddEditCompanyViewModel : BaseViewModel
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

        private string _country;

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _addCompanyCommand;

        public RelayCommand AddCompanyCommand
        {
            get
            {
                return _addCompanyCommand ??
                  (_addCompanyCommand = new RelayCommand(obj =>
                  {
                      AddCompany();
                  }));
            }
        }

        private RelayCommand _changeCompanyCommand;

        public RelayCommand ChangeCompanyCommand
        {
            get
            {
                return _changeCompanyCommand ??
                  (_changeCompanyCommand = new RelayCommand(obj =>
                  {
                      ChangeCompany();
                  }));
            }
        }

        public void AddCompany()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Companies.Add(new Company { Name = Name, Country = Country });
                db.SaveChanges();
            }
        }

        public void ChangeCompany()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Name))
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                Company company = db.Companies.Find(Id);
                company.Name = Name;
                company.Country = Country;
                db.SaveChanges();
            }
        }

        public AddEditCompanyViewModel()
        {
        }

        public AddEditCompanyViewModel(int id)
        {
            this.Id = id;
            using (ApplicationContext db = new ApplicationContext())
            {
                Company company = db.Companies.Find(id);
                Name = company.Name;
                Country = company.Country;
            }
        }
    }
}