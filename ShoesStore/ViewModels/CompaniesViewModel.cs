using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ShoesStore.ViewModels
{
    public class CompaniesViewModel : BaseViewModel
    {
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
        public CompaniesViewModel()
        {
            LoadData();
        }
        public void LoadData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
               Companies = new ObservableCollection<Company>(db.Companies.ToList());
            }
        }
    }
}
