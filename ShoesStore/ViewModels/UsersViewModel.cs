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
        public ICommand DeleteUserCommand { get; set; }

        public ICommand ChangeProfCommand { get; set; }

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
                SearchUser(_searchedText);
            }
        }

        public void SearchUser(string text)
        {
            //TODO CollectionView
            using (ApplicationContext db = new ApplicationContext())
            {
                if (text == "")
                {
                    LoadDataAsync();
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
        public void DeleteUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(SelectedUser);
                db.SaveChanges();
                Users.Remove(SelectedUser);
                SearchUser(_searchedText);
            }
        }
        public void ChangeProf()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User us = db.Users.FirstOrDefault(x => x == SelectedUser);
                if(us.Proffesion == "Admin")
                {
                    us.Proffesion = "Seller";
                }
                else
                {
                    us.Proffesion = "Admin";
                }
                db.SaveChanges();
            }
            SearchUser(_searchedText);
        }
        public UsersViewModel()
        {
            DeleteUserCommand = new DeleteUserCommand(this);
            ChangeProfCommand = new ChangeProfCommand(this);
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