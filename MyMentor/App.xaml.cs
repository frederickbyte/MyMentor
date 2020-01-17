using MyMentor.App.Dashboard;
using Xamarin.Forms;

namespace MyMentor
{
    public partial class MyMentor : Application
    {
        // Whether or not the current user is logged into the application
        public static bool IsUserLoggedIn { get; set; }

        public MyMentor()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
