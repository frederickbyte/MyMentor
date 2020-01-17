using MyMentor.Accounts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.ConnectionProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionProfile : ContentPage
    {
        public ConnectionProfile()
        {
            InitializeComponent();
        }

        public ConnectionProfile(UserInfoWithAvatar connection)
        {
            var viewModel = new ConnectionProfileViewModel(connection);
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}