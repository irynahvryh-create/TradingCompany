using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompany.BL.Interfaces;
using TradingCompany.WPF.Commands;
using TradingCompany.WPF.Interfaces;

namespace TradingCompany.WPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged, ICloseable, IDataErrorInfo
    {
        private readonly IAuthManager _security;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string? _loginError;
        public LoginViewModel(IAuthManager security)
        {
            _security = security;
            LoginCommand = new LoginCommand(this);
            CloseCommand = new CloseCommand(this);
        }

        public bool Login()
        {
            return _security.Login(Username, Password);
        }

        public Action LoginFailed { get; set; }
        public Action LoginSuccessful { get; set; }

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        public ICommand CloseCommand
        {
            get;
            private set;
        }

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

        public bool CanLogin =>
            string.IsNullOrEmpty(Error);

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

        private string[] ValidatedProperties
        {
            get
            {
                return new string[] { nameof(Password), nameof(Username) };
            }
        }

        private string GetErrorInfo(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Password):
                    return ValidatePassword();
                case nameof(Username):
                    return ValidateUsername();
                default:
                    return "";
            }
        }
        #region IDataErrorInfo
        public string Error
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var property in ValidatedProperties)
                {
                    var err = GetErrorInfo(property);
                    if (!string.IsNullOrWhiteSpace(err))
                    {
                        sb.AppendLine(err);
                    }
                }
                return sb.ToString();
            }
        }
        public string this[string propertyName]
        {
            get
            {
                return GetErrorInfo(propertyName);
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        #region ICloseable
        public Action Close { get; set; }
        #endregion
    }
}