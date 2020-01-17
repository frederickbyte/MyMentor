using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.MatchedMentors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchedMentors : ContentPage
    {
        public MatchedMentors()
        {
            InitializeComponent();
        }

        public MatchedMentors(List<UserInfo> mentors)
        {
            var viewModel = new MatchedMentorsViewModel(mentors);
            viewModel.OnAddConnection += async (UserInfoWithAvatar mentor) =>
            {
                var action = await DisplayAlert("Mentor Confirmation", "Are you sure you want to match with " + mentor.UserInfo.User.FullName + " ?", "Match", "Cancel");
                if (action)
                {
                    viewModel.AddConnection(mentor.UserInfo.User.UserId);
                    await DisplayAlert("Mentor request sent",
                                       "We will notify " + mentor.UserInfo.User.FullName + " of your request.", "OK");
                }
            };
            viewModel.OnAddStudent += async (UserInfoWithAvatar student) =>
                {
                    var action = await DisplayAlert("Student Confirmation", "Are you sure you want to match with " + student.UserInfo.User.FullName + " ?", "OK", "Cancel");
                    if (action)
                    {
                        await DisplayAlert("Student request sent",
                        "We will notify " + student.UserInfo.User.FullName + " of your request.", "OK");
                    }
                };
            viewModel.OnViewMentorProfile += async (UserInfoWithAvatar connectionToView) =>
            {
                await Navigation.PushAsync(new ConnectionProfile.ConnectionProfile(connectionToView));
            };
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}