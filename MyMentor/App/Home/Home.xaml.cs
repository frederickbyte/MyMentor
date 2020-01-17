using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {

        public Home()
        {
            var viewModel = new HomeViewModel();
            viewModel.OnFindMentors += async (List<UserInfo> mentors) =>
            {
                await Navigation.PushAsync(new MatchedMentors.MatchedMentors(mentors));
            };
            viewModel.OnFindStudents += async (List<UserInfo> students) =>
            {
                await Navigation.PushAsync(new MatchedMentors.MatchedMentors(students));
            };
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}