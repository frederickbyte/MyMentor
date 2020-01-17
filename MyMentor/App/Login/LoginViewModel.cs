using MyMentor.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MyMentor.Accounts;
using MyMentor.App.MVVM;
using Xamarin.Forms;

namespace MyMentor.App.Login
{
    public class LoginViewModel : MVVM.BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        public ICommand ForgotPasswordCommand { get; set; }

        public ICommand SignUpCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            ForgotPasswordCommand = new Command(ResetPassword);
            SignUpCommand = new Command(SignUp);
            IsLoggingIn = false;
        }

        public Action<UserInfo> OnLoginResult = delegate { };
        public Action OnResetPassword = delegate { };
        public Action OnSignUpClicked = delegate { };
        public async void Login(object obj)
        {
            IsLoggingIn = true;
            await IsValidUser(Username, Password);
        }

        public void ResetPassword()
        {
            OnResetPassword();
        }

        public void SignUp()
        {
            OnSignUpClicked();
        }

        public async Task IsValidUser(string username, string password)
        {
            var service = new RestService();
            var userInfo = await service.LoginUser(username, password);
            if (userInfo.User != null)
            {
                IsLoggingIn = false;
                MyMentor.IsUserLoggedIn = true;
                BaseViewModel.UserInfo = userInfo;
                OnLoginResult(userInfo);
            }
            else
            {
                IsLoggingIn = false;
                Error = "Invalid username/password";
                OnLoginResult(userInfo);
            }
        }



        private string _userName;

        public string Username
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                _isLoggingIn = value;
                OnPropertyChanged("IsLoggingIn");
            }
        }
    }
}