using MyMentor.Accounts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.Connections
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Connections : ContentPage
    {
        public Connections()
        {
            var viewModel = new ConnectionsViewModel();
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.OnViewConnectionProfile += async (UserInfoWithAvatar connection) =>
            {
                await Navigation.PushAsync(new ConnectionProfile.ConnectionProfile(connection));
            };

        }
    }
}