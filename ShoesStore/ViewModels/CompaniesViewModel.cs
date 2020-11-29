using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShoesStore.ViewModels
{
    public class CompaniesViewModel : BaseViewModel
    {
        private Company _selectedCompany;

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Company> _companies;

        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _deleteCompanyCommand;

        public RelayCommand DeleteCompanyCommand
        {
            get
            {
                return _deleteCompanyCommand ??
                  (_deleteCompanyCommand = new RelayCommand(obj =>
                  {
                      DeleteCompany();
                  }));
            }
        }

        public void DeleteCompany()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Companies.Remove(SelectedCompany);
                db.SaveChanges();
                Companies.Remove(SelectedCompany);
            }
        }

        public CompaniesViewModel()
        {
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            Companies = await DatabaseController.GetCompaniesAsync();
        }
    }
}