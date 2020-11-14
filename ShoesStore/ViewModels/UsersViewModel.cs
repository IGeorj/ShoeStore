using Microsoft.EntityFrameworkCore;
using ShoesStore.Commands;
using ShoesStore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ShoesStore.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
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

        private RelayCommand _changeUserCommand;

        public RelayCommand ChangeUserCommand
        {
            get
            {
                return _changeUserCommand ??
                  (_changeUserCommand = new RelayCommand(obj =>
                  {
                      ChangeUser();
                  }));
            }
        }

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
                SearchUser();
            }
        }

        public void SearchUser()
        {
            //TODO CollectionView
            using (ApplicationContext db = new ApplicationContext())
            {
                if (_searchedText == "")
                {
                    LoadDataAsync();
                }
                else
                {
                    Users = new ObservableCollection<User>(
                        db.Users.Where(x => x.Name.StartsWith(_searchedText) || x.Email.StartsWith(_searchedText) || x.Login.StartsWith(_searchedText) ||
                        x.Password.StartsWith(_searchedText) || x.Proffesion.StartsWith(_searchedText))
                        );
                }
            }
        }
        public void DeleteUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(SelectedUser);
                db.SaveChanges();
                Users.Remove(SelectedUser);
                SearchUser();
            }
        }
        public void ChangeUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SaveChanges();
            }
            SearchUser();
        }
        public UsersViewModel()
        {
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                _searchedText = "";
                var items = await Task.Run(() => db.Users.ToListAsync());
                Users = new ObservableCollection<User>(items);
            }
        }
    }
}