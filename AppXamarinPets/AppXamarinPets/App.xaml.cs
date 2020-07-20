using AppXamarinPets.Data;
using AppXamarinPets.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppXamarinPets
{
    public partial class App : Application
    {
        static PetsDatabase petsDatabase;
        public static PetsDatabase PetsDatabase
        {
            get
            {
                if (petsDatabase == null) petsDatabase = new PetsDatabase();
                return petsDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new PetsPage())
            {
                BackgroundColor = (Color)App.Current.Resources["primaryColor"],
                BarBackgroundColor = (Color)App.Current.Resources["secondColor"],
                Title = "App Pets",
                BarTextColor = Color.White
            };

            MainPage = nav;
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
