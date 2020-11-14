using ShoesStore.Commands;
using ShoesStore.Models;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public enum AccountType
    {
        Admin,
        Seller
    }

    public class EditUserViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        private AccountType _accType;

        public AccountType AccType
        {
            get => _accType;
            set
            {
                if (_accType == value)
                    return;

                _accType = value;
                OnPropertyChanged("AccType");
                OnPropertyChanged("IsSeller");
                OnPropertyChanged("IsAdmin");
            }
        }

        public bool IsAdmin
        {
            get { return AccType == AccountType.Admin; }
            set { AccType = value ? AccountType.Admin : AccType; }
        }

        public bool IsSeller
        {
            get { return AccType == AccountType.Seller; }
            set { AccType = value ? AccountType.Seller : AccType; }
        }

        public string GetAccType
        {
            get
            {
                switch (AccType)
                {
                    case AccountType.Seller:
                        return "Seller";

                    case AccountType.Admin:
                        return "Admin";
                }
                return "";
            }
        }

        private User _user;

        public User User { get => _user; set => _user = value; }

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

        public void ChangeUser()
        {
            if (string.IsNullOrEmpty(User.Login) || string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Name) || string.IsNullOrEmpty(User.Password))
            {
                MessageBox.Show("Please verify all data");
                return;
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User user = db.Users.Find(Id);
                    user.Email = User.Email;
                    user.Login = User.Login;
                    user.Name = User.Name;
                    user.Proffesion = GetAccType;
                    user.Password = User.Password;
                    db.SaveChanges();
                }
            }
        }

        public EditUserViewModel(int Id)
        {
            this.Id = Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                User = db.Users.Find(Id);
            }
            AccType = User.Proffesion == "Admin" ? AccountType.Admin : AccountType.Seller;
        }
    }
}