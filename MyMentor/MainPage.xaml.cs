using System;
using System.ComponentModel;
using MyMentor.App.MVVM;
using Xamarin.Forms;

namespace MyMentor
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            BaseViewModel.UserInfo = null;
            MyMentor.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}
