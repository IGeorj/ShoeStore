using ShoesStore.Models;

namespace ShoesStore.Utils
{
    internal class GlobalInfo
    {
        private static User _currentUser;

        public static User CurrentUser { get => _currentUser; set => _currentUser = value; }
    }
}