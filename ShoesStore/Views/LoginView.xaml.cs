using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoesStore.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private LoginViewModel lvm = new LoginViewModel();

        public LoginView()
        {
            using ( ApplicationContext db = new ApplicationContext() )
            {

            }
            DataContext = lvm;
            InitializeComponent();
            if (lvm.LoginAction == null)
            {
                lvm.LoginAction = new Action(() => Login());
            }
            UsernameBox.Focus();
        }
        public void Login()
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            Window.GetWindow(this).Close();
        }
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginCommand loginCommand = new LoginCommand(lvm);
                loginCommand.Execute(PasswordBox);
            }
        }
    }
}