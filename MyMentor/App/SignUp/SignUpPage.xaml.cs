using MyMentor.App.SignUp;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            signUpViewModel.OnSignUpResult += async (bool result) =>
            {
                if (result)
                {
                    await DisplayAlert("Sign Up Successful", "You'll soon be emailed with your MyMentor credentials.", "OK");
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Navigation.PopAsync();
                }
            };
            signUpViewModel.OnPrivacyPolicyClicked += async () =>
            {
                await DisplayAlert("Privacy",
                    "MyMentor uses your name for account verification and to better match you with other students and professors." +
                    "\r\n\r\nYour personal information is private to you. If we ever plan to share this information, we will explicitly ask for your permission.",
                    "OK");
            };

            BindingContext = signUpViewModel;
        }
    }
}