using XeMobile.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XeMobile.Views;
using Firebase.Auth;
using Xamarin.Essentials;

namespace XeMobile
{
    public partial class App : Application
    {
        static FotoDatabase database;

        // Create the database connection as a singleton.
        public static FotoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FotoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard());
            }
            MainPage.BackgroundImageSource = "96824796_3126275337393191_8458787589522980864_n.png"; // Caminho da imagem de fundo

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
