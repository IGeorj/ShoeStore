using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoesStore.Commands
{
    class CreateAccCommand : ICommand
    {
        RegistrationViewModel viewModel;

        public CreateAccCommand(RegistrationViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null || MaterialDesignThemes.Wpf.TextFieldAssist.GetUnderlineBrush(passwordBox) == Brushes.Red)
            {
                MessageBox.Show("Поля");
                return;
            }
            viewModel.Password = passwordBox.Password;
            viewModel.Register(viewModel.Password);
        }
    }
}
