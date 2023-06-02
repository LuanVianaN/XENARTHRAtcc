using XeMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using XeMobile.Views;
using System.Net.Http;


namespace XeMobile
{
    public partial class MainPage : ContentPage
    {

        public string WebApiKey = "AIzaSyCJtd6ZalAdrELxM8jsbaTZqtB64zZ3SgI";
        public MainPage()
        {
            InitializeComponent();
            GetProfileInformationAndRefreshToken();
        }

        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
                MeuLogin.Text = savedfirebaseauth.User.Email;
            }
            catch 
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seu login expirou!", "Ok");
            }
        }

        private async void NewPhotoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPhotoPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var photos = await App.Database.GetPhotosAsync();
            PhotoCollection.ItemsSource = null;
            PhotoCollection.ItemsSource = photos;
        }

        private async void DeleteButtonTapped(object sender, EventArgs e)
        {
            var result=await DisplayAlert("Aviso", "Deseja excluir a postagem?", "Sim", "Não");

            var button = sender as Frame;
            var photo = button.BindingContext as Foto;

            if (result)
            {
                await App.Database.DeletePhotoAsync(photo);

                var photos = await App.Database.GetPhotosAsync();
                PhotoCollection.ItemsSource = null;
                PhotoCollection.ItemsSource = photos;
            }
        }

        void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new Dashboard());
        }
        async private void Enviarf_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var photo = button.BindingContext as Foto;

            double latitude = photo.Latitude;
            double longitude = photo.Longitude;

            string latitudeStr = latitude.ToString();
            string longitudeStr = longitude.ToString();

            HttpClient client = new HttpClient();

            string apiUrl = "http://localhost:44342/api/Fotos";

            try
            {

                var data = new Dictionary<string, string>
                {
                    { "latitude", latitudeStr },
                    { "longitude", longitudeStr }
                };

                var formData = new FormUrlEncodedContent(data);

                HttpResponseMessage response = await client.PostAsync(apiUrl, formData);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "Enviada com sucesso!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "Erro!", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Erro desconhecido!", "OK");
            }
        }
    }
}
