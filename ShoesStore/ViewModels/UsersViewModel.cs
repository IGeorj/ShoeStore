using ShoesStore.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoesStore.ViewModels
{
    internal class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private string _searchedText;

        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                _searchedText = value;
                OnPropertyChanged();
                SearchUser(_searchedText);
            }
        }

        public void SearchUser(string text)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (text == "")
                {
                    Users = new ObservableCollection<User>(db.Users);
                }
                else
                {
                    Users = new ObservableCollection<User>(
                        db.Users.Where(x => x.Name.StartsWith(text) || x.Email.StartsWith(text) || x.Login.StartsWith(text) ||
                        x.Password.StartsWith(text) || x.Proffesion.StartsWith(text))
                        );
                }
            }
        }

        public UsersViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Users = new ObservableCollection<User>(db.Users);
            }
        }
    }
}