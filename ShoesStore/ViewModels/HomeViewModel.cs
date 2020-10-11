using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ShoesStore.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public HomeViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                _users = new ObservableCollection<User>(db.Users);
            }
        }
    }
}
