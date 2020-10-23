using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ShoesStore.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public ICommand CreateAccCommand { get; set; }
        private string _name;
        public string Name
        {
            get =>  _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public void Register(string password)
        {
            if(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(new User { Login = Login, Name = Name, Password = password, Proffesion = "Seller" });
                db.SaveChanges();
            }
        }
        public RegistrationViewModel()
        {
            CreateAccCommand = new CreateAccCommand(this);
        }
    }
}
