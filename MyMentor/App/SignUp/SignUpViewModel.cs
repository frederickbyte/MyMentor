using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using MyMentor.Accounts;

namespace MyMentor.App.SignUp
{
    public class SignUpViewModel : MVVM.BaseViewModel
    {

        public ICommand SignUpCommand { get; set; }

        public ICommand PrivacyPolicyCommand { get; set; }
        public SignUpViewModel()
        {
            SignUpCommand = new Command(SignUp);
            PrivacyPolicyCommand = new Command(ViewPrivacyPolicy);
        }


        public Action<bool> OnSignUpResult = delegate { };
        public Action OnPrivacyPolicyClicked = delegate { };

        public bool CanSignUp()
        {
            // Declare email regular expression for validation
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (String.Compare(Password, ConfirmPassword, StringComparison.Ordinal) != 0)
            {
                Error = "Password and Confirm Password do not match";
                return false;
            }
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) 
                                                    && !string.IsNullOrEmpty(Password) && EmailRegex.IsMatch(Email);
        }

        public void SignUp(object obj)
        {
            if (CanSignUp())
            {
                OnSignUpResult(true);
            }
            else
            {
                Error = "Please fill out all fields before signing up";
            }
        }

        public void ViewPrivacyPolicy()
        {
            OnPrivacyPolicyClicked();
        }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
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

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
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
    }
}
