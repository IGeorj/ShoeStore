using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    class UpdateSignInViewCommand : ICommand
    {
        private SignInViewModel viewModel;
        public UpdateSignInViewCommand(SignInViewModel viewModel)
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
            viewModel.SelectedViewModel = (parameter.ToString()) switch
            {
                "Login" => new LoginViewModel(),
                "Registration" => new RegistrationViewModel(),
                _ => new LoginViewModel(),
            };
        }
    }
}
