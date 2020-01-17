using System;
using MyMentor.Accounts;
using MyMentor.App.Dashboard;
using MyMentor.App.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var loginViewModel = new LoginViewModel();
            loginViewModel.OnLoginResult += async (UserInfo userInfo) =>
            {
                // Navigate user to dashboard if username/password combination is valid
                if (userInfo.User != null)
                {
                    Navigation.InsertPageBefore(new Dashboard(userInfo), this);
                    await Navigation.PopAsync();
                }
            };
            // User has clicked the RESET PASSWORD button
            loginViewModel.OnResetPassword += async () =>
            {
                var emailOfPasswordReset = await DisplayPromptAsync("Reset Password", "What is the email for the account?");
                if (!string.IsNullOrEmpty(emailOfPasswordReset))
                {
                    await DisplayAlert("Password successfully reset", "You will soon receive an email with a temporary password.", "OK");
                }
            };
            // User has clicked the SIGN UP button
            loginViewModel.OnSignUpClicked += async () => { await Navigation.PushAsync(new SignUpPage()); };
            BindingContext = loginViewModel;
        }
    }
}