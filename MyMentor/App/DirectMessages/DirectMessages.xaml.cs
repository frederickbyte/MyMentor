using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMentor.App.DirectMessages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DirectMessages : ContentPage
    {
        public DirectMessages()
        {
            //InitializeComponent();
        }

        public DirectMessages(List<UserInfoWithAvatar> matches)
        {
            var viewModel = new DirectMessagesViewModel(matches);
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}