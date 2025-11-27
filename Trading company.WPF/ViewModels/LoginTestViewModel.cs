using System.ComponentModel;
using System.Runtime.CompilerServices;
using TradingCompany.BL.Interfaces;

namespace TradingCompany.WPF.ViewModels
{
    public class LoginTestViewModel : INotifyPropertyChanged
    {
        private readonly IAuthManager _authManager;

        public LoginTestViewModel(IAuthManager authManager)
        {
            _authManager = authManager;

            _authManager.CurrentUserChanged += () =>
            {
                OnPropertyChanged(nameof(CurrentUserName));
                OnPropertyChanged(nameof(Role));
            };
        }

        public string CurrentUserName => _authManager.CurrentUser?.Login ?? "Немає користувача";

        public string Role
        {
            get
            {
                if (_authManager.CurrentUser == null) return "-";
                return _authManager.IsAdmin(_authManager.CurrentUser) ? "Admin" : "User";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
