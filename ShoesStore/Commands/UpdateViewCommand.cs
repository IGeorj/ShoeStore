using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    public class UpdateViewCommand : ICommand
    {
        //TODO DRY Refactoring
        private MainWindowModel viewModel;
        public UpdateViewCommand(MainWindowModel viewModel)
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
                "Home" => new HomeViewModel(),
                "Store" => new StoreViewModel(),
                "Users" => new UsersViewModel(),
                "Orders" => new OrdersViewModel(),
                _ => new HomeViewModel(),
            };
        }
    }
}
