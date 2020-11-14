using ShoesStore.ViewModels;
using System;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    public class UpdateViewCommand : ICommand
    {
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
                "Categories" => new CategoriesViewModel(),
                "Companies" => new CompaniesViewModel(),
                _ => new HomeViewModel(),
            };
        }
    }
}