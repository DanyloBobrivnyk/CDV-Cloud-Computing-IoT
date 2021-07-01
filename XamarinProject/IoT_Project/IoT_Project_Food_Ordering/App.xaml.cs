using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoT_Project_Food_Ordering
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            /*MainPage = new SettingsPageView();*/

            string uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                MainPage = new LoginView();
            }
            else
            {
                MainPage = new ProductsView();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
