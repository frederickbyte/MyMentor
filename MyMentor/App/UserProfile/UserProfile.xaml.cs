using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.UserProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            var viewModel = new UserProfileViewModel();
            InitializeComponent();
            viewModel.OnAddAcademicInterest += async () =>
            {
                var topicToAdd = await DisplayActionSheet("Select a topic to add", "Cancel", null, "Architecture", "Biology", "Chemistry ", "English", "History", "Mathematics");
                if (!string.IsNullOrEmpty(topicToAdd))
                {
                    viewModel.AddAcademicInterest(topicToAdd);
                }
            };
            viewModel.OnUpdateProfile += async () =>
            {
                var updatedProfileName = await DisplayPromptAsync("Update Name", "What would you like to be called?");
                if (!string.IsNullOrEmpty(updatedProfileName))
                {
                    viewModel.UpdateProfileName(updatedProfileName);
                    var updatedUsername = await DisplayPromptAsync("Update Username", "What would you like your username to be?");
                }
            };
            BindingContext = viewModel;
        }
    }
}