using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private UsersViewModel viewModel;

        public DeleteUserCommand(UsersViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.DeleteUser();
        }
    }
}
