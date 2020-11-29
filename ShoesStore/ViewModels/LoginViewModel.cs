using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.Utils;
using System;
using System.Linq;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private RelayCommand _LoginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _LoginCommand ??
                  (_LoginCommand = new RelayCommand(obj =>
                  {
                      LoginAcc();
                  }));
            }
        }

        public Action LoginAction { get; set; }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public void LoginAcc()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(x => (x.Login == Login || x.Email == Login));
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        GlobalInfo.CurrentUser = user;
                        LoginAction();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Login/Username");
                }
            }
        }

        public LoginViewModel()
        {
        }
    }
}