


using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TradingCompany.BL.Interfaces;
using TradingCompany.WPF.Commands;
using TradingCompany.WPF.Interfaces;
using TradingCompany.DTO;
/*
namespace IMDB2025.WPF.ViewModels
{
    public class LoginViewModel_2 : INotifyPropertyChanged, ICloseable, IDataErrorInfo
    {
        private readonly IAuthManager _authManager;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string? _loginError;

        public LoginViewModel_2(IAuthManager authManager)
        {
            _authManager = authManager;
            LoginCommand = new LoginCommand(this);
            CloseCommand = new CloseCommand(this);
        }

        public bool Login()
        {
            var user = _authManager.Login(Username, Password);
            if (user == null)
            {
                LoginError = "Невірний логін або пароль";
                return false;
            }

            // Перевірка ролей
            if (!user.Privileges.Any(p =>
                p.PrivilegeType == PrivilegeType.Admin ||
                p.PrivilegeType == PrivilegeType.Manager ||
                p.PrivilegeType == PrivilegeType.User))
            {
                LoginError = "У вас немає доступу до програми";
                return false;
            }

            return true;
        }

        public Action LoginFailed { get; set; }
        public Action LoginSuccessful { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }

        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value ?? string.Empty;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Error));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value ?? string.Empty;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Error));
            }
        }

        public string? LoginError
        {
            get => _loginError;
            internal set
            {
                if (_loginError == value) return;
                _loginError = value;
                OnPropertyChanged();
            }
        }

        public bool CanLogin => string.IsNullOrEmpty(Error);

        private string ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username))
                return "Username is required.";
            if (Username.Length < 3)
                return "Username must be at least 3 characters.";
            return string.Empty;
        }

        private string ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
                return "Password is required.";
            if (Password.Length < 6)
                return "Password must be at least 6 characters.";
            return string.Empty;
        }

        private string[] ValidatedProperties => new[] { nameof(Username), nameof(Password) };

        private string GetErrorInfo(string propertyName) =>
            propertyName switch
            {
                nameof(Username) => ValidateUsername(),
                nameof(Password) => ValidatePassword(),
                _ => string.Empty
            };

        public string Error
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var prop in ValidatedProperties)
                {
                    var err = GetErrorInfo(prop);
                    if (!string.IsNullOrWhiteSpace(err))
                        sb.AppendLine(err);
                }
                return sb.ToString();
            }
        }

        public string this[string propertyName] => GetErrorInfo(propertyName);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // ICloseable
        public Action Close { get; set; }
    }
}


*/