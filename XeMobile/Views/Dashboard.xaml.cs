using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;


namespace XeMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        //Login pelo firebase
        public string WebApiKey = "AIzaSyCJtd6ZalAdrELxM8jsbaTZqtB64zZ3SgI";

        public Dashboard()
        {
            InitializeComponent();
        }

        async void loginbtn_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(emailentry.Text, senhaentry.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Usuario ou senha invalido!", "OK");
            }
        }
        private async void cadastrarbtn_Clicked(object sender, EventArgs e)
        {
            var cadastro = new Cadastro();
            await Navigation.PushModalAsync(cadastro);
        }
    }
}
