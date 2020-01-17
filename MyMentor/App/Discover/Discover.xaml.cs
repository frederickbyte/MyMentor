using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MyMentor.Accounts;

namespace MyMentor.App.Discover
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Discover : ContentPage
    {
        public Discover()
        {
            var viewModel = new DiscoverViewModel();
            viewModel.OnViewEvent += async (CommunityEvent communityEvent) =>
            {
                await DisplayAlert(communityEvent.Title, communityEvent.Description + "\r\n\r\n" + "When: Friday, Nov. 6, 2019", "Ok");
            };
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}