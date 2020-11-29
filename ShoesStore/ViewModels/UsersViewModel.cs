using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.ObjectModel;

namespace ShoesStore.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

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
                LoadDataAsync();
            }
        }

        private RelayCommand _deleteUserCommand;

        public RelayCommand DeleteUserCommand
        {
            get
            {
                return _deleteUserCommand ??
                  (_deleteUserCommand = new RelayCommand(obj =>
                  {
                      DeleteUser();
                  }));
            }
        }

        public void DeleteUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //Remove from database
                db.Users.Remove(SelectedUser);
                db.SaveChanges();
                //Remove from UI
                Users.Remove(SelectedUser);
            }
        }

        public UsersViewModel()
        {
            SearchedText = "";
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            if (SearchedText == "")
            {
                Users = await DatabaseController.GetUsersAsync();
            }
            else
            {
                Users = await DatabaseController.SearchUsersAsync(SearchedText);
            }
        }
    }
}