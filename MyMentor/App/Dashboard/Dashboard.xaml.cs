using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using MyMentor.Accounts;
using MyMentor.App.MVVM;

namespace MyMentor.App.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : TabbedPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        public Dashboard(UserInfo userInfo) : this()
        {
            var dashboardViewModel = new DashboardViewModel(userInfo);
            dashboardViewModel.OnFindMentorsClick += async (bool result) =>
            {
                if (result)
                {
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
            };
            dashboardViewModel.OnViewNotifications += async (List<UserInfo> notifications) =>
            {
                await Navigation.PushAsync(new Notifications.Notifications(notifications));
            };
            dashboardViewModel.OnViewDirectMessages += async (List<UserInfoWithAvatar> matches) =>
            {
                await Navigation.PushAsync(new DirectMessages.DirectMessages(matches));
            };
            BindingContext = dashboardViewModel;
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