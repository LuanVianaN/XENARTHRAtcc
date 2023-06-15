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
        //api de login com firebase
        private ApiService apiService;
        public string WebApiKey = "AIzaSyCJtd6ZalAdrELxM8jsbaTZqtB64zZ3SgI";
        public MainPage()
        {
            InitializeComponent();
            GetProfileInformationAndRefreshToken();
            apiService = new ApiService();
        }
        //Manter o usuario logado
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
        //Excluir foto salva
        private async void DeleteButtonTapped(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Aviso", "Deseja excluir a postagem?", "Sim", "Não");

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
        //Desconectar do usuario
        void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new Dashboard());
        }
        async private void Enviarf_Clicked(object sender, EventArgs e)
        {

                var apiService = new ApiService();

            //Teste de api
                var foto = new FotoT
                {
                    ID_Usuario = 1,
                    ID_Animal = 1,
                    Longitude = 1.3f,
                    Latitude = 2.4f,
                    ID_Verificado = 0,
                    Imagem = 1,
                    OBS = "X1"
                };

                var resultado = await apiService.EnviarFoto(foto);

                if (resultado)
                {
                // A foto foi enviada com sucesso
                await DisplayAlert("Sucesso", "A foto foi enviada com sucesso", "OK");
                }
                else
                {
                // Ocorreu um erro ao enviar a foto
                await DisplayAlert("Erro", "Ocorreu um erro ao enviar a foto", "OK");
                }
            /* 
             
            */
        }
    }
}
