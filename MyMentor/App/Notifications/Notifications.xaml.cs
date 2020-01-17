using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMentor.Accounts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.Notifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();
        }

        public Notifications(List<UserInfo> notifications)
        {
            var viewModel = new NotificationsViewModel(notifications);
            
            viewModel.OnRespondToNotification += async (string userId, string userName) =>
            {
                var result = await DisplayAlert("Mentorship request from " + userName, "Would you like to accept this request?", "Accept", "Ignore");
                if (result)
                {
                    viewModel.DismissNotification(userId);
                    await DisplayAlert("Request Accepted!", "We will notify the student of your response.", "Ok");
                }
            };
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}